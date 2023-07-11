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

namespace YangTreeView
{
    public partial class Form1 : Form
    {
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["Path"] = tbPath.Text;
            Properties.Settings.Default.Save();
            string path = tbPath.Text;
            rtbParsedFiles.Text = String.Empty;
            rtbMissingFiles.Text = String.Empty;
            treeView1.Nodes.Clear();
            YangParser parser = new YangParser(path);
            rtbParsedFiles.Text = String.Join(Environment.NewLine, parser.Files.Select(x => x.Module));
            rtbMissingFiles.Text = String.Join(Environment.NewLine, parser.MissingImports);
            FindNextForTreeView(treeView1, parser);
        }

        private void AddContainerToTree(Container container, TreeNode node)
        {
            node.Nodes.Add(container.Name);
            var selectedNode = GetNodeByText(node.Nodes, container.Name);
            foreach (var leaf in container.Leafs)
            {
                AddLeafToTree(leaf, selectedNode);
            }

            foreach (var subContainer in container.Containers)
            {
                AddContainerToTree(subContainer, selectedNode);
            }

            foreach (var list in container.Lists)
            {
                AddListToTree(list, selectedNode);
            }
        }

        private void AddListToTree(List list, TreeNode node)
        {
            node.Nodes.Add(list.Name);
            var selectedNode = GetNodeByText(node.Nodes, list.Name);
            foreach (var leaf in list.Leafs)
            {
                AddLeafToTree(leaf, selectedNode);
            }

            foreach (var subList in list.Lists)
            {
                AddListToTree(subList, selectedNode);
            }

            foreach (var subContainer in list.Containers)
            {
                AddContainerToTree(subContainer, selectedNode);
            }
        }

        private void AddLeafToTree(Leaf leaf, TreeNode node)
        {
            node.Nodes.Add(leaf.Name);
        }

        public void FindNextForTreeView(TreeView treeView, YangParser parser)
        {
            foreach (var grouping in parser.Root.Groupings)
            {
                treeView1.Nodes.Add(grouping.Name);
                var selectedNode = GetNodeByText(treeView1.Nodes, grouping.Name);
                foreach (var leaf in grouping.Leafs)
                {
                    AddLeafToTree(leaf, selectedNode);
                }

                foreach (var container in grouping.Containers)
                {
                    AddContainerToTree(container, selectedNode);
                }
            }

            /*if (parser.Files.Any(x => x.RootUses != null))
            {
                foreach (var file in parser.Files)
                {
                    if (file.RootUses == null)
                    {
                        continue;
                    }

                    treeView1.Nodes.Add(file.RootUses.Name);
                    var selectedNode = GetNodeByText(treeView1.Nodes, file.RootUses.Name);
                    var groupings = parser.Groupings.OrderBy(x => x.Name).ToList();
                    var rootGroup = groupings.First(x => x.Name == file.RootUses.Name);
                    foreach (var container in rootGroup.Containers)
                    {
                        AddContainer(groupings, selectedNode, container);
                    }
                }
            }
            else
            {
                var groupings = parser.Groupings.OrderBy(x => x.Name).ToList();
                var root = FindRootForTreeView(groupings);
                foreach (var rootNode in root)
                {
                    treeView1.Nodes.Add(rootNode.Name);
                    var selectedNode = GetNodeByText(treeView1.Nodes, rootNode.Name);
                    foreach (var container in rootNode.Containers)
                    {
                        AddContainer(groupings, selectedNode, container);
                    }
                }
            }*/
        }

        /*public void FindNextForTreeView(TreeView treeView, List<Grouping> groupings)
        {
            var root = FindRootForTreeView(groupings);
            foreach (var rootNode in root)
            {
                treeView1.Nodes.Add(rootNode.Name);
                var selectedNode = GetNodeByText(treeView1.Nodes, rootNode.Name);
                foreach (var container in rootNode.Containers)
                {
                    AddContainer(groupings, selectedNode, container);
                }
            }
        }*/

