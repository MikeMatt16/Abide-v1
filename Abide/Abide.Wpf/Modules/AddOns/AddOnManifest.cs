using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Abide.Wpf.Modules.AddOns
{
    public sealed class AddOnManifest : IEnumerable<string>
    {
        public string Name { get; set; }
        public string PrimaryAssemblyFile { get; set; }
        public string this[int index]
        {
            get => files[index];
            set => files[index] = value;
        }

        private readonly List<string> files;

        public AddOnManifest()
        {
            //Initialize
            files = new List<string>();
        }
        public void SaveXml(string filename)
        {
            //Check
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentException("Invalid file name.", nameof(filename));
            }

            if (!Directory.Exists(Path.GetDirectoryName(filename)))
            {
                _ = Directory.CreateDirectory(Path.GetDirectoryName(filename));
            }

            //Create Stream
            using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
            {
                SaveXml(fs);
            }
        }
        public void SaveXml(Stream outStream)
        {
            //Check
            if (outStream == null)
            {
                throw new ArgumentNullException(nameof(outStream));
            }

            if (!outStream.CanWrite)
            {
                throw new ArgumentException("Stream does not support writing.", nameof(outStream));
            }

            //Prepare
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            //Create
            using (XmlWriter writer = XmlWriter.Create(outStream, settings))
            {
                //Write Header
                writer.WriteStartDocument();

                //Write Main Node
                writer.WriteStartElement("Manifest");

                //Write Name
                writer.WriteStartElement("Name");
                writer.WriteValue(Name);
                writer.WriteEndElement();

                //Write Name
                writer.WriteStartElement("PrimaryAssembly");
                writer.WriteValue(PrimaryAssemblyFile);
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
        public void LoadXml(string filename)
        {
            //Check
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentException("Invalid file name.", nameof(filename));
            }

            if (!File.Exists(filename))
            {
                throw new FileNotFoundException("Unable to open file.", filename);
            }

            //Create Stream
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                LoadXml(fs);
            }
        }
        public void LoadXml(Stream inStream)
        {
            //Check
            if (inStream == null)
            {
                throw new ArgumentNullException(nameof(inStream));
            }

            if (!inStream.CanRead)
            {
                throw new ArgumentException("Stream does not support reading.", nameof(inStream));
            }

            //Open Document
            XmlDocument document = new XmlDocument();
            document.Load(inStream);

            //Get Manifest
            var Manifest = document["Manifest"];

            //Get Name
            Name = Manifest?["Name"]?.InnerText;
            PrimaryAssemblyFile = Manifest?["PrimaryAssembly"]?.InnerText;

            //Loop
            files.Clear();
            foreach (XmlNode node in Manifest?["Files"]?.ChildNodes)
            {
                files.Add(node.InnerText);
            }
        }
        public void Add(string filename)
        {
            //Check
            if (filename == null)
            {
                throw new ArgumentNullException(nameof(filename));
            }

            //Add
            files.Add(filename);
        }
        public bool Remove(string filename)
        {
            //Check
            if (filename == null)
            {
                throw new ArgumentNullException(nameof(filename));
            }

            //Remove
            return files.Remove(filename);
        }
        public IEnumerator<string> GetEnumerator()
        {
            //Return
            return files.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            //Return
            return GetEnumerator();
        }
    }
}
