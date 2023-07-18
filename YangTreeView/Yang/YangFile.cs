using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YangTreeView.Yang.YangObjects;

namespace YangTreeView.Yang
{
    public class YangFile
    {
        public string Module { get; set; }

        public string YangVersion { get; set; }

        public string Namespace { get; set; }

        public string Prefix { get; set; }

        public string Description { get; set; }

        internal bool IsSubmodule { get; set; }

        internal bool IsResolved { get; set; }

        public BelongsTo BelongsTo { get; set; }

        public List<string> Includes { get; set; }

        public List<Augment> Augments { get; set; }

        public List<Grouping> Groupings { get; set; }

        public Uses RootUses { get; set; }

        public Grouping RootGrouping { get; set; }

        public List<Import> Imports { get; set; }

        public List<Uses> Uses { get; set; }

        public YangFile()
        {
            Description = String.Empty;
            IsSubmodule = false;
            BelongsTo = new BelongsTo();
            Includes = new List<string>();
            RootUses = new Uses();
            Uses = new List<Uses>();
            Imports = new List<Import>();
            Augments = new List<Augment>();
        }

        public void SolveInternalPrefix()
        {
            foreach (var grouping in Groupings)
            {
                foreach (var uses in grouping.Uses)
                {
                    SolvePrefix(uses);
                }

                SolveInternalPrefixContainers(grouping.Containers);
            }
        }

        private void SolveInternalPrefixContainers(List<Container> containers)
        {
            foreach (var container in containers)
            {
                foreach (var uses in container.Uses)
                {
                    SolvePrefix(uses);
                }

                SolveInternalPrefixContainers(container.Containers);
                SolveInternalPrefixLists(container.Lists);
            }
        }

        private void SolveInternalPrefixLists(List<List> lists)
        {
            foreach (var list in lists)
            {
                foreach (var uses in list.Uses)
                {
                    SolvePrefix(uses);
                }

                SolveInternalPrefixContainers(list.Containers);
                SolveInternalPrefixLists(list.Lists);
            }
        }

        private void SolvePrefix(Uses uses)
        {
            if (uses.InternalPrefix == Prefix)
            {
                uses.ExternalPrefix = Prefix;
                return;
            }

            var import = Imports.FirstOrDefault(x => x.InternalPrefix == uses.InternalPrefix);
            if (import == null)
            {
                return;
            }

            uses.ExternalPrefix = import.ExternalPrefix;
        }

        public void GetAllUses()
        {
            Uses = new List<Uses>();
            foreach (var grouping in Groupings)
            {
                GetUsesFromGrouping(grouping);
            }

            Uses = Uses.Distinct().ToList();
        }

        private void GetUsesFromGrouping(Grouping grouping)
        {
            Uses.AddRange(grouping.Uses);
            foreach (var container in grouping.Containers)
            {
                GetUsesFromContainer(container);
            }
        }

        private void GetUsesFromContainer(Container container)
        {
            Uses.AddRange(container.Uses);
            foreach (var subContainer in container.Containers)
            {
                GetUsesFromContainer(subContainer);
            }

            foreach (var list in container.Lists)
            {
                GetUsesFromList(list);
            }
        }

        private void GetUsesFromList(List list)
        {
            Uses.AddRange(list.Uses);
            foreach (var subList in list.Lists)
            {
                GetUsesFromList(subList);
            }

            foreach (var container in list.Containers)
            {
                GetUsesFromContainer(container);
            }
        }
    }
}
