using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Abide.Tag
{
    /// <summary>
    /// Represents a tag block.
    /// </summary>
    public abstract class Block : ITagBlock, IEnumerable<Field>
    {
        /// <summary>
        /// Gets and returns the size of the tag block.
        /// </summary>
        public int Size => GetBlockSize();
        /// <summary>
        /// Gets and returns the tag block's list of fields.
        /// </summary>
        public List<Field> Fields { get; } = new List<Field>();
        /// <summary>
        /// Gets and returns the maximum element count of the tag block.
        /// </summary>
        public abstract int MaximumElementCount { get; }
        /// <summary>
        /// Gets and returns the alignment of the tag block.
        /// </summary>
        public virtual int Alignment { get; } = 4;
        /// <summary>
        /// Gets and returns the name of the tag block.
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// Gets and returns the display name of the tag block.
        /// </summary>
        public abstract string DisplayName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Block"/> class.
        /// </summary>
        public Block() { }
        /// <summary>
        /// Returns a string reprsentation of the tag block.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            //Get name
            if (Fields.Any(f => f.IsBlockName)) return string.Join(", ",
                Fields.Where(f => f.IsBlockName).Select(f => f.Value.ToString()).ToArray());
            return DisplayName;
        }
        /// <summary>
        /// Initializes the tag block.
        /// </summary>
        public virtual void Initialize() { }
        /// <summary>
        /// Reads the tag block using the specified binary reader.
        /// </summary>
        /// <param name="reader">The <see cref="BinaryReader"/> used to read the tag block.</param>
        public virtual void Read(BinaryReader reader)
        {
            //Check
            if (reader == null) throw new ArgumentNullException(nameof(reader));

            //Read Fields
            foreach (Field field in Fields)
                field.Read(reader);
        }
        /// <summary>
        /// Writes the tag block using the specified binary writer.
        /// </summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> used to write the tag block.</param>
        public virtual void Write(BinaryWriter writer)
        {
            //Check
            if (writer == null) throw new ArgumentNullException(nameof(writer));
            
            //Write Fields
            foreach (Field field in Fields)
                field.Write(writer);
        }
        /// <summary>
        /// Performs any write operations that need to occur at a later time than the <see cref="Write(BinaryWriter)"/> method.
        /// </summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> used to post-write the tag block.</param>
        public virtual void PostWrite(BinaryWriter writer)
        {
            //Post-write Fields
            foreach (Field field in Fields)
                field.PostWrite(writer);
        }
        /// <summary>
        /// Gets the size in bytes of the tag block.
        /// </summary>
        /// <returns>The size of the tag block.</returns>
        private int GetBlockSize()
        {
            //Prepare
            int size = 0;

            //Loop
            Fields.ForEach(f => size += f.Size);

            //Return
            return size;
        }
        /// <summary>
        /// Gets an enumerator that iterates the <see cref="Field"/> objects in the <see cref="Block"/>.
        /// </summary>
        /// <returns>An enumerator.</returns>
        public virtual IEnumerator<Field> GetEnumerator()
        {
            return Fields.GetEnumerator();
        }
        /// <summary>
        /// Releases all resources used by this instance.
        /// </summary>
        public void Dispose()
        {
            //Dispose
            Fields.ForEach(f => f.Dispose());

            //Clear
            Fields.Clear();
        }

        int ITagBlock.Size => Size;
        List<Field> ITagBlock.Fields => Fields;
        int ITagBlock.MaximumElementCount => MaximumElementCount;
        int ITagBlock.Alignment => Alignment;
        string ITagBlock.Name => Name;
        string ITagBlock.DisplayName => DisplayName;
        void ITagBlock.Initialize()
        {
            //Initialize
            Initialize();
        }
        void ITagBlock.PostWrite(BinaryWriter writer)
        {
            //Post-write?
            PostWrite(writer ?? throw new ArgumentNullException(nameof(writer)));
        }
        void IReadable.Read(BinaryReader reader)
        {
            //Read
            Read(reader ?? throw new ArgumentNullException(nameof(reader)));
        }
        void IWritable.Write(BinaryWriter writer)
        {
            //Write?
            Write(writer ?? throw new ArgumentNullException(nameof(writer)));
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// Represents a tag block object.
    /// </summary>
    public interface ITagBlock : IReadWrite, IDisposable
    {
        /// <summary>
        /// Gets and returns the size of the <see cref="ITagBlock"/>.
        /// </summary>
        int Size { get; }
        /// <summary>
        /// Gets and returns the list of fields in the <see cref="ITagBlock"/>
        /// </summary>
        List<Field> Fields { get; }
        /// <summary>
        /// Gets and returns the maximum element count of the <see cref="ITagBlock"/>.
        /// </summary>
        int MaximumElementCount { get; }
        /// <summary>
        /// Gets and returns the alignment of the <see cref="ITagBlock"/>.
        /// </summary>
        int Alignment { get; }
        /// <summary>
        /// Gets and returns the name of the <see cref="ITagBlock"/>.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Gets and returns the display name of the <see cref="ITagBlock"/>.
        /// </summary>
        string DisplayName { get; }
        /// <summary>
        /// Initializes the <see cref="ITagBlock"/>.
        /// </summary>
        void Initialize();
        /// <summary>
        /// Writes any late data using the specified binary writer.
        /// </summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> used to post-write the tag block data to the underlying stream.</param>
        void PostWrite(BinaryWriter writer);
    }
}
