﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using Tabler.Docs.Icons;
using TabloRazor;

namespace IconGenerator
{
    public static class Utilities
    {

        public static string ExtractIconElements(IEnumerable<XElement> elements)
        {
            var elementsString = string.Join("", elements.Select(e => e));
            elementsString = elementsString.Replace(@"""", "'");
            elementsString = elementsString.Replace(Environment.NewLine, "");

            return elementsString;
        }


        public static string ExtractFlagElements(XElement svg)
        {
            var rewriteIds = ReWriteIds(svg);
            var elementsString = string.Join("", svg.Elements().ToList().Select(e => e));
            elementsString = elementsString.Replace(@"""", "'");
            elementsString = elementsString.Replace(Environment.NewLine, "");
            foreach (var rewriteId in rewriteIds)
            {
                elementsString = elementsString.Replace("'#" + rewriteId.Key + "'", "'#" + rewriteId.Value + "'");
            }

            return elementsString;
        }

        private static new Dictionary<string, string> ReWriteIds(XElement svg)
        {
            var idList = new Dictionary<string, string>();
            var idElements = svg.Descendants().Where(e => e.Attribute("id") != null).ToList();

            foreach (var idElement in idElements)
            {
                var id = idElement.Attribute("id").Value;
                var newId = "";

                if (idList.ContainsKey(id))
                {
                    newId = idList[id];
                }
                else
                {
                    newId = Guid.NewGuid().ToString("N");
                    idList.Add(id, newId);
                }
                idElement.Attribute("id").Value = newId;
            }

            return idList;



        }


        public static string ExtractIconElements(string svgContent)
        {
            var svg = XElement.Parse(svgContent);
            svg.RemoveAllNamespaces();
            var elements = svg.Elements();
            return ExtractIconElements(elements);
        }

        public static void GeneratFlagsFile(string fileName, IEnumerable<GeneratedFlag> flags)
        {
            flags = flags.ToList().OrderBy(z => z.Name);
            var directory = Directory.GetParent(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)).Parent.Parent.Parent.Parent;
            var fileOutput = new StringBuilder();


            fileOutput.AppendLine("using System;");
            fileOutput.AppendLine("using System.Collections.Generic;");
            fileOutput.AppendLine("using System.Linq;");
            fileOutput.AppendLine("using System.Text;");
            fileOutput.AppendLine("using System.Threading.Tasks;");
            fileOutput.AppendLine("");
            fileOutput.AppendLine("namespace TabloRazor.Assets");
            fileOutput.AppendLine("{");
            fileOutput.AppendLine("");
            fileOutput.AppendLine($"\tpublic static class {fileName}");
            fileOutput.AppendLine("\t{");


            foreach (var flag in flags)
            {

                fileOutput.AppendLine("\t\t" + flag.DotNetProperty);
            }

            fileOutput.AppendLine("\t}");
            fileOutput.AppendLine("}");


            File.WriteAllText(Path.Combine(directory.FullName, "src\\TabloRazor.Assets", fileName + ".cs"), fileOutput.ToString());

        }


        public static void GenerateIconsFile(string fileName, IEnumerable<GeneratedIcon> icons)
        {
            icons = icons.ToList().OrderBy(z => z.Name);
            var directory = Directory.GetParent(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)).Parent.Parent.Parent.Parent;
            var fileOutput = new StringBuilder();

            fileOutput.AppendLine("using System;");
            fileOutput.AppendLine("using System.Collections.Generic;");
            fileOutput.AppendLine("using System.Linq;");
            fileOutput.AppendLine("using System.Text;");
            fileOutput.AppendLine("using System.Threading.Tasks;");
            fileOutput.AppendLine("");
            fileOutput.AppendLine("namespace TabloRazor.Assets");
            fileOutput.AppendLine("{");
            fileOutput.AppendLine("");
            fileOutput.AppendLine($"\tpublic static class {fileName}");
            fileOutput.AppendLine("\t{");


            foreach (var icon in icons)
            {
                fileOutput.AppendLine("\t\t" + icon.DotNetProperty);
            }


            fileOutput.AppendLine("\t}");
            fileOutput.AppendLine("}");

            File.WriteAllText(Path.Combine(directory.FullName, "src\\TabloRazor.Assets", fileName + ".cs"), fileOutput.ToString());
        }
    }
}
