using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using YangTreeView.Yang.Extensions;
using YangTreeView.Yang.YangObjects;

namespace YangTreeView.Yang
{

    public class YangParser
    {
        public string Dir { get; set; }

        public string[] FilesDir { get; set; }

        public List<YangFile> Files { get; set; }

        public List<string> MissingImports { get; set; }

        public List<string> ImportFiles { get; set; }

        public Tree Root { get; set; }

        private bool isDir { get; set; }

        public YangParser(string path)
        {
            MissingImports = new List<string>();
            ImportFiles = new List<string>();
            var fileName = Path.GetFileName(path);
            Dir = Path.GetDirectoryName(path);
            if (!fileName.Contains("."))
            {
                Dir += "\\" + fileName;
                fileName = string.Empty;
            }

            FilesDir = Directory.GetFiles(Dir);
            Files = new List<YangFile>();
            if (string.IsNullOrWhiteSpace(fileName))
            {
                isDir = true;
                Parse(FilesDir.ToList());
            }
            else
            {
                Parse(fileName);
            }

            CombineSubModuleAndDelete();

            foreach (var file in Files)
            {
                List<string> tempMissingImports = new List<string>();
                foreach (var import in file.Imports)
                {
                    var findFile = Files.FirstOrDefault(x => x.Module == import.Name);
                    if (findFile != null)
                    {
                        import.ExternalPrefix = findFile.Prefix;
                    }
                    else
                    {
                        if (!MissingImports.Contains(import.Name))
                        {
                            MissingImports.Add(import.Name);
                        }

                        tempMissingImports.Add(import.Name);
                    }
                }

                file.Imports.RemoveAll(x => tempMissingImports.Contains(x.Name));
                file.SolveInternalPrefix();
            }

            Tree root = new Tree();
            FillRoot(root);

            var importFilesToResolve = FindImportFiles(Files.Where(x => x.RootGrouping != null).ToList());
            LoopImportAndDependencies(importFilesToResolve, root);

            /*foreach (var file in Files.Where(x => x.RootGrouping == null))
            {
                LoopImportAndDependencies(new List<string> { file.Module }, root);
            }*/

            foreach (var file in Files.Where(x => x.Augments.Count > 0))
            {
                LoopImportAndDependencies(new List<string> { file.Module }, root);
            }

            Root = root;
        }

        private void FillRoot(Tree root)
        {
            foreach (var file in Files.Where(x => x.RootGrouping != null))
            {
                foreach (var container in file.RootGrouping.Containers)
                {
                    SolveUsesForContainer(container);
                }

                root.Groupings.Add(file.RootGrouping);
                file.IsResolved = true;
            }

            if (root.Groupings.Count == 0)
            {
                foreach (var file in Files)
                {
                    file.GetAllUses();
                    foreach (var grouping in file.Groupings)
                    {
                        if (!file.Uses.Any(x => x.Name == grouping.Name))
                        {
                            foreach (var container in grouping.Containers)
                            {
                                SolveUsesForContainer(container);
                            }

                            root.Groupings.Add(grouping);
                        }
                    }
                }
            }
        }

        private void CombineSubModuleAndDelete()
        {
            var filesToDelete = new List<YangFile>();
            foreach (var file in Files.Where(x => x.IsSubmodule))
            {
                var mainFile = Files.FirstOrDefault(x => x.Prefix == file.BelongsTo.Prefix && !x.IsSubmodule);
                if (mainFile == null)
                {
                    return;
                }

                mainFile.Groupings.AddRange(file.Groupings);
                filesToDelete.Add(file);
            }

            foreach (var fileToDelete in filesToDelete)
            {
                Files.Remove(fileToDelete);
            }
        }

