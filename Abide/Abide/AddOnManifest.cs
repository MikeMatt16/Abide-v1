using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Abide
{
    /// <summary>
    /// Represents an AddOn manifest.
    /// </summary>
    public sealed class AddOnManifest : IEnumerable<string>
    {
        /// <summary>
        /// Gets or sets the name of the AddOn.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        /// <summary>
        /// Gets or sets the primary assembly file
        /// </summary>
        public string PrimaryAssemblyFile
        {
            get { return primaryAssemblyFile; }
            set { primaryAssemblyFile = value; }
        }
        public string this[int index]
        {
            get
            {
                return files[index];
            }

            set
            {
                files[index] = value;
            }
        }

        private string name;
        private string primaryAssemblyFile;
        private readonly List<string> files;

        /// <summary>
        /// Initializes a new <see cref="AddOnManifest"/> instance.
        /// </summary>
        public AddOnManifest()
        {
            //Initialize
            files = new List<string>();
        }
        /// <summary>
        /// Writes the manifest as an XML file to the specified file name.
        /// </summary>
        /// <param name="filename">The file name to save the manifest XML to.</param>
        public void SaveXml(string filename)
        {
            //Check
            if (string.IsNullOrEmpty(filename)) throw new ArgumentException("Invalid file name.", nameof(filename));
            if (!Directory.Exists(Path.GetDirectoryName(filename))) Directory.CreateDirectory(Path.GetDirectoryName(filename));

            //Create Stream
            using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
                SaveXml(fs);
        }
        /// <summary>
        /// Writes the manifest as an XML file to the specified stream.
        /// </summary>
        /// <param name="outStream">The stream to write the manifest XML to.</param>
        public void SaveXml(Stream outStream)
        {
            //Check
            if (outStream == null) throw new ArgumentNullException(nameof(outStream));
            if (!outStream.CanWrite) throw new ArgumentException("Stream does not support writing.", nameof(outStream));

            //Prepare
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            //Create
            using(XmlWriter writer = XmlWriter.Create(outStream))
            {
                //Write Header
                writer.WriteStartDocument();

                //Write Main Node
                writer.WriteStartElement("Manifest");

                //Write Name
                writer.WriteStartElement("Name");
                writer.WriteValue(name);
                writer.WriteEndElement();

                //Write Name
                writer.WriteStartElement("PrimaryAssembly");
                writer.WriteValue(primaryAssemblyFile);
                writer.WriteEndElement();

                //Write Files
                writer.WriteStartElement("Files");
                foreach (string file in files)
                {
                    writer.WriteStartElement("File");
                    writer.WriteValue(file);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();

                //End
                writer.WriteEndElement();

                //End
                writer.WriteEndDocument();
            }
        }
        /// <summary>
        /// Loads the manifest XML from the specified file.
        /// </summary>
        /// <param name="filename">The file to read the manifest XML from.</param>
        public void LoadXml(string filename)
        {
            //Check
            if (string.IsNullOrEmpty(filename)) throw new ArgumentException("Invalid file name.", nameof(filename));
            if (!File.Exists(filename)) throw new FileNotFoundException("Unable to open file.", filename);

            //Create Stream
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                LoadXml(fs);
        }
        /// <summary>
        /// Loads the manifest XML from the stream.
        /// </summary>
        /// <param name="inStream">The stream to read the manifest XML from.</param>
        public void LoadXml(Stream inStream)
        {
            //Check
            if (inStream == null) throw new ArgumentNullException(nameof(inStream));
            if (!inStream.CanRead) throw new ArgumentException("Stream does not support reading.", nameof(inStream));

            //Open Document
            XmlDocument document = new XmlDocument();
            document.Load(inStream);

            //Get Manifest
            var Manifest = document["Manifest"];

            //Get Name
            name = Manifest?["Name"]?.InnerText;
            primaryAssemblyFile = Manifest?["PrimaryAssembly"]?.InnerText;

            //Loop
            files.Clear();
            foreach (XmlNode node in Manifest?["Files"]?.ChildNodes)
                files.Add(node.InnerText);
        }
        /// <summary>
        /// Adds a file name to the manifest's file list.
        /// </summary>
        /// <param name="filename">The file name to add.</param>
        public void Add(string filename)
        {
            //Check
            if (filename == null) throw new ArgumentNullException(nameof(filename));

            //Add
            files.Add(filename);
        }
        /// <summary>
        /// Removes a file name from the manifest's file list.
        /// </summary>
        /// <param name="filename">The file name to remove.</param>
        public bool Remove(string filename)
        {
            //Check
            if (filename == null) throw new ArgumentNullException(nameof(filename));

            //Remove
            return files.Remove(filename);
        }
        /// <summary>
        /// Returns an enumerator that iterates through this instance.
        /// </summary>
        /// <returns>A <see cref="string"/> enumerator.</returns>
        public IEnumerator<string> GetEnumerator()
        {
            return ((IEnumerable<string>)files).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<string>)files).GetEnumerator();
        }
    }
}
