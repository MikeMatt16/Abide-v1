using System;
using System.IO;
using System.Xml;

namespace Abide.Tag.Definition
{
    /// <summary>
    /// Represents an Abide tag block definition.
    /// </summary>
    public sealed class AbideTagBlock : ICloneable
    {
        /// <summary>
        /// Gets or sets the name of the tag block.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        /// <summary>
        /// Gets or sets the display name of the tag block.
        /// </summary>
        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }
        /// <summary>
        /// Gets or sets the maximum number of tag blocks in an array.
        /// </summary>
        public int MaximumElementCount
        {
            get { return maximumElementCount; }
            set { maximumElementCount = value; }
        }
        /// <summary>
        /// Gets or sets the field set of the tag block.
        /// </summary>
        public AbideFieldSet FieldSet
        {
            get { return fieldSet; }
            set { fieldSet = value ?? throw new TagException("Field set cannot be null.", new ArgumentNullException(nameof(value))); }
        }

        private string name, displayName;
        private int maximumElementCount;
        private AbideFieldSet fieldSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbideTagBlock"/> class.
        /// </summary>
        public AbideTagBlock()
        {
            //Prepare
            name = string.Empty;
            displayName = string.Empty;
            maximumElementCount = 0;
            fieldSet = new AbideFieldSet(this);
        }
        /// <summary>
        /// Returns a copy of the <see cref="AbideTagBlock"/>.
        /// </summary>
        /// <returns>A copy of the current <see cref="AbideTagBlock"/> object.</returns>
        public object Clone()
        {
            //Create
            AbideTagBlock block = new AbideTagBlock()
            {
                name = name,
                displayName = displayName,
                maximumElementCount = maximumElementCount,
                fieldSet = (AbideFieldSet)fieldSet.Clone()
            };
            block.fieldSet.Owner = this;

