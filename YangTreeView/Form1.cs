using System;
//using System.ComponentModel;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel.Design.Serialization;
using YangTreeView.Yang;
using YangTreeView.Yang.YangObjects;
using System.Diagnostics;
using YangTreeView.Yang.Extensions;

namespace YangTreeView
{
    public partial class Form1 : Form
    {
        public YangParser parser;

        public Form1()
        {
            InitializeComponent();
            if (!String.IsNullOrWhiteSpace(Properties.Settings.Default.Path))
            {
                tbPath.Text = Properties.Settings.Default.Path;
            }

            tbIp.Text = Properties.Settings.Default.Ip;
            tbPort.Text = Properties.Settings.Default.Port;
            tbUsername.Text = Properties.Settings.Default.Username;
            tbPassword.Text = Properties.Settings.Default.Password;
            cbParseSubFolders.Checked = Properties.Settings.Default.SubFolder;
            cbSkipVerify.Checked = Properties.Settings.Default.SkipVerify;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["Path"] = tbPath.Text;
            Properties.Settings.Default.Save();
            string path = tbPath.Text;
            rtbParsedFiles.Text = String.Empty;
            rtbMissingFiles.Text = String.Empty;
            treeView1.Nodes.Clear();
            parser = new YangParser(path, Properties.Settings.Default.SubFolder);
            rtbParsedFiles.Text = String.Join(Environment.NewLine, parser.Files.Select(x => x.Module));
            rtbMissingFiles.Text = String.Join(Environment.NewLine, parser.MissingImports);
            TreeViewParsing.FindNextForTreeView(treeView1, parser);
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string[] splittedSelectedNode = e.Node.FullPath.Split('\\');
            if (splittedSelectedNode.Length == 1)
            {
                textBox2.Text = "Invalid Path Selection";
                return;
            }

            string[] selectedNodeWithoutRoot = new string[splittedSelectedNode.Length - 1];
            Array.Copy(splittedSelectedNode, 1, selectedNodeWithoutRoot, 0, selectedNodeWithoutRoot.Length);
            textBox2.Text = "/" + String.Join("/", selectedNodeWithoutRoot);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path = textBox2.Text;
            List<string> options = new List<string>();
            if (cbSkipVerify.Checked)
            {
                options.Add("--skip-verify");
            }
            else
            {
                options.Add("--insecure");
            }

            var response = ExecuteWSLCommand(@$"gnmic -a {tbIp.Text}:{tbPort.Text} -u {tbUsername.Text} -p {tbPassword.Text} {String.Join(" ", options)} \get --path {path}");
            rtbResponse.Text = response.Response == String.Empty ? response.Error : response.Response;
        }

        private static WslResponse ExecuteWSLCommand(string command)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "wsl",
                Arguments = command,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process())
            {
                process.StartInfo = startInfo;
                process.Start();

                // Read the output/error
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                WslResponse response = new WslResponse()
                {
                    Response = output,
                    Error = error,
                };
                return response;
            }
        }

        public class WslResponse
        {
            public string Response { get; set; }

            public string Error { get; set; }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["Ip"] = tbIp.Text;
            Properties.Settings.Default["Port"] = tbPort.Text;
            Properties.Settings.Default["Username"] = tbUsername.Text;
            Properties.Settings.Default["Password"] = tbPassword.Text;
            Properties.Settings.Default.Save();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string type = Find.NodeType(parser.Root, e.Node.FullPath);
            tbType.Text = type;
            switch (type)
            {
                case "module":
                    var module = Find.Module(parser, e.Node.FullPath);
                    pg.SelectedObject = module;
                    break;

                case "container":
                    var container = Find.Container(parser.Root, e.Node.FullPath);
                    pg.SelectedObject = container;
                    break;

                case "list":
                    var list = Find.List(parser.Root, e.Node.FullPath);
                    pg.SelectedObject = list;
                    break;

                case "leaf":
                    var leaf = Find.Leaf(parser.Root, e.Node.FullPath);
                    pg.SelectedObject = leaf;
                    break;
            }
        }

        private void pg_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            tbPgValue.Text = Convert.ToString(e.NewSelection.Value);
        }

        private void cbParseSubFolders_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SubFolder = cbParseSubFolders.Checked;
        }

        private void cbSkipVerify_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SkipVerify = cbSkipVerify.Checked;
        }
    }
}