        private void LoopImportAndDependencies(List<string> fileNames, Tree root)
        {
            List<string> importFilesToWait = new List<string>();
            List<string> dependenciesToAdd = new List<string>();
            foreach (var import in fileNames)
            {
                var dependencies = CheckImportFileForDependencies(import);
                if (dependencies.Count != 0)
                {
                    dependenciesToAdd.AddRange(dependencies.Select(x => x.Module));
                    importFilesToWait.Add(import);
                }
            }

            fileNames.AddRange(dependenciesToAdd);
            fileNames = fileNames.Except(importFilesToWait).ToList();
            var filesToResolve = Files.Where(x => fileNames.Contains(x.Module)).ToList();
            foreach (var file in filesToResolve)
            {
                SolveAugment(file, root);
            }

            if (importFilesToWait.Count > 0)
            {
                LoopImportAndDependencies(importFilesToWait, root);
            }
        }

        private List<string> FindImportFiles(List<YangFile> files)
        {
            var allImports = files.Select(x => x.Imports).ToList();
            var allImportsName = allImports.Select(x => x.Select(y => y.Name).ToList()).ToList();
            var uniqueList = new List<string>();
            foreach (var innerList in allImportsName)
            {
                uniqueList.AddRange(innerList);
            }

            uniqueList = uniqueList.Distinct().ToList();
            var resolvedFiles = Files.Where(x => x.IsResolved).Select(x => x.Module).ToList();
            var filteredUniqueList = uniqueList.Except(resolvedFiles).Intersect(Files.Select(x => x.Module)).ToList();
            return filteredUniqueList;
        }

        private List<YangFile> CheckImportFilesForDependencies(List<string> fileNames)
        {
            List<YangFile> dependencies = new List<YangFile>();
            foreach (var fileName in fileNames)
            {
                var file = Files.First(x => x.Module == fileName);
                var prefixesForAugments = new List<string>();
                foreach (var innerList in file.Augments)
                {
                    prefixesForAugments.AddRange(innerList.FilePrefixes);
                }

                prefixesForAugments = prefixesForAugments.Distinct().ToList();
                foreach (var prefix in prefixesForAugments)
                {
                    var importFile = Files.First(x => x.Prefix == file.Imports.FirstOrDefault(x => x.InternalPrefix == prefix).ExternalPrefix);
                    if (!importFile.IsResolved)
                    {
                        dependencies.Add(importFile);
                    }
                }
            }

            return dependencies;
        }

        private List<YangFile> CheckImportFileForDependencies(string fileName)
        {
            List<YangFile> dependencies = new List<YangFile>();
            var file = Files.First(x => x.Module == fileName);
            var prefixesForAugments = new List<string>();
            foreach (var innerList in file.Augments)
            {
                prefixesForAugments.AddRange(innerList.FilePrefixes);
            }

            prefixesForAugments = prefixesForAugments.Distinct().ToList();
            foreach (var prefix in prefixesForAugments)
            {
                var import = file.Imports.FirstOrDefault(x => x.InternalPrefix == prefix);
                if (import == null)
                {
                    continue;
                }

                var importFile = Files.First(x => x.Prefix == import.ExternalPrefix);
                if (!importFile.IsResolved)
                {
                    var files = CheckImportFileForDependencies(importFile.Module);
                    if (files.Count == 0)
                    {
                        dependencies.Add(importFile);
                    }
                    else
                    {
                        dependencies.AddRange(files);
                    }
                }
            }

            return dependencies.Distinct().ToList();
        }

        private void SolveAugment(YangFile file, Tree root)
        {
            int counter = 0;
            foreach (var augment in file.Augments)
            {
                var fileDestination = Files.FirstOrDefault(x => x.Prefix == augment.FilePrefixes[0]);
                if (fileDestination == null)
                {
                    continue;
                }

                string[] nextPath = new string[augment.Path.Count - 1];
                Array.Copy(augment.Path.ToArray(), 1, nextPath, 0, nextPath.Length);
                if (fileDestination.RootGrouping == null)
                {
                    continue;
                }

                //var selectedContainer = fileDestination.RootGrouping.Containers.FirstOrDefault(x => x.Name == augment.Path.First().Split(':')[1]);
                var selectedContainerRoot = root.Groupings.First(x => x.Name == fileDestination.RootGrouping.Name).Containers.First(x => x.Name == augment.Path.First().Split(':')[1]);
                counter++;
                if (nextPath.Length == 0)
                {
                    LastInPath(nextPath, selectedContainerRoot, augment);
                    continue;
                }

                NextInPath(file, nextPath, selectedContainerRoot, augment);
            }

            file.IsResolved = true;
            if (counter == file.Augments.Count)
            {
                if (file.RootGrouping != null)
                {
                    root.Groupings.Remove(file.RootGrouping);
                }
            }
        }

