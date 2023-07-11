using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YangTreeView.Yang.YangObjects;

namespace YangTreeView.Yang
{
    public class Tree
    {
        public List<Grouping> Groupings { get; set; }

        public Tree()
        {
            Groupings = new List<Grouping>();
        }
    }
}
