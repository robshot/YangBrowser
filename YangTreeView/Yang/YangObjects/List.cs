using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YangTreeView.Yang.YangObjects
{
    public class List
    {
        public string Name { get; set; }

        public string Key { get; set; }

        public string Description { get; set; }

        public List<Container> Containers { get; set; }

        public List<List> Lists { get; set; }

        public List<Leaf> Leafs { get; set; }

        public List<Uses> Uses { get; set; }

        public List()
        {
            Containers = new List<Container>();
            Lists = new List<List>();
            Leafs = new List<Leaf>();
            Uses = new List<Uses>();
        }
    }
}
