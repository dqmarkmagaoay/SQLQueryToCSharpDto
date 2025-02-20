using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace StoredToClass
{
    public partial class Form2 : Form
    {
        public SynchronizationContext Context { get; set; }
        
        public Form2()
        {
            InitializeComponent();
            Context = SynchronizationContext.Current;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            txtDatabase.Text = Properties.Settings.Default.DB;
            txtServer.Text = Properties.Settings.Default.DS;
            txtPort.Text = string.IsNullOrEmpty(Properties.Settings.Default.Port) ? "3306" : Properties.Settings.Default.Port;
            txtUserId.Text = Properties.Settings.Default.User;
            txtPassword.Text = Properties.Settings.Default.Password;
            txtQuery.Text = Properties.Settings.Default.Query;
            txtOutput.Text = Properties.Settings.Default.OutputText;
            txtJSON.Text = Properties.Settings.Default.Json;
            txtAPI.Text = Properties.Settings.Default.Api;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckInputs(((Control)sender).Parent);
            txtOutput.Text = string.Empty;
            var builder = new StringBuilder();
            builder.AppendLine("public class YourClassName");
            builder.AppendLine("{");
            Cursor = Cursors.WaitCursor;
            ToggleControls(false);
            var builder1 = builder;
            dataGridView.DataSource = null;
            Task.Factory.StartNew(async () =>
            {
                var hasException = false;
                try
                {
                    if (string.IsNullOrEmpty(txtServer.Text) || string.IsNullOrEmpty(txtServer.Text))
                    {
                        throw new Exception("Server Name and Database name are required");
                    }

                    if (!int.TryParse(txtPort.Text, out int port))
                        port = 3306;
                    var connectionString = $"Server={txtServer.Text};port={port};Database={txtDatabase.Text};Uid={txtUserId.Text};Pwd={txtPassword.Text};SslMode=Preferred;ConnectionTimeout=30;DefaultCommandTimeout=600;";
                    using (var conn = new MySqlConnection(connectionString))
                    using (var cmd = new MySqlCommand(txtQuery.Text, conn))
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        await conn.OpenAsync();
                        var table = new DataTable();
                        await adapter.FillAsync(table);
                        Context.Post(_ =>
                        {
                            dataGridView.DataSource = table;
                        }, null);
                        foreach (DataColumn column in table.Columns)
                        {
                            string str = Program
                                .FixDataTypes(
                                    $"public {column.DataType}{(column.AllowDBNull ? "?" : "")} {column.ColumnName}  {{ get; set; }}",
                                    cbAllowNullableString.Checked
                                );
                            builder1.AppendLine(str);
                        }
                    }
                    builder1.AppendLine("\n}");
                }
                catch (Exception exception)
                {
                    hasException = true;
                    builder1 = new StringBuilder();
                    builder1.Append(exception.Message);
                }
                Context.Post(_ =>
                {
                    Cursor = Cursors.Default;
                    ToggleControls(true);
                    if (!hasException)
                    {
                        txtOutput.Text = builder1.ToString().Replace("System.", string.Empty);
                    }
                    else
                    {
                        throw new Exception(builder1.ToString());
                    }
                    txtTotalRows.Text = $@"Total: {dataGridView.RowCount}";
                    Activate();
                }, null);
            }, TaskCreationOptions.LongRunning);
        }

        private static void CheckInputs(Control parent)
        {
            var list = new List<object>();
            foreach (Control control in parent.Controls)
            {
                switch (control)
                {
                    case TextBox _:
                        if (string.IsNullOrEmpty(control.Text))
                        {
                            list.Add(control);
                        }
                        break;
                }
            }

            if (list.Count <= 0)
            {
                return;
            } (list.FirstOrDefault() as TextBox)?.Focus();
            throw new Exception("All fields are required!");
        }

        private void ToggleControls(bool enable)
        {
            foreach (Control control in Controls)
            {
                switch (control)
                {
                    case Button _:
                    case TextBox _:
                    case TabControl _:
                    case TabPage _:
                        control.Enabled = enable;
                        break;
                }
            }
        }

        private void txtOutpput_MouseEnter(object sender, EventArgs e)
        {
            CopyToClipboard();
        }

        private void CopyToClipboard(string text = "")
        {
            var txt = txtOutput.Text ?? string.Empty;
            if (string.IsNullOrEmpty(text))
            {
                if (string.IsNullOrEmpty(txt))
                {
                    throw new DataException("Nothing to copy!");
                }
            }
            else
            {
                txt = text;
            }
            Clipboard.SetText(txt, TextDataFormat.UnicodeText);
            copyText.Text = @"👍 Copied ✅";
        }

        private void btnGenerateFromApi_Click(object sender, EventArgs e)
        {
            CheckInputs(((Control)sender).Parent);
            txtOutput.Text = string.Empty;
            var builder = new StringBuilder();
            builder.AppendLine("public class YourClassName");
            builder.AppendLine("{");
            Cursor = Cursors.WaitCursor;
            ToggleControls(false);
            var builder1 = builder;
            dataGridView.DataSource = null;
            Task.Factory.StartNew(() =>
            {
                try
                {
                    var table = new DataTable();
                    var handler = new HttpClientHandler
                    {
                        AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                    };
                    using (var client = new HttpClient(handler))
                    {
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.GetAsync(txtAPI.Text).Result;
                        if (!response.IsSuccessStatusCode)
                        {
                            throw new Exception(response.ReasonPhrase);
                        }

                        var stringData = response.Content.Headers.ContentType != null && response.Content.Headers.ContentType.MediaType.Equals("text/xml") ? XElement.Parse(response.Content.ReadAsStringAsync().Result).Value : response.Content.ReadAsStringAsync().Result;
                        stringData = Regex.Unescape(stringData);
                        stringData = stringData.TrimStart('"');
                        stringData = stringData.TrimEnd('"');
                        if (stringData != "\"Source sequence doesn't contain any elements.\"")
                        {
                            //stringData = JsonConvert.DeserializeObject<string>(stringData);
                            stringData = stringData.StartsWith("[") && stringData.EndsWith("]") ? $"{{\"data\":{stringData}}}" : stringData;
                            try
                            {
                                table = JsonConvert.DeserializeObject<DataSet>(stringData).Tables[0];
                            }
                            catch (Exception exception)
                            {
                                Console.WriteLine(exception);
                                var oldData = stringData;
                                stringData = $"{{\"data\":[{stringData}]}}";
                                try
                                {
                                    table = JsonConvert.DeserializeObject<DataSet>(stringData).Tables[0];
                                }
                                catch
                                {
                                    stringData = $"{{\"data\": [{{\"data\": \"{oldData}\"}}]}}";
                                    table = JsonConvert.DeserializeObject<DataSet>(stringData).Tables[0];
                                }
                            }
                        }
                    }

                    Context.Post(_ =>
                    {
                        dataGridView.DataSource = table;
                    }, null);

                    foreach (DataColumn column in table.Columns)
                    {
                        string str = Program
                            .FixDataTypes(
                                $"\npublic {column.DataType}{(column.AllowDBNull ? "?" : "")} {column.ColumnName}  {{ get; set; }}",
                                cbAllowNullableString.Checked
                            );
                        builder1.AppendLine(str);
                    }
                    builder1.AppendLine("\n}");
                }
                catch (Exception exception)
                {
                    builder1 = new StringBuilder();
                    builder1.Append(exception.Message);
                }
                Context.Post(_ =>
                {
                    Cursor = Cursors.Default;
                    ToggleControls(true);
                    txtOutput.Text = builder1.ToString().Replace("System.", string.Empty);
                    txtTotalRows.Text = $@"Total: {dataGridView.RowCount}";
                    Activate();
                }, null);
            }, TaskCreationOptions.LongRunning);
        }

        private void btnJSON_Click(object sender, EventArgs e)
        {
            CheckInputs(((Control)sender).Parent);
            txtOutput.Text = string.Empty;
            var builder = new StringBuilder();
            builder.AppendLine("public class YourClassName");
            builder.AppendLine("{");
            Cursor = Cursors.WaitCursor;
            ToggleControls(false);
            var builder1 = builder;
            dataGridView.DataSource = null;
            Task.Factory.StartNew(() =>
            {
                try
                {
                    var table = new DataTable();
                    var stringData = txtJSON.Text;
                    stringData = Regex.Unescape(stringData);
                    stringData = stringData.TrimStart('"');
                    stringData = stringData.TrimEnd('"');
                    if (stringData != "\"Source sequence doesn't contain any elements.\"")
                    {
                        stringData = stringData.StartsWith("[") && stringData.EndsWith("]") ? $"{{\"data\":{stringData}}}" : stringData;
                        try
                        {
                            table = JsonConvert.DeserializeObject<DataSet>(stringData).Tables[0];
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);
                            stringData = $"{{\"data\":[{stringData}]}}";
                            table = JsonConvert.DeserializeObject<DataSet>(stringData).Tables[0];
                        }
                    }
                    Context.Post(_ =>
                    {
                        dataGridView.DataSource = table;
                    }, null);
                    foreach (DataColumn column in table.Columns)
                    {
                        string str = Program
                            .FixDataTypes(
                                $"\npublic {column.DataType}{(column.AllowDBNull ? "?" : "")} {column.ColumnName}  {{ get; set; }}",
                                cbAllowNullableString.Checked
                            );
                        builder1.AppendLine(str);
                    }
                    builder1.AppendLine("\n}");
                }
                catch (Exception exception)
                {
                    builder1 = new StringBuilder();
                    builder1.Append(exception.Message);
                }
                Context.Post(_ =>
                {
                    Cursor = Cursors.Default;
                    ToggleControls(true);
                    txtOutput.Text = builder1.ToString().Replace("System.", string.Empty);
                    Activate();
                    txtTotalRows.Text = $@"Total: {dataGridView.RowCount}";
                }, null);
            }, TaskCreationOptions.LongRunning);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var mode = dataGridView.AutoSizeColumnsMode;
            switch (mode)
            {
                case DataGridViewAutoSizeColumnsMode.AllCells:
                    dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
                    break;
                case DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader:
                    dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                    break;
                case DataGridViewAutoSizeColumnsMode.DisplayedCells:
                    dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;
                    break;
                case DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader:
                    dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    break;
                case DataGridViewAutoSizeColumnsMode.None:
                    dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                    break;
                case DataGridViewAutoSizeColumnsMode.ColumnHeader:
                    dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    break;
                case DataGridViewAutoSizeColumnsMode.Fill:
                    dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    break;
            }
        }

        private void dataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(sender is DataGridView grid)) return;
            var text = grid.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue?.ToString() ?? string.Empty;
            CopyToClipboard(text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ((DataTable)dataGridView.DataSource).DefaultView.RowFilter = txtFilter.Text;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
	        Properties.Settings.Default.DB = txtDatabase.Text;
	        Properties.Settings.Default.DS = txtServer.Text;
	        Properties.Settings.Default.Port = txtPort.Text;
	        Properties.Settings.Default.User = txtUserId.Text;
	        Properties.Settings.Default.Password = txtPassword.Text;
	        Properties.Settings.Default.Query = txtQuery.Text;
	        Properties.Settings.Default.OutputText = txtOutput.Text;
	        Properties.Settings.Default.Json = txtJSON.Text;
			Properties.Settings.Default.Api = txtAPI.Text;
			Properties.Settings.Default.Save();
            Application.Exit();
        }

		private void txtServer_TextChanged(object sender, EventArgs e)
		{
			//TODO
		}
	}
}