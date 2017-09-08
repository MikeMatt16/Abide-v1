using Abide.HaloLibrary;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace Abide.Ifp
{
    /// <summary>
    /// Represents a single node in the IFP document.
    /// </summary>
    public sealed class IfpNode
    {
        /// <summary>
        /// Gets or sets the name of the node.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        /// <summary>
        /// Gets or sets the type of the node.
        /// </summary>
        public IfpNodeType Type
        {
            get { return type; }
            set { type = value; }
        }
        /// <summary>
        /// Gets and returns the collection of child <see cref="IfpNode"/> elements.
        /// </summary>
        public IfpNodeCollection Nodes
        {
            get { return nodes; }
        }
        /// <summary>
        /// Gets or sets the length of the element.
        /// </summary>
        public int Length
        {
            get { return length; }
            set { length = value; }
        }
        /// <summary>
        /// Gets or sets the field offset of this element.
        /// </summary>
        public int FieldOffset
        {
            get { return fieldOffset; }
            set { fieldOffset = value; }
        }
        /// <summary>
        /// Gets or sets the usibility of the field offset.
        /// </summary>
        public bool UseFieldOffset
        {
            get { return !inferOffset; }
            set { inferOffset = !value; }
        }
        /// <summary>
        /// Gets or sets the offset of the tag block reference.
        /// </summary>
        public int TagBlockOffset
        {
            get { return tagBlockOffset; }
            set { tagBlockOffset = value; }
        }
        /// <summary>
        /// Gets or sets the size of the tag block reference.
        /// </summary>
        public int TagBlockSize
        {
            get { return tagBlockSize; }
            set { tagBlockSize = value; }
        }
        /// <summary>
        /// Gets or sets the visiblity of the element.
        /// </summary>
        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }
        /// <summary>
        /// Gets or sets the class of the element.
        /// </summary>
        public Tag Class
        {
            get { return @class; }
            set { @class = value; }
        }
        /// <summary>
        /// Gets or sets the header size of the element.
        /// </summary>
        public int HeaderSize
        {
            get { return headerSize; }
            set { headerSize = value; }
        }
        /// <summary>
        /// Gets or sets the option value of the element.
        /// </summary>
        public int Value
        {
            get { return optionValue; }
            set { optionValue = value; }
        }
        
        private readonly IfpNodeCollection nodes;
        private bool inferOffset = true;
        private IfpNodeType type;
        private string name;
        private int length;
        private int fieldOffset;
        private int tagBlockOffset;
        private int tagBlockSize;
        private bool visible;
        private Tag @class;
        private int headerSize;
        private int optionValue;

        /// <summary>
        /// Initializes a new <see cref="IfpNode"/> instance.
        /// </summary>
        public IfpNode()
        {
            //Initialize
            nodes = new IfpNodeCollection();
        }
        /// <summary>
        /// Gets and returns a string representation of this instance.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(name)) return type.ToString();
            else return $"{name} Type: {type}";
        }
        /// <summary>
        /// Returns an <see cref="IfpNode"/> instance from a supplied <see cref="XmlNode"/>.
        /// </summary>
        /// <param name="xmlNode">The base XML node.</param>
        /// <returns>A <see cref="IfpNode"/> instance.</returns>
        internal static IfpNode FromXmlNode(XmlNode xmlNode)
        {
            //Prepare
            IfpNode node = new IfpNode();

            //Setup
            node.name = xmlNode.Attributes?["name"]?.InnerText;
            node.inferOffset = !int.TryParse(xmlNode.Attributes?["offset"]?.InnerText, out node.fieldOffset);
            int.TryParse(xmlNode.Attributes?["reflexiveoffset"]?.InnerText, out node.tagBlockOffset);
            int.TryParse(xmlNode.Attributes?["reflexiveSize"]?.InnerText, out node.tagBlockSize);
            int.TryParse(xmlNode.Attributes?["size"]?.InnerText, out node.length);
            int.TryParse(xmlNode.Attributes?["headersize"]?.InnerText, out node.headerSize);
            int.TryParse(xmlNode.Attributes?["value"]?.InnerText, out node.optionValue);
            bool.TryParse(xmlNode.Attributes?["visible"]?.InnerText, out node.visible);
            node.@class = xmlNode.Attributes?["class"]?.InnerText ?? string.Empty;

            //Load Children
            if (xmlNode.ChildNodes != null)
                foreach (XmlNode child in xmlNode.ChildNodes)
                    node.Nodes.Add(FromXmlNode(child));

            //Determine Type
            node.Type = xmlNode_GetType(xmlNode);

            //Return
            return node;
        }
        /// <summary>
        /// Returns an <see cref="IfpNodeType"/> from a supplied <see cref="XmlNode"/>.
        /// </summary>
        /// <param name="xmlNode">The XML node.</param>
        /// <returns>An <see cref="IfpNodeType"/>.</returns>
        private static IfpNodeType xmlNode_GetType(XmlNode xmlNode)
        {
            switch (xmlNode.Name)
            {
                case "option": return IfpNodeType.Option;
                case "struct": return IfpNodeType.TagBlock;
                case "unused": return IfpNodeType.Buffer;
                case "byte": return IfpNodeType.Byte;
                case "sbyte": return IfpNodeType.SignedByte;
                case "short": return IfpNodeType.Short;
                case "ushort": return IfpNodeType.UnsignedShort;
                case "int": return IfpNodeType.Int;
                case "uint": return IfpNodeType.UnsignedInt;
                case "long": return IfpNodeType.Long;
                case "ulong": return IfpNodeType.UnsignedLong;
                case "float": return IfpNodeType.Single;
                case "double": return IfpNodeType.Double;
                case "enum8": return IfpNodeType.Enumerator8;
                case "enum16": return IfpNodeType.Enumerator16;
                case "enum32": return IfpNodeType.Enumerator32;
                case "enum64": return IfpNodeType.Enumerator64;
                case "bitmask8": return IfpNodeType.Bitfield8;
                case "bitmask16": return IfpNodeType.Bitfield16;
                case "bitmask32": return IfpNodeType.Bitfield32;
                case "bitmask64": return IfpNodeType.Bitfield64;
                case "tag": return IfpNodeType.Tag;
                case "id": return IfpNodeType.TagId;
                case "stringid": return IfpNodeType.StringId;
                case "string32": return IfpNodeType.String32;
                case "string64": return IfpNodeType.String64;
                case "unicode128": return IfpNodeType.Unicode128;
                case "unicode256": return IfpNodeType.Unicode256;
                default: return IfpNodeType.Unspecified;
            }
        }
    }

    /// <summary>
    /// Represents a collection if <see cref="IfpNode"/> elements that can be accessed by an index.
    /// </summary>
    public sealed class IfpNodeCollection : ICollection<IfpNode>, IList<IfpNode>, IEnumerable<IfpNode>
    {
        /// <summary>
        /// Gets and returns the number of elements in this instance.
        /// </summary>
        public int Count
        {
            get { return nodes.Count; }
        }
        /// <summary>
        /// Gets and returns false.
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }
        /// <summary>
        /// Gets or sets an <see cref="IfpNode"/> at a given index.
        /// </summary>
        /// <param name="index">The index to get or set a node.</param>
        /// <returns>A node at the given index.</returns>
        public IfpNode this[int index]
        {
            get { return nodes[index]; }
            set { nodes[index] = value; }
        }

        private readonly List<IfpNode> nodes;

        /// <summary>
        /// Initializes a new <see cref="IfpNodeCollection"/> instance.
        /// </summary>
        public IfpNodeCollection()
        {
            //Initialize
            nodes = new List<IfpNode>();
        }
        /// <summary>
        /// Adds an <see cref="IfpNode"/> element to the current instance.
        /// </summary>
        /// <param name="node">The node to add.</param>
        public void Add(IfpNode node)
        {
            nodes.Add(node);
        }
        /// <summary>
        /// Clears the collection of nodes.
        /// </summary>
        public void Clear()
        {
            nodes.Clear();
        }
        /// <summary>
        /// Returns whether a specified node exists within the instance.
        /// </summary>
        /// <param name="node">The node to check for.</param>
        /// <returns>True if the node exists within the collection, false if not.</returns>
        public bool Contains(IfpNode node)
        {
            return nodes.Contains(node);
        }
        /// <summary>
        /// Copies the entire collection to a compatible one-dimension array, starting at the specified index of the target array.
        /// </summary>
        /// <param name="array">The array to copy into.</param>
        /// <param name="arrayIndex">The index in the array to begin copying.</param>
        public void CopyTo(IfpNode[] array, int arrayIndex)
        {
            nodes.CopyTo(array, arrayIndex);
        }
        /// <summary>
        /// Returns an enumerator that iterates through the instance.
        /// </summary>
        /// <returns>An enumerator.</returns>
        public IEnumerator<IfpNode> GetEnumerator()
        {
            return ((ICollection<IfpNode>)nodes).GetEnumerator();
        }
        /// <summary>
        /// Searches for the specified node and returns the zero-based index of the first occurance within the collection.
        /// </summary>
        /// <param name="node">The node to search for.</param>
        /// <returns>-1 if the nodes is not found, otherwise returns the first index found.</returns>
        public int IndexOf(IfpNode node)
        {
            return nodes.IndexOf(node);
        }
        /// <summary>
        /// Inserts a node into the collection at a given index.
        /// </summary>
        /// <param name="index">The zero-based index to insert at.</param>
        /// <param name="node">The node to insert.</param>
        public void Insert(int index, IfpNode node)
        {
            nodes.Insert(index, node);
        }
        /// <summary>
        /// Removes the first occurance of a specific node from the collection.
        /// </summary>
        /// <param name="node">The node to remove.</param>
        /// <returns>True if a node is removed, else returns false.</returns>
        public bool Remove(IfpNode node)
        {
            return nodes.Remove(node);
        }
        /// <summary>
        /// Removes the node at a given zero-based index of the collection.
        /// </summary>
        /// <param name="index">The index to remove the node at.</param>
        public void RemoveAt(int index)
        {
            nodes.RemoveAt(index);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((ICollection<IfpNode>)nodes).GetEnumerator();
        }
    }

    /// <summary>
    /// Represents an enumerator that contains the IronForgePlugin node types for an IronForgePlugin node.
    /// </summary>
    public enum IfpNodeType : byte
    {
        /// <summary>
        /// Represents an unspecified node type.
        /// </summary>
        Unspecified,
        /// <summary>
        /// Represents an option node type.
        /// </summary>
        Option,
        /// <summary>
        /// Represents a 64-bit tag block pointer.
        /// </summary>
        TagBlock,
        /// <summary>
        /// Represents a data buffer.
        /// </summary>
        Buffer,
        /// <summary>
        /// Represents an unsigned 8-bit integer.
        /// </summary>
        Byte,
        /// <summary>
        /// Represents a signed 8-bit integer.
        /// </summary>
        SignedByte,
        /// <summary>
        /// Represents a signed 16-bit integer.
        /// </summary>
        Short,
        /// <summary>
        /// Represents an unsigned 16-bit integer.
        /// </summary>
        UnsignedShort,
        /// <summary>
        /// Represents a signed 32-bit integer.
        /// </summary>
        Int,
        /// <summary>
        /// Represents an unsigned 32-bit integer.
        /// </summary>
        UnsignedInt,
        /// <summary>
        /// Represents a signed 64-bit integer.
        /// </summary>
        Long,
        /// <summary>
        /// Represents an unsigned 64-bit integer.
        /// </summary>
        UnsignedLong,
        /// <summary>
        /// Represents a single precision floating point number.
        /// </summary>
        Single,
        /// <summary>
        /// Represents a double precision floating point number.
        /// </summary>
        Double,
        /// <summary>
        /// Represents an 8-bit enumeration value.
        /// </summary>
        Enumerator8,
        /// <summary>
        /// Represents a 16-bit enumeration value.
        /// </summary>
        Enumerator16,
        /// <summary>
        /// Represents a 32-bit enumeration value.
        /// </summary>
        Enumerator32,
        /// <summary>
        /// Represents a 64-bit enumeration value.
        /// </summary>
        Enumerator64,
        /// <summary>
        /// Represents an 8-bit bit field value.
        /// </summary>
        Bitfield8,
        /// <summary>
        /// Represents a 16-bit bit field value.
        /// </summary>
        Bitfield16,
        /// <summary>
        /// Represents a 32-bit bit field value.
        /// </summary>
        Bitfield32,
        /// <summary>
        /// Represents a 64-bit bit field value.
        /// </summary>
        Bitfield64,
        /// <summary>
        /// Represents a 32-byte lengthed string value.
        /// </summary>
        String32,
        /// <summary>
        /// Represents a 64-byte lengthed string value.
        /// </summary>
        String64,
        /// <summary>
        /// Represents a 128-byte lengthed unicode string value.
        /// </summary>
        Unicode128,
        /// <summary>
        /// Represents a 256-byte lengthed unicode string value.
        /// </summary>
        Unicode256,
        /// <summary>
        /// Represents a 32-bit Tag string value.
        /// </summary>
        Tag,
        /// <summary>
        /// Represents a 32-bit Tag ID value.
        /// </summary>
        TagId,
        /// <summary>
        /// Represents a 32-bit String ID value.
        /// </summary>
        StringId
    }
}
