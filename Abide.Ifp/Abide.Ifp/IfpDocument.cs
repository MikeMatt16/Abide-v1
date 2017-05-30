using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace Abide.Ifp
{
    /// <summary>
    /// Represents an IFP document.
    /// </summary>
    public class IfpDocument
    {
        private IfpNode[] nodes;

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
            LoadDocument(doc);
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
            LoadDocument(doc);
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
            LoadDocument(doc);
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

        private void LoadDocument(XmlDocument doc)
        {
        }
    }

    /// <summary>
    /// Represents a single node in the IFP document.
    /// </summary>
    public class IfpNode : ICloneable, IEnumerable<IfpNode>, IXPathNavigable
    {
        public string Name
        {
            get { return name; }
        }

        public string name;
        private IfpAttribute[] attributes;
        private IfpNode[] nodes;

        public object Clone()
        {
            return new IfpNode() { name = name, attributes = attributes, nodes = nodes };
        }

        public IEnumerator<IfpNode> GetEnumerator()
        {
            return (IEnumerator<IfpNode>)nodes.GetEnumerator();
        }

        public XPathNavigator CreateNavigator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return nodes.GetEnumerator();
        }
    }


    public class IfpAttribute
    {
        public string Name
        {
            get { return name; }
        }
        public string Value
        {
            get { return value; }
        }

        private string name;
        private string value;

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
                return string.Format("{0}: {1}", name, value);
            else if (!string.IsNullOrEmpty(name)) return name;
            else return base.ToString();
        }
    }
}
