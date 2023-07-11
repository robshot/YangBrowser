using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YangTreeView.Yang.YangObjects
{
    public class Container
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool Config { get; set; }

        public List<Uses> Uses { get; set; }

        public List<List> Lists { get; set; }

        public List<Leaf> Leafs { get; set; }

        public List<Container> Containers
        {
            get
            {
                return containers.OrderBy(x => x.Name).ToList();
            }
        }

        private List<Container> containers;

        public Container()
        {
            Config = true;
            Uses = new List<Uses>();
            containers = new List<Container>();
            Lists = new List<List>();
            Leafs = new List<Leaf>();
        }

        public void AddContainer(Container container)
        {
            if (!containers.Contains(container))
            {
                containers.Add(container);
            }
        }
    }
}
