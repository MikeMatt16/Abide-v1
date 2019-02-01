using System;
using System.Collections;
using System.Collections.Generic;

namespace Abide.Tag
{
    /// <summary>
    /// Represents a tag block list.
    /// </summary>
    public sealed class BlockList : IList<ITagBlock>, ICollection<ITagBlock>, IEnumerable<ITagBlock>
    {
        /// <summary>
        /// Gets or sets the block at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the block to get or set.</param>
        /// <returns>The block at the specified index.</returns>
        public ITagBlock this[int index]
        {
            get { return blockList[index]; }
            set { blockList[index] = value ?? throw new ArgumentNullException(nameof(value)); }
        }
        /// <summary>
        /// Gets and returns the maximum number of blocks allowed in this list.
        /// </summary>
        public int MaximumCount { get; }
        /// <summary>
        /// Gets and returns the number of blocks in the list.
        /// </summary>
        public int Count => blockList.Count;

        private readonly List<ITagBlock> blockList;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlockList"/> class.
        /// </summary>
        /// <param name="maximumElementCount">The maximum number of blocks allowed in the list.</param>
        public BlockList(int maximumElementCount)
        {
            //Setup
            MaximumCount = maximumElementCount;
            blockList = new List<ITagBlock>();
        }
        /// <summary>
        /// Moves a specified tag block at a specified index to a different index.
        /// </summary>
        /// <param name="sourceIndex">The zero-based index of the block to move.</param>
        /// <param name="destinationIndex">The zero-based index of the destination of the block.</param>
        public void Move(int sourceIndex, int destinationIndex)
        {
            //Check
            if (sourceIndex < 0 || sourceIndex >= blockList.Count) throw new ArgumentOutOfRangeException(nameof(sourceIndex));
            if (destinationIndex < 0 || destinationIndex >= blockList.Count) throw new ArgumentOutOfRangeException(nameof(destinationIndex));

            //Remove
            ITagBlock block = blockList[sourceIndex];
            blockList.RemoveAt(sourceIndex);

            //Insert
            blockList.Insert(destinationIndex, block);
        }
        /// <summary>
        /// Attempts to add a block to the list.
        /// </summary>
        /// <param name="block">The block to add.</param>
        public void Add(ITagBlock block)
        {
            //Add
            Add(block, out bool success);
        }
        /// <summary>
        /// Attempts to add a block to the list.
        /// </summary>
        /// <param name="block">The block to add.</param>
        /// <param name="success">When this method returns, the boolean value will be <see langword="true"/> if the block was successfully added; otherwise, the boolean value will be <see langword="false"/>.</param>
        public void Add(ITagBlock block, out bool success)
        {
            //Prepare
            success = true;

            //Check if null
            if (block == null) success = false;

            //Check
            success &= Count < MaximumCount;

            //Check
            if (success) blockList.Add(block);
        }
        /// <summary>
        /// Determines whether a block exists in the <see cref="BlockList"/>.
        /// </summary>
        /// <param name="block">The block to locate in the <see cref="BlockList"/>. The value can be <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if <paramref name="block"/> is found in the <see cref="BlockList"/>; otherwise, <see langword="false"/></returns>
        public bool Contains(ITagBlock block)
        {
            return blockList.Contains(block);
        }
        /// <summary>
        /// Removes the first occurance of a specific block from the <see cref="BlockList"/>.
        /// </summary>
        /// <param name="block">The block to remove from the <see cref="BlockList"/>. The value can be <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if <paramref name="block"/> is successfully removed; otherwise, <see langword="false"/> if <paramref name="block"/> was not found in the <see cref="BlockList"/>.</returns>
        public bool Remove(ITagBlock block)
        {
            return blockList.Remove(block);
        }
        /// <summary>
        /// Removes all blocks from the <see cref="BlockList"/>.
        /// </summary>
        public void Clear()
        {
            blockList.Clear();
        }
        /// <summary>
        /// Copies the entire <see cref="BlockList"/> to a compatible one-dimensionsal array, starting at the specified index of the target array.
        /// </summary>
        /// <param name="array">the one-dimensional <see cref="Array"/> that is the destination of the elements copied from the <see cref="BlockList"/>. The <see cref="Array"/> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        public void CopyTo(ITagBlock[] array, int arrayIndex)
        {
            blockList.CopyTo(array, arrayIndex);
        }
        /// <summary>
        /// Searches for the specified block and returns the zero-based index of the first occurance within the entire <see cref="BlockList"/>.
        /// </summary>
        /// <param name="block">The block to locate in the <see cref="BlockList"/>. The value can be <see langword="null"/>.</param>
        /// <returns>The zero-based index of the first occurance of <paramref name="block"/> within the entire <see cref="BlockList"/>, if found; otherwise, -1.</returns>
        public int IndexOf(ITagBlock block)
        {
            return blockList.IndexOf(block);
        }
        /// <summary>
        /// Attempts to insert a block into the <see cref="BlockList"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="block"/> should be inserted.</param>
        /// <param name="block">The block to insert. The value can be <see langword="null"/>.</param>
        public void Insert(int index, ITagBlock block)
        {
            Insert(index, block, out bool success);
        }
        /// <summary>
        /// Attempts to insert a block into the <see cref="BlockList"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="block"/> should be inserted.</param>
        /// <param name="block">The block to insert. The value can be <see langword="null"/>.</param>
        /// <param name="success">When this method returns, the boolean value will be <see langword="true"/> if the block was successfully inserted; otherwise, the boolean value will be <see langword="false"/>.</param>
        public void Insert(int index, ITagBlock block, out bool success)
        {
            //Prepare
            success = true;

            //Check if null
            if (block == null) success = false;
            if (index < 0 || index > Count) success = false;

            //Check
            success &= Count < MaximumCount;

            //Check
            if (success) blockList.Insert(index, block);
        }
        /// <summary>
        /// Removes the block at the specified index of the <see cref="BlockList"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the block to remove.</param>
        public void RemoveAt(int index)
        {
            blockList.RemoveAt(index);
        }
        /// <summary>
        /// Converts this list to an array of tag block elements.
        /// </summary>
        /// <returns></returns>
        public ITagBlock[] ToArray()
        {
            return blockList.ToArray();
        }
        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="BlockList"/>.
        /// </summary>
        /// <returns>An enumerator.</returns>
        public IEnumerator<ITagBlock> GetEnumerator()
        {
            return blockList.GetEnumerator();
        }

        bool ICollection<ITagBlock>.IsReadOnly => false;
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
