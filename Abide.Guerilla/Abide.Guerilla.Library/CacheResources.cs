using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2.Retail;
using Abide.HaloLibrary.Halo2Map;
using Abide.Tag;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abide.Guerilla.Library
{
    /// <summary>
    /// Represents an object that stores references to cache file tags.
    /// </summary>
    public sealed class CacheResources : IDisposable
    {
        /// <summary>
        /// Returns the resource map file.
        /// </summary>
        public HaloMap ResourceMap { get; }
        /// <summary>
        /// Returns the globals tag group file.
        /// </summary>
        public AbideTagGroupFile Globals { get; } = new AbideTagGroupFile();
        /// <summary>
        /// Returns the sound cache file gestalt tag group file.
        /// </summary>
        public AbideTagGroupFile SoundCacheFileGestalt { get; } = new AbideTagGroupFile();
        /// <summary>
        /// Returns a list of tag resource information elements.
        /// </summary>
        public List<TagResourceInfo> TagResources { get; } = new List<TagResourceInfo>();
        /// <summary>
        /// Returns a list of tag group file elements.
        /// </summary>
        public List<AbideTagGroupFile> TagGroupFiles { get; } = new List<AbideTagGroupFile>();
        private readonly Dictionary<string, int> fileNameLookup = new Dictionary<string, int>();
        private readonly Dictionary<string, int> tagPathLookup = new Dictionary<string, int>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheResources"/> class.
        /// </summary>
        /// <param name="resourceMap">A resource map.</param>
        public CacheResources(HaloMap resourceMap)
        {
            //Set
            ResourceMap = resourceMap ?? throw new ArgumentNullException(nameof(resourceMap));
            string fileName = null;

            //Loop
            for (int i = 0; i < resourceMap.IndexEntries.Count; i++)
            {
                //Add to lookup
                tagPathLookup.Add($"{resourceMap.IndexEntries[i].Filename}.{resourceMap.IndexEntries[i].Root}", i);
                ITagGroup tagGroup = Abide.Tag.Cache.Generated.TagLookup.CreateTagGroup(resourceMap.IndexEntries[i].Root);
                fileName = $"{resourceMap.IndexEntries[i].Filename}.{tagGroup.GroupName}";
                fileNameLookup.Add(fileName, i);

                //Add resource
                TagGroupFiles.Add(new AbideTagGroupFile() { Id = resourceMap.IndexEntries[i].Id });
                TagResources.Add(new TagResourceInfo(resourceMap.IndexEntries[i].Filename, resourceMap.IndexEntries[i].Root, resourceMap.IndexEntries[i].Id)
                { FileName = fileName });
            }

            //Load globals and sound cache file gestalt
            Globals.TagGroup = new Abide.Tag.Cache.Generated.Globals();
            SoundCacheFileGestalt.TagGroup = new Abide.Tag.Cache.Generated.SoundCacheFileGestalt();
            using (var stream = resourceMap.Globals.Data.GetVirtualStream())
            using (var reader = stream.CreateReader())
            {
                //Goto globals
                Globals.Id = resourceMap.IndexEntries.First.Id;
                reader.BaseStream.Seek(resourceMap.IndexEntries.First.Address, SeekOrigin.Begin);
                Globals.TagGroup.Read(reader);

                //Goto sound cache file gestalt
                SoundCacheFileGestalt.Id = resourceMap.IndexEntries.Last.Id;
                reader.BaseStream.Seek(resourceMap.IndexEntries.Last.Address, SeekOrigin.Begin);
                SoundCacheFileGestalt.TagGroup.Read(reader);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public int FindIndex(TagResourceInfo resource)
        {
            string tagPath = $"{resource.TagPath}.{resource.Root}";
            if (tagPathLookup.ContainsKey(tagPath))
                return tagPathLookup[tagPath];
            return -1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public int FindIndex(string fileName)
        {
            if (fileNameLookup.ContainsKey(fileName))
                return fileNameLookup[fileName];
            return -1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagPath"></param>
        /// <param name="tagRoot"></param>
        /// <returns></returns>
        public int FindIndex(string tagPath, string tagRoot)
        {
            if (tagPathLookup.ContainsKey($"{tagPath}.{tagRoot}"))
                return tagPathLookup[$"{tagPath}.{tagRoot}"];
            return -1;
        }
        /// <summary>
        /// Releases all resources used by this instance.
        /// </summary>
        public void Dispose()
        {
            //Dispose
            ResourceMap.Dispose();

            //Clear
            fileNameLookup.Clear();
            tagPathLookup.Clear();
            TagResources.Clear();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class TagResourceInfo : IEquatable<TagResourceInfo>
    {
        public bool Merge { get; set; } = false;
        public TagId DestinationId { get; set; } = TagId.Null;
        public string FileName { get; set; } = string.Empty;

        public string TagPath { get; } = string.Empty;
        public TagFourCc Root { get; } = "____";
        public TagId Id { get; } = TagId.Null;

        public TagResourceInfo(string fileName, TagFourCc root, TagId id)
        {
            TagPath = fileName;
            Root = root;
            Id = id;
        }
        public override string ToString()
        {
            return $"{TagPath}.{Root.FourCc} 0x{Id}";
        }
        public bool Equals(TagResourceInfo other)
        {
            return TagPath.Equals(other.TagPath) && Root.Equals(other.Root) && Id.Equals(other.Id);
        }
    }
}