        private void SolveAugment(YangFile file)
        {
            foreach (var augment in file.Augments)
            {
                var fileDestination = Files.FirstOrDefault(x => x.Prefix == augment.FilePrefixes[0]);
                if (fileDestination == null)
                {
                    continue;
                }

                string[] nextPath = new string[augment.Path.Count - 1];
                Array.Copy(augment.Path.ToArray(), 1, nextPath, 0, nextPath.Length);
                if (fileDestination.RootGrouping == null)
                {
                    continue;
                }

                var selectedContainer = fileDestination.RootGrouping.Containers.FirstOrDefault(x => x.Name == augment.Path.First().Split(':')[1]);
                if (nextPath.Length == 0)
                {
                    LastInPath(nextPath, selectedContainer, augment);
                    continue;
                }

                NextInPath(file, nextPath, selectedContainer, augment);
            }
        }

        private void NextInPath(YangFile file, string[] path, Container container, Augment augment)
        {
            SolveUsesForContainer(container);
            string firstPath = path.First().Split(':')[1];
            var selectedContainer = container.Containers.FirstOrDefault(x => x.Name == firstPath);
            if (selectedContainer != null)
            {
                if (!LastInPath(path, selectedContainer, augment))
                {
                    string[] nextPath = new string[path.Length - 1];
                    Array.Copy(path, 1, nextPath, 0, nextPath.Length);
                    NextInPath(file, nextPath, selectedContainer, augment);
                }

                return;
            }

            var selectedList = container.Lists.FirstOrDefault(x => x.Name == firstPath);
            if (selectedList != null)
            {
                if (!LastInPath(path, selectedList, augment))
                {
                    string[] nextPath = new string[path.Length - 1];
                    Array.Copy(path, 1, nextPath, 0, nextPath.Length);
                    NextInPath(file, nextPath, selectedList, augment);
                }
                return;
            }
        }

        private void NextInPath(YangFile file, string[] path, List list, Augment augment)
        {
            SolveUsesForList(list);
            string[] firstPathSplitted = path.First().Split(':');
            if (firstPathSplitted.Length == 1)
            {
                return;
            }

            string firstPath = firstPathSplitted[1];
            var selectedContainer = list.Containers.FirstOrDefault(x => x.Name == firstPath);
            if (selectedContainer != null)
            {
                if (!LastInPath(path, selectedContainer, augment))
                {
                    string[] nextPath = new string[path.Length - 1];
                    Array.Copy(path, 1, nextPath, 0, nextPath.Length);
                    NextInPath(file, nextPath, selectedContainer, augment);
                }
                return;
            }

            var selectedList = list.Lists.FirstOrDefault(x => x.Name == firstPath);
            if (selectedList != null)
            {
                if (!LastInPath(path, selectedList, augment))
                {
                    string[] nextPath = new string[path.Length - 1];
                    Array.Copy(path, 1, nextPath, 0, nextPath.Length);
                    NextInPath(file, nextPath, selectedList, augment);
                }
                return;
            }
        }

        private bool LastInPath(string[] path, Container container, Augment augment)
        {
            if (path.Length > 1)
            {
                return false;
            }

            container.Uses.AddRange(augment.Uses);
            container.Leafs.AddRange(augment.Leafs);
            if (augment.Uses.Count > 0)
            {
                SolveUsesForContainer(container);
            }

            return true;
        }

        private bool LastInPath(string[] path, List list, Augment augment)
        {
            if (path.Length != 1)
            {
                return false;
            }

            list.Uses.AddRange(augment.Uses);
            list.Leafs.AddRange(augment.Leafs);
            if (augment.Uses.Count > 0)
            {
                SolveUsesForList(list);
            }

            return true;
        }

