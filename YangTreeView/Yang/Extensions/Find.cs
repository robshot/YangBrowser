using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YangTreeView.Yang.YangObjects;

namespace YangTreeView.Yang.Extensions
{
    public static class Find
    {
        public static string NodeType(Tree root, string path)
        {
            string[] pathSplitted = path.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            var grouping = root.Groupings.FirstOrDefault(x => x.Name == pathSplitted[0]);
            if (grouping == null)
            {
                return String.Empty;
            }

            if (pathSplitted.Length == 1)
            {
                return "module";
            }

            var container = grouping.Containers.FirstOrDefault(x => x.Name == pathSplitted[1]);
            if (container != null)
            {
                if (pathSplitted.Length == 2)
                {
                    return "container";
                }

                string[] partPathSplitted = new string[pathSplitted.Length - 2];
                Array.Copy(pathSplitted, 2, partPathSplitted, 0, partPathSplitted.Length);
                return ContainerType(partPathSplitted, container);
            }

            var leaf = grouping.Leafs.FirstOrDefault(x => x.Name == pathSplitted[1]);
            if (leaf != null)
            {
                return "leaf";
            }

            return String.Empty;
        }

        private static string ContainerType(string[] path, Container container)
        {
            var subContainer = container.Containers.FirstOrDefault(x => x.Name == path[0]);
            if (subContainer != null)
            {
                if (path.Length == 1)
                {
                    return "container";
                }

                string[] partPathSplitted = new string[path.Length - 1];
                Array.Copy(path, 1, partPathSplitted, 0, partPathSplitted.Length);
                return ContainerType(partPathSplitted, subContainer);
            }

            var list = container.Lists.FirstOrDefault(x => x.Name == path[0]);
            if (list != null)
            {
                if (path.Length == 1)
                {
                    return "list";
                }

                string[] partPathSplitted = new string[path.Length - 1];
                Array.Copy(path, 1, partPathSplitted, 0, partPathSplitted.Length);
                return ListType(partPathSplitted, list);
            }

            var leaf = container.Leafs.FirstOrDefault(x => x.Name == path[0]);
            if (leaf != null)
            {
                return "leaf";
            }

            return String.Empty;
        }

        private static string ListType(string[] path, List list)
        {
            var subList = list.Lists.FirstOrDefault(x => x.Name == path[0]);
            if (subList != null)
            {
                if (path.Length == 1)
                {
                    return "list";
                }

                string[] partPathSplitted = new string[path.Length - 1];
                Array.Copy(path, 1, partPathSplitted, 0, partPathSplitted.Length);
                return ListType(partPathSplitted, list);
            }

            var container = list.Containers.FirstOrDefault(x => x.Name == path[0]);
            if (container != null)
            {
                if (path.Length == 1)
                {
                    return "container";
                }

                string[] partPathSplitted = new string[path.Length - 1];
                Array.Copy(path, 1, partPathSplitted, 0, partPathSplitted.Length);
                return ContainerType(partPathSplitted, container);
            }

            var leaf = list.Leafs.FirstOrDefault(x => x.Name == path[0]);
            if (leaf != null)
            {
                return "leaf";
            }

            return String.Empty;
        }

        public static YangFile Module(YangParser parser, string name)
        {
            var module = parser.Files.FirstOrDefault(x => x.RootGrouping != null ? x.RootGrouping.Name == name : false);
            if (module != null)
            {
                return module;
            }

            return new YangFile();
        }

        public static Container Container(Tree root, string path)
        {
            string[] pathSplitted = path.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            var grouping = root.Groupings.First(x => x.Name == pathSplitted[0]);
            var container = grouping.Containers.FirstOrDefault(x => x.Name == pathSplitted[1]);
            if (container != null)
            {
                if (pathSplitted.Length == 2)
                {
                    return container;
                }

                string[] partPathSplitted = new string[pathSplitted.Length - 2];
                Array.Copy(pathSplitted, 2, partPathSplitted, 0, partPathSplitted.Length);
                return ContainerInContainer(partPathSplitted, container);
            }

            return new Container();
        }

        private static Container ContainerInContainer(string[] path, Container container)
        {
            var subContainer = container.Containers.FirstOrDefault(x => x.Name == path[0]);
            if (subContainer != null)
            {
                if (path.Length == 1)
                {
                    return subContainer;
                }

                string[] partPathSplitted = new string[path.Length - 1];
                Array.Copy(path, 1, partPathSplitted, 0, partPathSplitted.Length);
                return ContainerInContainer(partPathSplitted, subContainer);
            }

            var list = container.Lists.FirstOrDefault(x => x.Name == path[0]);
            if (list != null)
            {
                string[] partPathSplitted = new string[path.Length - 1];
                Array.Copy(path, 1, partPathSplitted, 0, partPathSplitted.Length);
                return ContainerInList(partPathSplitted, list);
            }

            return new Container();
        }

