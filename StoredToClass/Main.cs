﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    this.Hide();
                    (new Form2()).Show();
                    break;
                case "MSSQL":
                    (new Form1()).Show();
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