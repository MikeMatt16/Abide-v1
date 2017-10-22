using Abide.HaloLibrary;
using Abide.Ifp;
using System;
using System.Collections.Generic;
using System.IO;

namespace Abide.Builder
{
    static class CsGenerator
    {
        private static readonly Dictionary<Tag, string> classNameTagPairs = new Dictionary<Tag, string>();

        [STAThread]
        static void Main(string[] args)
        {
            //Begin
            Console.WriteLine("Welcome to Abide Builder. This application builds C# files from .ent files!");
            Console.WriteLine("Begin by dragging your IFP folder into the console.");
            string ifpDirectory = Console.ReadLine().Replace("\"", string.Empty);

            //Check
            if (Directory.Exists(ifpDirectory))
            {
                //Get File list
                string[] files = Directory.GetFiles(ifpDirectory);
                classNameTagPairs.Clear();

                //Loop
                foreach (string file in files)
                    try { using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) ReadIfp(fs); }
                    catch { }

                //Write dictionary
                using (CsWriter writer = new CsWriter(@"G:\Github\Abide\AddOns\Abide Builder\Abide Builder\Tags\Tags.cs"))
                {
                    writer.WriteUsing("System");
                    writer.WriteUsing("Abide.HaloLibrary");
                    writer.WriteUsing("Abide.Builder.Tags");
                    writer.WriteUsing("Abide.Builder.Tags.TagDefinition");
                    writer.WriteStartNamespace("Abide.Builder.Tags");
                    writer.WriteStartClass("AbideTags", "public", "static");
                    writer.WriteMethod("GetTagDefinition", "Type", new CsWriter.TypeNamePair[] { new CsWriter.TypeNamePair("Tag", "tag") }, "public", "static");
                    writer.Write("switch(tag)");
                    writer.Write("{");
                    foreach (var classNamePair in classNameTagPairs)
                        writer.Write($"case \"{classNamePair.Key}\": return typeof({classNamePair.Value});");
                    writer.Write("default: return null;");
                    writer.Write("}");
                    writer.WriteEndMethod();
                    writer.WriteEndClass();
                    writer.WriteEndNamespace();
                }
            }
        }

        private static void ReadIfp(Stream stream)
        {
            //Prepare
            IfpDocument document = new IfpDocument();

            //Load
            try
            {
                //Load Docuemnt
                document.Load(stream);

                using (CsWriter writer = new CsWriter($@"G:\Github\Abide\AddOns\Abide Builder\Abide Builder\Tags\{document.Plugin.Class.FourCc.Replace("<", string.Empty).Replace(">", string.Empty)}.cs"))
                    WriteIfp(document, writer);
            }
            catch { }
        }

        private static void WriteIfp(IfpDocument document, CsWriter writer)
        {
            //Check
            if (!classNameTagPairs.ContainsKey(document.Plugin.Class))
                classNameTagPairs.Add(document.Plugin.Class, $"_{CsWriter.GetSafeString(document.Plugin.Class.FourCc, CsWriter.NameType.Class)}");
            //Begin
            writer.WriteUsing("Abide.Builder.Tags.TagDefinition");
            writer.WriteStartNamespace("Abide.Builder.Tags");
            writer.WriteAttribute("TagDefinition", $"\"{document.Plugin.Class.FourCc}\"", document.Plugin.HeaderSize);
            writer.WriteStartClass($"_{document.Plugin.Class.FourCc}", "internal", "sealed");
            foreach (var node in document.Plugin.Nodes)
                WriteNode(node, writer);
            writer.WriteEndClass();
            writer.WriteEndNamespace();
        }

        private static void WriteNode(IfpNode node, CsWriter writer)
        {
            //Check
            switch (node.Type)
            {
                case IfpNodeType.Tag: writer.WriteAttribute("Tag", $"\"{node.Name}\"", node.FieldOffset); break;
                case IfpNodeType.TagId: writer.WriteAttribute("TagIdentifier", $"\"{node.Name}\"", node.FieldOffset); break;
                case IfpNodeType.StringId: writer.WriteAttribute("StringIdentifier", $"\"{node.Name}\"", node.FieldOffset); break;
                case IfpNodeType.TagBlock: writer.WriteAttribute("TagBlock", $"\"{node.Name}\"", node.FieldOffset, node.Length, node.MaxElements, node.Alignment); break;
                default: return;
            }

            //Write
            writer.WriteStartClass($"_{node.FieldOffset}_{node.Name}", "public", "sealed");

            //Check
            foreach (var child in node.Nodes)
                WriteNode(child, writer);

            //End
            writer.WriteEndClass();
        }
    }
}
