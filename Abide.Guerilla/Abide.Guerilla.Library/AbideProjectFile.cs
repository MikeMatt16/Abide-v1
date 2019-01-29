using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Abide.Guerilla.Library
{
    /// <summary>
    /// Represents an Abide project file.
    /// </summary>
    public sealed class AbideProjectFile
    {
        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the path to the project directory.
        /// </summary>
        public string Directory { get; set; } = string.Empty;
        /// <summary>
        /// Gets and returns the tag path list.
        /// </summary>
        public List<string> Tags { get; } = new List<string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="AbideProjectFile"/> class.
        /// </summary>
        public AbideProjectFile() { throw new NotImplementedException(); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        public void Load(string fileName)
        {
            //Check
            if (!File.Exists(fileName ?? throw new ArgumentNullException(nameof(fileName))))
                throw new FileNotFoundException("Unable to open file.", fileName);

            //Open
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                Load(fs);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inStream"></param>
        /// <exception cref="ArgumentNullException"><paramref name="inStream"/> is null.</exception>
        public void Load(Stream inStream)
        {
            //Check
            if (inStream == null) throw new ArgumentNullException(nameof(inStream));

            //Open
            XmlDocument projectXml = new XmlDocument();
            projectXml.Load(inStream);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        public void Save(string fileName)
        {
            //Check
            if (fileName == null)
                throw new ArgumentNullException(nameof(fileName));

            //Open
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Read))
                Save(fs);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="outStream"></param>
        public void Save(Stream outStream)
        {
            //Write
            using (XmlWriter writer = XmlWriter.Create(outStream, new XmlWriterSettings() { Indent = true, IndentChars = "\t" }))
            {
                //Start document
                writer.WriteStartDocument();

                //Start AbideProject
                writer.WriteStartElement("AbideProject");

                //Write name
                writer.WriteStartElement("Name");
                writer.WriteValue(Name);
                writer.WriteEndElement();

                //Start tags
                writer.WriteStartElement("Tags");

                //Loop
                foreach (string tag in Tags)
                {
                    //Write tag
                    writer.WriteStartElement("Tag");
                    writer.WriteStartAttribute("Path");
                    writer.WriteValue(tag);
                    writer.WriteEndAttribute();
                    writer.WriteEndElement();
                }

                //End tags
                writer.WriteEndElement();

                //End AbideProject
                writer.WriteEndElement();

                //End document
                writer.WriteEndDocument();
            }
        }
    }
}
