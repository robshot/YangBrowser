using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YangTreeView.Yang.YangObjects
{
    public class Import
    {
        public string Name { get; set; }

        public string InternalPrefix { get; set; }

        public string ExternalPrefix { get; set; }

        public static Import Parse(List<string> lines)
        {
            Import import = new Import();
            int indexOfPrefixIndexed = 0;
            int indexOfSemi = 0;
            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i].Trim();
                switch (line)
                {
                    case string s when s.StartsWith("import"):
                        int indexOfFirstAcco = line.IndexOf("{");
                        import.Name = line.Substring(6, indexOfFirstAcco - 6).Trim();
                        if (line.Contains("prefix"))
                        {
                            indexOfPrefixIndexed = line.IndexOf("prefix", indexOfFirstAcco) + 6;
                            indexOfSemi = line.IndexOf(';', indexOfPrefixIndexed);
                            import.InternalPrefix = line.Substring(indexOfPrefixIndexed, indexOfSemi - indexOfPrefixIndexed).Trim();
                        }
                        break;

                    case string s when s.StartsWith("prefix"):
                        indexOfPrefixIndexed = line.IndexOf("prefix") + 6;
                        indexOfSemi = line.IndexOf(';', indexOfPrefixIndexed);
                        import.InternalPrefix = line.Substring(indexOfPrefixIndexed, indexOfSemi - indexOfPrefixIndexed).Trim();
                        break;
                }
            }
            return import;
        }
    }
}
