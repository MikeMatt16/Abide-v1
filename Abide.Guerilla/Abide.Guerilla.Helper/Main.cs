using Abide.Guerilla.Library;
using Abide.HaloLibrary.Halo2Map;
using Abide.Tag;
using Abide.Tag.Cache.Generated;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Abide.Guerilla.Helper
{
    public partial class Main : Form
    {
        private MapFile m_MapFile = null;
        private SharedTagResources sharedResources = null;

        public Main()
        {
            InitializeComponent();
        }

        private void mapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check
            if (sender is ToolStripItem mapItem)
            {
                //Open
                m_MapFile = new MapFile();
                m_MapFile.Load(mapItem.Name);

                //Load
                listBox1.Items.Clear();
                foreach (IndexEntry entry in m_MapFile.IndexEntries)
                    listBox1.Items.Add($"{entry.Id} {entry.Filename}.{entry.Root}");

                //Set
                tagCountToolStripTextBox.Text = $"{m_MapFile.IndexEntries.Count} tags";

                //Dispose
                m_MapFile.Dispose();
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            foreach (string file in Directory.GetFiles(@"F:\XBox\Original\Games\Halo 2\Clean Maps", "*.map"))
            {
                ToolStripItem mapItem = openToolStripMenuItem.DropDownItems.Add(Path.GetFileName(file));
                mapItem.Click += mapToolStripMenuItem_Click;
                mapItem.Name = file;
            }
        }

        private void analyzeMapsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Prepare
            ITagGroup tagGroup = null;
            sharedResources = new SharedTagResources();
            List<string> header = new List<string>();
            DateTime start = DateTime.Now;

            //Add
            header.Add("Abide Guerilla Analyzer");
            header.Add(string.Empty);
            
            //Loop
            foreach (string file in Directory.GetFiles(@"F:\XBox\Original\Games\Halo 2\Clean Maps", "*.map"))
                using (MapFile map = new MapFile())
                {
                    //Load
                    start = DateTime.Now;
                    map.Load(file);

                    //Check type
                    using (var reader = map.Scenario.TagData.CreateReader())
                    {
                        reader.BaseStream.Seek(map.Scenario.Offset + 16, SeekOrigin.Begin);
                        short mapType = reader.ReadInt16();
                        switch (mapType)
                        {
                            case 0:
                                sharedResources.CollideSingleplayer(map.IndexEntries.Select(
                                    i => { tagGroup = TagLookup.CreateTagGroup(i.Root); return $"{i.Filename}.{tagGroup.GroupName}"; }).ToArray());
                                break;
                            case 1:
                                sharedResources.CollideMultiplayer(map.IndexEntries.Select(
                                    i => { tagGroup = TagLookup.CreateTagGroup(i.Root); return $"{i.Filename}.{tagGroup.GroupName}"; }).ToArray());
                                break;
                        }
                    }

                    //Add
                    header.Add($"{file} loaded in {(DateTime.Now - start).TotalSeconds} seconds");
                }

            //Create file
            using (StreamWriter writer = File.CreateText(Path.Combine(Application.StartupPath, "resource analysis.log")))
            {
                //Write header
                foreach (string line in header)
                    writer.WriteLine(line);

                //Write line
                writer.WriteLine();
                writer.WriteLine();
                
                //Write ui resources
                writer.WriteLine($"ui shared resources (count {sharedResources.UiSharedResourceCount}):");
                foreach (string resource in sharedResources.GetUiResources())
                    writer.WriteLine($"@\"{resource}\",");

                //Write line
                writer.WriteLine();
                writer.WriteLine();

                //Write multiplayer resources
                writer.WriteLine($"multiplayer shared resources (count {sharedResources.MultiplayerSharedResourceCount}):");
                foreach (string resource in sharedResources.GetMultiplayerSharedResources())
                    writer.WriteLine($"@\"{resource}\",");

                //Write line
                writer.WriteLine();
                writer.WriteLine();
                
                //Write singleplayer resources
                writer.WriteLine($"singleplayer shared resources (count {sharedResources.SingleplayerSharedResourceCount}):");
                foreach (string resource in sharedResources.GetSingleplayerSharedResources())
                    writer.WriteLine($"@\"{resource}\",");
            }
        }

        private void analyzeTagsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Prepare
            List<TagHierarchy> tags = new List<TagHierarchy>();

            //Load
            using (MapFile shared = new MapFile())
            {
                //Load
                shared.Load(RegistrySettings.SharedFileName);

                //Merge tags
                tags.AddRange(shared.Tags);
            }

            //Create file
            using (StreamWriter writer = File.CreateText(Path.Combine(Application.StartupPath, "tag analysis.log")))
            {
                //Header
                writer.WriteLine("Abide Guerilla Analyzer");
                writer.WriteLine(string.Empty);

                //Write
                writer.WriteLine($"tags (count {tags.Count})");
                foreach (var tag in tags)
                    writer.WriteLine($"new TagHierarcy(\"{tag.Root.FourCc.Trim('\0').Trim('\uffff')}\", \"{tag.Parent.FourCc.Trim('\0').Trim('\uffff')}\", \"{tag.Class.FourCc.Trim('\0').Trim('\uffff')}\"),");
            }
        }

        private string[] Directory_Scan(string directoryName)
        {
            //Prepare
            List<string> files = new List<string>();

            //Loop through files and directories
            foreach (string file in Directory.GetFiles(directoryName)) files.Add(file);
            foreach (string nestedDirectoryName in Directory.GetDirectories(directoryName))
                files.AddRange(Directory_Scan(nestedDirectoryName));

            //Return
            return files.ToArray();
        }
        
        private class SharedTagResources
        {
            public int UiSharedResourceCount
            {
                get { return m_UiSharedResources.Count; }
            }
            public int MultiplayerSharedResourceCount
            {
                get { return m_MultiplayerSharedResources.Count; }
            }
            public int SingleplayerSharedResourceCount
            {
                get { return m_SingleplayerSharedResources.Count; }
            }

            private List<string> m_UiSharedResources = new List<string>();
            private List<string> m_MultiplayerSharedResources = new List<string>();
            private List<string> m_SingleplayerSharedResources = new List<string>();

            public SharedTagResources()
            {
                //Prepare
                MapFile resourceMap = null;
                ITagGroup tagGroup = null;

                //Gather ui resources
                using (resourceMap = new MapFile())
                {
                    //Load
                    resourceMap.Load(RegistrySettings.MainmenuFileName);

                    //Get tags
                    m_UiSharedResources.AddRange(resourceMap.IndexEntries.Select(e => { tagGroup = TagLookup.CreateTagGroup(e.Root); return $"{e.Filename}.{tagGroup.GroupName}"; }));
                }

                //Gather multiplayer shared resources
                using (resourceMap = new MapFile())
                {
                    //Load
                    resourceMap.Load(RegistrySettings.SharedFileName);

                    //Get tags
                    m_MultiplayerSharedResources.AddRange(resourceMap.IndexEntries.Select(e => { tagGroup = TagLookup.CreateTagGroup(e.Root); return $"{e.Filename}.{tagGroup.GroupName}"; }));
                }

                //Gather singleplayer shared resources
                using (resourceMap = new MapFile())
                {
                    //Load
                    resourceMap.Load(RegistrySettings.SharedFileName);

                    //Get tags
                    m_SingleplayerSharedResources.AddRange(resourceMap.IndexEntries.Select(e => { tagGroup = TagLookup.CreateTagGroup(e.Root); return $"{e.Filename}.{tagGroup.GroupName}"; }));
                }
            }
            public void CollideMultiplayer(string[] resources)
            {
                m_UiSharedResources = m_UiSharedResources.Intersect(resources).ToList();
                m_MultiplayerSharedResources = m_UiSharedResources.Intersect(resources).ToList();
            }
            public void CollideSingleplayer(string[] resources)
            {
                m_UiSharedResources = m_UiSharedResources.Intersect(resources).ToList();
                m_SingleplayerSharedResources = m_SingleplayerSharedResources.Intersect(resources).ToList();
            }
            public string[] GetUiResources()
            {
                return m_UiSharedResources.ToArray();
            }
            public string[] GetMultiplayerSharedResources()
            {
                return m_MultiplayerSharedResources.ToArray();
            }
            public string[] GetSingleplayerSharedResources()
            {
                return m_SingleplayerSharedResources.ToArray();
            }
        }
    }
}
