using Abide.Guerilla.Library;
using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using Abide.Tag;
using Abide.Tag.Cache.Generated;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Convert = Abide.Guerilla.Library.Convert;

namespace Abide.Decompiler
{
    public partial class TagDecompiler : Form
    {
        private readonly MapFile m_MapFile = new MapFile();
        private readonly List<TagId> m_CheckedTagIds = new List<TagId>();
        private readonly ResourceManager m_ResourceManager = new ResourceManager();
        private readonly Dictionary<string, TagId> m_ResourceLookup = new Dictionary<string, TagId>();
        private readonly ITagGroup m_SoundCacheFileGestalt = new SoundCacheFileGestalt();
        private CacheResources m_CacheResources = null;

        public TagDecompiler(MapFile mapFile) : this()
        {
            m_MapFile = mapFile ?? throw new ArgumentNullException(nameof(mapFile));
            Map_Load(m_MapFile);
        }
        
        public TagDecompiler(string fileName) : this()
        {
            //Load
            m_MapFile.Load(fileName ?? throw new ArgumentNullException(nameof(fileName)));
            Map_Load(m_MapFile);
        }

        private TagDecompiler()
        {
            m_ResourceManager.ResolveResource += ResourceManager_ResolveResource;

            InitializeComponent();
        }

        private void Map_Load(MapFile map)
        {
            //Load
            m_CacheResources = new CacheResources(m_MapFile);

            //Prepare
            m_CheckedTagIds.Clear();
            m_ResourceManager.Clear();
            m_ResourceLookup.Clear();
            tagTreeView.BeginUpdate();
            tagTreeView.Nodes.Clear();
            tagTreeView.PathSeparator = "\\";
            
            //Loop
            foreach (IndexEntry indexEntry in map.IndexEntries)
            {
                TreeView_CreateTagNode(tagTreeView, $"{indexEntry.Filename}.{ indexEntry.Root.FourCc}", indexEntry.Id);
                ITagGroup tagGroup = TagLookup.CreateTagGroup(indexEntry.Root);
                m_ResourceLookup.Add($"{indexEntry.Filename}.{tagGroup.Name}", indexEntry.Id);
            }

            //End
            tagTreeView.TreeViewNodeSorter = new TagNodeSorter();
            tagTreeView.Sort();
            tagTreeView.EndUpdate();

            //Read sound cache file gestalt
            using (BinaryReader reader = m_MapFile.TagDataStream.CreateReader())
            {
                reader.BaseStream.Seek(m_MapFile.IndexEntries.Last.Offset, SeekOrigin.Begin);
                m_SoundCacheFileGestalt.Read(reader);
            }
        }

        private void TreeView_CreateTagNode(TreeView treeView, string fileName, TagId id)
        {
            //Split
            TreeNode node = null;
            string[] parts = fileName.Split('\\');
            TreeNodeCollection collection = treeView.Nodes;
            string lastPart = parts.Last();

            //Loop
            for (int i = 0; i < parts.Length - 1; i++)
            {
                //Check
                if (!collection.ContainsKey(parts[i]))
                    node = collection.Add(parts[i], parts[i], 0);
                else node = collection[parts[i]];

                //Get nodes
                collection = node.Nodes;
            }

            //Add file node
            node = collection.Add(lastPart, lastPart, 1);
            node.Tag = id;
        }

        private void tagTreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //Check
            if(e.Node.Tag is TagId id)
            {
                if (e.Node.Checked == false) m_CheckedTagIds.Remove(id);
                else m_CheckedTagIds.Add(id);
            }

            //Enable or disable
            decompileButton.Enabled = m_CheckedTagIds.Count > 0;
        }

        private void decompileButton_Click(object sender, EventArgs e)
        {
            //Loop
            foreach (TagId id in m_CheckedTagIds)
            {
                //Get index entry
                IndexEntry entry = m_MapFile.IndexEntries[id];

                //Check
                if(entry != null)
                {
                    //Read
                    ITagGroup tagGroup = TagLookup.CreateTagGroup(entry.Root);
                    using (BinaryReader reader = entry.TagData.CreateReader())
                    {
                        reader.BaseStream.Seek((uint)entry.PostProcessedOffset, SeekOrigin.Begin);
                        tagGroup.Read(reader);
                    }

                    //Collect references
                    AbideTagGroupFile tagGroupFile = new AbideTagGroupFile() { TagGroup = Convert.ToGuerilla(tagGroup, m_SoundCacheFileGestalt, entry, m_MapFile) };
                    m_ResourceManager.CollectResources(tagGroupFile.TagGroup);

                    //Get file name
                    string fileName = Path.Combine(RegistrySettings.WorkspaceDirectory, "tags", $"{entry.Filename}.{tagGroup.Name}");

                    //Create directory?
                    if (!Directory.Exists(Path.GetDirectoryName(fileName)))
                        Directory.CreateDirectory(Path.GetDirectoryName(fileName));

                    //Save tag file
                    tagGroupFile.Save(fileName);
                }
            }

            //Decompile resources
            foreach (string resourceString in m_ResourceManager.GetResources())
            {
                //Find entry
                IndexEntry entry = m_MapFile.IndexEntries[m_CacheResources.FindIndex(resourceString)];

                //Check
                if (entry != null)
                {
                    //Read
                    ITagGroup tagGroup = TagLookup.CreateTagGroup(entry.Root);
                    using (BinaryReader reader = entry.TagData.CreateReader())
                    {
                        reader.BaseStream.Seek((uint)entry.PostProcessedOffset, SeekOrigin.Begin);
                        tagGroup.Read(reader);
                    }

                    //Collect references
                    AbideTagGroupFile tagGroupFile = new AbideTagGroupFile() { TagGroup = Convert.ToGuerilla(tagGroup, m_SoundCacheFileGestalt, entry, m_MapFile) };

                    //Get file name
                    string fileName = Path.Combine(RegistrySettings.WorkspaceDirectory, "tags", $"{entry.Filename}.{tagGroup.Name}");

                    //Create directory?
                    if (!Directory.Exists(Path.GetDirectoryName(fileName)))
                        Directory.CreateDirectory(Path.GetDirectoryName(fileName));

                    //Save tag file
                    tagGroupFile.Save(fileName);
                }
            }
        }

        private ITagGroup ResourceManager_ResolveResource(string resourceName)
        {
            //Prepare
            IndexEntry entry = m_MapFile.IndexEntries[m_ResourceLookup[resourceName]];
            ITagGroup tagGroup = TagLookup.CreateTagGroup(entry.Root);

            //Read
            using (BinaryReader reader = entry.TagData.CreateReader())
            {
                //Read
                reader.BaseStream.Seek((uint)entry.PostProcessedOffset, SeekOrigin.Begin);
                tagGroup.Read(reader);
            }

            //Return
            return Convert.ToGuerilla(tagGroup, m_SoundCacheFileGestalt, entry, m_MapFile);
        }

        private class TagNodeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x is TreeNode n1 && y is TreeNode n2)
                    return CompareNodes(n1, n2);
                return 0;
            }

            private int CompareNodes(TreeNode n1, TreeNode n2)
            {
                if (n1.Tag == null && n2.Tag != null) return -1;
                else if (n1.Tag != null && n2.Tag == null) return 1;
                else return n1.Name.CompareTo(n2.Name);
            }
        }
    }
}