        private void SolveUsesForContainer(Container container)
        {
            foreach (var subContainer in container.Containers)
            {
                SolveUsesForContainer(subContainer);
            }

            foreach (var uses in container.Uses)
            {
                if (!uses.IsParsed)
                {
                    SolveUses(container, uses);
                    uses.IsParsed = true;
                }
            }

            foreach (var list in container.Lists)
            {
                SolveUsesForList(list);

                foreach (var subContainer in list.Containers)
                {
                    SolveUsesForContainer(subContainer);
                    //SolveUsesForContainerInList(subContainer);
                }
            }
        }

        private void SolveUses(Container container, Uses uses)
        {
            var file = Files.FirstOrDefault(x => x.Prefix == uses.ExternalPrefix);
            if (file == null)
            {
                return;
            }

            var grouping = file.Groupings.FirstOrDefault(x => x.Name == uses.Name);
            if (grouping == null)
            {
                return;
            }

            foreach (var leaf in grouping.Leafs)
            {
                if (!container.Leafs.Contains(leaf))
                {
                    container.Leafs.Add(leaf);
                }
            }

            foreach (var subContainer in grouping.Containers)
            {
                container.AddContainer(subContainer);
                SolveUsesForContainer(subContainer);
            }

            foreach (var subUses in grouping.Uses)
            {
                SolveUses(container, subUses);
            }
        }

        private void SolveUsesForList(List list)
        {
            foreach (var uses in list.Uses)
            {
                if (uses.IsParsed)
                {
                    continue;
                }

                var file = Files.FirstOrDefault(x => x.Prefix == uses.ExternalPrefix);
                if (file == null)
                {
                    continue;
                }

                var grouping = file.Groupings.FirstOrDefault(x => x.Name == uses.Name);
                if (grouping == null)
                {
                    continue;
                }

                foreach (var container in grouping.Containers)
                {
                    if (!list.Containers.Contains(container))
                    {
                        list.Containers.Add(container);
                        SolveUsesForContainer(container);
                    }
                }

                foreach (var leaf in grouping.Leafs)
                {
                    if (!list.Leafs.Contains(leaf))
                    {
                        list.Leafs.Add(leaf);
                    }
                }
            }
        }

        private void Parse(List<string> files)
        {
            foreach (var file in files)
            {
                ImportFiles.AddRange(ParseYangFile(file));
            }

            ImportFiles = ImportFiles.Distinct().Except(FilesDir).ToList();
            ParseImportFiles();
        }

        private void Parse(string fileName)
        {
            ImportFiles = ParseYangFile(fileName);
            List<string> importFilesToParse = new List<string>();
            foreach (var file in ImportFiles)
            {
                importFilesToParse.AddRange(ParseYangFile(file));
            }

            ImportFiles = importFilesToParse.Distinct().Except(ImportFiles).Except(FilesDir).ToList();
            if (ImportFiles.Any())
            {
                ParseImportFiles();
            }
        }

        private void ParseImportFiles()
        {
            List<string> importFiles = new List<string>();
            foreach (var file in ImportFiles)
            {
                importFiles.AddRange(ParseYangFile(file));
            }

            ImportFiles = importFiles.Distinct().Except(ImportFiles).Except(FilesDir).ToList();
            if (ImportFiles.Any())
            {
                ParseImportFiles();
            }
        }

        private void ParseImportFiles(List<string> importFiles)
        {
            List<string> importFilesToParse = new List<string>();
            foreach (var file in FilesDir)
            {
                importFilesToParse.AddRange(ParseYangFile(file));
            }

            importFilesToParse = importFilesToParse.Distinct().Except(FilesDir).ToList();
            ParseImportFiles(importFilesToParse);
        }

        private int IsBlockComment(string line, int comment)
        {
            if (line.StartsWith("/*"))
            {
                return ++comment;
            }
            else if (line.StartsWith("*/"))
            {
                return --comment;
            }

            return comment;
        }

