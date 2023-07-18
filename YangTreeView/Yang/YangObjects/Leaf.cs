using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YangTreeView.Yang.Extensions;

namespace YangTreeView.Yang.YangObjects
{
    public class Leaf
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        internal bool IsList { get; set; }

        public LeafRef LeafRef { get; set; }



        public static Leaf Parse(List<string> leafLines)
        {
            Leaf leaf = new Leaf();
            int indexOfFirstAcco = 0;
            for (int i = 0; i < leafLines.Count; i++)
            {
                string line = leafLines[i].Trim();
                switch (line)
                {
                    case string s when s.StartsWith("leaf"):
                        if (line.Contains("leaf-list"))
                        {
                            leaf.IsList = true;
                        }

                        int indexOfLeafIndexed = leaf.IsList ? line.IndexOf("leaf") + 9 : line.IndexOf("leaf") + 4;
                        indexOfFirstAcco = line.IndexOf("{", indexOfLeafIndexed);
                        leaf.Name = line.Substring(indexOfLeafIndexed, indexOfFirstAcco - indexOfLeafIndexed).Trim();
                        break;

                    case string s when s.StartsWith("description"):
                        leaf.Description = Parsing.GetMultilineText(leafLines, i, out int descriptionIndex);
                        i = descriptionIndex;
                        break;

                    case string s when s.StartsWith("type"):
                        if (line.Contains("leafref"))
                        {
                            var leafRefLines = Parsing.GetSubObject(leafLines, i, out int index);
                            i = index;
                            leaf.LeafRef = LeafRef.Parse(leafRefLines);
                        }
                        else
                        {
                            int indexOfTypeIndexed = line.IndexOf("type") + 4;
                            int indexOfFirst = 0;
                            if (line.Contains(";"))
                            {
                                indexOfFirst = line.IndexOf(";", indexOfTypeIndexed);
                            }
                            else
                            {
                                indexOfFirst = line.IndexOf("{", indexOfTypeIndexed);
                            }

                            leaf.Type = line.Substring(indexOfTypeIndexed, indexOfFirst - indexOfTypeIndexed).Trim();
                        }

                        break;
                }
            }

            return leaf;
        }
    }
}
