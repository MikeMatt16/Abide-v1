using Abide.AddOnApi;
using Abide.HaloLibrary.Halo2Map;
using Abide.Tag;
using Abide.TagBuilder.Halo2;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using IHalo2MenuButton = Abide.AddOnApi.IMenuButton<Abide.HaloLibrary.Halo2Map.MapFile, Abide.HaloLibrary.Halo2Map.IndexEntry, YeloDebug.Xbox>;
using IHalo2Tool = Abide.AddOnApi.ITool<Abide.HaloLibrary.Halo2Map.MapFile, Abide.HaloLibrary.Halo2Map.IndexEntry, YeloDebug.Xbox>;

namespace Abide.TagBuilder
{
    public partial class Main : Form, IHost
    {
        private Dictionary<IndexEntry, IHalo2Tool> entryEditors = new Dictionary<IndexEntry, IHalo2Tool>();
        private Dictionary<IAddOn, IndexEntry> editorEntryLookup = new Dictionary<IAddOn, IndexEntry>();
        private MapFile map = null;
        private IndexEntry selectedEntry = null;
        private string currentFile = string.Empty;
        private IHalo2MenuButton newTagButton = null;
        private IHalo2MenuButton tagDuplicator = null;
        private IHalo2MenuButton tagImporter = null;
        private IHalo2MenuButton tagExporter = null;

        public Main()
        {
            InitializeComponent();

            //
            // tagTreeView
            //
            tagTreeView.TreeViewNodeSorter = new TagTreeViewSorter();

            //
            // newTagButton
            //
            newTagButton = new NewTagButton();
            newTagButton.Initialize(this);
            newTagButton.OnMapLoad();
            ToolStripItem newTagButtonToolStripMenuButton = mainMenuStrip.Items.Add(newTagButton.Name, newTagButton.Icon);
            newTagButtonToolStripMenuButton.Click += NewTagButtonToolStripMenuButton_Click;

            //
            // tagDuplicator
            //
            tagDuplicator = new TagDuplicateButton();
            tagDuplicator.Initialize(this);
            tagDuplicator.OnMapLoad();
            ToolStripItem tagDuplicatorToolStripMenuButton = mainMenuStrip.Items.Add(tagDuplicator.Name, tagDuplicator.Icon);
            tagDuplicatorToolStripMenuButton.Click += TagDuplicatorToolStripMenuButton_Click;

            //
            // tagImporter
            //
            tagImporter = new TagImportButton();
            tagImporter.Initialize(this);
            tagImporter.OnMapLoad();
            ToolStripItem tagImporterToolStripMenuButton = mainMenuStrip.Items.Add(tagImporter.Name, tagImporter.Icon);
            tagImporterToolStripMenuButton.Click += TagImporterToolStripMenuButton_Click;

            //
            // tagExporter
            //
            tagExporter = new TagExportButton();
            tagExporter.Initialize(this);
            tagExporter.OnMapLoad();
            ToolStripItem tagExporterToolStripMenuButton = mainMenuStrip.Items.Add(tagExporter.Name, tagExporter.Icon);
            tagExporterToolStripMenuButton.Click += TagExporterToolStripMenuButton_Click;
        }

