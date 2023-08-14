namespace YangTreeView
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            treeView1 = new TreeView();
            tbPath = new TextBox();
            textBox2 = new TextBox();
            tabControl1 = new TabControl();
            filesTabPage = new TabPage();
            label2 = new Label();
            rtbMissingFiles = new RichTextBox();
            label1 = new Label();
            rtbParsedFiles = new RichTextBox();
            treeViewTabPage = new TabPage();
            pg = new PropertyGrid();
            tbType = new TextBox();
            lblType = new Label();
            tbPgValue = new TextBox();
            rtbResponse = new RichTextBox();
            button2 = new Button();
            tabPage1 = new TabPage();
            gbOpenConfigSettings = new GroupBox();
            cbSkipVerify = new CheckBox();
            groupBox2 = new GroupBox();
            cbParseSubFolders = new CheckBox();
            groupBox1 = new GroupBox();
            label3 = new Label();
            button3 = new Button();
            label4 = new Label();
            tbPassword = new TextBox();
            label5 = new Label();
            tbUsername = new TextBox();
            label6 = new Label();
            tbPort = new TextBox();
            tbIp = new TextBox();
            tabControl1.SuspendLayout();
            filesTabPage.SuspendLayout();
            treeViewTabPage.SuspendLayout();
            tabPage1.SuspendLayout();
            gbOpenConfigSettings.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button1.Location = new Point(6, 27);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(682, 22);
            button1.TabIndex = 0;
            button1.Text = "Parse";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // treeView1
            // 
            treeView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            treeView1.Location = new Point(0, 0);
            treeView1.Margin = new Padding(2);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(416, 549);
            treeView1.TabIndex = 1;
            treeView1.AfterSelect += treeView1_AfterSelect;
            treeView1.NodeMouseClick += treeView1_NodeMouseClick;
            // 
            // tbPath
            // 
            tbPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbPath.Location = new Point(4, 4);
            tbPath.Margin = new Padding(2);
            tbPath.Name = "tbPath";
            tbPath.Size = new Size(684, 23);
            tbPath.TabIndex = 2;
            tbPath.Text = "C:\\Users\\[User]\\Documents\\YangFiles";
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Cursor = Cursors.IBeam;
            textBox2.Location = new Point(419, 4);
            textBox2.Margin = new Padding(2);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(268, 20);
            textBox2.TabIndex = 4;
            textBox2.Text = "No Path";
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(filesTabPage);
            tabControl1.Controls.Add(treeViewTabPage);
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Location = new Point(1, -1);
            tabControl1.Margin = new Padding(3, 2, 3, 2);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(698, 575);
            tabControl1.TabIndex = 5;
            // 
            // filesTabPage
            // 
            filesTabPage.Controls.Add(label2);
            filesTabPage.Controls.Add(rtbMissingFiles);
            filesTabPage.Controls.Add(label1);
            filesTabPage.Controls.Add(rtbParsedFiles);
            filesTabPage.Controls.Add(tbPath);
            filesTabPage.Controls.Add(button1);
            filesTabPage.Location = new Point(4, 24);
            filesTabPage.Margin = new Padding(3, 2, 3, 2);
            filesTabPage.Name = "filesTabPage";
            filesTabPage.Padding = new Padding(3, 2, 3, 2);
            filesTabPage.Size = new Size(690, 547);
            filesTabPage.TabIndex = 0;
            filesTabPage.Text = "Files";
            filesTabPage.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(286, 61);
            label2.Name = "label2";
            label2.Size = new Size(74, 15);
            label2.TabIndex = 6;
            label2.Text = "Missing Files";
            // 
            // rtbMissingFiles
            // 
            rtbMissingFiles.Location = new Point(286, 78);
            rtbMissingFiles.Margin = new Padding(3, 2, 3, 2);
            rtbMissingFiles.Name = "rtbMissingFiles";
            rtbMissingFiles.ReadOnly = true;
            rtbMissingFiles.Size = new Size(277, 462);
            rtbMissingFiles.TabIndex = 5;
            rtbMissingFiles.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(4, 61);
            label1.Name = "label1";
            label1.Size = new Size(68, 15);
            label1.TabIndex = 4;
            label1.Text = "Parsed Files";
            // 
            // rtbParsedFiles
            // 
            rtbParsedFiles.Location = new Point(4, 78);
            rtbParsedFiles.Margin = new Padding(3, 2, 3, 2);
            rtbParsedFiles.Name = "rtbParsedFiles";
            rtbParsedFiles.ReadOnly = true;
            rtbParsedFiles.Size = new Size(277, 462);
            rtbParsedFiles.TabIndex = 3;
            rtbParsedFiles.Text = "";
            // 
            // treeViewTabPage
            // 
            treeViewTabPage.Controls.Add(pg);
            treeViewTabPage.Controls.Add(tbType);
            treeViewTabPage.Controls.Add(lblType);
            treeViewTabPage.Controls.Add(tbPgValue);
            treeViewTabPage.Controls.Add(rtbResponse);
            treeViewTabPage.Controls.Add(button2);
            treeViewTabPage.Controls.Add(treeView1);
            treeViewTabPage.Controls.Add(textBox2);
            treeViewTabPage.Location = new Point(4, 24);
            treeViewTabPage.Margin = new Padding(3, 2, 3, 2);
            treeViewTabPage.Name = "treeViewTabPage";
            treeViewTabPage.Padding = new Padding(3, 2, 3, 2);
            treeViewTabPage.Size = new Size(690, 547);
            treeViewTabPage.TabIndex = 1;
            treeViewTabPage.Text = "Tree View";
            treeViewTabPage.UseVisualStyleBackColor = true;
            // 
            // pg
            // 
            pg.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pg.Location = new Point(420, 52);
            pg.Margin = new Padding(3, 2, 3, 2);
            pg.Name = "pg";
            pg.Size = new Size(267, 214);
            pg.TabIndex = 12;
            pg.SelectedGridItemChanged += pg_SelectedGridItemChanged;
            // 
            // tbType
            // 
            tbType.Enabled = false;
            tbType.Location = new Point(487, 28);
            tbType.Margin = new Padding(3, 2, 3, 2);
            tbType.Name = "tbType";
            tbType.Size = new Size(200, 23);
            tbType.TabIndex = 11;
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Location = new Point(419, 30);
            lblType.Name = "lblType";
            lblType.Size = new Size(34, 15);
            lblType.TabIndex = 10;
            lblType.Text = "Type:";
            // 
            // tbPgValue
            // 
            tbPgValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbPgValue.Enabled = false;
            tbPgValue.Location = new Point(420, 271);
            tbPgValue.Margin = new Padding(3, 2, 3, 2);
            tbPgValue.Multiline = true;
            tbPgValue.Name = "tbPgValue";
            tbPgValue.ScrollBars = ScrollBars.Vertical;
            tbPgValue.Size = new Size(268, 64);
            tbPgValue.TabIndex = 9;
            // 
            // rtbResponse
            // 
            rtbResponse.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbResponse.Location = new Point(420, 364);
            rtbResponse.Margin = new Padding(3, 2, 3, 2);
            rtbResponse.Name = "rtbResponse";
            rtbResponse.Size = new Size(267, 185);
            rtbResponse.TabIndex = 6;
            rtbResponse.Text = "";
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button2.Location = new Point(419, 338);
            button2.Margin = new Padding(3, 2, 3, 2);
            button2.Name = "button2";
            button2.Size = new Size(268, 22);
            button2.TabIndex = 5;
            button2.Text = "Get Value";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(gbOpenConfigSettings);
            tabPage1.Controls.Add(groupBox2);
            tabPage1.Controls.Add(groupBox1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Margin = new Padding(3, 2, 3, 2);
            tabPage1.Name = "tabPage1";
            tabPage1.Size = new Size(690, 547);
            tabPage1.TabIndex = 2;
            tabPage1.Text = "Settings";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // gbOpenConfigSettings
            // 
            gbOpenConfigSettings.Controls.Add(cbSkipVerify);
            gbOpenConfigSettings.Location = new Point(3, 209);
            gbOpenConfigSettings.Name = "gbOpenConfigSettings";
            gbOpenConfigSettings.Size = new Size(216, 53);
            gbOpenConfigSettings.TabIndex = 11;
            gbOpenConfigSettings.TabStop = false;
            gbOpenConfigSettings.Text = "OpenConfig Settings";
            // 
            // cbSkipVerify
            // 
            cbSkipVerify.AutoSize = true;
            cbSkipVerify.Location = new Point(5, 22);
            cbSkipVerify.Name = "cbSkipVerify";
            cbSkipVerify.Size = new Size(80, 19);
            cbSkipVerify.TabIndex = 12;
            cbSkipVerify.Text = "Skip Verify";
            cbSkipVerify.UseVisualStyleBackColor = true;
            cbSkipVerify.CheckedChanged += cbSkipVerify_CheckedChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(cbParseSubFolders);
            groupBox2.Location = new Point(3, 152);
            groupBox2.Margin = new Padding(3, 2, 3, 2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 2, 3, 2);
            groupBox2.Size = new Size(216, 52);
            groupBox2.TabIndex = 10;
            groupBox2.TabStop = false;
            groupBox2.Text = "Parse Settings";
            // 
            // cbParseSubFolders
            // 
            cbParseSubFolders.AutoSize = true;
            cbParseSubFolders.Location = new Point(5, 20);
            cbParseSubFolders.Margin = new Padding(3, 2, 3, 2);
            cbParseSubFolders.Name = "cbParseSubFolders";
            cbParseSubFolders.Size = new Size(118, 19);
            cbParseSubFolders.TabIndex = 0;
            cbParseSubFolders.Text = "Parse Sub Folders";
            cbParseSubFolders.UseVisualStyleBackColor = true;
            cbParseSubFolders.CheckedChanged += cbParseSubFolders_CheckedChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(tbPassword);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(tbUsername);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(tbPort);
            groupBox1.Controls.Add(tbIp);
            groupBox1.Location = new Point(3, 2);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(216, 146);
            groupBox1.TabIndex = 9;
            groupBox1.TabStop = false;
            groupBox1.Text = "Device Settings";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(5, 17);
            label3.Name = "label3";
            label3.Size = new Size(65, 15);
            label3.TabIndex = 0;
            label3.Text = "IP Address:";
            // 
            // button3
            // 
            button3.Location = new Point(5, 114);
            button3.Margin = new Padding(3, 2, 3, 2);
            button3.Name = "button3";
            button3.Size = new Size(198, 22);
            button3.TabIndex = 8;
            button3.Text = "Save";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(5, 42);
            label4.Name = "label4";
            label4.Size = new Size(32, 15);
            label4.TabIndex = 1;
            label4.Text = "Port:";
            // 
            // tbPassword
            // 
            tbPassword.Location = new Point(94, 89);
            tbPassword.Margin = new Padding(3, 2, 3, 2);
            tbPassword.Name = "tbPassword";
            tbPassword.PasswordChar = '*';
            tbPassword.Size = new Size(110, 23);
            tbPassword.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(5, 67);
            label5.Name = "label5";
            label5.Size = new Size(63, 15);
            label5.TabIndex = 2;
            label5.Text = "Username:";
            // 
            // tbUsername
            // 
            tbUsername.Location = new Point(94, 64);
            tbUsername.Margin = new Padding(3, 2, 3, 2);
            tbUsername.Name = "tbUsername";
            tbUsername.Size = new Size(110, 23);
            tbUsername.TabIndex = 6;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(5, 92);
            label6.Name = "label6";
            label6.Size = new Size(60, 15);
            label6.TabIndex = 3;
            label6.Text = "Password:";
            // 
            // tbPort
            // 
            tbPort.Location = new Point(94, 40);
            tbPort.Margin = new Padding(3, 2, 3, 2);
            tbPort.Name = "tbPort";
            tbPort.Size = new Size(110, 23);
            tbPort.TabIndex = 5;
            // 
            // tbIp
            // 
            tbIp.Location = new Point(94, 15);
            tbIp.Margin = new Padding(3, 2, 3, 2);
            tbIp.Name = "tbIp";
            tbIp.Size = new Size(110, 23);
            tbIp.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(699, 582);
            Controls.Add(tabControl1);
            Margin = new Padding(3, 2, 3, 2);
            MinimumSize = new Size(715, 621);
            Name = "Form1";
            Text = "Form1";
            tabControl1.ResumeLayout(false);
            filesTabPage.ResumeLayout(false);
            filesTabPage.PerformLayout();
            treeViewTabPage.ResumeLayout(false);
            treeViewTabPage.PerformLayout();
            tabPage1.ResumeLayout(false);
            gbOpenConfigSettings.ResumeLayout(false);
            gbOpenConfigSettings.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private TreeView treeView1;
        private TextBox tbPath;
        private TextBox textBox2;
        private TabControl tabControl1;
        private TabPage filesTabPage;
        private TabPage treeViewTabPage;
        private RichTextBox rtbParsedFiles;
        private Label label1;
        private Label label2;
        private RichTextBox rtbMissingFiles;
        private RichTextBox rtbResponse;
        private Button button2;
        private TabPage tabPage1;
        private TextBox tbPassword;
        private TextBox tbUsername;
        private TextBox tbPort;
        private TextBox tbIp;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Button button3;
        private Label label7;
        private TextBox tbPgValue;
        private TextBox tbType;
        private Label lblType;
        private PropertyGrid pg;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private CheckBox checkBox1;
        private GroupBox gbOpenConfigSettings;
        private CheckBox cbSkipVerify;
        private CheckBox cbParseSubFolders;
    }
}