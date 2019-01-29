using Abide.Compiler;
using Abide.Decompiler;
using Abide.Guerilla.Library;
using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using Abide.HaloLibrary.IO;
using Abide.Tag;
using Abide.Tag.Cache.Generated;
using Abide.Tag.Definition;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Abide.Guerilla
{
    public partial class AbideGuerilla : Form
    {
        private Dictionary<string, TagGroupFileEditor> openEditors = new Dictionary<string, TagGroupFileEditor>();

        public AbideGuerilla()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Exit
            Application.Exit();
        }

        private void decompilerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Initialize CacheDecompiler instance...
            using (CacheDecompiler decompiler = new CacheDecompiler() { Icon = Properties.Resources.abide_icon, StartPosition = FormStartPosition.CenterParent })
                decompiler.ShowDialog();    //Show
        }

        private void compilerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Initialize CaceCompiler instance...
            using (CacheCompiler compiler = new CacheCompiler() { Icon = Properties.Resources.abide_icon, StartPosition = FormStartPosition.CenterParent })
                compiler.ShowDialog();  //Show
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Prepare
            TagGroupFile file = null;

            //Initialize
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                //Setup
                openDlg.Filter = "All files (*.*)|*.*";
                openDlg.CustomPlaces.Add(new FileDialogCustomPlace(RegistrySettings.WorkspaceDirectory));

                //Show
                if (openDlg.ShowDialog() == DialogResult.OK)
                {

                    //Check
                    if (openEditors.ContainsKey(openDlg.FileName))
                        openEditors[openDlg.FileName].Show();
#if DEBUG
                    //Load file
                    file = new TagGroupFile();
                    file.Load(openDlg.FileName);
#else
                    try
                    {
                        using (Stream stream = openDlg.OpenFile())
                        {
                            //Load file
                            file = new TagGroupFile();
                            file.Load(stream);
                        }
                    }
                    catch { file = null; MessageBox.Show($"An error occured while opening {openDlg.FileName}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); } 
#endif

                    //Check
                    if (file != null)
                    {
                        //Create editor
                        TagGroupFileEditor editor = new TagGroupFileEditor(openDlg.FileName, file) { MdiParent = this };
                        editor.FileNameChanged += Editor_FileNameChanged;
                        editor.FormClosed += Editor_FormClosed;
                        
                        //Show
                        editor.Show();
                    }
                }
            }
        }
        
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create dialog
            using (Dialogs.NewTagGroupDialog groupDialog = new Dialogs.NewTagGroupDialog())
            {
                //Show
                if(groupDialog.ShowDialog() == DialogResult.OK)
                {
                    //Create file
                    TagGroupFile file = new TagGroupFile() { TagGroup = groupDialog.SelectedGroup };

                    //Create editor
                    TagGroupFileEditor editor = new TagGroupFileEditor($"{groupDialog.FileName}.{file.TagGroup.Name}", file) { MdiParent = this };
                    editor.FileNameChanged += Editor_FileNameChanged;
                    editor.FormClosed += Editor_FormClosed;
                    
                    //Show
                    editor.Show();
                }
            }
        }

        private void Editor_FileNameChanged(object sender, EventArgs e)
        {
            //Clear
            openEditors.Clear();

            //Loop
            foreach (Form child in MdiChildren)
                if (child is TagGroupFileEditor editor)
                    openEditors.Add(editor.FileName, editor);
        }

        private void Editor_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Prepare
            TagGroupFileEditor editor = null;

            //Get form
            if (sender is TagGroupFileEditor)
            {
                editor = (TagGroupFileEditor)sender;
                openEditors.Remove(editor.FileName);
            }
            else
            {
                //Clear
                openEditors.Clear();

                //Loop
                foreach (Form child in MdiChildren)
                    if (child is TagGroupFileEditor)
                    {
                        editor = (TagGroupFileEditor)child;
                        openEditors.Add(editor.FileName, editor);
                    }
            }
        }

        private void configureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Initialize StartupScreen instance...
            using (StartupScreen startup = new StartupScreen())
            startup.ShowDialog();   //Show
        }

        private void secretTestButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Prepare
            long bspAddress = 0, bspLength = 0, tagDataAddress = 0, tagDataLength = 0;

            //Open
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                openDlg.Filter = "Halo 2 Map Files (*.map)|*.map";
                if (openDlg.ShowDialog() == DialogResult.OK)
                {
                    //Load
                    MapFile mapFile = new MapFile();
                    mapFile.Load(openDlg.FileName);

                    //Prepare
                    bspAddress = Index.IndexMemoryAddress + mapFile.IndexLength - Index.Length;
                    for (int i = 0; i < mapFile.BspCount; i++)
                    {
                        IndexEntry sbsp = null, ltmp = null;
                        bool hasLightmap = false;
                        ITagGroup structureBsp = new ScenarioStructureBsp();
                        ITagGroup structureLightmap = new ScenarioStructureLightmap();
                        using (BinaryReader reader = mapFile.GetBspTagDataStream(i).CreateReader())
                        {
                            //Read header
                            reader.BaseStream.Seek(bspAddress, SeekOrigin.Begin);
                            StructureBspBlockHeader header = reader.Read<StructureBspBlockHeader>();
                            sbsp = mapFile.IndexEntries.FirstOrDefault(t =>
                                t.PostProcessedOffset == (int)header.StructureBspOffset && t.Root == HaloTags.sbsp);
                            ltmp = mapFile.IndexEntries.FirstOrDefault(t => 
                                t.PostProcessedOffset == (int)header.StructureLightmapOffset && t.Root == HaloTags.ltmp);
                            
                            //Read sbsp
                            reader.BaseStream.Seek(header.StructureBspOffset, SeekOrigin.Begin);
                            structureBsp.Read(reader);

                            //Read ltmp?
                            if (hasLightmap = header.StructureLightmapOffset > 0)
                            {
                                reader.BaseStream.Seek(header.StructureLightmapOffset, SeekOrigin.Begin);
                                structureLightmap.Read(reader);
                            }
                        }

                        //Process
                        foreach (ITagBlock tagBlock in structureBsp)
                            TagBlock_NullSounds(mapFile, tagBlock);
                        foreach (ITagBlock tagBlock in structureLightmap)
                            TagBlock_NullSounds(mapFile, tagBlock);

                        //Rewrite
                        using (VirtualStream bspStream = new VirtualStream(bspAddress))
                        using (BinaryWriter writer = new BinaryWriter(bspStream))
                        {
                            //Prepare
                            StructureBspBlockHeader header = new StructureBspBlockHeader();

                            //Seek
                            header.StructureBspOffset = (uint)bspStream.Seek(StructureBspBlockHeader.Length, SeekOrigin.Current);
                            using (VirtualStream scenarioStructureStream = new VirtualStream(bspStream.Position))
                            using (BinaryReader bspReader = new BinaryReader(scenarioStructureStream))
                            using (BinaryWriter bspWriter = new BinaryWriter(scenarioStructureStream))
                            {
                                //Write
                                structureBsp.Write(bspWriter);
                                scenarioStructureStream.Align(4096);

                                //Repoint
                                Tag_RepointRaw(sbsp, scenarioStructureStream, bspWriter, bspReader);

                                //Write to underlying stream
                                writer.Write(scenarioStructureStream.ToArray());
                            }

                            if(hasLightmap)
                            {
                                header.StructureLightmapOffset = (uint)bspStream.Position;
                                using (VirtualStream scenarioStructureStream = new VirtualStream(bspStream.Position))
                                using (BinaryReader bspReader = new BinaryReader(scenarioStructureStream))
                                using (BinaryWriter bspWriter = new BinaryWriter(scenarioStructureStream))
                                {
                                    //Write
                                    structureLightmap.Write(bspWriter);
                                    scenarioStructureStream.Align(4096);

                                    //Repoint
                                    Tag_RepointRaw(ltmp, scenarioStructureStream, bspWriter, bspReader);

                                    //Write to underlying stream
                                    writer.Write(scenarioStructureStream.ToArray());
                                }
                            }

                            //Check
                            if (bspStream.Length > bspLength) bspLength = bspStream.Length;

                            //Finish header
                            header.BlockLength = (int)bspStream.Length;
                            header.StructureBsp = HaloTags.sbsp;

                            //Write header
                            bspStream.Seek(bspAddress, SeekOrigin.Begin);
                            writer.Write(header);

                            //Swap bsp buffer
                            mapFile.SwapBspTagBuffer(bspStream.ToArray(), i, bspAddress);
                        }
                    }

                    //Create new tag data
                    tagDataAddress = bspAddress + bspLength;
                    using (VirtualStream tagDataStream = new VirtualStream(tagDataAddress))
                    using (BinaryWriter writer = new BinaryWriter(tagDataStream))
                    {
                        //Loop through tags
                        ITagGroup tagGroup = null;
                        foreach (IndexEntry entry in mapFile.IndexEntries.Where(t => t.Offset > 0 && t.Size > 0))   //limit only non-scenario structure tags
                            using (BinaryReader reader = entry.TagData.CreateReader())
                            {
                                //Read
                                tagGroup = TagLookup.CreateTagGroup(entry.Root);
                                reader.BaseStream.Seek(entry.Offset, SeekOrigin.Begin);
                                tagGroup.Read(reader);

                                //Process
                                foreach (ITagBlock tagBlock in tagGroup)
                                    TagBlock_NullSounds(mapFile, tagBlock);

                                //Write
                                using (VirtualStream tagStream = new VirtualStream(tagDataStream.Position))
                                using (BinaryWriter tagWriter = new BinaryWriter(tagStream))
                                using (BinaryReader tagReader = new BinaryReader(tagStream))
                                {
                                    //Write
                                    tagGroup.Write(tagWriter);

                                    //Change entry
                                    entry.SetObjectEntry(entry.Root, entry.Id, (uint)tagStream.Length, (uint)tagStream.MemoryAddress);

                                    //Repoint
                                    Tag_RepointRaw(entry, tagStream, tagWriter, tagReader);

                                    //Write
                                    writer.Write(tagStream.ToArray());
                                }
                            }

                        //Get length
                        tagDataLength = tagDataStream.Length;

                        //Copy
                        mapFile.SwapTagBuffer(tagDataStream.ToArray(), tagDataAddress);
                    }
                    
                    //Confirm
                    if (MessageBox.Show("Are you sure?", "Are you really?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        mapFile.Save(openDlg.FileName);
                }
            }
        }

        private void Tag_RepointRaw(IndexEntry tag, VirtualStream tagData, BinaryWriter writer, BinaryReader reader)
        {
            //Prepare
            long offsetAddress = 0, lengthAddress = 0;
            int rawOffset = 0;

            //Handle
            switch (tag.Root)
            {
                #region ugh!
                case HaloTags.ugh_:
                    tagData.Seek(tagData.MemoryAddress + 64, SeekOrigin.Begin);
                    int soundsCount = reader.ReadInt32();
                    int soundsOffset = reader.ReadInt32();
                    for (int i = 0; i < soundsCount; i++)
                    {
                        //Goto
                        tagData.Seek(soundsOffset + (i * 12), SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        lengthAddress = tagData.Position;

                        //Check
                        if (tag.Raws[RawSection.Sound].ContainsRawOffset(rawOffset))
                        {
                            tag.Raws[RawSection.Sound][rawOffset].OffsetAddresses.Clear();
                            tag.Raws[RawSection.Sound][rawOffset].LengthAddresses.Clear();
                            tag.Raws[RawSection.Sound][rawOffset].OffsetAddresses.Add(offsetAddress);
                            tag.Raws[RawSection.Sound][rawOffset].LengthAddresses.Add(lengthAddress);
                        }
                    }

                    tagData.Seek(tagData.MemoryAddress + 80, SeekOrigin.Begin);
                    int extraInfosCount = reader.ReadInt32();
                    int extraInfosOffset = reader.ReadInt32();
                    for (int i = 0; i < extraInfosCount; i++)
                    {
                        //Goto
                        tagData.Seek(extraInfosOffset + (i * 44) + 8, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        lengthAddress = tagData.Position;

                        //Check
                        if (tag.Raws[RawSection.LipSync].ContainsRawOffset(rawOffset))
                        {
                            tag.Raws[RawSection.LipSync][rawOffset].OffsetAddresses.Clear();
                            tag.Raws[RawSection.LipSync][rawOffset].LengthAddresses.Clear();
                            tag.Raws[RawSection.LipSync][rawOffset].OffsetAddresses.Add(offsetAddress);
                            tag.Raws[RawSection.LipSync][rawOffset].LengthAddresses.Add(lengthAddress);
                        }
                    }
                    break;
                #endregion
                #region mode
                case HaloTags.mode:
                    tagData.Seek(tagData.MemoryAddress + 36, SeekOrigin.Begin);
                    int sectionCount = reader.ReadInt32();
                    int sectionOffset = reader.ReadInt32();
                    for (int i = 0; i < sectionCount; i++)
                    {
                        tagData.Seek(sectionOffset + (i * 92) + 56, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        lengthAddress = tagData.Position;

                        //Check
                        if (tag.Raws[RawSection.Model].ContainsRawOffset(rawOffset))
                        {
                            tag.Raws[RawSection.Model][rawOffset].OffsetAddresses.Clear();
                            tag.Raws[RawSection.Model][rawOffset].LengthAddresses.Clear();
                            tag.Raws[RawSection.Model][rawOffset].OffsetAddresses.Add(offsetAddress);
                            tag.Raws[RawSection.Model][rawOffset].LengthAddresses.Add(lengthAddress);
                        }
                    }

                    tagData.Seek(tagData.MemoryAddress + 116, SeekOrigin.Begin);
                    int prtCount = reader.ReadInt32();
                    int prtOffset = reader.ReadInt32();
                    for (int i = 0; i < prtCount; i++)
                    {
                        tagData.Seek(prtOffset + (i * 88) + 52, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        lengthAddress = tagData.Position;

                        //Check
                        if (tag.Raws[RawSection.Model].ContainsRawOffset(rawOffset))
                        {
                            tag.Raws[RawSection.Model][rawOffset].OffsetAddresses.Clear();
                            tag.Raws[RawSection.Model][rawOffset].LengthAddresses.Clear();
                            tag.Raws[RawSection.Model][rawOffset].OffsetAddresses.Add(offsetAddress);
                            tag.Raws[RawSection.Model][rawOffset].LengthAddresses.Add(lengthAddress);
                        }
                    }
                    break;
                #endregion
                #region weat
                case HaloTags.weat:
                    tagData.Seek(tagData.MemoryAddress, SeekOrigin.Begin);
                    int particleSystemCount = reader.ReadInt32();
                    int particleSystemOffset = reader.ReadInt32();
                    for (int i = 0; i < particleSystemCount; i++)
                    {
                        tagData.Seek(particleSystemOffset + (i * 140) + 64, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        lengthAddress = tagData.Position;

                        //Check
                        if (tag.Raws[RawSection.Weather].ContainsRawOffset(rawOffset))
                        {
                            tag.Raws[RawSection.Weather][rawOffset].OffsetAddresses.Clear();
                            tag.Raws[RawSection.Weather][rawOffset].LengthAddresses.Clear();
                            tag.Raws[RawSection.Weather][rawOffset].OffsetAddresses.Add(offsetAddress);
                            tag.Raws[RawSection.Weather][rawOffset].LengthAddresses.Add(lengthAddress);
                        }
                    }
                    break;
                #endregion
                #region DECR
                case HaloTags.DECR:
                    tagData.Seek(tagData.MemoryAddress + 56, SeekOrigin.Begin);
                    offsetAddress = tagData.Position;
                    rawOffset = reader.ReadInt32();
                    lengthAddress = tagData.Position;

                    //Check
                    if (tag.Raws[RawSection.DecoratorSet].ContainsRawOffset(rawOffset))
                    {
                        tag.Raws[RawSection.DecoratorSet][rawOffset].OffsetAddresses.Clear();
                        tag.Raws[RawSection.DecoratorSet][rawOffset].LengthAddresses.Clear();
                        tag.Raws[RawSection.DecoratorSet][rawOffset].OffsetAddresses.Add(offsetAddress);
                        tag.Raws[RawSection.DecoratorSet][rawOffset].LengthAddresses.Add(lengthAddress);
                    }
                    break;
                #endregion
                #region PRTM
                case HaloTags.PRTM:
                    tagData.Seek(tagData.MemoryAddress + 160, SeekOrigin.Begin);
                    offsetAddress = tagData.Position;
                    rawOffset = reader.ReadInt32();
                    lengthAddress = tagData.Position;

                    //Check
                    if (tag.Raws[RawSection.ParticleModel].ContainsRawOffset(rawOffset))
                    {
                        tag.Raws[RawSection.ParticleModel][rawOffset].OffsetAddresses.Clear();
                        tag.Raws[RawSection.ParticleModel][rawOffset].LengthAddresses.Clear();
                        tag.Raws[RawSection.ParticleModel][rawOffset].OffsetAddresses.Add(offsetAddress);
                        tag.Raws[RawSection.ParticleModel][rawOffset].LengthAddresses.Add(lengthAddress);
                    }
                    break;
                #endregion
                #region jmad
                case HaloTags.jmad:
                    tagData.Seek(tagData.MemoryAddress + 172, SeekOrigin.Begin);
                    int animationCount = reader.ReadInt32();
                    int animationOffset = reader.ReadInt32();
                    for (int i = 0; i < animationCount; i++)
                    {
                        tagData.Seek(animationOffset + (i * 20) + 4, SeekOrigin.Begin);
                        lengthAddress = tagData.Position;
                        reader.ReadInt32();
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();

                        //Check
                        if (tag.Raws[RawSection.Animation].ContainsRawOffset(rawOffset))
                        {
                            tag.Raws[RawSection.Animation][rawOffset].OffsetAddresses.Clear();
                            tag.Raws[RawSection.Animation][rawOffset].LengthAddresses.Clear();
                            tag.Raws[RawSection.Animation][rawOffset].OffsetAddresses.Add(offsetAddress);
                            tag.Raws[RawSection.Animation][rawOffset].LengthAddresses.Add(lengthAddress);
                        }
                    }
                    break;
                #endregion
                #region bitm
                case HaloTags.bitm:
                    tagData.Seek(tagData.MemoryAddress + 68, SeekOrigin.Begin);
                    int bitmapCount = reader.ReadInt32();
                    int bitmapOffset = reader.ReadInt32();
                    for (int i = 0; i < bitmapCount; i++)
                    {
                        //LOD0
                        tagData.Seek(bitmapOffset + (i * 116) + 28, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        tagData.Seek(bitmapOffset + (i * 116) + 52, SeekOrigin.Begin);
                        lengthAddress = tagData.Position;

                        //Check
                        if (tag.Raws[RawSection.Bitmap].ContainsRawOffset(rawOffset))
                        {
                            tag.Raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Clear();
                            tag.Raws[RawSection.Bitmap][rawOffset].LengthAddresses.Clear();
                            tag.Raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Add(offsetAddress);
                            tag.Raws[RawSection.Bitmap][rawOffset].LengthAddresses.Add(lengthAddress);
                        }

                        //LOD1
                        tagData.Seek(bitmapOffset + (i * 116) + 32, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        tagData.Seek(bitmapOffset + (i * 116) + 56, SeekOrigin.Begin);
                        lengthAddress = tagData.Position;

                        //Check
                        if (tag.Raws[RawSection.Bitmap].ContainsRawOffset(rawOffset))
                        {
                            tag.Raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Clear();
                            tag.Raws[RawSection.Bitmap][rawOffset].LengthAddresses.Clear();
                            tag.Raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Add(offsetAddress);
                            tag.Raws[RawSection.Bitmap][rawOffset].LengthAddresses.Add(lengthAddress);
                        }

                        //LOD2
                        tagData.Seek(bitmapOffset + (i * 116) + 36, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        tagData.Seek(bitmapOffset + (i * 116) + 60, SeekOrigin.Begin);
                        lengthAddress = tagData.Position;

                        //Check
                        if (tag.Raws[RawSection.Bitmap].ContainsRawOffset(rawOffset))
                        {
                            tag.Raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Clear();
                            tag.Raws[RawSection.Bitmap][rawOffset].LengthAddresses.Clear();
                            tag.Raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Add(offsetAddress);
                            tag.Raws[RawSection.Bitmap][rawOffset].LengthAddresses.Add(lengthAddress);
                        }

                        //LOD3
                        tagData.Seek(bitmapOffset + (i * 116) + 40, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        tagData.Seek(bitmapOffset + (i * 116) + 64, SeekOrigin.Begin);
                        lengthAddress = tagData.Position;

                        //Check
                        if (tag.Raws[RawSection.Bitmap].ContainsRawOffset(rawOffset))
                        {
                            tag.Raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Clear();
                            tag.Raws[RawSection.Bitmap][rawOffset].LengthAddresses.Clear();
                            tag.Raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Add(offsetAddress);
                            tag.Raws[RawSection.Bitmap][rawOffset].LengthAddresses.Add(lengthAddress);
                        }

                        //LOD4
                        tagData.Seek(bitmapOffset + (i * 116) + 44, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        tagData.Seek(bitmapOffset + (i * 116) + 68, SeekOrigin.Begin);
                        lengthAddress = tagData.Position;

                        //Check
                        if (tag.Raws[RawSection.Bitmap].ContainsRawOffset(rawOffset))
                        {
                            tag.Raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Clear();
                            tag.Raws[RawSection.Bitmap][rawOffset].LengthAddresses.Clear();
                            tag.Raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Add(offsetAddress);
                            tag.Raws[RawSection.Bitmap][rawOffset].LengthAddresses.Add(lengthAddress);
                        }

                        //LOD5
                        tagData.Seek(bitmapOffset + (i * 116) + 48, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        tagData.Seek(bitmapOffset + (i * 116) + 72, SeekOrigin.Begin);
                        lengthAddress = tagData.Position;

                        //Check
                        if (tag.Raws[RawSection.Bitmap].ContainsRawOffset(rawOffset))
                        {
                            tag.Raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Clear();
                            tag.Raws[RawSection.Bitmap][rawOffset].LengthAddresses.Clear();
                            tag.Raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Add(offsetAddress);
                            tag.Raws[RawSection.Bitmap][rawOffset].LengthAddresses.Add(lengthAddress);
                        }
                    }
                    break;
                #endregion
                #region sbsp
                case HaloTags.sbsp:
                    //Goto Clusters
                    tagData.Seek(tagData.MemoryAddress + 156, SeekOrigin.Begin);
                    uint clusterCount = reader.ReadUInt32();
                    uint clusterOffset = reader.ReadUInt32();
                    for (int i = 0; i < clusterCount; i++)
                    {
                        tagData.Seek(clusterOffset + (i * 176) + 40, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        lengthAddress = tagData.Position;

                        //Check
                        if (tag.Raws[RawSection.BSP].ContainsRawOffset(rawOffset))
                        {
                            tag.Raws[RawSection.BSP][rawOffset].OffsetAddresses.Clear();
                            tag.Raws[RawSection.BSP][rawOffset].LengthAddresses.Clear();
                            tag.Raws[RawSection.BSP][rawOffset].OffsetAddresses.Add(offsetAddress);
                            tag.Raws[RawSection.BSP][rawOffset].LengthAddresses.Add(lengthAddress);
                        }
                    }

                    //Goto Geometries definitions
                    tagData.Seek(tagData.MemoryAddress + 312, SeekOrigin.Begin);
                    uint geometriesCount = reader.ReadUInt32();
                    uint geometriesOffset = reader.ReadUInt32();
                    for (int i = 0; i < geometriesCount; i++)
                    {
                        tagData.Seek(geometriesOffset + (i * 200) + 40, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        lengthAddress = tagData.Position;

                        //Check
                        if (tag.Raws[RawSection.BSP].ContainsRawOffset(rawOffset))
                        {
                            tag.Raws[RawSection.BSP][rawOffset].OffsetAddresses.Clear();
                            tag.Raws[RawSection.BSP][rawOffset].LengthAddresses.Clear();
                            tag.Raws[RawSection.BSP][rawOffset].OffsetAddresses.Add(offsetAddress);
                            tag.Raws[RawSection.BSP][rawOffset].LengthAddresses.Add(lengthAddress);
                        }
                    }

                    //Goto Water definitions
                    tagData.Seek(tagData.MemoryAddress + 532, SeekOrigin.Begin);
                    uint watersCount = reader.ReadUInt32();
                    uint watersOffset = reader.ReadUInt32();
                    for (int i = 0; i < watersCount; i++)
                    {
                        tagData.Seek(watersOffset + (i * 172) + 16, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        lengthAddress = tagData.Position;

                        //Check
                        if (tag.Raws[RawSection.BSP].ContainsRawOffset(rawOffset))
                        {
                            tag.Raws[RawSection.BSP][rawOffset].OffsetAddresses.Clear();
                            tag.Raws[RawSection.BSP][rawOffset].LengthAddresses.Clear();
                            tag.Raws[RawSection.BSP][rawOffset].OffsetAddresses.Add(offsetAddress);
                            tag.Raws[RawSection.BSP][rawOffset].LengthAddresses.Add(lengthAddress);
                        }
                    }

                    //Goto Decorators Definitions
                    tagData.Seek(tagData.MemoryAddress + 564, SeekOrigin.Begin);
                    uint decoratorsCount = reader.ReadUInt32();
                    uint decoratorsOffset = reader.ReadUInt32();
                    for (int i = 0; i < decoratorsCount; i++)
                    {
                        tagData.Seek(decoratorsOffset + (i * 48) + 16, SeekOrigin.Begin);
                        uint cachesCount = reader.ReadUInt32();
                        uint cachesOffset = reader.ReadUInt32();
                        for (int j = 0; j < cachesCount; j++)
                        {
                            tagData.Seek(cachesOffset + (j * 44), SeekOrigin.Begin);
                            offsetAddress = tagData.Position;
                            rawOffset = reader.ReadInt32();
                            lengthAddress = tagData.Position;

                            //Check
                            if (tag.Raws[RawSection.BSP].ContainsRawOffset(rawOffset))
                            {
                                tag.Raws[RawSection.BSP][rawOffset].OffsetAddresses.Clear();
                                tag.Raws[RawSection.BSP][rawOffset].LengthAddresses.Clear();
                                tag.Raws[RawSection.BSP][rawOffset].OffsetAddresses.Add(offsetAddress);
                                tag.Raws[RawSection.BSP][rawOffset].LengthAddresses.Add(lengthAddress);
                            }
                        }
                    }
                    break;
                #endregion
                #region ltmp
                case HaloTags.ltmp:
                    //Goto Lightmap Groups
                    tagData.Seek(tagData.MemoryAddress + 128);
                    uint groupsCount = reader.ReadUInt32();
                    uint groupsPointer = reader.ReadUInt32();
                    for (int i = 0; i < groupsCount; i++)
                    {
                        //Goto Cluster Definitions
                        tagData.Seek(groupsPointer + (i * 104) + 32, SeekOrigin.Begin);
                        uint clustersCount = reader.ReadUInt32();
                        uint clustersOffset = reader.ReadUInt32();
                        for (int j = 0; j < clustersCount; j++)
                        {
                            tagData.Seek(clustersOffset + (j * 84) + 40, SeekOrigin.Begin);
                            offsetAddress = tagData.Position;
                            rawOffset = reader.ReadInt32();
                            lengthAddress = tagData.Position;

                            //Check
                            if (tag.Raws[RawSection.BSP].ContainsRawOffset(rawOffset))
                            {
                                tag.Raws[RawSection.BSP][rawOffset].OffsetAddresses.Clear();
                                tag.Raws[RawSection.BSP][rawOffset].LengthAddresses.Clear();
                                tag.Raws[RawSection.BSP][rawOffset].OffsetAddresses.Add(offsetAddress);
                                tag.Raws[RawSection.BSP][rawOffset].LengthAddresses.Add(lengthAddress);
                            }
                        }

                        //Goto Poop Definitions
                        tagData.Seek(groupsPointer + (i * 104) + 48, SeekOrigin.Begin);
                        uint poopsCount = reader.ReadUInt32();
                        uint poopsOffset = reader.ReadUInt32();
                        for (int j = 0; j < poopsCount; j++)
                        {
                            tagData.Seek(poopsOffset + (j * 84) + 40, SeekOrigin.Begin);
                            offsetAddress = tagData.Position;
                            rawOffset = reader.ReadInt32();
                            lengthAddress = tagData.Position;

                            //Check
                            if (tag.Raws[RawSection.BSP].ContainsRawOffset(rawOffset))
                            {
                                tag.Raws[RawSection.BSP][rawOffset].OffsetAddresses.Clear();
                                tag.Raws[RawSection.BSP][rawOffset].LengthAddresses.Clear();
                                tag.Raws[RawSection.BSP][rawOffset].OffsetAddresses.Add(offsetAddress);
                                tag.Raws[RawSection.BSP][rawOffset].LengthAddresses.Add(lengthAddress);
                            }
                        }

                        //Goto Geometry Buckets
                        tagData.Seek(groupsPointer + (i * 104) + 64, SeekOrigin.Begin);
                        uint bucketsCount = reader.ReadUInt32();
                        uint bucketsOffset = reader.ReadUInt32();
                        for (int j = 0; j < bucketsCount; j++)
                        {
                            tagData.Seek(bucketsOffset + (j * 56) + 12, SeekOrigin.Begin);
                            offsetAddress = tagData.Position;
                            rawOffset = reader.ReadInt32();
                            lengthAddress = tagData.Position;

                            //Check
                            if (tag.Raws[RawSection.BSP].ContainsRawOffset(rawOffset))
                            {
                                tag.Raws[RawSection.BSP][rawOffset].OffsetAddresses.Clear();
                                tag.Raws[RawSection.BSP][rawOffset].LengthAddresses.Clear();
                                tag.Raws[RawSection.BSP][rawOffset].OffsetAddresses.Add(offsetAddress);
                                tag.Raws[RawSection.BSP][rawOffset].LengthAddresses.Add(lengthAddress);
                            }
                        }
                    }
                    break;
                    #endregion
            }
        }
        
        private void TagBlock_NullSounds(MapFile map, ITagBlock tagBlock)
        {
            //Prepare
            IndexEntry referencedEntry = null;

            //Loop
            foreach (Field field in tagBlock.Fields)
            {
                switch(field.Type)
                {
                    case FieldType.FieldBlock:
                        foreach (ITagBlock nestedTagBlock in ((BaseBlockField)field).BlockList)
                            TagBlock_NullSounds(map, nestedTagBlock);
                        break;
                    case FieldType.FieldStruct:
                        TagBlock_NullSounds(map, (ITagBlock)field.Value);
                        break;
                    case FieldType.FieldTagIndex:
                        if (field.Value is TagId id && !id.IsNull)
                            referencedEntry = map.IndexEntries[id];
                        if (referencedEntry != null && referencedEntry.Root == HaloTags.snd_)
                            field.Value = TagId.Null;
                        break;
                    case FieldType.FieldTagReference:
                        if(field.Value is TagReference tagRef && !tagRef.Id.IsNull)
                            referencedEntry = map.IndexEntries[tagRef.Id];
                        if (referencedEntry != null && referencedEntry.Root == HaloTags.snd_)
                            field.Value = new TagReference() { Tag = HaloTags.snd_, Id = TagId.Null };
                        break;
                }
            }
        }
    }
}