            //Return
            return block;
        }
        /// <summary>
        /// Loads the Abide tag block document from the specified URL.
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
        /// Loads the Abide tag block document from the specified stream.
        /// </summary>
        /// <param name="inStream">The stream containing the Abide tag block document to load.</param>
        public void Load(Stream inStream)
        {
            //Create XML Doc
            XmlDocument doc = new XmlDocument();
            doc.Load(inStream);

            //Read IFP
            LoadFromXmlDocument(doc);
        }
        /// <summary>
        /// Loads the Abide tag block document from the specified <see cref="TextReader"/>.
        /// </summary>
        /// <param name="txtReader">The <see cref="TextReader"/> used to feed the Abide tag block data into the document.</param>
        public void Load(TextReader txtReader)
        {
            //Create XML Doc
            XmlDocument doc = new XmlDocument();
            doc.Load(txtReader);

            //Read IFP
            LoadFromXmlDocument(doc);
        }
        /// <summary>
        /// Loads the Abide tag block document from the specified string.
        /// </summary>
        /// <param name="atb">String contianing the Abide tag block document to load.</param>
        /// <exception cref="TagException"></exception>
        public void LoadAtb(string atb)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(atb);
        }
        /// <summary>
        /// Saves the Abide tag block definition to the specified file. If the file exists, this method overwrites it.
        /// </summary>
        /// <param name="filename">The location of the file where you want to save the definition.</param>
        public void Save(string filename)
        {
            //Create XML writer
            using (XmlWriter xmlWriter = XmlWriter.Create(filename ?? throw new ArgumentNullException(nameof(filename))))
                Save(xmlWriter);
        }
        /// <summary>
        /// Saves the Abide tag block definition to the specified stream.
        /// </summary>
        /// <param name="outStream">The stream to which you want to save.</param>
        public void Save(Stream outStream)
        {
            //Create XML writer
            using (XmlWriter xmlWriter = XmlWriter.Create(outStream ?? throw new ArgumentNullException(nameof(outStream))))
                Save(xmlWriter);
        }
        /// <summary>
        /// Saves the Abide tag block definition to the specified <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="writer">The <see cref="TextWriter"/> to which you want to save.</param>
        public void Save(TextWriter writer)
        {
            //Create XML writer
            using (XmlWriter xmlWriter = XmlWriter.Create(writer ?? throw new ArgumentNullException(nameof(writer))))
                Save(xmlWriter);
        }
        /// <summary>
        /// Saves the Abide tag block definition to the specified <see cref="XmlWriter"/>.
        /// </summary>
        /// <param name="w">The <see cref="XmlWriter"/> to which you want to save.</param>
        public void Save(XmlWriter w)
        {
            //Start document
            w.WriteStartDocument();

            //Start tag block
            w.WriteStartElement("AbideTagBlock");

            //Write version
            w.WriteStartAttribute("Version");
            w.WriteValue("1.0");
            w.WriteEndAttribute();

            //Write name
            w.WriteStartAttribute("Name");
            w.WriteValue(name);
            w.WriteEndAttribute();

            //Write display name
            w.WriteStartAttribute("DisplayName");
            w.WriteValue(displayName);
            w.WriteEndAttribute();

            //Write maximum element count
            w.WriteStartAttribute("MaximumElementCount");
            w.WriteValue(maximumElementCount);
            w.WriteEndAttribute();

            //Start field set
            w.WriteStartElement("FieldSet");

            //Write alignment
            w.WriteStartAttribute("Alignment");
            w.WriteValue(fieldSet.Alignment);
            w.WriteEndAttribute();

            //Write count
            w.WriteStartAttribute("Count");
            w.WriteValue(fieldSet.Count);
            w.WriteEndAttribute();

            //Loop
            int index = 0;
            foreach (AbideTagField tagField in fieldSet)
            {
                //Start field
                w.WriteStartElement(Enum.GetName(typeof(FieldType), tagField.FieldType));

                //Write index
                w.WriteStartAttribute("Index");
                w.WriteValue(index++);
                w.WriteEndAttribute();
                
                //Write field defintion
                tagField.Save(w);

                //Write Options
                foreach (ObjectName option in tagField.Options)
                {
                    //Write option
                    w.WriteStartElement("Option");
                    w.WriteStartAttribute("Name");
                    w.WriteValue(option.ToString());
                    w.WriteEndAttribute();
                    w.WriteEndElement();
                }

                //End field
                w.WriteEndElement();
            }

            //End field set
            w.WriteEndElement();

            //End tag block
            w.WriteEndElement();

            //End document
            w.WriteEndDocument();
        }
        /// <summary>
        /// Loads the Abide tag block document document from the specified <see cref="XmlDocument"/>.
        /// </summary>
        /// <param name="doc">The <see cref="XmlDocument"/> to load the <see cref="AbideTagBlock"/> from.</param>
        private void LoadFromXmlDocument(XmlDocument doc)
        {
            try
            {
                //Get tag block
                XmlNode tagBlock = doc["AbideTagBlock"] ?? throw new TagException("Document doesn't contain a tag block node.");

                //Setup
                name = tagBlock.Attributes["Name"]?.Value ?? throw new TagException("Tag block node doesn't specify a name.");
                displayName = tagBlock.Attributes["DisplayName"]?.Value ?? throw new TagException("Tag block node doesn't specify a display name.");
                if (!int.TryParse(tagBlock.Attributes["MaximumElementCount"]?.Value, out maximumElementCount)) throw new TagException("Tag block node doesn't specify a maximum element count.");

                //Get field set
                XmlNode fieldSetNode = tagBlock["FieldSet"] ?? throw new TagException("Tag block node doesn't contain a field set node.");

                //Setup
                if (!int.TryParse(fieldSetNode.Attributes["Alignment"]?.Value, out int alignment)) throw new TagException("Field set node doesn't specify an alignment.");
                fieldSet.Alignment = alignment;

                //Loop
                foreach (XmlNode field in fieldSetNode)
                    fieldSet.Add(AbideTagField.FromXmlNode(field));
            }
            catch (Exception ex) { throw new TagException("An error occured while loading the tag group document.", ex); }
        }
        /// <summary>
        /// Returns a string representation of this <see cref="AbideTagBlock"/>.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"{name} ({displayName})";
        }
    }
}
