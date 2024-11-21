using System;
using System.Windows.Forms;

namespace StoredToClass
{
    public partial class Main : Form
    {
        private static readonly object[] ServerTypes = { "MYSQL", "MSSQL" };

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            cmbServerType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbServerType.Items.AddRange(ServerTypes);
            cmbServerType.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (cmbServerType.Text)
            {
                case "MYSQL":
					Hide();
                    new Form2().Show();
                    break;
                case "MSSQL":
	                Hide();
                    new Form1().Show();
                    break;
                default:
                    throw new Exception("SELECT A SERVER TYPE");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