        private List<string> ParseYangFile(string fileName)
        {
            List<string> importFiles = new List<string>();
            YangFile yangFile = new YangFile();
            List<string> files = FilesDir.Where(x => x.Contains(fileName)).ToList();
            if (files.Count == 0)
            {
                return new List<string>();
            }

            string fillToProcess = files.Where(x => x.Contains(fileName)).ToList()[0];
            List<string> file = File.ReadAllLines(fillToProcess).ToList();
            List<List<string>> groupingsLines = new List<List<string>>();
            string prefix = string.Empty;
            for (int i = 0; i < file.Count; i++)
            {
                string line = file[i].Trim();
                if (line.StartsWith("//"))
                {
                    continue;
                }

                switch (line)
                {
                    case string s when s.StartsWith("submodule "):
                        yangFile.Module = line.Split(' ')[1];
                        yangFile.IsSubmodule = true;
                        break;

                    case string s when s.StartsWith("module "):
                        yangFile.Module = line.Split(' ')[1];
                        break;

                    case string s when s.StartsWith("yang-version"):
                        yangFile.YangVersion = line.Split(' ')[1].Replace(";", string.Empty).Replace("\"", string.Empty);
                        break;

                    case string s when s.StartsWith("namespace"):
                        yangFile.Namespace = line.Split(' ')[1].Replace(";", string.Empty).Replace("\"", string.Empty);
                        break;

                    case string s when s.StartsWith("prefix "):
                        prefix = line.Split(' ')[1].Replace(";", string.Empty).Replace("\"", string.Empty);
                        yangFile.Prefix = prefix;

                        if (line.Split(' ').Length > 2)
                        {
                            MessageBox.Show("Prefix contains spaces! " + fillToProcess + "|" + line);
                        }
                        break;

                    case string s when s.StartsWith("belongs-to"):
                        List<string> belongsToLines = Parsing.GetSubObject(file, i, out int belongsToIndex);
                        yangFile.BelongsTo = BelongsTo.Parse(file);
                        prefix = yangFile.BelongsTo.Prefix;
                        i = belongsToIndex;
                        break;

                    case string s when s.StartsWith("import"):
                        var importLines = Parsing.GetSubObject(file, i, out int importIndex);
                        var import = Import.Parse(importLines);
                        i = importIndex;
                        /*int indexOfFirstAcco = line.IndexOf("{");
                        string subFileName = line.Substring(6, indexOfFirstAcco - 6).Trim(); 
                        int indexOfPrefixIndexed = line.IndexOf("prefix", indexOfFirstAcco) + 6;
                        int indexOfSemi = line.IndexOf(';', indexOfPrefixIndexed);
                        string internalPrefix = line.Substring(indexOfPrefixIndexed, indexOfSemi - indexOfPrefixIndexed).Trim();
                        Import import = new Import()
                        {
                            Name = subFileName,
                            InternalPrefix = internalPrefix,
                        };*/

                        yangFile.Imports.Add(import);
                        if (Files.Any(x => x.Module == import.Name))
                        {
                            continue;
                        }

                        importFiles.Add(import.Name);
                        break;

                    case string s when s.StartsWith("include"):
                        yangFile.Includes.Add(line.Split(" ")[1].Replace("\"", string.Empty).Replace(";", String.Empty));
                        break;

                    case string s when s.StartsWith("description"):
                        Parsing.GetMultilineText(file, i, out int descriptionIndex);
                        i = descriptionIndex;
                        break;

                    case string s when s.StartsWith("grouping "):
                        groupingsLines.Add(Parsing.GetSubObject(file, i, out int index));
                        i = index;
                        break;

                    case string s when s.StartsWith("uses "):
                        Uses uses = new Uses(line, yangFile.Prefix);
                        yangFile.RootUses = uses;
                        break;

                    case string s when s.StartsWith("identity "):
                        Parsing.GetSubObject(file, i, out int identityIndex);
                        i = identityIndex;
                        break;

                    case string s when s.StartsWith("augment"):
                        List<string> lines = Parsing.GetSubObject(file, i, out int augmentIndex);
                        yangFile.Augments.Add(new Augment(lines, yangFile.Prefix));
                        i = augmentIndex;
                        break;

                    default:
                        break;
                }
            }

            if (Files.Any(x => x.Prefix == yangFile.Prefix))
            {
                return new List<string>();
            }

            yangFile.Groupings = LoopGroupings(groupingsLines, prefix);
            if (!String.IsNullOrWhiteSpace(yangFile.RootUses.Name))
            {
                yangFile.RootGrouping = yangFile.Groupings.FirstOrDefault(x => x.Name == yangFile.RootUses.Name);
            }

            Files.Add(yangFile);
            return importFiles;
        }