        private static Container ContainerInList(string[] path, List list)
        {
            var container = list.Containers.FirstOrDefault(x => x.Name == path[0]);
            if (container != null)
            {
                if (path.Length == 1)
                {
                    return container;
                }
            }

            var subList = list.Lists.FirstOrDefault(x => x.Name == path[0]);
            if (subList != null)
            {
                string[] partPathSplitted = new string[path.Length - 1];
                Array.Copy(path, 1, partPathSplitted, 0, partPathSplitted.Length);
                return ContainerInList(partPathSplitted, subList);
            }
            
            return new Container();
        }

        public static List List(Tree root, string path)
        {
            string[] pathSplitted = path.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            var grouping = root.Groupings.First(x => x.Name == pathSplitted[0]);
            var container = grouping.Containers.FirstOrDefault(x => x.Name == pathSplitted[1]);
            if (container != null)
            {
                string[] partPathSplitted = new string[pathSplitted.Length - 2];
                Array.Copy(pathSplitted, 2, partPathSplitted, 0, partPathSplitted.Length);
                return ListInContainer(partPathSplitted, container);
            }
            return new List();
        }

        private static List ListInContainer(string[] path, Container container)
        {
            var subContainer = container.Containers.FirstOrDefault(x => x.Name == path[0]);
            if (subContainer != null)
            {
                string[] partPathSplitted = new string[path.Length - 1];
                Array.Copy(path, 1, partPathSplitted, 0, partPathSplitted.Length);
                return ListInContainer(partPathSplitted, subContainer);
            }

            var list = container.Lists.FirstOrDefault(x => x.Name == path[0]);
            if (list != null)
            {
                if (path.Length == 1)
                {
                    return list;
                }

                string[] partPathSplitted = new string[path.Length - 1];
                Array.Copy(path, 1, partPathSplitted, 0, partPathSplitted.Length);
                return ListInList(partPathSplitted, list);
            }
            return new List();
        }

        private static List ListInList(string[] path, List list)
        {
            var subList = list.Lists.FirstOrDefault(x => x.Name == path[0]);
            if (subList != null)
            {
                if (path.Length == 1)
                {
                    return list;
                }

                string[] partPathSplitted = new string[path.Length - 1];
                Array.Copy(path, 1, partPathSplitted, 0, partPathSplitted.Length);
                return ListInList(partPathSplitted, list);
            }

            /*var container = list.Containers.FirstOrDefault(x => x.Name == path[0]);
            if (container != null)
            {
                string[] partPathSplitted = new string[path.Length - 1];
                Array.Copy(path, 1, partPathSplitted, 0, partPathSplitted.Length);
                return ContainerInList(partPathSplitted, list);
            }*/

            return new List();
        }

        public static Leaf Leaf(Tree root, string path)
        {
            string[] pathSplitted = path.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            var grouping = root.Groupings.First(x => x.Name == pathSplitted[0]);
            var container = grouping.Containers.FirstOrDefault(x => x.Name == pathSplitted[1]);
            if (container != null)
            {
                string[] partPathSplitted = new string[pathSplitted.Length - 2];
                Array.Copy(pathSplitted, 2, partPathSplitted, 0, partPathSplitted.Length);
                return LeafInContainer(partPathSplitted, container);
            }

            return new Leaf();
        }

        private static Leaf LeafInContainer(string[] path, Container container)
        {
            var leaf = container.Leafs.FirstOrDefault(x => x.Name == path[0]);
            if (leaf != null)
            {
                if (path.Length == 1)
                {
                    return leaf;
                }
            }

            var subContainer = container.Containers.FirstOrDefault(x => x.Name == path[0]);
            if (subContainer != null)
            {
                string[] partPathSplitted = new string[path.Length - 1];
                Array.Copy(path, 1, partPathSplitted, 0, partPathSplitted.Length);
                return LeafInContainer(partPathSplitted, subContainer);
            }

            var list = container.Lists.FirstOrDefault(x => x.Name == path[0]);
            if (list != null)
            {
                string[] partPathSplitted = new string[path.Length - 1];
                Array.Copy(path, 1, partPathSplitted, 0, partPathSplitted.Length);
                return LeafInList(partPathSplitted, list);
            }

            return new Leaf();
        }

        private static Leaf LeafInList(string[] path, List list)
        {
            var leaf = list.Leafs.FirstOrDefault(x => x.Name == path[0]);
            if (leaf != null)
            {
                if (path.Length == 1)
                {
                    return leaf;
                }
            }

            var subContainer = list.Containers.FirstOrDefault(x => x.Name == path[0]);
            if (subContainer != null)
            {
                string[] partPathSplitted = new string[path.Length - 1];
                Array.Copy(path, 1, partPathSplitted, 0, partPathSplitted.Length);
                return LeafInContainer(partPathSplitted, subContainer);
            }

            var subList = list.Lists.FirstOrDefault(x => x.Name == path[0]);
            if (list != null)
            {
                string[] partPathSplitted = new string[path.Length - 1];
                Array.Copy(path, 1, partPathSplitted, 0, partPathSplitted.Length);
                return LeafInList(partPathSplitted, subList);
            }

            return new Leaf();
        }
    }
}
