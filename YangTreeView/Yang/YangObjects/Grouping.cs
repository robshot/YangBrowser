using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YangTreeView.Yang.YangObjects
{
    public class Grouping
    {
        public string Name { get; set; }

        public string Prefix { get; set; }

        public string Description { get; set; }

        public List<Leaf> Leafs { get; set; }

        public List<Uses> Uses { get; set; }

        private bool isUsed { get; set; }

        public List<Container> Containers
        {
            get
            {
                return containers.OrderBy(x => x.Name).ToList();
            }
        }

        private List<Container> containers { get; set; }

        public Grouping()
        {
            Leafs = new List<Leaf>();
            containers = new List<Container>();
            Uses = new List<Uses>();
        }

        public void AddContainer(Container container, bool addToContainers = true)
        {
            if (addToContainers)
            {
                containers.Add(container);
            }

            /*foreach (var subContainer in container.Containers)
            {
                AddContainer(subContainer, false);
            }*/
        }

        public void AddContainers(List<Container> containers)
        {
            foreach (var container in containers)
            {
                AddContainer(container);
            }
        }
    }
}
