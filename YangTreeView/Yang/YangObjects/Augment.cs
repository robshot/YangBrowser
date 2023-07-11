using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YangTreeView.Yang.Extensions;

namespace YangTreeView.Yang.YangObjects
{
    public class Augment
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<Uses> Uses { get; set; }

        public List<string> FilePrefixes { get; set; }

        public List<string> Path { get; set; }

        public List<Leaf> Leafs { get; set; }

        public Augment()
        {
            Uses = new List<Uses>();
            Leafs = new List<Leaf>();
        }

        public Augment(List<string> lines, string prefix)
        {
            Uses = new List<Uses>();
            Leafs = new List<Leaf>();
            Name = Parsing.GetMultilineName(lines, out int index);
            for (int i = index; i < lines.Count; i++)
            {
                string line = lines[i].Trim();
                switch (line)
                {
                    case string s when s.StartsWith("description"):
                        Description = Parsing.GetMultilineText(lines, i, out int descriptionIndex);
                        i = descriptionIndex;
                        break;

                    case string s when s.StartsWith("uses"):
                        Uses.Add(new Uses(line, prefix));
                        break;

                    case string s when s.StartsWith("leaf"):
                        List<string> leafLines = Parsing.GetSubObject(lines, i, out int leafIndex);
                        Leafs.Add(Leaf.Parse(leafLines));
                        i = leafIndex;
                        break;
                }
            }

            Path = Name.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            FilePrefixes = Path.Select(x => x.Split(':')[0]).Distinct().ToList();
            //FilePrefixes = Path[0].Split(':')[0];
        }
    }
}
