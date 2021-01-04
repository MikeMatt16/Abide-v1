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
        private IHalo2MenuButton fixSystemLink = null;

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

            //
            // fixSystemLink
            //
            fixSystemLink = new FixSystemLinkButton();
            fixSystemLink.Initialize(this);
            fixSystemLink.OnMapLoad();
            ToolStripItem fixSystemLinkToolStripMenuButton = mainMenuStrip.Items.Add(fixSystemLink.Name, fixSystemLink.Icon);
            fixSystemLinkToolStripMenuButton.Click += FixSystemLinkToolStripMenuButton_Click;
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
        private void FixSystemLinkToolStripMenuButton_Click(object sender, EventArgs e)
        {
            if (map == null) return;
            fixSystemLink.OnClick();
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
#if DEBUG
                        catch { throw; }
#else
                        catch { MessageBox.Show("Failed to load map.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error); success = false; }
#endif

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
                ITagGroup tagGroup = null;
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
                                    xmlWriter.WriteStartAttribute("headersize"); xmlWriter.WriteValue(fields_GetSize(tagGroup.SelectMany(b=>b).ToArray())); xmlWriter.WriteEndAttribute();

                                    //Write revision
                                    xmlWriter.WriteStartElement("revision");
                                    xmlWriter.WriteStartAttribute("author"); xmlWriter.WriteValue("Abide"); xmlWriter.WriteEndAttribute();
                                    xmlWriter.WriteStartAttribute("version"); xmlWriter.WriteValue("1.0"); xmlWriter.WriteEndAttribute();
                                    xmlWriter.WriteValue("This code is generated by a tool.");
                                    xmlWriter.WriteEndElement();

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
            foreach (var field in tagBlock)
            {
                //Check
                if (field.Type == Abide.Tag.Definition.FieldType.FieldUselessPad || field.Type == Abide.Tag.Definition.FieldType.FieldExplanation) continue;

                //TODO: Generate fields
                switch (field.Type)
                {
                    case Abide.Tag.Definition.FieldType.FieldString:
                        xmlWriter.WriteStartElement("string32");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        break;
                    case Abide.Tag.Definition.FieldType.FieldLongString:
                        xmlWriter.WriteStartElement("string256");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();

                        break;
                    case Abide.Tag.Definition.FieldType.FieldStringId:
                    case Abide.Tag.Definition.FieldType.FieldOldStringId:
                        xmlWriter.WriteStartElement("stringid");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();

                        break;
                    case Abide.Tag.Definition.FieldType.FieldCharInteger:
                        xmlWriter.WriteStartElement("byte");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        break;
                    case Abide.Tag.Definition.FieldType.FieldShortInteger:
                        xmlWriter.WriteStartElement("short");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        break;
                    case Abide.Tag.Definition.FieldType.FieldLongInteger:
                        xmlWriter.WriteStartElement("int");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        break;
                    case Abide.Tag.Definition.FieldType.FieldAngle:
                        xmlWriter.WriteStartElement("float");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        break;
                    case Abide.Tag.Definition.FieldType.FieldTag:
                        xmlWriter.WriteStartElement("tag");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        break;
                    case Abide.Tag.Definition.FieldType.FieldCharEnum:
                        xmlWriter.WriteStartElement("byte");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name + " (ENUM)");
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        break;
                    case Abide.Tag.Definition.FieldType.FieldEnum:
                        xmlWriter.WriteStartElement("short");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name + " (ENUM)");
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        break;
                    case Abide.Tag.Definition.FieldType.FieldLongEnum:
                        xmlWriter.WriteStartElement("int");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name + " (ENUM)");
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        break;
                    case Abide.Tag.Definition.FieldType.FieldLongFlags:
                        xmlWriter.WriteStartElement("int");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name + " (FLAGS)");
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        break;
                    case Abide.Tag.Definition.FieldType.FieldWordFlags:
                        xmlWriter.WriteStartElement("short");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name + " (FLAGS)");
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        break;
                    case Abide.Tag.Definition.FieldType.FieldByteFlags:
                        xmlWriter.WriteStartElement("byte");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name + " (FLAGS)");
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        break;
                    case Abide.Tag.Definition.FieldType.FieldPoint2D:

                        break;
                    case Abide.Tag.Definition.FieldType.FieldRectangle2D:
                        break;
                    case Abide.Tag.Definition.FieldType.FieldRgbColor:
                        xmlWriter.WriteStartElement("byte");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name + " R");
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("byte");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset + 1);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name + " G");
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("byte");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset + 2);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name + " B");
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        break;
                    case Abide.Tag.Definition.FieldType.FieldArgbColor:
                        xmlWriter.WriteStartElement("byte");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name + " A");
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("byte");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset + 1);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name + " R");
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("byte");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset + 2);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name + " G");
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("byte");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset + 3);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name + " B");
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        break;
                    case Abide.Tag.Definition.FieldType.FieldReal:
                        xmlWriter.WriteStartElement("float");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        break;
                    case Abide.Tag.Definition.FieldType.FieldRealFraction:
                        xmlWriter.WriteStartElement("float");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        break;
                    case Abide.Tag.Definition.FieldType.FieldRealPoint2D:
                        xmlWriter.WriteStartElement("float");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name + " X");
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("float");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset+4);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name + " Y");
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        break;
                    case Abide.Tag.Definition.FieldType.FieldRealPoint3D:
                        xmlWriter.WriteStartElement("float");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name + " X");
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("float");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset + 4);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name + " Y");
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("float");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset + 8);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name + " Z");
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        break;
                    case Abide.Tag.Definition.FieldType.FieldRealVector2D:
                        xmlWriter.WriteStartElement("float");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name + " i");
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("float");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset + 4);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name + " j");
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        break;
                    case Abide.Tag.Definition.FieldType.FieldRealVector3D:
                        xmlWriter.WriteStartElement("float");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name + " i");
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("float");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset + 4);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name + " j");
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("float");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset + 8);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name + " k");
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        break;
                    case Abide.Tag.Definition.FieldType.FieldQuaternion:
                        xmlWriter.WriteStartElement("float");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name + " w");
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("float");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset + 4);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name + " i");
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("float");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset + 8);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name + " j");
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("float");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset + 12);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name + " k");
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        break;
                    case Abide.Tag.Definition.FieldType.FieldEulerAngles2D:
                        xmlWriter.WriteStartElement("float");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name + " yaw");
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("float");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset + 4);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name + " pitch");
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        break;
                    case Abide.Tag.Definition.FieldType.FieldEulerAngles3D:
                        xmlWriter.WriteStartElement("float");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name + " yaw");
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("float");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset + 4);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name + " pitch");
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("float");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset + 8);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name + " roll");
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        break;
                    case Abide.Tag.Definition.FieldType.FieldRealPlane2D:
                        break;
                    case Abide.Tag.Definition.FieldType.FieldRealPlane3D:
                        break;
                    case Abide.Tag.Definition.FieldType.FieldRealRgbColor:
                        break;
                    case Abide.Tag.Definition.FieldType.FieldRealArgbColor:
                        break;
                    case Abide.Tag.Definition.FieldType.FieldRealHsvColor:
                        break;
                    case Abide.Tag.Definition.FieldType.FieldRealAhsvColor:
                        break;
                    case Abide.Tag.Definition.FieldType.FieldShortBounds:
                        break;
                    case Abide.Tag.Definition.FieldType.FieldAngleBounds:
                        break;
                    case Abide.Tag.Definition.FieldType.FieldRealBounds:
                        break;
                    case Abide.Tag.Definition.FieldType.FieldRealFractionBounds:
                        break;

                    case Abide.Tag.Definition.FieldType.FieldTagReference:
                        xmlWriter.WriteStartElement("tag");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("id");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset + 4);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        break;

                    case Abide.Tag.Definition.FieldType.FieldBlock:
                        BlockField BlockField = (BlockField)field;
                        ITagBlock block = BlockField.Add(out bool created);

                        if (block != null)
                        {
                            xmlWriter.WriteStartElement("struct");

                            xmlWriter.WriteStartAttribute("offset");
                            xmlWriter.WriteValue(offset);
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute("name");
                            xmlWriter.WriteValue(field.Name);
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute("size");
                            xmlWriter.WriteValue(block.Size);
                            xmlWriter.WriteEndAttribute();

                            string labelName = string.Empty;
                            if (block.Any(f => f.IsBlockName)) labelName = block.First(f => f.IsBlockName).Name;
                            xmlWriter.WriteStartAttribute("label");
                            xmlWriter.WriteValue(labelName);
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute("visible");
                            xmlWriter.WriteValue(true);
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute("maxelements");
                            xmlWriter.WriteValue(BlockField.BlockList.MaximumCount);
                            xmlWriter.WriteEndAttribute();

                            int blockOffset = 0;
                            tagBlock_WriteEnt(xmlWriter, block, ref blockOffset);

                            xmlWriter.WriteEndElement();
                        }
                        break;

                    case Abide.Tag.Definition.FieldType.FieldLongBlockFlags:
                        break;
                    case Abide.Tag.Definition.FieldType.FieldWordBlockFlags:
                        break;
                    case Abide.Tag.Definition.FieldType.FieldByteBlockFlags:
                        break;

                    case Abide.Tag.Definition.FieldType.FieldCharBlockIndex1:
                    case Abide.Tag.Definition.FieldType.FieldCharBlockIndex2:
                        break;

                    case Abide.Tag.Definition.FieldType.FieldShortBlockIndex1:
                    case Abide.Tag.Definition.FieldType.FieldShortBlockIndex2:
                        break;

                    case Abide.Tag.Definition.FieldType.FieldLongBlockIndex1:
                    case Abide.Tag.Definition.FieldType.FieldLongBlockIndex2:
                        break;
                    case Abide.Tag.Definition.FieldType.FieldData:
                        DataField dataField = (DataField)field;
                        xmlWriter.WriteStartElement("unused");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("size");
                        xmlWriter.WriteValue(field.Size);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        break;

                    case Abide.Tag.Definition.FieldType.FieldVertexBuffer:
                        break;
                    case Abide.Tag.Definition.FieldType.FieldArrayStart:
                        break;
                    case Abide.Tag.Definition.FieldType.FieldArrayEnd:
                        break;

                    case Abide.Tag.Definition.FieldType.FieldPad:
                    case Abide.Tag.Definition.FieldType.FieldSkip:
                        xmlWriter.WriteStartElement("unused");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("size");
                        xmlWriter.WriteValue(field.Size);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        break;

                    case Abide.Tag.Definition.FieldType.FieldStruct:
                        break;
                    case Abide.Tag.Definition.FieldType.FieldTagIndex:
                        xmlWriter.WriteStartElement("id");

                        xmlWriter.WriteStartAttribute("offset");
                        xmlWriter.WriteValue(offset);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(field.Name);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("visible");
                        xmlWriter.WriteValue(true);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                        break;
                }

                //Increment offset
                offset += field.Size;
            }
        }

        private object fields_GetSize(ITagField[] fields)
        {
            return fields?.Sum(f => f.Size) ?? 0;
        }
    }
}
