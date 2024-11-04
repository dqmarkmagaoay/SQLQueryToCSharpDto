﻿namespace StoredToClass
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.btnGenerate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGenerateFromApi = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnEmail = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.txtPrism = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnJSON = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.txtTotalRows = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.txtJSON = new System.Windows.Forms.TextBox();
            this.txtAPI = new System.Windows.Forms.TextBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnGenerate.Location = new System.Drawing.Point(3, 185);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(0);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(408, 23);
            this.btnGenerate.TabIndex = 3;
            this.btnGenerate.Text = "GENERATE";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "DATA SOURCE:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 252);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "OUTPUT";
            // 
            // btnGenerateFromApi
            // 
            this.btnGenerateFromApi.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnGenerateFromApi.Location = new System.Drawing.Point(3, 185);
            this.btnGenerateFromApi.Name = "btnGenerateFromApi";
            this.btnGenerateFromApi.Size = new System.Drawing.Size(408, 23);
            this.btnGenerateFromApi.TabIndex = 5;
            this.btnGenerateFromApi.Text = "GENERATE";
            this.btnGenerateFromApi.UseVisualStyleBackColor = true;
            this.btnGenerateFromApi.Click += new System.EventHandler(this.btnGenerateFromApi_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(422, 237);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.txtEmail);
            this.tabPage5.Controls.Add(this.btnEmail);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(414, 211);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "EMAIL TO HRID";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // txtEmail
            // 
            this.txtEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEmail.Location = new System.Drawing.Point(3, 3);
            this.txtEmail.MaxLength = 0;
            this.txtEmail.Multiline = true;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEmail.Size = new System.Drawing.Size(408, 182);
            this.txtEmail.TabIndex = 10;
            // 
            // btnEmail
            // 
            this.btnEmail.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnEmail.Location = new System.Drawing.Point(3, 185);
            this.btnEmail.Name = "btnEmail";
            this.btnEmail.Size = new System.Drawing.Size(408, 23);
            this.btnEmail.TabIndex = 11;
            this.btnEmail.Text = "GENERATE";
            this.btnEmail.UseVisualStyleBackColor = true;
            this.btnEmail.Click += new System.EventHandler(this.btnEmail_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.txtPrism);
            this.tabPage4.Controls.Add(this.button4);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(414, 211);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "HRID TO NT";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // txtPrism
            // 
            this.txtPrism.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPrism.Location = new System.Drawing.Point(3, 3);
            this.txtPrism.MaxLength = 0;
            this.txtPrism.Multiline = true;
            this.txtPrism.Name = "txtPrism";
            this.txtPrism.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPrism.Size = new System.Drawing.Size(408, 182);
            this.txtPrism.TabIndex = 8;
            // 
            // button4
            // 
            this.button4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button4.Location = new System.Drawing.Point(3, 185);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(408, 23);
            this.button4.TabIndex = 9;
            this.button4.Text = "GENERATE";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtJSON);
            this.tabPage3.Controls.Add(this.btnJSON);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(414, 211);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "JSON TO CLASS";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnJSON
            // 
            this.btnJSON.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnJSON.Location = new System.Drawing.Point(3, 185);
            this.btnJSON.Name = "btnJSON";
            this.btnJSON.Size = new System.Drawing.Size(408, 23);
            this.btnJSON.TabIndex = 7;
            this.btnJSON.Text = "GENERATE";
            this.btnJSON.UseVisualStyleBackColor = true;
            this.btnJSON.Click += new System.EventHandler(this.btnJSON_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtAPI);
            this.tabPage2.Controls.Add(this.btnGenerateFromApi);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(414, 211);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "API TO CLASS";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.txtPort);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.txtPassword);
            this.tabPage1.Controls.Add(this.txtUserId);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtQuery);
            this.tabPage1.Controls.Add(this.btnGenerate);
            this.tabPage1.Controls.Add(this.txtDatabase);
            this.tabPage1.Controls.Add(this.txtServer);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(414, 211);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "SQL TO CLASS";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(222, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "PASSWORD:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(222, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "USER ID:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "SQL:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "DATABASE:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 268);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(422, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "CLICK TO COPY THE TEXT BELOW";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.txtOutpput_MouseEnter);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView.Location = new System.Drawing.Point(440, 69);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(797, 518);
            this.dataGridView.TabIndex = 11;
            this.dataGridView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentDoubleClick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(440, 11);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(797, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "CLICK TO CHANGE COLUMN MODE";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtTotalRows
            // 
            this.txtTotalRows.AutoSize = true;
            this.txtTotalRows.Location = new System.Drawing.Point(437, 590);
            this.txtTotalRows.Name = "txtTotalRows";
            this.txtTotalRows.Size = new System.Drawing.Size(42, 13);
            this.txtTotalRows.TabIndex = 13;
            this.txtTotalRows.Text = "TOTAL";
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(440, 40);
            this.txtFilter.Multiline = true;
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(716, 23);
            this.txtFilter.TabIndex = 14;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1162, 40);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 15;
            this.button3.Text = "SEARCH";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtPort
            // 
            this.txtPort.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::StoredToClass.Properties.Settings.Default, "Port", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtPort.Location = new System.Drawing.Point(143, 19);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(76, 20);
            this.txtPort.TabIndex = 18;
            this.txtPort.Text = global::StoredToClass.Properties.Settings.Default.Port;
            // 
            // txtPassword
            // 
            this.txtPassword.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::StoredToClass.Properties.Settings.Default, "Password", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtPassword.Location = new System.Drawing.Point(225, 58);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(183, 20);
            this.txtPassword.TabIndex = 15;
            this.txtPassword.Text = global::StoredToClass.Properties.Settings.Default.Password;
            // 
            // txtUserId
            // 
            this.txtUserId.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::StoredToClass.Properties.Settings.Default, "User", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtUserId.Location = new System.Drawing.Point(225, 19);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(183, 20);
            this.txtUserId.TabIndex = 14;
            this.txtUserId.Text = global::StoredToClass.Properties.Settings.Default.User;
            // 
            // txtQuery
            // 
            this.txtQuery.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::StoredToClass.Properties.Settings.Default, "Query", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtQuery.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtQuery.Location = new System.Drawing.Point(3, 97);
            this.txtQuery.Multiline = true;
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtQuery.Size = new System.Drawing.Size(408, 88);
            this.txtQuery.TabIndex = 2;
            this.txtQuery.Text = global::StoredToClass.Properties.Settings.Default.Query;
            // 
            // txtDatabase
            // 
            this.txtDatabase.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::StoredToClass.Properties.Settings.Default, "DB", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtDatabase.Location = new System.Drawing.Point(3, 58);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(216, 20);
            this.txtDatabase.TabIndex = 1;
            this.txtDatabase.Text = global::StoredToClass.Properties.Settings.Default.DB;
            // 
            // txtServer
            // 
            this.txtServer.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::StoredToClass.Properties.Settings.Default, "DS", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtServer.Location = new System.Drawing.Point(3, 19);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(134, 20);
            this.txtServer.TabIndex = 0;
            this.txtServer.Text = global::StoredToClass.Properties.Settings.Default.DS;
            // 
            // txtJSON
            // 
            this.txtJSON.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::StoredToClass.Properties.Settings.Default, "Json", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtJSON.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtJSON.Location = new System.Drawing.Point(3, 3);
            this.txtJSON.MaxLength = 0;
            this.txtJSON.Multiline = true;
            this.txtJSON.Name = "txtJSON";
            this.txtJSON.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtJSON.Size = new System.Drawing.Size(408, 182);
            this.txtJSON.TabIndex = 6;
            this.txtJSON.Text = global::StoredToClass.Properties.Settings.Default.Json;
            // 
            // txtAPI
            // 
            this.txtAPI.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::StoredToClass.Properties.Settings.Default, "Api", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtAPI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAPI.Location = new System.Drawing.Point(3, 3);
            this.txtAPI.Multiline = true;
            this.txtAPI.Name = "txtAPI";
            this.txtAPI.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAPI.Size = new System.Drawing.Size(408, 182);
            this.txtAPI.TabIndex = 4;
            this.txtAPI.Text = global::StoredToClass.Properties.Settings.Default.Api;
            // 
            // txtOutput
            // 
            this.txtOutput.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::StoredToClass.Properties.Settings.Default, "OutputText", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtOutput.Location = new System.Drawing.Point(12, 297);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutput.Size = new System.Drawing.Size(422, 290);
            this.txtOutput.TabIndex = 8;
            this.txtOutput.Text = global::StoredToClass.Properties.Settings.Default.OutputText;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(140, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "PORT:";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1249, 612);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtTotalRows);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtOutput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form2";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GENERATOR";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form2_FormClosed);
            this.tabControl1.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.TextBox txtQuery;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAPI;
        private System.Windows.Forms.Button btnGenerateFromApi;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox txtJSON;
        private System.Windows.Forms.Button btnJSON;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label txtTotalRows;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox txtPrism;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnEmail;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label7;
    }
}