        private void AddUses(List<Grouping> groupings, TreeNode node, Uses uses)
        {
            var group = groupings.FirstOrDefault(x => x.Name == uses.Name);
            if (group == null)
            {
                return;
            }

            foreach (var container in group.Containers)
            {
                AddContainer(groupings, node, container);
            }

            foreach (var leaf in group.Leafs)
            {
                AddLeaf(groupings, node, leaf);
            }
        }

        private void AddContainer(List<Grouping> groupings, TreeNode node, Container container)
        {
            node.Nodes.Add(container.Name);
            var selectedNode = GetNodeByText(node.Nodes, container.Name);
            foreach (var subContainer in container.Containers)
            {
                AddContainer(groupings, selectedNode, subContainer);
            }

            foreach (var list in container.Lists)
            {
                AddList(groupings, selectedNode, list);
            }

            foreach (var uses in container.Uses)
            {
                AddUses(groupings, selectedNode, uses);
                //FindNextForTreeView(treeView, groupings, uses);
            }
        }

        private void AddList(List<Grouping> groupings, TreeNode node, List list)
        {
            node.Nodes.Add(list.Name);
            var selectedNode = GetNodeByText(node.Nodes, list.Name);
            foreach (var subContainer in list.Containers)
            {
                AddContainer(groupings, selectedNode, subContainer);
            }

            foreach (var subList in list.Lists)
            {
                AddList(groupings, selectedNode, subList);
            }

            foreach (var subLeaf in list.Leafs)
            {
                AddLeaf(groupings, selectedNode, subLeaf);
            }

            foreach (var uses in list.Uses)
            {
                AddUses(groupings, selectedNode, uses);
            }
        }

        private void AddLeaf(List<Grouping> groupings, TreeNode node, Leaf leaf)
        {
            node.Nodes.Add(leaf.Name);
        }

        private TreeNode GetNodeByName(TreeNodeCollection nodes, string searchtext)
        {
            TreeNode n_found_node = null;
            bool b_node_found = false;

            foreach (TreeNode node in nodes)
            {

                if (node.Name == searchtext)
                {
                    b_node_found = true;
                    n_found_node = node;

                    return n_found_node;
                }

                if (!b_node_found)
                {
                    n_found_node = GetNodeByName(node.Nodes, searchtext);

                    if (n_found_node != null)
                    {
                        return n_found_node;
                    }
                }
            }
            return null;
        }

        private TreeNode GetNodeByText(TreeNodeCollection nodes, string searchtext)
        {
            TreeNode n_found_node = null;
            bool b_node_found = false;

            foreach (TreeNode node in nodes)
            {

                if (node.Text == searchtext)
                {
                    b_node_found = true;
                    n_found_node = node;

                    return n_found_node;
                }

                /*if (!b_node_found)
                {
                    n_found_node = GetNodeByText(node.Nodes, searchtext);

                    if (n_found_node != null)
                    {
                        return n_found_node;
                    }
                }*/
            }

            return null;
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string selectedNode = e.Node.FullPath;
            string[] splittedSelectedNode = e.Node.FullPath.Split('\\');
            if (splittedSelectedNode.Length == 1)
            {
                return;
            }

            string[] selectedNodeWithoutRoot = new string[splittedSelectedNode.Length - 1];
            Array.Copy(splittedSelectedNode, 1, selectedNodeWithoutRoot, 0, selectedNodeWithoutRoot.Length);
            textBox2.Text = String.Join("/", selectedNodeWithoutRoot);
            //MessageBox.Show(selectedNode);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path = "/" + textBox2.Text;
            var response = ExecuteWSLCommand(@$"gnmic -a {tbIp.Text}:{tbPort.Text} -u {tbUsername.Text} -p {tbPassword.Text} --insecure \get --path {path}");
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

                // Display the output/error
                //Console.WriteLine("Output:");
                //Console.WriteLine(output);

                //Console.WriteLine("Error:");
                //Console.WriteLine(error);
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
    }
}