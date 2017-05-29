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
        /// Gets and returns the root of the tag. 
        /// </summary>
        [Category("Object Properties"), Description("The root tag of the object.")]
        public TAG Root
        {
            get { return tagHierarchy.Root; }
        }
        /// <summary>
        /// Gets and returns the ID of the object entry.
        /// </summary>
        [Category("Object Properties"), Description("The Tag Identifier of the object.")]
        public TAGID ID
        {
            get { return objectEntry.ID; }
        }
        /// <summary>
        /// Gets and returns the offset of the object entry's tag data.
        /// </summary>
        [Category("Object Properties"), Description("The offset of the the object tag data.")]
        public uint Offset
        {
            get { return objectEntry.Offset; }
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
        /// <summary>
        /// Gets and returns a list of localized strings contained within this tag data.
        /// </summary>
        [Browsable(false)]
        public StringContainer Strings
        {
            get { return strings; }
        }

        private FixedMemoryMappedStream tagData;
        private readonly StringContainer strings;
        private readonly RawContainer raws;
        private readonly TAGHIERARCHY tagHierarchy;
        private readonly OBJECT objectEntry;
        private int postProcessedOffset;
        private int postProcessedSize;
        private string filename;

        /// <summary>
        /// Initializes a new <see cref="IndexEntry"/> using the supplied object entry, file name, and tag hierarchy.
        /// </summary>
        /// <param name="objectEntry">The object entry for this index entry.</param>
        /// <param name="filename">The file path of this index entry.</param>
        /// <param name="tagHierarchy">The tag hierarchy for this index entry.</param>
        public IndexEntry(OBJECT objectEntry, string filename, TAGHIERARCHY tagHierarchy)
        {
            //Setup
            this.tagHierarchy = tagHierarchy;
            this.objectEntry = objectEntry;
            this.filename = filename;
            raws = new RawContainer();
            strings = new StringContainer();
        }
        /// <summary>
        /// Returns this index entry's index object entry.
        /// </summary>
        /// <returns>This index entry's object entry.</returns>
        public OBJECT GetObjectEntry()
        {
            return objectEntry;
        }
        /// <summary>
        /// Returns this index entry's tag hierarchy.
        /// </summary>
        /// <returns>This index entry's tag hierarchy.</returns>
        public TAGHIERARCHY GetTagHierarchy()
        {
            return tagHierarchy;
        }
        /// <summary>
        /// Returns a string representation of this index entry.
        /// </summary>
        /// <returns>A string representation of the index entry.</returns>
        public override string ToString()
        {
            return string.Format("{0}.{1} {2} {3}", filename, tagHierarchy.Root, tagHierarchy, objectEntry.ID);
        }
        /// <summary>
        /// Releases any resources used by the index entry.
        /// </summary>
        public void Dispose()
        {
            //Dispose
            raws.Dispose();
            strings.Dispose();
        }
    }
}