        public object Request(IAddOn sender, string request, params object[] args)
        {
            //Handle
            switch (request)
            {
                case "Map": return map;
                case "SelectedEntry": if (editorEntryLookup.ContainsKey(sender)) return editorEntryLookup[sender]; else return selectedEntry;
                case "ReloadMap": Map_BuildTagTreeView(map, tagTreeView); return null;
                default: return null;
            }
        }
        private void TagExporterToolStripMenuButton_Click(object sender, EventArgs e)
        {
            if (map == null) return;
            tagExporter.OnClick();
        }
        private void TagImporterToolStripMenuButton_Click(object sender, EventArgs e)
        {
            if (map == null) return;
            tagImporter.OnClick();
        }
        private void TagDuplicatorToolStripMenuButton_Click(object sender, EventArgs e)
        {
            if (map == null || selectedEntry == null) return;
            tagDuplicator.OnClick();
        }
        private void NewTagButtonToolStripMenuButton_Click(object sender, EventArgs e)
        {
            if (map == null) return;
            newTagButton.OnClick();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Exit
            Application.Exit();
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Initialize new OpenFileDialog instance
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                //Setup
                openDlg.Filter = "Halo 2 Maps (*.map)|*.map";
                openDlg.Title = "Open Halo 2 Map file...";

                //Show
                if (openDlg.ShowDialog() == DialogResult.OK)
                    using (Stream mapStream = openDlg.OpenFile())
                    {
                        //Create new instance
                        MapFile map = new MapFile();
                        bool success = true;

                        //Open
                        try { map.Load(mapStream); }
                        catch { MessageBox.Show("Failed to load map.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error); success = false; }

                        //Check
                        if (success)
                        {
                            //Set current file
                            currentFile = openDlg.FileName;

                            //Close all
                            foreach (Form child in MdiChildren)
                                child.Close();

                            //Reset
                            entryEditors.Clear();
                            editorEntryLookup.Clear();
                            newTagButton.OnMapLoad();
                            tagDuplicator.OnMapLoad();
                            tagImporter.OnMapLoad();
                            tagExporter.OnMapLoad();

                            //Dispose
                            if (this.map != null) { this.map.Close(); this.map.Dispose(); }
                            this.map = map;

                            //Build tag tree
                            Map_BuildTagTreeView(map, tagTreeView);
                        }
                    }
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check
            if (File.Exists(currentFile))
            { map.Save(currentFile); return; }

            //Initialize new SaveFileDialog instance
            using (SaveFileDialog saveDlg = new SaveFileDialog())
            {
                //Setup
                saveDlg.Filter = "Halo 2 Maps (*.map)|*.map";
                saveDlg.Title = "Save Halo 2 Map file as...";

                //Show
                if (saveDlg.ShowDialog() == DialogResult.OK)
                    using (Stream mapStream = saveDlg.OpenFile())
                        try { map.Save(mapStream); }
                        catch { MessageBox.Show("Failed to save map.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
        private void Map_BuildTagTreeView(MapFile map, TreeView tagTreeView)
        {
            //Check
            if (map == null || tagTreeView == null) return;

            //Begin
            tagTreeView.BeginUpdate();
            tagTreeView.Nodes.Clear();

            //Prepare
            TreeNodeCollection currentCollection = null;
            TreeNode node = null;

            //Loop
            foreach (IndexEntry tag in map.IndexEntries)
            {
                //Setup
                currentCollection = tagTreeView.Nodes;
                node = null;

                //Split
                string[] parts = ($"{tag.Filename}.{tag.Root}").Split('\\');

                //Loop
                for (int i = 0; i < parts.Length - 1; i++)
                {
                    //Get or create node
                    if (currentCollection.ContainsKey(parts[i])) node = currentCollection[parts[i]];
                    else node = currentCollection.Add(parts[i], parts[i], 0, 0);

                    //Get collection
                    currentCollection = node.Nodes;
                }

                //Create tag node
                node = currentCollection.Add(parts.Last(), parts.Last(), 1, 1);
                node.Tag = tag;
            }

            //End
            tagTreeView.Sort();
            tagTreeView.EndUpdate();
        }
        private void tagTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Check
            this.selectedEntry = null;
            if (e.Node.Tag is IndexEntry selectedEntry)
            {
                //Change
                this.selectedEntry = selectedEntry;

                //Invoke
                newTagButton.OnSelectedEntryChanged();
                tagDuplicator.OnSelectedEntryChanged();
                tagImporter.OnSelectedEntryChanged();
                tagExporter.OnSelectedEntryChanged();
            }
        }
        private void tagTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //Check
            if (e.Node.Tag is IndexEntry entry)
            {
                //Create ChunkCloner AddOn instance
                IHalo2Tool cloner = new ChunkCloner();
                cloner.Initialize(this);

                //Add
                entryEditors.Add(entry, cloner);
                editorEntryLookup.Add(cloner, entry);

                //Create form
                Form chunkClonerForm = new Form() { Icon = Properties.Resources.chunk_cloner_icon, Tag = entry, Size = new Size(720, 480), Text = e.Node.Text };
                chunkClonerForm.Controls.Add(cloner.UserInterface);
                chunkClonerForm.FormClosed += ChunkClonerForm_FormClosed;

                //Set
                chunkClonerForm.MdiParent = this;
                chunkClonerForm.Show();

                //Invoke
                cloner.OnMapLoad();
                cloner.OnSelectedEntryChanged();
            }
        }
        private void ChunkClonerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Check
            if (sender is Form chunkClonerForm && chunkClonerForm.Tag is IndexEntry entry)
            {
                //Dispose and remove
                editorEntryLookup.Remove(entryEditors[entry]);
                entryEditors[entry].Dispose();
                entryEditors.Remove(entry);

                //Dispose of form
                chunkClonerForm.Dispose();
            }
        }

        private void generateentPluginsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Initialize FolderBrowserDialog instance
            using (FolderBrowserDialog folderDlg = new FolderBrowserDialog())
            {
                //Setup
                folderDlg.Description = "Browse to the directory to generate the *.ent files.";

                //Show
                Tag.ITagGroup tagGroup = null;
                if (folderDlg.ShowDialog() == DialogResult.OK)
                {
                    //Get tag groups
                    string[] groupTags = {
                        "$#!+",
                        "*cen",
                        "*eap",
                        "*ehi",
                        "*igh",
                        "*ipd",
                        "*qip",
                        "*rea",
                        "*sce",
                        "<fx>",
                        "BooM",
                        "DECP",
                        "DECR",
                        "MGS2",
                        "PRTM",
                        "adlg",
                        "ai**",
                        "ant!",
                        "bipd",
                        "bitm",
                        "bloc",
                        "bsdt",
                        "char",
                        "cin*",
                        "clu*",
                        "clwd",
                        "coll",
                        "coln",
                        "colo",
                        "cont",
                        "crea",
                        "ctrl",
                        "dc*s",
                        "dec*",
                        "deca",
                        "devi",
                        "devo",
                        "dgr*",
                        "dobc",
                        "effe",
                        "egor",
                        "eqip",
                        "fog ",
                        "foot",
                        "fpch",
                        "garb",
                        "gldf",
                        "goof",
                        "grhi",
                        "hlmt",
                        "hmt ",
                        "hsc*",
                        "hud#",
                        "hudg",
                        "item",
                        "itmc",
                        "jmad",
                        "jpt!",
                        "lens",
                        "lifi",
                        "ligh",
                        "lsnd",
                        "ltmp",
                        "mach",
                        "matg",
                        "mdlg",
                        "metr",
                        "mode",
                        "mpdt",
                        "mply",
                        "mulg",
                        "nhdt",
                        "obje",
                        "phmo",
                        "phys",
                        "pmov",
                        "pphy",
                        "proj",
                        "prt3",
                        "sbsp",
                        "scen",
                        "scnr",
                        "sfx+",
                        "shad",
                        "sily",
                        "skin",
                        "sky ",
                        "slit",
                        "sncl",
                        "snd!",
                        "snde",
                        "snmx",
                        "spas",
                        "spk!",
                        "ssce",
                        "sslt",
                        "stem",
                        "styl",
                        "tdtl",
                        "trak",
                        "trg*",
                        "udlg",
                        "ugh!",
                        "unhi",
                        "unic",
                        "unit",
                        "vehc",
                        "vehi",
                        "vrtx",
                        "weap",
                        "weat",
                        "wgit",
                        "wgtz",
                        "whip",
                        "wigl",
                        "wind",
                        "wphi",
                    };

                    //Loop
                    foreach (string groupTag in groupTags)
                        if ((tagGroup = Abide.Tag.Cache.Generated.TagLookup.CreateTagGroup(groupTag)) != null)
                        {
                            //Get safe name and file path
                            string safeFileName = groupTag.Replace("<", "_").Replace(">", "_").Replace("?", "_").Replace("\\", "_").Replace("*", "_");
                            string entFileName = Path.Combine(folderDlg.SelectedPath, $"{safeFileName}.ent");

                            //Create stream writer
                            using (StreamWriter writer = new StreamWriter(entFileName))
                            {
                                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings() { IndentChars = "\t", Indent = true };
                                using (XmlWriter xmlWriter = XmlWriter.Create(writer, xmlWriterSettings))
                                {
                                    //Start document
                                    xmlWriter.WriteStartDocument();

                                    //Write plugin
                                    xmlWriter.WriteStartElement("plugin");

                                    //Write class
                                    xmlWriter.WriteStartAttribute("class"); xmlWriter.WriteValue(groupTag); xmlWriter.WriteEndAttribute();

                                    //Write author
                                    xmlWriter.WriteStartAttribute("author"); xmlWriter.WriteValue("Abide"); xmlWriter.WriteEndAttribute();

                                    //Write version
                                    xmlWriter.WriteStartAttribute("version"); xmlWriter.WriteValue("1.0"); xmlWriter.WriteEndAttribute();

                                    //Write headersize
                                    xmlWriter.WriteStartAttribute("headersize"); xmlWriter.WriteValue(fields_GetSize(tagGroup.SelectMany(b => b.Fields).ToArray())); xmlWriter.WriteEndAttribute();

                                    //Write tag block
                                    int offset = 0;
                                    foreach (ITagBlock tagBlock in tagGroup)
                                        tagBlock_WriteEnt(xmlWriter, tagBlock, ref offset);

                                    //End plugin
                                    xmlWriter.WriteEndElement();

                                    //End document
                                    xmlWriter.WriteEndDocument();
                                }
                            }
                        }
                }
            }
        }

        private void tagBlock_WriteEnt(XmlWriter xmlWriter, ITagBlock tagBlock, ref int offset)
        {
            //Loop
            foreach (Field field in tagBlock.Fields)
            {
                //Check
                if (field.Type == Abide.Tag.Definition.FieldType.FieldUselessPad || field.Type == Abide.Tag.Definition.FieldType.FieldExplanation) continue;

                //TODO: Generate fields

                //Increment offset
                offset += field.Size;
            }
        }

        private object fields_GetSize(Field[] fields)
        {
            int length = 0;
            foreach (Field field in fields)
                length += field.Size;
            return length;
        }
    }
}
