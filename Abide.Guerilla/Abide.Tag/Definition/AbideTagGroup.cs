using Abide.HaloLibrary;
using System;
using System.IO;
using System.Xml;

namespace Abide.Tag.Definition
{
    /// <summary>
    /// Represents an Abide tag group definition.
    /// </summary>
    public sealed class AbideTagGroup
    {
        /// <summary>
        /// Gets and returns the tag group's group tag.
        /// </summary>
        public TagFourCc GroupTag { get; private set; }
        /// <summary>
        /// Gets and returns the tag group's parent group tag.
        /// </summary>
        public TagFourCc ParentGroupTag { get; private set; }
        /// <summary>
        /// Gets and returns the name of the tag group.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Gets and returns the name of the tag group's tag block definition.
        /// </summary>
        public string BlockName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbideTagGroup"/> class.
        /// </summary>
        public AbideTagGroup()
        {
            //Prepare
            GroupTag = string.Empty;
            ParentGroupTag = string.Empty;
            Name = string.Empty;
            BlockName = string.Empty;
        }
        /// <summary>
        /// Loads the Abide tag group document from the specified URL.
        /// </summary>
        /// <param name="filename">URL for the file containing the Abide tag group document to load.
        /// The URL can either be a local file or an HTTP URL (a Web address).</param>
        public void Load(string filename)
        {
            //Create XML Doc
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            //Read IFP
            LoadFromXmlDocument(doc);
        }
        /// <summary>
        /// Loads the Abide tag group document from the specified stream.
        /// </summary>
        /// <param name="inStream">The stream containing the Abide tag group document to load.</param>
        public void Load(Stream inStream)
        {
            //Create XML Doc
            XmlDocument doc = new XmlDocument();
            doc.Load(inStream);

            //Read IFP
            LoadFromXmlDocument(doc);
        }
        /// <summary>
        /// Loads the Abide tag group document from the specified <see cref="TextReader"/>.
        /// </summary>
        /// <param name="txtReader">The <see cref="TextReader"/> used to feed the Abide tag group data into the document.</param>
        public void Load(TextReader txtReader)
        {
            //Create XML Doc
            XmlDocument doc = new XmlDocument();
            doc.Load(txtReader);

            //Read IFP
            LoadFromXmlDocument(doc);
        }
        /// <summary>
        /// Loads the Abide tag group document from the specified string.
        /// </summary>
        /// <param name="atg">String contianing the Abide tag group document to load.</param>
        /// <exception cref="TagException"></exception>
        public void LoadAtg(string atg)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(atg);
        }
        /// <summary>
        /// Loads the Abide tag group document from the specified <see cref="XmlDocument"/>.
        /// </summary>
        /// <param name="doc">The <see cref="XmlDocument"/> to load the <see cref="AbideTagGroup"/> from.</param>
        private void LoadFromXmlDocument(XmlDocument doc)
        {
            try
            {
                //Get tag group
                XmlNode tagGroup = doc["AbideTagGroup"] ?? throw new TagException("Document doesn't contain a tag group node.");

                //Setup
                GroupTag = tagGroup.Attributes["GroupTag"]?.Value ?? throw new TagException("Tag group node doesn't contain a group tag.");
                ParentGroupTag = tagGroup.Attributes["ParentGroupTag"]?.Value ?? throw new TagException("Tag group node doesn't contain a parent group tag.");
                Name = tagGroup.Attributes["Name"]?.Value ?? throw new TagException("Tag group node doesn't specify a name.");
                BlockName = tagGroup.Attributes["Block"]?.Value ?? throw new TagException("Tag group node doesn't specify a tag block.");
            }
            catch (Exception ex) { throw new TagException("An error occured while loading the tag group document.", ex); }
        }
        /// <summary>
        /// Returns a string representation of this <see cref="AbideTagGroup"/>.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            string groupTag = this.GroupTag;
            if (!string.IsNullOrEmpty(ParentGroupTag)) groupTag += $".{ParentGroupTag}";
            return $"{groupTag} {Name} ({BlockName})";
        }
    }
}
