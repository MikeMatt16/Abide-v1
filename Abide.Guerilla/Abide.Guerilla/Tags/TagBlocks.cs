using Abide.Guerilla.Tags;
using Abide.HaloLibrary;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Abide.Guerilla.Tags
{
    /// <summary>
    /// Represents a base block list.
    /// </summary>
    /// <typeparam name="T">The block type.</typeparam>
    public abstract class BlockList<T> : ICollection<T>, IEnumerable<T>
    {
        /// <summary>
        /// Determines whether the <see cref="BlockList{T}"/> is full.
        /// </summary>
        public bool IsFull
        {
            get { return list.Count == maximumElementCount; }
        }
        /// <summary>
        /// Gets the number of elements within the <see cref="BlockList{T}"/>.
        /// </summary>
        public int Count
        {
            get { return list.Count; }
        }
        /// <summary>
        /// Gets the maximum number of elements allowed within the <see cref="BlockList{T}"/>.
        /// </summary>
        public int MaximumElementCount
        {
            get { return maximumElementCount; }
        }

        private readonly List<T> list = new List<T>();
        private readonly int maximumElementCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlockList{T}"/> class using the supplied maximum count.
        /// </summary>
        /// <param name="maximumElementCount">The maximum number of elements within the list.</param>
        public BlockList(int maximumElementCount)
        {
            this.maximumElementCount = maximumElementCount;
        }
        /// <summary>
        /// Attemtpts to add a block to the <see cref="BlockList{T}"/>.
        /// </summary>
        /// <param name="block"></param>
        public void Add(T block)
        {
            if (list.Count < maximumElementCount)
                list.Add(block);
        }
        /// <summary>
        /// Attemtpts to add a block to the <see cref="BlockList{T}"/>.
        /// </summary>
        /// <param name="block">The block to add.</param>
        /// <param name="added">true if the block was added; otherwise false.</param>
        public void Add(T block, out bool added)
        {
            if (list.Count < maximumElementCount)
            { list.Add(block); added = true; }
            else added = false;
        }
        /// <summary>
        /// Clears the <see cref="BlockList{T}"/>.
        /// </summary>
        public void Clear()
        {
            list.Clear();
        }
        /// <summary>
        /// Determines whether a block is in the <see cref="BlockList{T}"/>.
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        public bool Contains(T block)
        {
            return list.Contains(block);
        }
        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="BlockList{T}"/>.
        /// </summary>
        /// <returns>An enumerator.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }
        /// <summary>
        /// Removes the first occurance of the specified block from the <see cref="BlockList{T}"/>.
        /// </summary>
        /// <param name="block">The block to remove.</param>
        /// <returns>true if a block was removed; otherwise false.</returns>
        public bool Remove(T block)
        {
            return list.Remove(block);
        }
        /// <summary>
        /// Returns a string representation of this <see cref="BlockList{T}"/>.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"Count: {list.Count}";
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }
        bool ICollection<T>.IsReadOnly => false;
        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }

    /// <summary>
    /// Represents a data list.
    /// </summary>
    public sealed class DataList : BlockList<byte>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataList"/> class.
        /// </summary>
        /// <param name="maximumElementCount">The maximum number of bytes within the list.</param>
        public DataList(int maximumElementCount) : base(maximumElementCount) { }
        /// <summary>
        /// Reads the data list from the current stream using the supplied binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        /// <param name="tagBlock">The tag block.</param>
        public void Read(BinaryReader reader, TagBlock tagBlock)
        {
            //Store position
            long position = reader.BaseStream.Position;

            //Clear
            Clear();

            //Check
            if(tagBlock.Count > 0 && tagBlock.Offset > 0)
            {
                //Goto
                reader.BaseStream.Seek(tagBlock.Offset, SeekOrigin.Begin);

                //Read
                for (int i = 0; i < tagBlock.Count; i++)
                    Add(reader.ReadByte());
            }

            //Goto
            reader.BaseStream.Seek(position, SeekOrigin.Begin);
        }
    }

    /// <summary>
    /// Represents a tag block list.
    /// </summary>
    /// <typeparam name="T">The <see cref="IDataStructure"/> type.</typeparam>
    public sealed class TagBlockList<T> : BlockList<T> where T: IReadable, IWritable, IDataStructure, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagBlockList{T}"/>.
        /// </summary>
        /// <param name="maximumElementCount">The maximum number of blocks within the list.</param>
        public TagBlockList(int maximumElementCount) : base(maximumElementCount) { }
        /// <summary>
        /// Reads the tag block list from the current stream using the supplied binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        /// <param name="tagBlock">The tag block.</param>
        public void Read(BinaryReader reader, TagBlock tagBlock)
        {
            //Store position
            long position = reader.BaseStream.Position;

            //Clear
            Clear();

            //Check
            if (tagBlock.Count > 0 && tagBlock.Offset > 0)
            {
                //Goto
                reader.BaseStream.Seek(tagBlock.Offset, SeekOrigin.Begin);

                //Read
                for (int i = 0; i < tagBlock.Count; i++)
                    Add(reader.ReadDataStructure<T>());
            }

            //Goto
            reader.BaseStream.Seek(position, SeekOrigin.Begin);
        }
        /// <summary>
        /// Attempts to create a new block and adds it to the list.
        /// </summary>
        /// <returns>true if the block was added to the list; otherwise false.</returns>
        public bool CreateNew()
        {
            //Create instance
            T block = new T();

            //Initialize
            block.Initialize();

            //Add?
            Add(block, out bool added);

            //Return
            return added;
        }
    }
}
