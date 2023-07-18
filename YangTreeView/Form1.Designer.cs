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
            button3 = new Button();
            tbPassword = new TextBox();
            tbUsername = new TextBox();
            tbPort = new TextBox();
            tbIp = new TextBox();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            tabControl1.SuspendLayout();
            filesTabPage.SuspendLayout();
            treeViewTabPage.SuspendLayout();
            tabPage1.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button1.Location = new Point(7, 36);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(779, 30);
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
            treeView1.Size = new Size(475, 731);
            treeView1.TabIndex = 1;
            treeView1.AfterSelect += treeView1_AfterSelect;
            treeView1.NodeMouseClick += treeView1_NodeMouseClick;
            // 
            // tbPath
            // 
            tbPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbPath.Location = new Point(5, 5);
            tbPath.Margin = new Padding(2);
            tbPath.Name = "tbPath";
            tbPath.Size = new Size(781, 27);
            tbPath.TabIndex = 2;
            tbPath.Text = "C:\\Users\\[User]\\Documents\\YangFiles";
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Cursor = Cursors.IBeam;
            textBox2.Location = new Point(479, 5);
            textBox2.Margin = new Padding(2);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(306, 27);
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
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(798, 767);
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
            filesTabPage.Location = new Point(4, 29);
            filesTabPage.Name = "filesTabPage";
            filesTabPage.Padding = new Padding(3);
            filesTabPage.Size = new Size(790, 734);
            filesTabPage.TabIndex = 0;
            filesTabPage.Text = "Files";
            filesTabPage.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(327, 81);
            label2.Name = "label2";
            label2.Size = new Size(92, 20);
            label2.TabIndex = 6;
            label2.Text = "Missing Files";
            // 
            // rtbMissingFiles
            // 
            rtbMissingFiles.Location = new Point(327, 104);
            rtbMissingFiles.Name = "rtbMissingFiles";
            rtbMissingFiles.ReadOnly = true;
            rtbMissingFiles.Size = new Size(316, 614);
            rtbMissingFiles.TabIndex = 5;
            rtbMissingFiles.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(5, 81);
            label1.Name = "label1";
            label1.Size = new Size(85, 20);
            label1.TabIndex = 4;
            label1.Text = "Parsed Files";
            // 
            // rtbParsedFiles
            // 
            rtbParsedFiles.Location = new Point(5, 104);
            rtbParsedFiles.Name = "rtbParsedFiles";
            rtbParsedFiles.ReadOnly = true;
            rtbParsedFiles.Size = new Size(316, 614);
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
            treeViewTabPage.Location = new Point(4, 29);
            treeViewTabPage.Name = "treeViewTabPage";
            treeViewTabPage.Padding = new Padding(3);
            treeViewTabPage.Size = new Size(790, 734);
            treeViewTabPage.TabIndex = 1;
            treeViewTabPage.Text = "Tree View";
            treeViewTabPage.UseVisualStyleBackColor = true;
            // 
            // pg
            // 
            pg.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pg.Location = new Point(480, 70);
            pg.Name = "pg";
            pg.Size = new Size(305, 285);
            pg.TabIndex = 12;
            pg.SelectedGridItemChanged += pg_SelectedGridItemChanged;
            // 
            // tbType
            // 
            tbType.Enabled = false;
            tbType.Location = new Point(557, 37);
            tbType.Name = "tbType";
            tbType.Size = new Size(228, 27);
            tbType.TabIndex = 11;
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Location = new Point(479, 40);
            lblType.Name = "lblType";
            lblType.Size = new Size(43, 20);
            lblType.TabIndex = 10;
            lblType.Text = "Type:";
            // 
            // tbPgValue
            // 
            tbPgValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbPgValue.Enabled = false;
            tbPgValue.Location = new Point(480, 361);
            tbPgValue.Multiline = true;
            tbPgValue.Name = "tbPgValue";
            tbPgValue.ScrollBars = ScrollBars.Vertical;
            tbPgValue.Size = new Size(306, 84);
            tbPgValue.TabIndex = 9;
            // 
            // rtbResponse
            // 
            rtbResponse.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbResponse.Location = new Point(480, 486);
            rtbResponse.Name = "rtbResponse";
            rtbResponse.Size = new Size(305, 245);
            rtbResponse.TabIndex = 6;
            rtbResponse.Text = "";
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button2.Location = new Point(479, 451);
            button2.Name = "button2";
            button2.Size = new Size(306, 29);
            button2.TabIndex = 5;
            button2.Text = "Get Value";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(button3);
            tabPage1.Controls.Add(tbPassword);
            tabPage1.Controls.Add(tbUsername);
            tabPage1.Controls.Add(tbPort);
            tabPage1.Controls.Add(tbIp);
            tabPage1.Controls.Add(label6);
            tabPage1.Controls.Add(label5);
            tabPage1.Controls.Add(label4);
            tabPage1.Controls.Add(label3);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Size = new Size(790, 734);
            tabPage1.TabIndex = 2;
            tabPage1.Text = "Settings";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(7, 138);
            button3.Name = "button3";
            button3.Size = new Size(226, 29);
            button3.TabIndex = 8;
            button3.Text = "Save";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // tbPassword
            // 
            tbPassword.Location = new Point(108, 105);
            tbPassword.Name = "tbPassword";
            tbPassword.PasswordChar = '*';
            tbPassword.Size = new Size(125, 27);
            tbPassword.TabIndex = 7;
            // 
            // tbUsername
            // 
            tbUsername.Location = new Point(108, 72);
            tbUsername.Name = "tbUsername";
            tbUsername.Size = new Size(125, 27);
            tbUsername.TabIndex = 6;
            // 
            // tbPort
            // 
            tbPort.Location = new Point(108, 39);
            tbPort.Name = "tbPort";
            tbPort.Size = new Size(125, 27);
            tbPort.TabIndex = 5;
            // 
            // tbIp
            // 
            tbIp.Location = new Point(108, 6);
            tbIp.Name = "tbIp";
            tbIp.Size = new Size(125, 27);
            tbIp.TabIndex = 4;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(7, 108);
            label6.Name = "label6";
            label6.Size = new Size(73, 20);
            label6.TabIndex = 3;
            label6.Text = "Password:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(7, 75);
            label5.Name = "label5";
            label5.Size = new Size(78, 20);
            label5.TabIndex = 2;
            label5.Text = "Username:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(7, 42);
            label4.Name = "label4";
            label4.Size = new Size(38, 20);
            label4.TabIndex = 1;
            label4.Text = "Port:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(7, 9);
            label3.Name = "label3";
            label3.Size = new Size(81, 20);
            label3.TabIndex = 0;
            label3.Text = "IP Address:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(797, 768);
            Controls.Add(tabControl1);
            Margin = new Padding(3, 2, 3, 2);
            MinimumSize = new Size(815, 815);
            Name = "Form1";
            Text = "Form1";
            tabControl1.ResumeLayout(false);
            filesTabPage.ResumeLayout(false);
            filesTabPage.PerformLayout();
            treeViewTabPage.ResumeLayout(false);
            treeViewTabPage.PerformLayout();
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
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
    }
}