        private List<Grouping> LoopGroupings(List<List<string>> groupingsLines, string prefix)
        {
            List<Grouping> groupings = new List<Grouping>();
            foreach (var group in groupingsLines)
            {
                Grouping grouping = new Grouping();
                List<Leaf> leafs = new List<Leaf>();
                List<Container> containers = new List<Container>();
                List<List> lists = new List<List>();
                List<Uses> usess = new List<Uses>();
                int comment = 0;
                for (int i = 0; i < group.Count; i++)
                {
                    string line = group[i].Trim();
                    if (line.StartsWith("//"))
                    {
                        continue;
                    }

                    comment = IsBlockComment(line, comment);

                    if (comment != 0)
                    {
                        continue;
                    }

                    switch (line)
                    {
                        case string s when s.StartsWith("grouping "):
                            int indexOfGroupingIndexed = line.IndexOf("grouping") + 8;
                            int indexOfFirstAcco = line.IndexOf("{");
                            grouping.Prefix = prefix;
                            grouping.Name = line.Substring(indexOfGroupingIndexed, indexOfFirstAcco - indexOfGroupingIndexed).Trim();
                            break;

                        case string s when s.StartsWith("description"):
                            grouping.Description = Parsing.GetMultilineText(group, i, out int descriptionIndex);
                            i = descriptionIndex;
                            break;

                        case string s when s.StartsWith("container"):
                            List<string> container = Parsing.GetSubObject(group, i, out int containerIndex);
                            containers.Add(ParseContainer(container, prefix));
                            i = containerIndex;
                            break;

                        case string s when s.StartsWith("list"):
                            List<string> list = Parsing.GetSubObject(group, i, out int listIndex);
                            lists.Add(ParseList(list, prefix));
                            i = listIndex;
                            break;

                        case string s when s.StartsWith("leaf"):
                            List<string> leaf = Parsing.GetSubObject(group, i, out int index);
                            leafs.Add(Leaf.Parse(leaf));
                            i = index;
                            break;

                        case string s when s.StartsWith("uses "):
                            Uses uses = new Uses();
                            string[] fullUses = line.Split(' ')[1].Replace(";", string.Empty).Split(':');
                            if (fullUses.Length == 1)
                            {
                                uses.InternalPrefix = prefix;
                                uses.Name = fullUses[0];
                            }
                            else
                            {
                                uses.InternalPrefix = fullUses[0];
                                uses.Name = fullUses[1];
                            }

                            usess.Add(uses);
                            break;
                    }
                }

                grouping.AddContainers(containers);
                grouping.Leafs = leafs;
                grouping.Uses.AddRange(usess);
                groupings.Add(grouping);
            }

            return groupings;
        }

        private void ParseIdentify()
        {

        }

