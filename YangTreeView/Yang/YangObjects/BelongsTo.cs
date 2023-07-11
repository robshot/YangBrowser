using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YangTreeView.Yang.Extensions;

namespace YangTreeView.Yang.YangObjects
{
    public class BelongsTo
    {
        public string Name { get; set; }

        public string Prefix { get; set; }

        public static BelongsTo Parse(List<string> lines)
        {
            BelongsTo belongsTo = new BelongsTo();
            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i].Trim();
                switch (line)
                {
                    case string s when s.StartsWith("belongs-to"):
                        belongsTo.Name = line.Split(" ")[1];
                        break;

                    case string s when s.StartsWith("prefix"):
                        belongsTo.Prefix = Parsing.GetMultilineText(lines, i, out int index);
                        i = index;
                        break;
                }
            }

            return belongsTo;
        }
    }
}
