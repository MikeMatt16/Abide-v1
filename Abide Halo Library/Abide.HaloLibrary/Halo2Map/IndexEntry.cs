using Abide.HaloLibrary.IO;
using System;
using System.ComponentModel;

namespace Abide.HaloLibrary.Halo2Map
{
    /// <summary>
    /// Represents a Halo 2 object index entry.
    /// </summary>
    [Serializable]
    public sealed class IndexEntry : MarshalByRefObject, IDisposable
    {
        /// <summary>
        /// Gets and returns the owner of this index entry.
        /// </summary>
        public MapFile Owner
        {
            get { return owner; }
            internal set { owner = value; }
        }
        /// <summary>
        /// Gets and returns the root of the tag. 
        /// </summary>
        [Category("Object Properties"), Description("The root tag of the object.")]
        public TagFourCc Root
        {
            get { return tagHierarchy.Root; }
        }
        /// <summary>
        /// Gets or sets returns the ID of the object entry.
        /// </summary>
        [Category("Object Properties"), Description("The Tag Identifier of the object.")]
        public TagId Id
        {
            get { return objectEntry.Id; }
            set { objectEntry.Id = value; }
        }
        /// <summary>
        /// Gets or sets the offset of the object entry's tag data.
        /// </summary>
        [Category("Object Properties"), Description("The offset of the the object tag data.")]
        public uint Offset
        {
            get { return objectEntry.Offset; }
            set { objectEntry.Offset = value; }
        }
        /// <summary>
        /// Gets or sets returns the size of the object entry's tag data.
        /// </summary>
        [Category("Object Properties"), Description("The length of the the object tag data.")]
        public int Size
        {
            get { return (int)objectEntry.Size; }
            set { objectEntry.Size = (uint)value; }
        }
        /// <summary>
        /// Gets or sets the filename of the index entry.
        /// </summary>
        [Category("Entry Properties"), Description("The file name of the tag entry.")]
        public string Filename { get; set; }
        /// <summary>
        /// Gets or sets the post-processed offset of this entry's tag data.
        /// </summary>
        [Category("Post Processed Properties"), Description("The post-processed offset of the object tag data.")]
        public int PostProcessedOffset { get; set; }
        /// <summary>
        /// Gets or sets the post-processed size of this entry's tag data.
        /// </summary>
        [Category("Post Processed Properties"), Description("The post-processed length of the object tag data.")]
        public int PostProcessedSize { get; set; }
        /// <summary>
        /// Gets or sets the stream that contains this tag's data.
        /// </summary>
        [Browsable(false)]
        public VirtualStream TagData { get; set; }
        /// <summary>
        /// Gets and returns a list of internal raw datas contained within this tag data. 
        /// </summary>
        [Browsable(false)]
        public RawContainer Raws { get; }
        /// <summary>
        /// Gets and returns a list of localized strings contained within this tag data.
        /// </summary>
        [Browsable(false)]
        public StringContainer Strings { get; }

        private MapFile owner;
        private TagHierarchy tagHierarchy;
        private ObjectEntry objectEntry;

        /// <summary>
        /// Initializes a new <see cref="IndexEntry"/> using the supplied object entry, file name, and tag hierarchy.
        /// </summary>
        /// <param name="objectEntry">The object entry for this index entry.</param>
        /// <param name="filename">The file path of this index entry.</param>
        /// <param name="tagHierarchy">The tag hierarchy for this index entry.</param>
        public IndexEntry(ObjectEntry objectEntry, string filename, TagHierarchy tagHierarchy)
        {
            //Setup
            this.tagHierarchy = tagHierarchy;
            this.objectEntry = objectEntry;
            Filename = filename;
            Raws = new RawContainer();
            Strings = new StringContainer();
        }
        /// <summary>
        /// Sets the index entry's internal tag hierarchy.
        /// </summary>
        /// <param name="tagHierarchy">The tag hierarchy value.</param>
        public void SetTagHeirarchy(TagHierarchy tagHierarchy)
        {
            this.tagHierarchy = tagHierarchy;
        }
        /// <summary>
        /// Sets the index entry's internal object entry.
        /// </summary>
        /// <param name="objectEntry">The object entry value.</param>
        public void SetObjectEntry(ObjectEntry objectEntry)
        {
            this.objectEntry = objectEntry;
        }
        /// <summary>
        /// Sets the index entry's internal object entry.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="id">The tag identifier.</param>
        /// <param name="size">The length of the data.</param>
        /// <param name="address">The memory address of the data.</param>
        public void SetObjectEntry(TagFourCc tag, TagId id, uint size, uint address)
        {
            objectEntry = new ObjectEntry()
            {
                Tag = tag,
                Id = id,
                Size = size,
                Offset = address
            };
        }
        /// <summary>
        /// Returns this index entry's index object entry.
        /// </summary>
        /// <returns>This index entry's object entry.</returns>
        public ObjectEntry GetObjectEntry()
        {
            return objectEntry;
        }
        /// <summary>
        /// Returns this index entry's tag hierarchy.
        /// </summary>
        /// <returns>This index entry's tag hierarchy.</returns>
        public TagHierarchy GetTagHierarchy()
        {
            return tagHierarchy;
        }
        /// <summary>
        /// Returns a string representation of this index entry.
        /// </summary>
        /// <returns>A string representation of the index entry.</returns>
        public override string ToString()
        {
            return string.Format("{0}.{1} {2} {3}", Filename, tagHierarchy.Root, tagHierarchy, objectEntry.Id);
        }
        /// <summary>
        /// Releases any resources used by the index entry.
        /// </summary>
        public void Dispose()
        {
            Raws.Dispose();
            Strings.Clear();
            TagData = null;
        }
    }
}