        private List ParseList(List<string> listLines, string prefix)
        {
            List list = new List();
            List<List> lists = new List<List>();
            List<Leaf> leafs = new List<Leaf>();
            List<Container> containers = new List<Container>();
            for (int i = 0; i < listLines.Count; i++)
            {
                string line = listLines[i].Trim();
                switch (line)
                {
                    case string s when s.StartsWith("list"):
                        if (i == 0)
                        {
                            int indexOfListIndexed = line.IndexOf("list") + 4;
                            int indexOfFirstAcco = line.IndexOf("{", indexOfListIndexed);
                            list.Name = line.Substring(indexOfListIndexed, indexOfFirstAcco - indexOfListIndexed).Trim();
                            continue;
                        }

                        List<string> subList = Parsing.GetSubObject(listLines, i, out int listIndex);
                        lists.Add(ParseList(subList, prefix));
                        i = listIndex;
                        break;

                    case string s when s.StartsWith("description"):
                        list.Description = Parsing.GetMultilineText(listLines, i, out int index);
                        i = index;
                        break;

                    case string s when s.StartsWith("key"):
                        int indexOfFirst = line.IndexOf('"');
                        int indexOfLast = line.LastIndexOf('"');
                        list.Key = line.Substring(indexOfFirst, indexOfLast - indexOfFirst).Trim();
                        break;

                    case string s when s.StartsWith("leaf"):
                        List<string> leaf = Parsing.GetSubObject(listLines, i, out int leafIndex);
                        leafs.Add(Leaf.Parse(leaf));
                        i = leafIndex;
                        break;

                    case string s when s.StartsWith("container"):
                        List<string> container = Parsing.GetSubObject(listLines, i, out int containerIndex);
                        containers.Add(ParseContainer(container, prefix));
                        i = containerIndex;
                        break;

                    case string s when s.StartsWith("uses"):
                        Uses uses = new Uses();
                        string[] fullUses = line.Split(' ')[1].Replace(";", string.Empty).Split(':');
                        if (fullUses.Length == 1)
                        {
                            uses.InternalPrefix = prefix;
                            uses.Name = fullUses[0];
                        }
                        else
                        {
                            uses.InternalPrefix = fullUses[0];
                            uses.Name = fullUses[1];
                        }

                        list.Uses.Add(uses);
                        break;
                }
            }

            list.Lists = lists;
            list.Containers = containers;
            list.Leafs = leafs;

            return list;
        }

        private Container ParseContainer(List<string> containerLines, string prefix)
        {
            Container container = new Container();
            List<List<string>> containers = new List<List<string>>();
            for (int i = 0; i < containerLines.Count; i++)
            {
                string line = containerLines[i].Trim();
                switch (line)
                {
                    case string s when s.StartsWith("container"):
                        if (i == 0)
                        {
                            int indexOfContainerIndexed = line.IndexOf("container") + 9;
                            int indexOfFirstAcco = line.IndexOf("{", indexOfContainerIndexed);
                            container.Name = line.Substring(indexOfContainerIndexed, indexOfFirstAcco - indexOfContainerIndexed).Trim();
                            continue;
                        }

                        List<string> subContainer = Parsing.GetSubObject(containerLines, i, out int containerIndex);
                        container.AddContainer(ParseContainer(subContainer, prefix));
                        i = containerIndex;
                        break;

                    case string s when s.StartsWith("description"):
                        container.Description = Parsing.GetMultilineText(containerLines, i, out int index);
                        i = index;
                        break;

                    case string s when s.StartsWith("config"):
                        container.Config = Convert.ToBoolean(line.Split(' ')[1].Replace(";", string.Empty));
                        break;

                    case string s when s.StartsWith("uses"):
                        Uses uses = new Uses();
                        string[] fullUses = line.Split(' ')[1].Replace(";", string.Empty).Split(':');
                        if (fullUses.Length == 1)
                        {
                            uses.InternalPrefix = prefix;
                            uses.Name = fullUses[0];
                        }
                        else
                        {
                            uses.InternalPrefix = fullUses[0];
                            uses.Name = fullUses[1];
                        }

                        container.Uses.Add(uses);
                        break;

                    case string s when s.StartsWith("list"):
                        List<string> listLines = Parsing.GetSubObject(containerLines, i, out int listIndex);
                        container.Lists.Add(ParseList(listLines, prefix));
                        i = listIndex;
                        break;

                    case string s when s.StartsWith("leaf "):
                        List<string> leaf = Parsing.GetSubObject(containerLines, i, out int leafIndex);
                        container.Leafs.Add(Leaf.Parse(leaf));
                        i = leafIndex;
                        break;
                }
            }

            return container;
        }
    }
}
