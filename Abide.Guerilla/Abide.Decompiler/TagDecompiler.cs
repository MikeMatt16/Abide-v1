using Abide.Guerilla.Library;
using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2.Retail;
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
        private readonly List<TagId> checkedTagIds = new List<TagId>();
        private readonly ResourceManager resourceManager = new ResourceManager();
        private readonly Dictionary<string, TagId> resourceLookup = new Dictionary<string, TagId>();
        private readonly ITagGroup soundCacheFileGestalt = new SoundCacheFileGestalt();
        private CacheResources cacheResources = null;
        private HaloMap mapFile = null;

        public TagDecompiler(HaloMap map) : this()
        {
            mapFile = map ?? throw new ArgumentNullException(nameof(map));
            Map_Load(map);
        }
        
        public TagDecompiler(string fileName) : this()
        {
            //Load
            mapFile = new HaloMap(fileName ?? throw new ArgumentNullException(nameof(fileName)));
            Map_Load(mapFile);
        }

        private TagDecompiler()
        {
            resourceManager.ResolveResource += ResourceManager_ResolveResource;

            InitializeComponent();
        }

        private void Map_Load(HaloMap map)
        {
            //Load
            cacheResources = new CacheResources(mapFile);

            //Prepare
            checkedTagIds.Clear();
            resourceManager.Clear();
            resourceLookup.Clear();
            tagTreeView.BeginUpdate();
            tagTreeView.Nodes.Clear();
            tagTreeView.PathSeparator = "\\";
            
            //Loop
            foreach (IndexEntry indexEntry in map.IndexEntries)
            {
                TreeView_CreateTagNode(tagTreeView, $"{indexEntry.Filename}.{ indexEntry.Root.FourCc}", indexEntry.Id);
                ITagGroup tagGroup = TagLookup.CreateTagGroup(indexEntry.Root);
                resourceLookup.Add($"{indexEntry.Filename}.{tagGroup.GroupName}", indexEntry.Id);
            }

            //End
            tagTreeView.TreeViewNodeSorter = new TagNodeSorter();
            tagTreeView.Sort();
            tagTreeView.EndUpdate();

            //Read sound cache file gestalt
            var ugh = mapFile.IndexEntries.Last;
            using (var reader = ugh.Data.GetVirtualStream().CreateReader())
            {
                reader.BaseStream.Seek(mapFile.IndexEntries.Last.Address, SeekOrigin.Begin);
                soundCacheFileGestalt.Read(reader);
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
                if (e.Node.Checked == false) checkedTagIds.Remove(id);
                else checkedTagIds.Add(id);
            }

            //Enable or disable
            decompileButton.Enabled = checkedTagIds.Count > 0;
        }

        private void decompileButton_Click(object sender, EventArgs e)
        {
            //Loop
            foreach (TagId id in checkedTagIds)
            {
                //Get index entry
                IndexEntry entry = mapFile.IndexEntries[id];

                //Check
                if(entry != null)
                {
                    //Read
                    ITagGroup tagGroup = TagLookup.CreateTagGroup(entry.Root);
                    using (var stream = entry.Data.GetVirtualStream())
                    using (var reader = stream.CreateReader())
                    {
                        reader.BaseStream.Seek(entry.Address, SeekOrigin.Begin);
                        tagGroup.Read(reader);
                    }

                    //Collect references
                    AbideTagGroupFile tagGroupFile = new AbideTagGroupFile() { TagGroup = Convert.ToGuerilla(tagGroup, soundCacheFileGestalt, entry, mapFile) };
                    resourceManager.CollectResources(tagGroupFile.TagGroup);

                    //Get file name
                    string fileName = Path.Combine(RegistrySettings.WorkspaceDirectory, "tags", $"{entry.Filename}.{tagGroup.GroupName}");

                    //Create directory?
                    if (!Directory.Exists(Path.GetDirectoryName(fileName)))
                        Directory.CreateDirectory(Path.GetDirectoryName(fileName));

                    //Save tag file
                    tagGroupFile.Save(fileName);
                }
            }

            //Decompile resources
            foreach (string resourceString in resourceManager.GetResources())
            {
                //Find entry
                IndexEntry entry = mapFile.IndexEntries[cacheResources.FindIndex(resourceString)];

                //Check
                if (entry != null)
                {
                    //Read
                    ITagGroup tagGroup = TagLookup.CreateTagGroup(entry.Root);
                    using (var stream = entry.Data.GetVirtualStream())
                    using (var reader = stream.CreateReader())
                    {
                        reader.BaseStream.Seek(entry.Address, SeekOrigin.Begin);
                        tagGroup.Read(reader);
                    }

                    //Collect references
                    AbideTagGroupFile tagGroupFile = new AbideTagGroupFile() { TagGroup = Convert.ToGuerilla(tagGroup, soundCacheFileGestalt, entry, mapFile) };

                    //Get file name
                    string fileName = Path.Combine(RegistrySettings.WorkspaceDirectory, "tags", $"{entry.Filename}.{tagGroup.GroupName}");

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
            IndexEntry entry = mapFile.IndexEntries[resourceLookup[resourceName]];
            ITagGroup tagGroup = TagLookup.CreateTagGroup(entry.Root);

            //Read
            using (BinaryReader reader = entry.Data.GetVirtualStream().CreateReader())
            {
                //Read
                reader.BaseStream.Seek(entry.Address, SeekOrigin.Begin);
                tagGroup.Read(reader);
            }

            //Return
            return Convert.ToGuerilla(tagGroup, soundCacheFileGestalt, entry, mapFile);
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
