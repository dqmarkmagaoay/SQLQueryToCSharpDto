using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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

namespace StoredToClass
{
    public partial class Form1 : Form
    {

        public SynchronizationContext Context { get; set; }
        public Form1()
        {
            InitializeComponent();
            Context = SynchronizationContext.Current;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
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
            Task.Factory.StartNew(() =>
            {
                var hasException = false;
                try
                {
                    if (string.IsNullOrEmpty(txtServer.Text) || string.IsNullOrEmpty(txtServer.Text))
                    {
                        throw new Exception("Server Name and Database name are required");
                    }

                    var connectionString = $"Data Source={txtServer.Text}; Initial Catalog={txtDatabase.Text}; Integrated Security=True; MultipleActiveResultSets=True;";
                    if (!string.IsNullOrEmpty(txtUserId.Text))
                    {
                        connectionString = $"Data Source={txtServer.Text}; Initial Catalog={txtDatabase.Text};User Id={txtUserId.Text};Password={txtPassword.Text};";
                    }
                    using (var conn = new SqlConnection(connectionString))
                    using (var cmd = new SqlCommand(txtQuery.Text, conn))
                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        var table = new DataTable();
                        adapter.Fill(table);
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
            throw new Exception("All field is required!");
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
            throw new Exception("Copied");
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
                    var handler = new HttpClientHandler()
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

        private void button4_Click(object sender, EventArgs e)
        {
            CheckInputs(((Control)sender).Parent);
            txtOutput.Text = string.Empty;
            var txt = txtPrism.Text ?? string.Empty;
            txt = txt.Trim();
            Cursor = Cursors.WaitCursor;
            ToggleControls(false);
            dataGridView.DataSource = null;
            Task.Factory.StartNew(() =>
            {
                try
                {
                    DataTable table = null;
                    if (string.IsNullOrEmpty(txt))
                        throw new Exception("No value to search");
                    var strArray = new List<string>();
                    if (!txt.Contains(","))
                    {
                        strArray.Add(txt);
                    }
                    else
                    {
                        strArray.AddRange(txt.Split(',').ToList());
                    }

                    foreach (var str in strArray)
                    {
                        var handler = new HttpClientHandler()
                        {
                            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                        };
                        using (var client = new HttpClient(handler))
                        {
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var response = client.GetAsync($"https://services.vxi.com.ph/prismsvc/employee/{str}").Result;
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
                                stringData = stringData.StartsWith("[") && stringData.EndsWith("]") ? $"{{\"data\":{stringData}}}" : stringData;
                                try
                                {
                                    var dt = JsonConvert.DeserializeObject<DataSet>(stringData).Tables[0];
                                    if (table == null)
                                    {
                                        table = dt.Clone();
                                    }
                                    foreach (DataRow dataRow in dt.Rows)
                                    {
                                        table.ImportRow(dataRow);
                                    }
                                }
                                catch (Exception exception)
                                {
                                    Console.WriteLine(exception);
                                    var oldData = stringData;
                                    stringData = $"{{\"data\":[{stringData}]}}";
                                    try
                                    {
                                        var dt = JsonConvert.DeserializeObject<DataSet>(stringData).Tables[0];
                                        if (table == null)
                                        {
                                            table = dt.Clone();
                                        }
                                        foreach (DataRow dataRow in dt.Rows)
                                        {
                                            table.ImportRow(dataRow);
                                        }
                                    }
                                    catch (Exception exception1)
                                    {
                                        Console.WriteLine(exception1.Message);
                                        stringData = $"{{\"data\": [{{\"data\": \"{oldData}\"}}]}}";
                                        var dt = JsonConvert.DeserializeObject<DataSet>(stringData).Tables[0];
                                        if (table == null)
                                        {
                                            table = dt.Clone();
                                        }
                                        foreach (DataRow dataRow in dt.Rows)
                                        {
                                            table.ImportRow(dataRow);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (table != null && table.Rows.Count > 0)
                    {
                        table.Columns.Remove("fileImage");
                        table.Columns.Remove("projectId");
                        var dC = new DataColumn("Domain", typeof(string))
                        {
                            DefaultValue = "VXIPHP"
                        };
                        table.Columns.Add(dC);
                        table.Columns["Domain"].SetOrdinal(0);
                        table.Columns["windowsNT"].SetOrdinal(1);

                        foreach (DataColumn dataColumn in table.Columns)
                        {
                            dataColumn.ColumnName = dataColumn.ColumnName.ToUpper();
                        }
                    }

                    Context.Post(_ =>
                    {
                        dataGridView.DataSource = table;
                    }, null);
                }
                catch (Exception exception)
                {
                    Context.Post(_ => { txtOutput.Text = exception.Message; }, null);
                }
                Context.Post(_ =>
                {
                    Cursor = Cursors.Default;
                    ToggleControls(true);
                    txtTotalRows.Text = $@"Total: {dataGridView.RowCount}";
                    Activate();
                }, null);
            }, TaskCreationOptions.LongRunning);
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            CheckInputs(((Control)sender).Parent);
            txtOutput.Text = string.Empty;
            var txt = txtEmail.Text ?? string.Empty;
            txt = txt.Trim();
            Cursor = Cursors.WaitCursor;
            ToggleControls(false);
            dataGridView.DataSource = null;
            Task.Factory.StartNew(() =>
            {
                try
                {
                    DataTable table = null;
                    if (string.IsNullOrEmpty(txt))
                        throw new Exception("No value to search");
                    var strArray = new List<string>();
                    if (!txt.Contains(","))
                    {
                        strArray.Add(txt);
                    }
                    else
                    {
                        strArray.AddRange(txt.Split(',').ToList());
                    }
                    var watch = new Stopwatch();
                    watch.Start();
                    foreach (var str in strArray)
                    {
                        var handler = new HttpClientHandler()
                        {
                            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                        };
                        using (var client = new HttpClient(handler))
                        {
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var response = client.GetAsync($"https://vxilamisweb01.vxiusa.com/hr-services/api/v1/employees/email/{str}").Result;
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
                                stringData = stringData.StartsWith("[") && stringData.EndsWith("]") ? $"{{\"data\":{stringData}}}" : stringData;
                                try
                                {
                                    var dt = JsonConvert.DeserializeObject<DataSet>(stringData).Tables[0];
                                    if (table == null)
                                    {
                                        table = dt.Clone();
                                    }
                                    foreach (DataRow dataRow in dt.Rows)
                                    {
                                        table.ImportRow(dataRow);
                                    }
                                }
                                catch (Exception exception)
                                {
                                    Console.WriteLine(exception);
                                    var oldData = stringData;
                                    stringData = $"{{\"data\":[{stringData}]}}";
                                    try
                                    {
                                        var dt = JsonConvert.DeserializeObject<DataSet>(stringData).Tables[0];
                                        if (table == null)
                                        {
                                            table = new DataTable();
                                            table.Columns.Add("Id");
                                            //table = dt.Clone();
                                        }
                                        foreach (DataRow dataRow in dt.Rows)
                                        {
                                            var row = table.NewRow()[0] = dataRow["id"]?.ToString().Split('-')[1];
                                            table.Rows.Add(row);
                                            //dataRow["id"] = dataRow["id"]?.ToString().Split('-')[1];
                                            //table.ImportRow(dataRow);
                                        }
                                    }
                                    catch (Exception exception1)
                                    {
                                        Console.WriteLine(exception1.Message);
                                        stringData = $"{{\"data\": [{{\"data\": \"{oldData}\"}}]}}";
                                        var dt = JsonConvert.DeserializeObject<DataSet>(stringData).Tables[0];
                                        if (table == null)
                                        {
                                            table = dt.Clone();
                                        }
                                        foreach (DataRow dataRow in dt.Rows)
                                        {
                                            dataRow["id"] = dataRow["id"]?.ToString().Split('-')[1];
                                            table.ImportRow(dataRow);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //if (table != null && table.Rows.Count > 0)
                    //{
                    //    table.Columns.Remove("fileImage");
                    //    table.Columns.Remove("projectId");
                    //    table.Columns["windowsNT"].SetOrdinal(0);

                    //    foreach (DataRow tableRow in table.Rows)
                    //    {
                    //        var nt = tableRow["windowsNT"].ToString();
                    //        tableRow["windowsNT"] = $@"VXIPHP\{nt}";
                    //        nt = tableRow["windowsNT"].ToString();
                    //    }
                    //    foreach (DataColumn dataColumn in table.Columns)
                    //    {
                    //        dataColumn.ColumnName = dataColumn.ColumnName.ToUpper();
                    //    }
                    //}
                    watch.Stop();
                    var timeSpan = watch.Elapsed;
                    var total = $"Elapsed Time: {timeSpan.Hours}h {timeSpan.Minutes}m {timeSpan.Seconds}s {timeSpan.Milliseconds}ms";
                    Context.Post(_ =>
                    {
                        dataGridView.DataSource = table;
                        txtOutput.Text = total;
                    }, null);
                }
                catch (Exception exception)
                {
                    Context.Post(_ => { txtOutput.Text = exception.Message; }, null);
                }
                Context.Post(_ =>
                {
                    Cursor = Cursors.Default;
                    ToggleControls(true);
                    txtTotalRows.Text = $@"Total: {dataGridView.RowCount}";
                    Activate();
                }, null);
            }, TaskCreationOptions.LongRunning);
        }
    }
}
