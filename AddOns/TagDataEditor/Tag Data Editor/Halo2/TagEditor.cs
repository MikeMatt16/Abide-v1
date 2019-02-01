using Abide.AddOnApi;
using Abide.AddOnApi.Halo2;
using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using Abide.HaloLibrary.IO;
using Abide.Ifp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Tag_Data_Editor.Halo2
{
    [ComVisible(true), AddOn]
    public partial class TagEditor : AbideTool
    {
        private IEnumerable<DataObject> dataObjects;
        private readonly TagDocumentFormatter formatter;
        private readonly DataWrapper wrapper;

        public TagEditor()
        {
            wrapper = new DataWrapper();
            formatter = new TagDocumentFormatter();
            InitializeComponent();

            //Load Splitter Distance
            if(Properties.Settings.Default.SplitterDistance >= 0)
            {
                tagDataSplitter.SplitterDistance = Properties.Settings.Default.SplitterDistance;
            }

            //Setup browser
            tagDataWebBrowser.TagButtonClickCallback = tagButton_Click;
            tagDataWebBrowser.StringIdButtonClickCallback = stringIdButton_Click;
            tagDataWebBrowser.ValueSetCallback = value_Set;
            tagDataWebBrowser.EnumSetCallback = enum_Set;
            tagDataWebBrowser.BitmaskSetCallback = bitmask_Set;
            tagDataWebBrowser.StringSetCallback = string_Set;
            tagDataWebBrowser.UnicodeSetCallback = unicode_Set;
        }
        
        private void tagDataSplitter_SplitterMoved(object sender, SplitterEventArgs e)
        {
            //Set
            Properties.Settings.Default.SplitterDistance = tagDataSplitter.SplitterDistance;
            Properties.Settings.Default.Save();
        }

        private void TagEditor_Initialize(object sender, AddOnHostEventArgs e)
        {
            //Get Version
            string versionString = BrowserEmulation.ServiceVersion ?? BrowserEmulation.Version;
            if (!string.IsNullOrEmpty(versionString) && int.TryParse(versionString.Substring(0, versionString.IndexOf('.')), out int major))
            {
                //Check
                if (BrowserEmulation.Abide < major * 1000)
                    BrowserEmulation.Abide = major * 1000;
                else if (BrowserEmulation.Abide > major * 1000)
                    BrowserEmulation.Abide = major * 1000;

                //Check
                if (BrowserEmulation.TagDataEditor < major * 1000)
                    BrowserEmulation.TagDataEditor = major * 1000;
                else if (BrowserEmulation.TagDataEditor > major * 1000)
                    BrowserEmulation.TagDataEditor = major * 1000;

            }
        }

        private void TagEditor_XboxChanged(object sender, EventArgs e)
        {
            //Set
            xboxConnectionToolStripLabel.Text = Xbox.Connected ? Xbox.DebugName : "Not Connected";
            pokeToolStripButton.Enabled = Xbox.Connected;
        }

        private void TagEditor_SelectedEntryChanged(object sender, EventArgs e)
        {
            //Prepare
            IfpDocument document = null;

            //Check
            if (wrapper.Root != SelectedEntry.Root && plugin_GetPath(SelectedEntry.Root, out string ifpFile))
            {
                //Load
                try { document = new IfpDocument(); document.Load(ifpFile); }
                finally { wrapper.Layout(document, SelectedEntry); }
            }

            //Read
            wrapper.Wrap((uint)SelectedEntry.PostProcessedOffset);

            //Create Nodes
            structureView.BeginUpdate();

            //Clear
            structureView.Nodes.Clear();
            
            //Create Node(s)
            TreeNode mainNode = new TreeNode($"{SelectedEntry.Filename}.{SelectedEntry.Root}");
            dataObjects_MakeNodes(wrapper, mainNode.Nodes);
            mainNode.Tag = wrapper;

            //Add
            structureView.Nodes.Add(mainNode);
            mainNode.Expand();

            //Select
            structureView.SelectedNode = mainNode;
            mainNode.EnsureVisible();

            //End
            structureView.EndUpdate();
        }

        private void pokeToolStripButton_Click(object sender, EventArgs e)
        {
            //Loop
            foreach (DataObject dataObject in wrapper.GetAllObjects())
            {
                //Prepare
                byte[] buffer = null;

                //Check
                switch (dataObject.Node.Type)
                {
                    case IfpNodeType.SignedByte: buffer = BitConverter.GetBytes((sbyte)dataObject.Value); break;
                    case IfpNodeType.Int: buffer = BitConverter.GetBytes((int)dataObject.Value); break;
                    case IfpNodeType.Short: buffer = BitConverter.GetBytes((short)dataObject.Value); break;
                    case IfpNodeType.Long: buffer = BitConverter.GetBytes((long)dataObject.Value); break;
                    case IfpNodeType.Single: buffer = BitConverter.GetBytes((float)dataObject.Value); break;
                    case IfpNodeType.Double: buffer = BitConverter.GetBytes((double)dataObject.Value); break;

                    case IfpNodeType.Byte:
                    case IfpNodeType.Enumerator8:
                    case IfpNodeType.Bitfield8: buffer = BitConverter.GetBytes((byte)dataObject.Value); break;

                    case IfpNodeType.UnsignedShort:
                    case IfpNodeType.Enumerator16:
                    case IfpNodeType.Bitfield16: buffer = BitConverter.GetBytes((ushort)dataObject.Value); break;

                    case IfpNodeType.UnsignedInt:
                    case IfpNodeType.Enumerator32:
                    case IfpNodeType.Bitfield32: buffer = BitConverter.GetBytes((uint)dataObject.Value); break;

                    case IfpNodeType.UnsignedLong:
                    case IfpNodeType.Enumerator64:
                    case IfpNodeType.Bitfield64: buffer = BitConverter.GetBytes((ulong)dataObject.Value); break;

                    case IfpNodeType.TagId: buffer = BitConverter.GetBytes((uint)(TagId)dataObject.Value); break;
                    case IfpNodeType.StringId: buffer = BitConverter.GetBytes((uint)(StringId)dataObject.Value); break;

                    case IfpNodeType.String32: buffer = Encoding.UTF8.GetBytes(((string)dataObject.Value).PadRight(32, '\0').Substring(0, 32)); break;
                    case IfpNodeType.String64: buffer = Encoding.UTF8.GetBytes(((string)dataObject.Value).PadRight(64, '\0').Substring(0, 64)); break;
                    case IfpNodeType.Unicode128: buffer = Encoding.UTF8.GetBytes(((string)dataObject.Value).PadRight(128, '\0').Substring(0, 128)); break;
                    case IfpNodeType.Unicode256: buffer = Encoding.UTF8.GetBytes(((string)dataObject.Value).PadRight(256, '\0').Substring(0, 256)); break;
                }

                //Set Xbox memory
                if (buffer != null) //Xbox.SetMemory(dataObject.Address, buffer, buffer.Length);
                {
                    ////Test
                    //Xbox.SendCommand("getmem", new CommandArgument("addr", dataObject.Address), new CommandArgument("length", buffer.Length));
                    //Xbox.GetResponse(out Status getMemStatus, out string setMemMessage);
                    //if(getMemStatus == Status.MultilineResponseFollows)
                    //{
                    //    //Prepare
                    //    string line = string.Empty;
                    //
                    //    //Loop
                    //    do
                    //    {
                    //        //Receive
                    //        line = Xbox.ReceiveLine();
                    //    }
                    //    while (line != ".");
                    //}

                    ////Prepare
                    //StringBuilder memoryStringBuilder = new StringBuilder(buffer.Length * 2);
                    //foreach (byte b in buffer) memoryStringBuilder.AppendFormat("{0:X2}", b);

                    ////Stop
                    //Xbox.SendCommand("stop");
                    //Xbox.GetResponse();

                    ////Send getmem2 command
                    //Xbox.SendCommand("setmem", new CommandArgument("addr", $"0x{dataObject.Address:X8}"), memoryStringBuilder.ToString());
                    //Xbox.GetResponse(out Status status, out string message);
                    //bool success = status.HasFlag(Status.OK);

                    ////Go
                    //Xbox.SendCommand("go");
                    //Xbox.GetResponse();
                }
            }
        }

        private void structureView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Prepare
            tagBlockIndexToolStripComboBox.Visible = false;
            TagBlock block = TagBlock.Zero;
            TagDataNode dataNode = null;

            //Clear
            tagBlockIndexToolStripComboBox.Items.Clear();

            //Check
            if (e.Node is TagDataNode)
            {
                //Get Node
                dataNode = (TagDataNode)e.Node;

                //Check
                if (dataNode.DataObject.IsValid && dataNode.DataObject.Value != null)
                {
                    //Build Html
                    tagEditor_BuildHtml(dataNode.DataObject);

                    //Get block
                    block = (TagBlock)dataNode.DataObject.Value;

                    //Get Names
                    string[] names = dataNode.DataObject.GetDisplayNames(Map);

                    //Loop
                    for (int i = 0; i < block.Count; i++)
                        tagBlockIndexToolStripComboBox.Items.Add($"{i}: {names[i]}");

                    //Select
                    if (block.Count > dataNode.SelectedIndex) tagBlockIndexToolStripComboBox.SelectedIndex = dataNode.SelectedIndex;

                    //Set
                    tagBlockIndexToolStripComboBox.Visible = block.Count > 0;
                }
            }
            else
            {
                //Build Html
                tagEditor_BuildHtml(wrapper);

                //Set
                dataObjects = wrapper;

                //Add event listener, or just call set values.
                if (tagDataWebBrowser.ReadyState == WebBrowserReadyState.Loading || tagDataWebBrowser.IsBusy)
                    tagDataWebBrowser.DocumentCompleted += tagDataWebBrowser_DocumentCompleted;
                else tagEditor_SetValues(wrapper);
            }
        }

        private void tagDataWebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //Unhook
            tagDataWebBrowser.DocumentCompleted -= tagDataWebBrowser_DocumentCompleted;

            //Loop through bitmasks
            foreach (DataObject dataObject in dataObjects)
                foreach (IfpNode option in dataObject.Node.Nodes)
                    switch (dataObject.Node.Type)
                    {
                        case IfpNodeType.Bitfield8:
                            tagDataWebBrowser.Document.InvokeScript("nameBit", new object[] { dataObject.UniqueId, option.Value, option.Name });
                            tagDataWebBrowser.Document.InvokeScript("revealBit", new object[] { dataObject.GetHtmlId(), option.Value, 8 });
                            break;
                        case IfpNodeType.Bitfield16:
                            tagDataWebBrowser.Document.InvokeScript("nameBit", new object[] { dataObject.UniqueId, option.Value, option.Name });
                            tagDataWebBrowser.Document.InvokeScript("revealBit", new object[] { dataObject.GetHtmlId(), option.Value, 16 });
                            break;
                        case IfpNodeType.Bitfield32:
                            tagDataWebBrowser.Document.InvokeScript("nameBit", new object[] { dataObject.UniqueId, option.Value, option.Name });
                            tagDataWebBrowser.Document.InvokeScript("revealBit", new object[] { dataObject.GetHtmlId(), option.Value, 32 });
                            break;
                    }

            //Load
            tagEditor_SetValues(dataObjects);
        }

        private void tagBlockIndexToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Prepare
            TagDataNode dataNode = null;

            //Change
            if (structureView.SelectedNode is TagDataNode)
            {
                //Get Node
                dataNode = (TagDataNode)structureView.SelectedNode;

                //Check
                if (dataNode.DataObject.IsValid && dataNode.DataObject.Value != null)
                    dataNode.SelectedIndex = tagBlockIndexToolStripComboBox.SelectedIndex;
            }

            //Load
            if (dataNode != null)
            {
                //Set...
                dataObjects = dataNode.DataObject;

                //Add event listener, or just call set values.
                if (tagDataWebBrowser.ReadyState == WebBrowserReadyState.Loading || tagDataWebBrowser.IsBusy)
                    tagDataWebBrowser.DocumentCompleted += tagDataWebBrowser_DocumentCompleted;
                else tagEditor_SetValues(dataNode.DataObject);
            }
        }

        private bool plugin_GetPath(TagFourCc root, out string path)
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
                    //Create Node(s)
                    TagDataNode node = new TagDataNode(dataObject, wrapper);
                    dataObjects_MakeNodes(dataObject, node.Nodes);

                    nodes.Add(node);
                }
        }
        
        private void tagEditor_BuildHtml(IEnumerable<DataObject> dataObjects)
        {
            //Prepare
            TagBlock block = TagBlock.Zero;
            StringBuilder options = null;
            string typeLabel = null;
            formatter.Clear();

            //Loop
            foreach (DataObject dataObject in dataObjects) {
                if (!dataObject.IsValid) continue;
                typeLabel = Enum.GetName(typeof(IfpNodeType), dataObject.Node.Type);

                //Get block level
                if (!string.IsNullOrEmpty(dataObject.Node.Layer))
                {
                    //Prepare
                    options = new StringBuilder();
                    options.AppendLine(TagDataFormatter.CreateOption(-1, "Null"));
                    switch (dataObject.Node.Layer)
                    {
                        case "root":
                            dataObject.DataStream.Seek(SelectedEntry.PostProcessedOffset + dataObject.Node.TagBlockOffset, SeekOrigin.Begin);
                            using (BinaryReader reader = dataObject.DataStream.CreateReader())
                                block = reader.ReadInt64();
                            for (int i = 0; i < block.Count; i++)
                                options.AppendLine(TagDataFormatter.CreateOption(i, $"{i}: {value_GetLabel(dataObject.Node.ItemType, dataObject.DataStream, block.Offset + (dataObject.Node.TagBlockSize * i) + dataObject.Node.ItemOffset)}"));
                            formatter.AddBlockSelect(dataObject.UniqueId, typeLabel, options.ToString(), dataObject.Node.Name);
                            break;
                        default:
                            break;
                    }
                }
                else
                    switch (dataObject.Node.Type)
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
                        case IfpNodeType.Double:
                            formatter.AddValue(dataObject.UniqueId, typeLabel, dataObject.Node.Name);
                            break;

                        case IfpNodeType.Enumerator8:
                        case IfpNodeType.Enumerator16:
                        case IfpNodeType.Enumerator32:
                        case IfpNodeType.Enumerator64:
                            options = new StringBuilder();
                            foreach (IfpNode option in dataObject.Node.Nodes)
                                options.AppendLine(TagDataFormatter.CreateOption(option.Value, option.Name));
                            formatter.AddEnum(dataObject.UniqueId, typeLabel, options.ToString(), dataObject.Node.Name);
                            break;

                        case IfpNodeType.Bitfield8: formatter.AddBitField(dataObject.UniqueId, typeLabel, 8, dataObject.Node.Name); break;
                        case IfpNodeType.Bitfield16: formatter.AddBitField(dataObject.UniqueId, typeLabel, 16, dataObject.Node.Name); break;
                        case IfpNodeType.Bitfield32: formatter.AddBitField(dataObject.UniqueId, typeLabel, 32, dataObject.Node.Name); break;
                        case IfpNodeType.Bitfield64: formatter.AddBitField(dataObject.UniqueId, typeLabel, 64, dataObject.Node.Name); break;

                        case IfpNodeType.String32: formatter.AddString(dataObject.UniqueId, 32, typeLabel, dataObject.Node.Name); break;
                        case IfpNodeType.String64: formatter.AddString(dataObject.UniqueId, 64, typeLabel, dataObject.Node.Name); break;
                        case IfpNodeType.Unicode128: formatter.AddString(dataObject.UniqueId, 128, typeLabel, dataObject.Node.Name); break;
                        case IfpNodeType.Unicode256: formatter.AddString(dataObject.UniqueId, 256, typeLabel, dataObject.Node.Name); break;

                        case IfpNodeType.TagId: formatter.AddTagId(dataObject.UniqueId, typeLabel, dataObject.Node.Name); break;
                        case IfpNodeType.StringId: formatter.AddStringId(dataObject.UniqueId, typeLabel, dataObject.Node.Name); break;
                    }
            }

            //Set
            tagDataWebBrowser.DocumentText = formatter.GetHtml();
        }

        private string value_GetLabel(IfpNodeType itemType, VirtualStream dataStream, long address)
        {
            //Goto
            dataStream.Seek(address, SeekOrigin.Begin);
            using (BinaryReader reader = dataStream.CreateReader())
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
                        IndexEntry entry = Map.IndexEntries[reader.Read<TagId>()];
                        if (entry == null) return "Null";
                        else return $"{entry.Filename}.{entry.Root}";
                    case IfpNodeType.StringId: return Map.Strings[reader.Read<StringId>().Index];
                }
            return string.Empty;
        }

        private void tagEditor_SetValues(IEnumerable<DataObject> dataObjects)
        {
            //Check
            if (dataObjects == null) return;

            //Prepare
            object[] args = null;

            //Loop
            foreach (DataObject dataObject in dataObjects)
                if (dataObject.IsValid)
                    if (dataObject.IsBlockIndex)
                        switch (dataObject.Node.Type)
                        {
                            case IfpNodeType.Byte:
                            case IfpNodeType.SignedByte:
                                args = new object[] { dataObject.GetHtmlId(), Convert.ToSByte(dataObject.Value) };
                                tagDataWebBrowser.Document.InvokeScript("selectBlock", args); break;
                            case IfpNodeType.Short:
                            case IfpNodeType.UnsignedShort:
                                args = new object[] { dataObject.GetHtmlId(), Convert.ToInt16(dataObject.Value) };
                                tagDataWebBrowser.Document.InvokeScript("selectBlock", args); break;
                            case IfpNodeType.Int:
                            case IfpNodeType.UnsignedInt:
                                args = new object[] { dataObject.GetHtmlId(), Convert.ToInt32(dataObject.Value) };
                                tagDataWebBrowser.Document.InvokeScript("selectBlock", args); break;
                            case IfpNodeType.Long:
                            case IfpNodeType.UnsignedLong:
                                args = new object[] { dataObject.GetHtmlId(), Convert.ToInt64(dataObject.Value) };
                                tagDataWebBrowser.Document.InvokeScript("selectBlock", args); break;
                        }
                    else
                        switch (dataObject.Node.Type)
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
                            case IfpNodeType.Double:
                                args = new object[] { dataObject.GetHtmlId(), dataObject.Value };
                                tagDataWebBrowser.Document.InvokeScript("setValue", args); break;

                            case IfpNodeType.Enumerator8:
                                args = new object[] { dataObject.GetHtmlId(), (byte)dataObject.Value };
                                tagDataWebBrowser.Document.InvokeScript("selectEnum", args); break;
                            case IfpNodeType.Enumerator16:
                                args = new object[] { dataObject.GetHtmlId(), (ushort)dataObject.Value };
                                tagDataWebBrowser.Document.InvokeScript("selectEnum", args); break;
                            case IfpNodeType.Enumerator32:
                                args = new object[] { dataObject.GetHtmlId(), (uint)dataObject.Value };
                                tagDataWebBrowser.Document.InvokeScript("selectEnum", args); break;
                            case IfpNodeType.Enumerator64:
                                args = new object[] { dataObject.GetHtmlId(), (ulong)dataObject.Value };
                                tagDataWebBrowser.Document.InvokeScript("selectEnum", args); break;

                            case IfpNodeType.Bitfield8:
                                byte bit8 = (byte)dataObject.Value;
                                for (int i = 0; i < 8; i++)
                                {
                                    args = new object[] { dataObject.UniqueId, i, bit8 % 2 != 0 };
                                    tagDataWebBrowser.Document.InvokeScript("checkBit", args);
                                    bit8 /= 2;
                                }
                                break;
                            case IfpNodeType.Bitfield16:
                                ushort bit16 = (ushort)dataObject.Value;
                                for (int i = 0; i < 16; i++)
                                {
                                    args = new object[] { dataObject.UniqueId, i, bit16 % 2 != 0 };
                                    tagDataWebBrowser.Document.InvokeScript("checkBit", args);
                                    bit16 /= 2;
                                }
                                break;
                            case IfpNodeType.Bitfield32:
                                uint bit32 = (uint)dataObject.Value;
                                for (int i = 0; i < 32; i++)
                                {
                                    args = new object[] { dataObject.UniqueId, i, bit32 % 2 != 0 };
                                    tagDataWebBrowser.Document.InvokeScript("checkBit", args);
                                    bit32 /= 2;
                                }
                                break;

                            case IfpNodeType.String32:
                            case IfpNodeType.String64:
                            case IfpNodeType.Unicode128:
                            case IfpNodeType.Unicode256:
                                args = new object[] { dataObject.GetHtmlId(), (string)dataObject.Value };
                                tagDataWebBrowser.Document.InvokeScript("stringAssign", args); break;

                            case IfpNodeType.TagId: //function tagAssignIdent(id, ident, tagClass, tagPath)
                                args = new object[] { dataObject.GetHtmlId(), ((TagId)dataObject.Value).Id, tagId_GetPath((TagId)dataObject.Value), tagId_GetRoot((TagId)dataObject.Value) };
                                tagDataWebBrowser.Document.InvokeScript("tagAssignIdent", args); break;

                            case IfpNodeType.StringId: //function stringIdAssignID(id, sid, stringName)
                                args = new object[] { dataObject.GetHtmlId(), ((StringId)dataObject.Value).ID, stringid_GetString((StringId)dataObject.Value) };
                                tagDataWebBrowser.Document.InvokeScript("stringIdAssignID", args); break;
                        }
        }

        private string stringid_GetString(StringId value)
        {
            if (Map.Strings.Count > value.Index) return Map.Strings[value.Index];
            else return "Nulled String Reference";
        }

        private string tagId_GetRoot(TagId value)
        {
            if (Map.IndexEntries[value] != null) return Map.IndexEntries[value].Filename;
            return "Nulled Tag Reference";
        }

        private string tagId_GetPath(TagId value)
        {
            if (Map.IndexEntries[value] != null) return Map.IndexEntries[value].Root;
            return "null";
        }

        private void tagButton_Click(int uid, string idString)
        {
            //Prepare
            DataObject dataObject = wrapper.ObjectFromUniqueId(uid);
            if (!dataObject.IsValid) return;
            TagId id = int.Parse(idString);

            //Write
            dataObject.Value = (TagId)Host.Request(this, "TagBrowserDialog", id);
            dataObject.WriteValue();

            //Set
            object[] args = new object[] { dataObject.GetHtmlId(), ((TagId)dataObject.Value).Id, tagId_GetPath((TagId)dataObject.Value), tagId_GetRoot((TagId)dataObject.Value) };
            tagDataWebBrowser.Document.InvokeScript("tagAssignIdent", args);
        }

        private void stringIdButton_Click(int uid, string idString)
        {
            //Prepare
            DataObject dataObject = wrapper.ObjectFromUniqueId(uid);
            if (!dataObject.IsValid) return;
            StringId sid = int.Parse(idString);

            //Write
            dataObject.Value = (StringId)Host.Request(this, "StringBrowserDialog", sid);
            dataObject.WriteValue();

            //Set
            object[] args = new object[] { dataObject.GetHtmlId(), ((StringId)dataObject.Value).ID, stringid_GetString((StringId)dataObject.Value) };
            tagDataWebBrowser.Document.InvokeScript("stringIdAssignID", args);
        }

        private void value_Set(int uid, string valueString)
        {
            //Prepare
            DataObject dataObject = wrapper.ObjectFromUniqueId(uid);
            if (!dataObject.IsValid) return;
            bool write = true;

            //Set Value
            try
            {
                switch (dataObject.Node.Type)
                {
                    case IfpNodeType.Byte: dataObject.Value = byte.Parse(valueString); break;
                    case IfpNodeType.SignedByte: dataObject.Value = sbyte.Parse(valueString); break;
                    case IfpNodeType.Short: dataObject.Value = short.Parse(valueString); break;
                    case IfpNodeType.UnsignedShort: dataObject.Value = ushort.Parse(valueString); break;
                    case IfpNodeType.Int: dataObject.Value = int.Parse(valueString); break;
                    case IfpNodeType.UnsignedInt: dataObject.Value = uint.Parse(valueString); break;
                    case IfpNodeType.Long: dataObject.Value = long.Parse(valueString); break;
                    case IfpNodeType.UnsignedLong: dataObject.Value = ulong.Parse(valueString); break;
                    case IfpNodeType.Single: dataObject.Value = float.Parse(valueString); break;
                    case IfpNodeType.Double: dataObject.Value = double.Parse(valueString); break;
                }
            }
            catch (FormatException) { write = false; }

            //Write
            if (write) dataObject.WriteValue();
        }

        private void enum_Set(int uid, string valueString)
        {
            //Prepare
            DataObject dataObject = wrapper.ObjectFromUniqueId(uid);
            if (!dataObject.IsValid) return;
            bool write = true;

            //Set Value
            try
            {
                switch (dataObject.Node.Type)
                {
                    case IfpNodeType.Enumerator8: dataObject.Value = byte.Parse(valueString); break;
                    case IfpNodeType.Enumerator16: dataObject.Value = ushort.Parse(valueString); break;
                    case IfpNodeType.Enumerator32: dataObject.Value = uint.Parse(valueString); break;
                    case IfpNodeType.Enumerator64: dataObject.Value = ulong.Parse(valueString); break;
                }
            }
            catch (FormatException) { write = false; }

            //Write
            if (write) dataObject.WriteValue();
        }

        private void bitmask_Set(int uid, string bitString)
        {
            //Prepare
            DataObject dataObject = wrapper.ObjectFromUniqueId(uid);
            if (!dataObject.IsValid) return;
            bool write = true;

            //Set Value
            try
            {
                switch (dataObject.Node.Type)
                {
                    case IfpNodeType.Bitfield8: dataObject.Value = byte.Parse(bitString); break;
                    case IfpNodeType.Bitfield16: dataObject.Value = ushort.Parse(bitString); break;
                    case IfpNodeType.Bitfield32: dataObject.Value = uint.Parse(bitString); break;
                    case IfpNodeType.Bitfield64: dataObject.Value = ulong.Parse(bitString); break;
                }
            }
            catch (FormatException) { write = false; }

            //Write
            if (write) dataObject.WriteValue();
        }

        private void string_Set(int uid, string valueString)
        {
            //Prepare
            DataObject dataObject = wrapper.ObjectFromUniqueId(uid);
            if (!dataObject.IsValid) return;

            //Get Object
            dataObject = wrapper.ObjectFromUniqueId(uid);
            dataObject.Value = valueString;
        }

        private void unicode_Set(int uid, string valueString)
        {
            //Prepare
            DataObject dataObject = wrapper.ObjectFromUniqueId(uid);
            if (!dataObject.IsValid) return;

            //Get Object
            dataObject = wrapper.ObjectFromUniqueId(uid);
            dataObject.Value = valueString;
        }

        /// <summary>
        /// Represents a tag data tree node.
        /// </summary>
        private class TagDataNode : TreeNode
        {
            /// <summary>
            /// Gets and returns the tag data wrapper.
            /// </summary>
            public DataWrapper Wrapper
            {
                get { return wrapper; }
            }
            /// <summary>
            /// Gets or sets the block's selected index.
            /// </summary>
            public int SelectedIndex
            {
                get { return dataObject.SelectedIndex; }
                set
                {
                    bool changed = dataObject.SelectedIndex != value;
                    dataObject.SelectedIndex = value;
                    if (changed) { wrapper.Wrap(dataObject, dataObject.BaseAddress); Update(); }
                }
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
            private readonly DataWrapper wrapper;
            private readonly DataObject dataObject;

            /// <summary>
            /// Initializes a new instance of the <see cref="TagDataNode"/> class using the supplied data object.
            /// </summary>
            /// <param name="dataObject">The data object.</param>
            public TagDataNode(DataObject dataObject, DataWrapper wrapper)
            {
                //Check
                if (dataObject == null) throw new ArgumentNullException(nameof(dataObject));
                if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
                this.dataObject = dataObject;
                this.wrapper = wrapper;

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

                //Check
                if (DataObject.Value != null) block = (TagBlock)DataObject.Value;

                //Setup
                if (block.Count == 0) { ForeColor = SystemColors.GrayText; Text = DataObject.Node.Name; }
                else { ForeColor = Color.Black; Text = $"{dataObject.Node.Name} [{SelectedIndex + 1}/{block.Count}]"; }

                //Loop
                foreach (TagDataNode node in Nodes)
                    node.Update();
            }
        }
    }
}
