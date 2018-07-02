using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using HaloTag = Abide.HaloLibrary.TagFourCc;

namespace Abide.Tag
{
    /// <summary>
    /// Represents a tag group.
    /// </summary>
    public abstract class Group : ITagGroup
    {
        /// <summary>
        /// Gets and returns the number of tag blocks within the tag group.
        /// </summary>
        public int Count => TagBlocks.Count;
        /// <summary>
        /// Gets and returns the name of the tag group.
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// Gets and returns the group tag of the tag group.
        /// </summary>
        public abstract HaloTag GroupTag { get; }
        /// <summary>
        /// Gets and returns the list of tag blocks in the tag group.
        /// </summary>
        public List<ITagBlock> TagBlocks { get; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Group"/> class.
        /// </summary>
        public Group()
        {
            //Initialize
            TagBlocks = new List<ITagBlock>();
        }
        /// <summary>
        /// Reads the tag group using the specified binary reader.
        /// </summary>
        /// <param name="reader">The <see cref="BinaryReader"/> used to read the tag block.</param>
        public virtual void Read(BinaryReader reader)
        {
            //Check
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            
            //Read
            foreach (Block block in TagBlocks)
                block.Read(reader);
        }
        /// <summary>
        /// Writes the tag group using the specified binary writer.
        /// </summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> used to write the tag block.</param>
        public virtual void Write(BinaryWriter writer)
        {
            //Check
            if (writer == null) throw new ArgumentNullException(nameof(writer));

            //Write
            foreach (Block block in TagBlocks)
                block.Write(writer);

            //Post-write
            foreach (Block block in TagBlocks)
                block.PostWrite(writer);
        }
        /// <summary>
        /// Returns a string representation of the tag group.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return Name ?? base.ToString();
        }
        /// <summary>
        /// Releases all resources used by this instance.
        /// </summary>
        public void Dispose()
        {
            //Dispose
            TagBlocks.ForEach(b => b.Dispose());

            //Clear
            TagBlocks.Clear();
        }
        /// <summary>
        /// Gets an enumerator that iterates the <see cref="Block"/> objects in the <see cref="Group"/>.
        /// </summary>
        /// <returns>An enumerator.</returns>
        public IEnumerator<ITagBlock> GetEnumerator()
        {
            return TagBlocks.GetEnumerator();
        }

        ITagBlock ITagGroup.this[int index]
        {
            get { return TagBlocks[index]; }
            set { TagBlocks[index] = value; }
        }
        string ITagGroup.Name => Name;
        HaloTag ITagGroup.GroupTag => GroupTag;
        ITagBlock[] ITagGroup.TagBlocks => TagBlocks.ToArray();
        void IReadable.Read(BinaryReader reader)
        {
            //Read
            Read(reader);
        }
        void IWritable.Write(BinaryWriter writer)
        {
            //Write
            Write(writer);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// Represents a tag group object.
    /// </summary>
    public interface ITagGroup : IReadWrite, IDisposable, IEnumerable<ITagBlock>
    {
        /// <summary>
        /// Gets and returns the number of <see cref="ITagBlock"/> elements within the <see cref="ITagGroup"/>.
        /// </summary>
        int Count { get; }
        /// <summary>
        /// Gets or sets a tag block within the the <see cref="ITagGroup"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the tag block.</param>
        /// <returns>A <see cref="ITagBlock"/> instance if one is found at the given index, otherwise <see langword="null"/>.</returns>
        ITagBlock this[int index] { get; set; }
        /// <summary>
        /// Gets and returns the name of the <see cref="ITagGroup"/>.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Gets and returns the group tag of the <see cref="ITagGroup"/>.
        /// </summary>
        HaloTag GroupTag { get; }
        /// <summary>
        /// Gets and returns the tag block of the <see cref="ITagGroup"/>.
        /// </summary>
        [Obsolete("ITagBlock[] ITagGroup.TagBlocks is deprecated, use ITagBlock this[int index] instead.")]
        ITagBlock[] TagBlocks { get; }
    }
}
