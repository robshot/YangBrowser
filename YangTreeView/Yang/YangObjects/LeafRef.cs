using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YangTreeView.Yang.YangObjects
{
    public class LeafRef
    {
        public string Path { get; set; }

        public static LeafRef Parse(List<string> leafRefLines)
        {
            LeafRef leafRef = new LeafRef();
            for (int i = 0; i < leafRefLines.Count; i++)
            {
                string line = leafRefLines[i].Trim();
                switch (line)
                {
                    case string s when s.StartsWith("path"):
                        leafRef.Path = GetLeafRefPath(leafRefLines);
                        break;
                }
            }

            return leafRef;
        }
        private static string GetLeafRefPath(List<string> lines)
        {
            int i = 0;
            string line = lines[i].Trim();
            int indexOfPathIndexed = line.IndexOf("path") + 4;
            int indexOfLast = line.IndexOf(";", indexOfPathIndexed);
            line = line.Substring(indexOfPathIndexed).Trim();
            List<string> descriptionPreviousLine = new List<string>();
            while (indexOfLast == -1)
            {
                descriptionPreviousLine.Add(line.Replace("\"", string.Empty));
                i++;
                line = lines[i].Trim();
                indexOfLast = line.IndexOf(";");
            }

            if (descriptionPreviousLine.Count > 0)
            {
                descriptionPreviousLine.Add(line.Substring(0, indexOfLast));
                return string.Join(" ", descriptionPreviousLine);
            }
            else
            {
                return line.Substring(indexOfPathIndexed, indexOfLast - indexOfPathIndexed).Trim();
            }
        }
    }
}
