using Abide.HaloLibrary.IO;
using System;
using System.ComponentModel;

namespace Abide.HaloLibrary.Halo2BetaMap
{
    /// <summary>
    /// Represents a Halo 2 beta object index entry.
    /// </summary>
    [Serializable]
    public class IndexEntry : MarshalByRefObject, IDisposable
    {
        /// <summary>
        /// Gets and returns the root of the tag. 
        /// </summary>
        [Category("Object Properties"), Description("The root tag of the object.")]
        public Tag Root
        {
            get { return objectEntry.Root; }
        }
        /// <summary>
        /// Gets and returns the ID of the object entry.
        /// </summary>
        [Category("Object Properties"), Description("The Tag Identifier of the object.")]
        public TagId Id
        {
            get { return objectEntry.Id; }
        }
        /// <summary>
        /// Gets and returns the offset of the object entry's tag data.
        /// </summary>
        [Category("Object Properties"), Description("The offset of the the object tag data.")]
        public uint Offset
        {
            get { return objectEntry.TagDataOffset; }
        }
        /// <summary>
        /// Gets and returns the size of the object entry's tag data.
        /// </summary>
        [Category("Object Properties"), Description("The length of the the object tag data.")]
        public int Size
        {
            get { return (int)objectEntry.Size; }
        }
        /// <summary>
        /// Gets or sets the filename of the index entry.
        /// </summary>
        [Category("Entry Properties"), Description("The file name of the tag entry.")]
        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }
        /// <summary>
        /// Gets or sets the post-processed offset of this entry's tag data.
        /// </summary>
        [Category("Post Processed Properties"), Description("The post-processed offset of the object tag data.")]
        public int PostProcessedOffset
        {
            get { return postProcessedOffset; }
            set { postProcessedOffset = value; }
        }
        /// <summary>
        /// Gets or sets the post-processed size of this entry's tag data.
        /// </summary>
        [Category("Post Processed Properties"), Description("The post-processed length of the object tag data.")]
        public int PostProcessedSize
        {
            get { return postProcessedSize; }
            set { postProcessedSize = value; }
        }
        /// <summary>
        /// Gets or sets the filename offset of the index entry.
        /// </summary>
        [Browsable(false)]
        public uint FilenameOffset
        {
            get { return objectEntry.FileNameOffset; }
            set { objectEntry.FileNameOffset = value; }
        }
        /// <summary>
        /// Gets or sets the stream that contains this tag's data.
        /// </summary>
        [Browsable(false)]
        public FixedMemoryMappedStream TagData
        {
            get { return tagData; }
            set { tagData = value; }
        }
        /// <summary>
        /// Gets and returns a list of internal raw datas contained within this tag data. 
        /// </summary>
        [Browsable(false)]
        public RawContainer Raws
        {
            get { return raws; }
        }
        
        private readonly RawContainer raws;
        private FixedMemoryMappedStream tagData;
        private ObjectEntry objectEntry;
        private int postProcessedOffset;
        private int postProcessedSize;
        private string filename;

        /// <summary>
        /// Initializes a new <see cref="IndexEntry"/> using the supplied object entry and file name.
        /// </summary>
        /// <param name="objectEntry">The object entry for this index entry.</param>
        /// <param name="filename">The file path of this index entry.</param>
        public IndexEntry(ObjectEntry objectEntry, string filename)
        {
            //Setup
            this.objectEntry = objectEntry;
            this.filename = filename;
            raws = new RawContainer();
        }
        /// <summary>
        /// Sets the index entry's internal object entry.
        /// </summary>
        /// <param name="objectEntry">The object entry.</param>
        public void SetObjectEntry(ObjectEntry objectEntry)
        {
            this.objectEntry = objectEntry;
        }
        /// <summary>
        /// Set's the index entry's internal object entry.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="parent"></param>
        /// <param name="class"></param>
        /// <param name="id"></param>
        /// <param name="size"></param>
        /// <param name="address"></param>
        public void SetObjectEntry(Tag root, Tag parent, Tag @class, TagId id, uint size, uint address)
        {
            objectEntry = new ObjectEntry()
            {
                Root = root,
                Class = @class,
                Parent = parent,
                Id = id,
                Size = size,
                TagDataOffset = address,
                FileNameOffset = Index.IndexMemoryAddress
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
        /// Returns a string representation of this index entry.
        /// </summary>
        /// <returns>A string representation of the index entry.</returns>
        public override string ToString()
        {
            return string.Format("{0}.{1} {1}>{2}>{3} {4}", filename, objectEntry.Root, objectEntry.Parent, objectEntry.Class, objectEntry.Id);
        }
        /// <summary>
        /// Releases any resources used by the index entry.
        /// </summary>
        public void Dispose()
        {
            //Dispose
            raws.Dispose();
            tagData = null;
        }
    }
}
