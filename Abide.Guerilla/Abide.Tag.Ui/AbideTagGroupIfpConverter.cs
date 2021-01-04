using Abide.Tag.Definition;
using System.IO;
using System.Xml;

namespace Abide.Tag.Ui
{
    public static class AbideTagGroupIfpConverter
    {
        public static void WriteIfp(this AbideTagGroup tagGroup, TagDefinitionCollection collection, string path)
        {
            //Prepare
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Indent = true,
                NewLineOnAttributes = false,
                IndentChars = "\t"
            };

            //Create stream
            using (FileStream fs = File.OpenWrite(path))
            using (XmlWriter writer = XmlWriter.Create(fs, settings))
            {
                //Write start document
                writer.WriteStartDocument();

                //Write plugin element
                writer.WriteStartElement("plugin");

                //Write attributes
                writer.WriteAttribute("class", tagGroup.GroupTag);
                writer.WriteAttribute("author", "Abide");
                writer.WriteAttribute("version", "0.1");

                //Write end plugin element
                writer.WriteEndElement();

                //End document
                writer.WriteEndDocument();
                fs.Flush();
            }
        }

        private static void WriteAttribute(this XmlWriter writer, string name, object value)
        {

        }
    }
}
