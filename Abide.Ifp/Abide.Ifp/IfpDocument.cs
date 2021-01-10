using System;
using System.IO;
using System.Xml;

namespace Abide.Ifp
{
    /// <summary>
    /// Represents an IronForgePlugin document.
    /// </summary>
    [Serializable]
    public sealed class IfpDocument
    {
        private IfpNode main;

        /// <summary>
        /// Gets and returns the plugin node.
        /// </summary>
        public IfpNode Plugin
        {
            get { return main; }
        }

        /// <summary>
        /// Initializes a new <see cref="IfpDocument"/> instance.
        /// </summary>
        public IfpDocument()
        {
            //Setup
            main = new IfpNode();
        }
        /// <summary>
        /// Loads the IFP document from the specified URL.
        /// </summary>
        /// <param name="fileName">URL for the file containing the IFP document to load.
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
        /// Loads the IFP document from the specified stream.
        /// </summary>
        /// <param name="inStream">The stream containing the IFP document to load.</param>
        public void Load(Stream inStream)
        {
            //Create XML Doc
            XmlDocument doc = new XmlDocument();
            doc.Load(inStream);

            //Read IFP
            LoadFromXmlDocument(doc);
        }
        /// <summary>
        /// Loads the IFP document from the specified <see cref="TextReader"/>.
        /// </summary>
        /// <param name="txtReader">The <see cref="TextReader"/> used to feed the IFP data into the document.</param>
        public void Load(TextReader txtReader)
        {
            //Create XML Doc
            XmlDocument doc = new XmlDocument();
            doc.Load(txtReader);

            //Read IFP
            LoadFromXmlDocument(doc);
        }
        /// <summary>
        /// Loads the IFP document from the specified string.
        /// </summary>
        /// <param name="ifp">String contianing the IFP document to load.</param>
        /// <exception cref="IfpException"></exception>
        public void LoadIfp(string ifp)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(ifp);
        }
        /// <summary>
        /// Loads the IFP document from the specified <see cref="XmlDocument"/>.
        /// </summary>
        /// <param name="doc">The <see cref="XmlDocument"/> to load the <see cref="IfpDocument"/> from.</param>
        private void LoadFromXmlDocument(XmlDocument doc)
        {
            //Get Plugin
            try { main = IfpNode.FromXmlNode(doc["plugin"]); }
            catch(Exception ex) { throw new IfpException("An error occured while loading the plugin document.", ex); }
        }
    }
}
