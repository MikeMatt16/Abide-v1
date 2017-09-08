using Abide.AddOnApi.Halo2;
using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using Abide.HaloLibrary.IO;
using Abide.Ifp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Tag_Data_Editor.Halo2
{
    public partial class TagEditor2 : AbideTool
    {
        private readonly TagDocumentFormatter formatter;
        private readonly DataWrapper wrapper;

        public TagEditor2()
        {
            wrapper = new DataWrapper();
            formatter = new TagDocumentFormatter();
            InitializeComponent();

            //Set document
            tagDataWebBrowser.DocumentText = formatter.GetHtml();
        }

        private void TagEditor2_SelectedEntryChanged(object sender, EventArgs e)
        {
            //Prepare
            IfpDocument document = null;
            string ifpFile = null;
            
            //Check
            if (plugin_GetPath(SelectedEntry.Root, out ifpFile))
            {
                //Load
                try { document = new IfpDocument(); document.Load(ifpFile); }
                catch { }
            }

            //Load
            wrapper.Wrap(document, SelectedEntry);
        }
        
        private void structureView_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
        
        private bool plugin_GetPath(Tag root, out string path)
        {
            //Set path
            path = Path.Combine(AbideRegistry.Halo2PluginsDirectory, $"{root.FourCc.Replace('<', '_').Replace('>', '_')}.ent");

            //Return
            return File.Exists(path);
        }
        
        private void dataObjects_MakeNodes(IEnumerable<DataObject> objects, TreeNodeCollection nodes)
        {
            //Loop
            foreach (var dataObject in objects)
                if (dataObject.Node.Type == IfpNodeType.TagBlock)
                {
                    //Create Node
                    TagDataNode node = new TagDataNode(dataObject);
                    if (node.BlockCount > 0)    //Check
                    {
                        //Add
                        nodes.Add(node);
                    }
                }
        }

        /// <summary>
        /// Represents a tag data tree node.
        /// </summary>
        private class TagDataNode : TreeNode
        {
            /// <summary>
            /// Gets or sets the block's selected index.
            /// </summary>
            public int SelectedIndex
            {
                get { return selectedIndex; }
                set { selectedIndex = value; }
            }
            /// <summary>
            /// Gets and returns the associated data object.
            /// </summary>
            public DataObject DataObject
            {
                get { return dataObject; }
            }
            /// <summary>
            /// Gets and returns the tag block offset.
            /// </summary>
            public uint BlockOffset
            {
                get { return block.Offset; }
            }
            /// <summary>
            /// Gets and returns the tag block count.
            /// </summary>
            public uint BlockCount
            {
                get { return block.Count; }
            }
            
            private TagBlock block;
            private readonly DataObject dataObject;
            private int selectedIndex = -1;

            /// <summary>
            /// Initializes a new instance of the <see cref="TagDataNode"/> class using the supplied data object.
            /// </summary>
            /// <param name="dataObject">The data object.</param>
            public TagDataNode(DataObject dataObject)
            {
                //Check
                if (dataObject == null) throw new ArgumentNullException(nameof(dataObject));
                this.dataObject = dataObject;

                //Set Text
                Update();
            }
            /// <summary>
            /// Performs an update on the node.
            /// This updates the tag block, information, and label.
            /// </summary>
            public void Update()
            {
                //Prepare
                block = TagBlock.Zero;

                //Goto
                dataObject.DataStream.Seek(dataObject.Address, SeekOrigin.Begin);

                //Initialize reader
                using (BinaryReader reader = new BinaryReader(dataObject.DataStream))
                    block = reader.ReadUInt64();

                //Setup
                Text = $"{dataObject.Node.Name} [{selectedIndex + 1}/{block.Count}]";
                if (block.Count == 0) ForeColor = Color.Red;
                else ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Represents a tag data wrapper.
        /// </summary>
        private class DataWrapper : IEnumerable<DataObject>
        {
            private readonly List<DataObject> objects = new List<DataObject>();

            /// <summary>
            /// Wraps an index entry using an IFP document.
            /// </summary>
            /// <param name="document">The IFP document.</param>
            /// <param name="entry">The index entry.</param>
            public void Wrap(IfpDocument document, IndexEntry entry)
            {
                //Clear
                objects.Clear();

                //Loop
                foreach (IfpNode node in document.Plugin.Nodes)
                {
                    //Prepare
                    DataObject dataObject = null;

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
                            dataObject = new DataObject(node, entry.TagData);
                            dataObject.Address = (uint)(entry.PostProcessedOffset + node.FieldOffset);
                            dataObject.ReadValue();
                            break;
                    }

                    //Check
                    if (dataObject != null && node.Type == IfpNodeType.TagBlock)
                    {
                        //Get TagBlock
                        TagBlock block = (TagBlock)dataObject.Value;
                        if (block.Count > 0) Wrap(dataObject, block.Offset, entry);
                        objects.Add(dataObject);
                    }
                }
            }
            /// <summary>
            /// Wraps a child data object using an IFP node.
            /// </summary>
            /// <param name="parentObject">The parent object.</param>
            /// <param name="entry">The index entry.</param>
            private void Wrap(DataObject parentObject, uint offset, IndexEntry entry)
            {
                //Loop
                foreach (IfpNode node in parentObject.Node.Nodes)
                {
                    //Prepare
                    DataObject dataObject = null;

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
                            dataObject = new DataObject(parentObject, node, entry.TagData);
                            dataObject.Address = (uint)(offset + node.FieldOffset);
                            dataObject.ReadValue();
                            break;
                    }

                    //Check
                    if (dataObject != null && node.Type == IfpNodeType.TagBlock)
                    {
                        //Get TagBlock
                        TagBlock block = (TagBlock)dataObject.Value;
                        if (block.Count > 0) Wrap(dataObject, block.Offset, entry);
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
        private class DataObject : IEnumerable<DataObject>
        {
            /// <summary>
            /// Gets and returns the value of this object.
            /// </summary>
            public object Value
            {
                get { return value; }
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
            public Stream DataStream
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
            /// Gets and returns the validity of the object's current address.
            /// </summary>
            public bool IsValid
            {
                get { return address >= dataStream.MemoryAddress; }
            }

            private object value = null;
            private readonly List<DataObject> children = new List<DataObject>();
            private readonly DataObject parent;
            private readonly FixedMemoryMappedStream dataStream;
            private readonly IfpNode node;
            private uint address = 0;

            /// <summary>
            /// Initializes a new instance of the <see cref="DataObject"/> class using the supplied parent object, ifp node and data stream.
            /// </summary>
            /// <param name="parent">The object's parent.</param>
            /// <param name="node">The object's ifp node.</param>
            /// <param name="dataStream">The tag's data stream.</param>
            public DataObject(DataObject parent, IfpNode node, FixedMemoryMappedStream dataStream) : this(node, dataStream)
            {
                //Check
                if (parent == null) throw new ArgumentNullException(nameof(parent));

                //Set
                this.parent = parent;
                parent.children.Add(this);
            }
            /// <summary>
            /// Initializes a new instance of the <see cref="DataObject"/> class using the supplied ifp node and data stream.
            /// </summary>
            /// <param name="node">The object's ifp node.</param>
            /// <param name="dataStream">The tag's data stream.</param>
            public DataObject(IfpNode node, FixedMemoryMappedStream dataStream)
            {
                //Check
                if (node == null) throw new ArgumentNullException(nameof(node));
                if (dataStream == null) throw new ArgumentNullException(nameof(dataStream));

                //Set
                this.node = node;
                this.dataStream = dataStream;
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
                        case IfpNodeType.SignedByte:
                        case IfpNodeType.Byte:
                        case IfpNodeType.Enumerator8:
                        case IfpNodeType.Bitfield8: value = reader.ReadByte(); break;
                            
                        case IfpNodeType.Short:
                        case IfpNodeType.UnsignedShort:
                        case IfpNodeType.Enumerator16:
                        case IfpNodeType.Bitfield16: value = reader.ReadUInt16(); break;

                        case IfpNodeType.Int:
                        case IfpNodeType.UnsignedInt:
                        case IfpNodeType.Enumerator32:
                        case IfpNodeType.Bitfield32: value = reader.ReadInt32(); break;

                        case IfpNodeType.Long:
                        case IfpNodeType.UnsignedLong:
                        case IfpNodeType.Enumerator64:
                        case IfpNodeType.Bitfield64: value = reader.ReadInt64(); break;

                        case IfpNodeType.Single: value = reader.ReadSingle(); break;
                        case IfpNodeType.Double: value = reader.ReadDouble(); break;

                        case IfpNodeType.String32: value = reader.ReadUTF8(32); break;
                        case IfpNodeType.String64: value = reader.ReadUTF8(64); break;
                        case IfpNodeType.Unicode128: value = reader.ReadUTF8(128); break;
                        case IfpNodeType.Unicode256: value = reader.ReadUTF8(256); break;

                        case IfpNodeType.Tag: value = reader.ReadStructure<Tag>(); break;
                        case IfpNodeType.TagId: value = reader.ReadStructure<TagId>(); break;
                        case IfpNodeType.StringId: value = reader.ReadStructure<StringId>(); break;
                        case IfpNodeType.TagBlock: value = reader.ReadStructure<TagBlock>(); break;
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
                            case IfpNodeType.SignedByte:
                            case IfpNodeType.Byte:
                            case IfpNodeType.Enumerator8:
                            case IfpNodeType.Bitfield8: writer.Write((byte)value); break;
                                
                            case IfpNodeType.Short:
                            case IfpNodeType.UnsignedShort:
                            case IfpNodeType.Enumerator16:
                            case IfpNodeType.Bitfield16: writer.Write((ushort)value); break;

                            case IfpNodeType.Int:
                            case IfpNodeType.UnsignedInt:
                            case IfpNodeType.Enumerator32:
                            case IfpNodeType.Bitfield32: writer.Write((uint)value); break;

                            case IfpNodeType.Long:
                            case IfpNodeType.UnsignedLong:
                            case IfpNodeType.Enumerator64:
                            case IfpNodeType.Bitfield64: writer.Write((ulong)value); break;

                            case IfpNodeType.Single: writer.Write((float)value); break;
                            case IfpNodeType.Double: writer.Write((double)value); break;

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
            IEnumerator IEnumerable.GetEnumerator()
            {
                return children.GetEnumerator();
            }
        }
    }
}
