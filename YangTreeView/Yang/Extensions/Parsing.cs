using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YangTreeView.Yang.Extensions
{
    public class Parsing
    {
        public static string GetMultilineText(List<string> lines, int i, out int index)
        {
            string line = lines[i].Trim();
            int indexOfFirst = line.IndexOf("\"") + 1;
            while (indexOfFirst == 0)
            {
                i++;
                line = lines[i].Trim();
                indexOfFirst = line.IndexOf("\"") + 1;
            }

            int indexOfLast = line.IndexOf("\"", indexOfFirst);
            List<string> descriptionPreviousLine = new List<string>();
            while (indexOfLast == -1)
            {
                descriptionPreviousLine.Add(line.Replace("\"", string.Empty));
                i++;
                line = lines[i].Trim();
                indexOfLast = line.IndexOf("\"");
            }

            if (descriptionPreviousLine.Count > 0)
            {
                descriptionPreviousLine.Add(line.Substring(0, indexOfLast));
                index = i;
                return string.Join(" ", descriptionPreviousLine);
            }
            else
            {
                index = i;
                return line.Substring(indexOfFirst, indexOfLast - indexOfFirst).Trim();
            }
        }

        public static string GetMultilineName(List<string> lines, out int index)
        {
            int i = 0;
            string line = lines[i].Trim();
            int indexOfFirst = line.IndexOf("\"") + 1;
            List<string> descriptionPreviousLine = new List<string>();
            while (indexOfFirst == 0)
            {
                i++;
                line = lines[i].Trim();
                indexOfFirst = line.IndexOf("\"") + 1;
            }

            int indexOfLast = line.IndexOf("{", indexOfFirst);
            if (indexOfLast != -1)
            {
                descriptionPreviousLine.Add(line.Substring(indexOfFirst, indexOfLast - indexOfFirst).Replace("\"", string.Empty));
            }
            else
            {
                descriptionPreviousLine.Add(line.Substring(indexOfFirst, line.Length - indexOfFirst).Replace("\"", string.Empty));
            }

            while (indexOfLast == -1)
            {
                i++;
                line = lines[i].Trim();
                if (line.Contains("{"))
                {
                    indexOfLast = line.IndexOf("{");
                    descriptionPreviousLine.Add(line.Substring(0, indexOfLast)/*.Replace("{", string.Empty)*/);
                    break;
                }

                descriptionPreviousLine.Add(line);
                indexOfLast = line.IndexOf("{");
            }

            index = i;
            if (descriptionPreviousLine.Count > 0)
            {
                //descriptionPreviousLine.Add(line.Substring(0, indexOfLast));
                return string.Join(" ", descriptionPreviousLine).Replace("+", String.Empty).Replace("\"", string.Empty).Replace(" ", String.Empty);
            }
            else
            {
                return line.Substring(indexOfFirst, indexOfLast - indexOfFirst).Trim();
            }
        }

        public static List<string> GetSubObject(List<string> lines, int i, out int index)
        {
            List<string> groupingLines = new List<string>();
            groupingLines.Add(lines[i]);
            bool firstFound = false;
            int openBrackets = 0;
            if (lines[i].Contains("{"))
            {
                firstFound = true;
                openBrackets++;
            }

            if (lines[i].Contains("}"))
            {
                openBrackets--;
            }

            while (openBrackets != 0 || !firstFound)
            {
                i++;
                string nextLine = lines[i].Trim();
                groupingLines.Add(nextLine);
                switch (nextLine)
                {
                    case string t when t.Contains("{"):
                        firstFound = true;
                        openBrackets += t.Count(x => x == '{');
                        break;

                    case string t when t.Contains("}"):
                        firstFound = true;
                        openBrackets -= t.Count(x => x == '}');
                        break;
                }
            }

            index = i;
            return groupingLines;
        }
    }
}
