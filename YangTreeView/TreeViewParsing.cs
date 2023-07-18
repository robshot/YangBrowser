using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YangTreeView.Yang;
using YangTreeView.Yang.YangObjects;

namespace YangTreeView
{
    public class TreeViewParsing
    {
        public static void FindNextForTreeView(TreeView treeView, YangParser parser)
        {
            foreach (var grouping in parser.Root.Groupings)
            {
                treeView.Nodes.Add(grouping.Name);
                var selectedNode = TreeViewParsing.GetNodeByText(treeView.Nodes, grouping.Name);
                foreach (var leaf in grouping.Leafs)
                {
                    TreeViewParsing.AddLeafToTree(leaf, selectedNode);
                }

                foreach (var container in grouping.Containers)
                {
                    TreeViewParsing.AddContainerToTree(container, selectedNode);
                }
            }
        }

        public TreeNode GetNodeByName(TreeNodeCollection nodes, string searchtext)
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

        public static TreeNode GetNodeByText(TreeNodeCollection nodes, string searchtext)
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

        public static void AddLeafToTree(Leaf leaf, TreeNode node)
        {
            node.Nodes.Add(leaf.Name);
        }

        public static void AddContainerToTree(Container container, TreeNode node)
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

        private static void AddListToTree(List list, TreeNode node)
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
    }
}
