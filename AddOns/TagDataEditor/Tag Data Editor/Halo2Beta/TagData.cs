using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2BetaMap;
using Abide.HaloLibrary.IO;
using Abide.Ifp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Tag_Data_Editor.Halo2Beta
{
    /// <summary>
    /// Represents a tag data wrapper.
    /// </summary>
    public class DataWrapper : IEnumerable<DataObject>
    {
        public Tag Root
        {
            get { return root; }
        }

        private Tag root = "null";
        private readonly List<DataObject> objects = new List<DataObject>();
        private readonly Dictionary<int, DataObject> objectLookup = new Dictionary<int, DataObject>();

        /// <summary>
        /// Gets all valid data objects within the wrapper.
        /// </summary>
        /// <returns>An array of data objects.</returns>
        public DataObject[] GetAllObjects()
        {
            //Prepare
            List<DataObject> dataObjects = new List<DataObject>();

            //Loop
            foreach (DataObject childObject in objects)
                if (childObject.IsValid)
                {
                    dataObjects.Add(childObject);
                    dataObjects.AddRange(GetObjects(childObject));
                }

            //Return
            return dataObjects.ToArray();
        }
        /// <summary>
        /// Gets all valid data objects within the given data object.
        /// </summary>
        /// <param name="dataObject">The data object to iterate.</param>
        /// <returns>An array of data objects.</returns>
        private DataObject[] GetObjects(DataObject dataObject)
        {
            //Prepare
            List<DataObject> dataObjects = new List<DataObject>();

            //Loop
            foreach (DataObject childObject in dataObject)
                if (childObject.IsValid)
                {
                    dataObjects.Add(childObject);
                    dataObjects.AddRange(GetObjects(childObject));
                }

            //Return
            return dataObjects.ToArray();
        }
        /// <summary>
        /// Gets a data object by its unique ID.
        /// </summary>
        /// <param name="uniqueId">The unique ID of the object.</param>
        /// <returns>A data object with the specified unique ID or null.</returns>
        public DataObject ObjectFromUniqueId(int uniqueId)
        {
            if (objectLookup.ContainsKey(uniqueId)) return objectLookup[uniqueId];
            return null;
        }
        /// <summary>
        /// Creates a map of data objects.
        /// </summary>
        /// <param name="document">The IFP document.</param>
        /// <param name="entry">The index entry.</param>
        public void Layout(IfpDocument document, IndexEntry entry)
        {
            //Clear
            objects.Clear();
            objectLookup.Clear();

            //Set
            root = document.Plugin.Class;

            //Loop
            int index = 0;
            foreach (IfpNode node in document.Plugin.Nodes)
            {
                //Prepare
                DataObject childObject = null;

                //Handle type
                switch (node.Type)
                {
                    case IfpNodeType.TagBlock:
                    case IfpNodeType.Byte:
                    case IfpNodeType.SignedByte:
                    case IfpNodeType.Short:
                    case IfpNodeType.UnsignedShort:
                    case IfpNodeType.Int:
                    case IfpNodeType.UnsignedInt:
                    case IfpNodeType.Long:
                    case IfpNodeType.UnsignedLong:
                    case IfpNodeType.Single:
                    case IfpNodeType.Double:
                    case IfpNodeType.Enumerator8:
                    case IfpNodeType.Enumerator16:
                    case IfpNodeType.Enumerator32:
                    case IfpNodeType.Enumerator64:
                    case IfpNodeType.Bitfield8:
                    case IfpNodeType.Bitfield16:
                    case IfpNodeType.Bitfield32:
                    case IfpNodeType.Bitfield64:
                    case IfpNodeType.String32:
                    case IfpNodeType.String64:
                    case IfpNodeType.Unicode128:
                    case IfpNodeType.Unicode256:
                    case IfpNodeType.Tag:
                    case IfpNodeType.TagId:
                    case IfpNodeType.StringId:
                        childObject = new DataObject(node, entry.TagData) { UniqueId = index++ };
                        break;
                }

                //Check
                if (childObject != null)
                {
                    //Add
                    objects.Add(childObject);
                    objectLookup.Add(childObject.UniqueId, childObject);

                    //Layout Child
                    if (childObject.Node.Type == IfpNodeType.TagBlock)
                        Layout(childObject, ref index);
                }
            }
        }
        /// <summary>
        /// Creates a map of a child data object.
        /// </summary>
        /// <param name="dataObject">A tag data object.</param>
        private void Layout(DataObject dataObject, ref int index)
        {
            //Loop
            foreach (IfpNode node in dataObject.Node.Nodes)
            {
                //Prepare
                DataObject childObject = null;

                //Handle type
                switch (node.Type)
                {
                    case IfpNodeType.TagBlock:
                    case IfpNodeType.Byte:
                    case IfpNodeType.SignedByte:
                    case IfpNodeType.Short:
                    case IfpNodeType.UnsignedShort:
                    case IfpNodeType.Int:
                    case IfpNodeType.UnsignedInt:
                    case IfpNodeType.Long:
                    case IfpNodeType.UnsignedLong:
                    case IfpNodeType.Single:
                    case IfpNodeType.Double:
                    case IfpNodeType.Enumerator8:
                    case IfpNodeType.Enumerator16:
                    case IfpNodeType.Enumerator32:
                    case IfpNodeType.Enumerator64:
                    case IfpNodeType.Bitfield8:
                    case IfpNodeType.Bitfield16:
                    case IfpNodeType.Bitfield32:
                    case IfpNodeType.Bitfield64:
                    case IfpNodeType.String32:
                    case IfpNodeType.String64:
                    case IfpNodeType.Unicode128:
                    case IfpNodeType.Unicode256:
                    case IfpNodeType.Tag:
                    case IfpNodeType.TagId:
                    case IfpNodeType.StringId:
                        childObject = new DataObject(dataObject, node, dataObject.DataStream) { UniqueId = index++ };
                        break;
                }

                //Check
                if (childObject != null)
                {
                    //Add
                    objectLookup.Add(childObject.UniqueId, childObject);

                    //Layout Child
                    Layout(childObject, ref index);
                }
            }
        }
        /// <summary>
        /// Wraps all objects using the supplied offset.
        /// </summary>
        /// <param name="offset">The base offset.</param>
        public void Wrap(uint offset)
        {
            //Loop
            objects.ForEach(o => Wrap(o, offset));
        }
        /// <summary>
        /// Wraps a data object using the supplied offset.
        /// </summary>
        /// <param name="dataObject">The base offset.</param>
        public void Wrap(DataObject dataObject, uint offset)
        {
            //Check
            if (offset == 0) { dataObject.Reset(); return; }

            //Set address
            dataObject.BaseAddress = offset;
            dataObject.Address = (uint)(offset + dataObject.Node.FieldOffset);
            dataObject.ReadValue();

            //Check
            if (dataObject.IsTagBlock)
            {
                //Get Block
                TagBlock block = (TagBlock)dataObject.Value;
                uint childOffset = 0;

                //Get Block Offset
                int blockOffset = dataObject.Node.Length * dataObject.SelectedIndex;
                if (block.Offset > 0) childOffset = (uint)(block.Offset + blockOffset);

                //Loop
                foreach (DataObject childObject in dataObject)
                {
                    Wrap(childObject, childOffset);
                    if (childObject.SelectedIndex >= block.Count) childObject.SelectedIndex = 0;
                }
            }
        }

        public IEnumerator<DataObject> GetEnumerator()
        {
            return objects.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return objects.GetEnumerator();
        }
    }

    /// <summary>
    /// Represents a data object within a tag.
    /// </summary>
    public class DataObject : IEnumerable<DataObject>
    {
        /// <summary>
        /// Gets or sets this object's unique id.
        /// </summary>
        public int UniqueId
        {
            get { return uid; }
            set { uid = value; }
        }
        /// <summary>
        /// Gets or sets the block's selected index.
        /// </summary>
        public int SelectedIndex
        {
            get { return (int)selectedIndex; }
            set { selectedIndex = (uint)value; }
        }
        /// <summary>
        /// Gets and returns the value of this object.
        /// </summary>
        public object Value
        {
            get { return value; }
            set { this.value = value; }
        }
        /// <summary>
        /// Gets and returns the number of child objects in this instance.
        /// </summary>
        public int ChildCount
        {
            get { return children.Count; }
        }
        /// <summary>
        /// Gets and returns a child object at a given index.
        /// </summary>
        /// <param name="index">The zero-based index of the child object.</param>
        /// <returns>A data object instance.</returns>
        public DataObject this[int index]
        {
            get { if (index < 0 || index > children.Count) throw new ArgumentOutOfRangeException(nameof(index)); return children[index]; }
        }
        /// <summary>
        /// Gets and returns this object's parent
        /// </summary>
        public DataObject Parent
        {
            get { return parent; }
        }
        /// <summary>
        /// Gets and returns the tag's data stream.
        /// </summary>
        public FixedMemoryMappedStream DataStream
        {
            get { return dataStream; }
        }
        /// <summary>
        /// Gets and returns the IFP node associated with this data object.
        /// </summary>
        public IfpNode Node
        {
            get { return node; }
        }
        /// <summary>
        /// Gets or sets the object's address.
        /// </summary>
        public uint Address
        {
            get { return address; }
            set { address = value; }
        }
        /// <summary>
        /// Gets or sets the object's base address.
        /// </summary>
        public uint BaseAddress
        {
            get { return baseAddress; }
            set { baseAddress = value; }
        }
        /// <summary>
        /// Gets and returns true if the address of the object is valid; otherwise false.
        /// </summary>
        public bool IsValid
        {
            get { return address >= dataStream.MemoryAddress && baseAddress >= dataStream.MemoryAddress; }
        }
        /// <summary>
        /// Gets and returns true if the object representative of a tag block; otherwise false.
        /// </summary>
        public bool IsTagBlock
        {
            get
            {
                if (node == null) return false;
                return node.Type == IfpNodeType.TagBlock;
            }
        }
        /// <summary>
        /// Gets and returns true if the object is a block index value.
        /// </summary>
        public bool IsBlockIndex
        {
            get { return !string.IsNullOrEmpty(node.Layer) && node.TagBlockOffset >= 0 && node.TagBlockSize >= 0; }
        }

        private int uid = -1;
        private object value = null;
        private readonly List<DataObject> children = new List<DataObject>();
        private readonly DataObject parent;
        private readonly FixedMemoryMappedStream dataStream;
        private readonly IfpNode node;
        private uint selectedIndex = 0;
        private uint baseAddress = 0;
        private uint address = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataObject"/> class using the supplied parent object, ifp node and data stream.
        /// </summary>
        /// <param name="parent">The object's parent.</param>
        /// <param name="node">The object's ifp node.</param>
        /// <param name="dataStream">The tag's data stream.</param>
        public DataObject(DataObject parent, IfpNode node, FixedMemoryMappedStream dataStream) : this(node, dataStream)
        {

            //Set
            this.parent = parent ?? throw new ArgumentNullException(nameof(parent));
            parent.children.Add(this);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DataObject"/> class using the supplied ifp node and data stream.
        /// </summary>
        /// <param name="node">The object's ifp node.</param>
        /// <param name="dataStream">The tag's data stream.</param>
        public DataObject(IfpNode node, FixedMemoryMappedStream dataStream)
        {
            //Set
            this.node = node ?? throw new ArgumentNullException(nameof(node));
            this.dataStream = dataStream ?? throw new ArgumentNullException(nameof(dataStream));
        }
        /// <summary>
        /// Gets and returns the display name(s) of the data object using the supplied map file.
        /// </summary>
        /// <returns>An array of string elements.</returns>
        /// <param name="map">The map file.</param>
        public string[] GetDisplayNames(MapFile map)
        {
            //Prepare
            List<string> names = new List<string>();
            string name = node.Name;

            //Check
            if (IsTagBlock && value is TagBlock tagBlock)
            {
                //Check Label
                if (!string.IsNullOrEmpty(node.Label))
                {
                    //Check Count
                    if (tagBlock.Count > 0)
                    {
                        //Get Node
                        IfpNode labelNode = node.Nodes[node.Label];

                        //Loop
                        if (labelNode != null)
                            using (BinaryReader reader = new BinaryReader(dataStream))
                                for (int i = 0; i < tagBlock.Count; i++)
                                {
                                    //Set name...
                                    name = GetNodeDisplayName(labelNode.Type, dataStream, tagBlock.Offset + (uint)(i * node.Length), map);

                                    //Check
                                    if (!string.IsNullOrEmpty(name))
                                        names.Add(name);
                                    else names.Add(node.Name);
                                }
                    }
                }
                else
                {
                    //Add names...
                    for (int i = 0; i < tagBlock.Count; i++)
                        names.Add(name);
                }
            }

            //Add element name once if no names are retrieved
            if (names.Count == 0) names.Add(name);

            //Return
            return names.ToArray();
        }
        /// <summary>
        /// Reads and returns the value of this data object.
        /// </summary>
        /// <returns>The value of the data object.</returns>
        public object ReadValue()
        {
            //Check
            if (!IsValid) return null;

            //Goto
            dataStream.Seek(address, SeekOrigin.Begin);
            using (BinaryReader reader = new BinaryReader(dataStream))
                switch (node.Type)
                {
                    case IfpNodeType.Single: value = reader.ReadSingle(); break;
                    case IfpNodeType.Double: value = reader.ReadDouble(); break;

                    case IfpNodeType.SignedByte: value = reader.ReadSByte(); break;
                    case IfpNodeType.Byte:
                    case IfpNodeType.Enumerator8:
                    case IfpNodeType.Bitfield8: value = reader.ReadByte(); break;

                    case IfpNodeType.Short: value = reader.ReadInt16(); break;
                    case IfpNodeType.UnsignedShort:
                    case IfpNodeType.Enumerator16:
                    case IfpNodeType.Bitfield16: value = reader.ReadUInt16(); break;

                    case IfpNodeType.Int: value = reader.ReadInt32(); break;
                    case IfpNodeType.UnsignedInt:
                    case IfpNodeType.Enumerator32:
                    case IfpNodeType.Bitfield32: value = reader.ReadUInt32(); break;

                    case IfpNodeType.Long: reader.ReadInt64(); break;
                    case IfpNodeType.UnsignedLong:
                    case IfpNodeType.Enumerator64:
                    case IfpNodeType.Bitfield64: value = reader.ReadUInt64(); break;

                    case IfpNodeType.String32: value = reader.ReadUTF8(32).Trim('\0'); break;
                    case IfpNodeType.String64: value = reader.ReadUTF8(64).Trim('\0'); break;
                    case IfpNodeType.Unicode128: value = reader.ReadUTF8(128).Trim('\0'); break;
                    case IfpNodeType.Unicode256: value = reader.ReadUTF8(256).Trim('\0'); break;

                    case IfpNodeType.Tag: value = reader.Read<Tag>(); break;
                    case IfpNodeType.TagId: value = reader.Read<TagId>(); break;
                    case IfpNodeType.StringId: value = reader.Read<StringId>(); break;
                    case IfpNodeType.TagBlock: value = reader.Read<TagBlock>(); break;
                }

            //Return
            return value;
        }
        /// <summary>
        /// Writes the value to the tag data.
        /// </summary>
        /// <returns>true if the value is successfully written; otherwise false.</returns>
        public bool WriteValue()
        {
            //Prepare
            bool success = false;

            //Goto
            dataStream.Seek(address, SeekOrigin.Begin);
            using (BinaryWriter writer = new BinaryWriter(dataStream))
                try
                {
                    switch (node.Type)
                    {
                        case IfpNodeType.Single: writer.Write((float)value); break;
                        case IfpNodeType.Double: writer.Write((double)value); break;

                        case IfpNodeType.SignedByte: writer.Write((sbyte)value); break;
                        case IfpNodeType.Byte:
                        case IfpNodeType.Enumerator8:
                        case IfpNodeType.Bitfield8: writer.Write((byte)value); break;

                        case IfpNodeType.Short: writer.Write((short)value); break;
                        case IfpNodeType.UnsignedShort:
                        case IfpNodeType.Enumerator16:
                        case IfpNodeType.Bitfield16: writer.Write((ushort)value); break;

                        case IfpNodeType.Int: writer.Write((int)value); break;
                        case IfpNodeType.UnsignedInt:
                        case IfpNodeType.Enumerator32:
                        case IfpNodeType.Bitfield32: writer.Write((uint)value); break;

                        case IfpNodeType.Long: writer.Write((long)value); break;
                        case IfpNodeType.UnsignedLong:
                        case IfpNodeType.Enumerator64:
                        case IfpNodeType.Bitfield64: writer.Write((ulong)value); break;

                        case IfpNodeType.String32: writer.WriteUTF8(((string)value).PadRight(32, '\0').Substring(0, 32)); break;
                        case IfpNodeType.String64: writer.WriteUTF8(((string)value).PadRight(64, '\0').Substring(0, 32)); break;
                        case IfpNodeType.Unicode128: writer.WriteUTF8(((string)value).PadRight(128, '\0').Substring(0, 128)); break;
                        case IfpNodeType.Unicode256: writer.WriteUTF8(((string)value).PadRight(256, '\0').Substring(0, 256)); break;

                        case IfpNodeType.Tag: writer.Write((Tag)value); break;
                        case IfpNodeType.TagId: writer.Write((TagId)value); break;
                        case IfpNodeType.StringId: writer.Write((StringId)value); break;
                    }

                    //Set
                    success = true;
                }
                catch (InvalidCastException) { }

            //Return
            return success;
        }
        /// <summary>
        /// Resets a data object and all of it's children.
        /// </summary>
        public void Reset()
        {
            //Reset
            address = 0;
            baseAddress = 0;
            selectedIndex = 0;
            value = null;

            //Loop
            children.ForEach(c => c.Reset());
        }
        /// <summary>
        /// Returns the ID of this element in the HTML document.
        /// </summary>
        /// <returns>An ID string.</returns>
        public string GetHtmlId()
        {
            //Check
            if (IsBlockIndex)
                switch (node.Type)
                {
                    case IfpNodeType.Byte:
                    case IfpNodeType.SignedByte:
                    case IfpNodeType.Short:
                    case IfpNodeType.UnsignedShort:
                    case IfpNodeType.Int:
                    case IfpNodeType.UnsignedInt:
                    case IfpNodeType.Long:
                    case IfpNodeType.UnsignedLong:
                        return $"valueSelect{uid}";
                }

            //Handle
            switch (node.Type)
            {
                case IfpNodeType.Byte:
                case IfpNodeType.SignedByte:
                case IfpNodeType.Short:
                case IfpNodeType.UnsignedShort:
                case IfpNodeType.Int:
                case IfpNodeType.UnsignedInt:
                case IfpNodeType.Long:
                case IfpNodeType.UnsignedLong:
                case IfpNodeType.Single:
                case IfpNodeType.Double: return $"value{uid}";

                case IfpNodeType.Enumerator8:
                case IfpNodeType.Enumerator16:
                case IfpNodeType.Enumerator32:
                case IfpNodeType.Enumerator64: return $"enumSelect{uid}";

                case IfpNodeType.Bitfield8:
                case IfpNodeType.Bitfield16:
                case IfpNodeType.Bitfield32:
                case IfpNodeType.Bitfield64: return $"bitmask{uid}";

                case IfpNodeType.String32:
                case IfpNodeType.String64: return $"string{uid}";

                case IfpNodeType.Unicode128:
                case IfpNodeType.Unicode256: return $"unicode{uid}";

                case IfpNodeType.TagId: return $"tag{uid}";
                case IfpNodeType.StringId: return $"stringId{uid}";
                default: return string.Empty;
            }
        }
        /// <summary>
        /// Returns an enumerator that iterates through the object's children.
        /// </summary>
        /// <returns>An enumerator</returns>
        public IEnumerator<DataObject> GetEnumerator()
        {
            return children.GetEnumerator();
        }
        /// <summary>
        /// Returns a string representation of this object.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"{node.Name} = {value.ToString()}";
        }

        /// <summary>
        /// Gets and returns a display string from a specified node type, stream, address, and map file.
        /// </summary>
        /// <param name="itemType">The item type.</param>
        /// <param name="dataStream">The data stream.</param>
        /// <param name="address">The address of the data.</param>
        /// <param name="map">The map file.</param>
        /// <returns>A string.</returns>
        private static string GetNodeDisplayName(IfpNodeType itemType, Stream dataStream, long address, MapFile map)
        {
            //Goto
            dataStream.Seek(address, SeekOrigin.Begin);
            using (BinaryReader reader = new BinaryReader(dataStream))
                try
                {
                    switch (itemType)
                    {
                        case IfpNodeType.Byte: return reader.ReadByte().ToString();
                        case IfpNodeType.SignedByte: return reader.ReadSByte().ToString();
                        case IfpNodeType.Short: return reader.ReadInt16().ToString();
                        case IfpNodeType.UnsignedShort: return reader.ReadUInt16().ToString();
                        case IfpNodeType.Int: return reader.ReadInt32().ToString();
                        case IfpNodeType.UnsignedInt: return reader.ReadUInt32().ToString();
                        case IfpNodeType.Long: return reader.ReadInt64().ToString();
                        case IfpNodeType.UnsignedLong: return reader.ReadUInt64().ToString();
                        case IfpNodeType.Single: return reader.ReadSingle().ToString();
                        case IfpNodeType.Double: return reader.ReadDouble().ToString();
                        case IfpNodeType.String32: return reader.ReadUTF8(32).Trim('\0');
                        case IfpNodeType.String64: return reader.ReadUTF8(64).Trim('\0');
                        case IfpNodeType.Unicode128: return reader.ReadUTF8(128).Trim('\0');
                        case IfpNodeType.Unicode256: return reader.ReadUTF8(256).Trim('\0');
                        case IfpNodeType.TagId:
                            IndexEntry entry = map.IndexEntries[reader.Read<TagId>()];
                            if (entry == null) return "Null";
                            else return $"{entry.Filename}.{entry.Root}";
                        case IfpNodeType.StringId: return map.Strings[reader.Read<StringId>().Index];
                    }
                }
                catch { }
            return string.Empty;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return children.GetEnumerator();
        }
    }
}
