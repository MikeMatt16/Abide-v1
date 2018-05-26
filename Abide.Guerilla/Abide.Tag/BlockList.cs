using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abide.Tag
{
    /// <summary>
    /// Represents a tag block list.
    /// </summary>
    /// <typeparam name="TBlock">The tag block type.</typeparam>
    public sealed class BlockList<TBlock> : IList<TBlock>, ICollection<TBlock>, IEnumerable<TBlock> where TBlock : ITagBlock, new()
    {
        /// <summary>
        /// Gets or sets the block at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the block to get or set.</param>
        /// <returns>The block at the specified index.</returns>
        public TBlock this[int index]
        {
            get { return blockList[index]; }
            set { if (value == null) throw new ArgumentNullException(nameof(value)); blockList[index] = value; }
        }
        /// <summary>
        /// Gets and returns the maximum number of blocks allowed in this list.
        /// </summary>
        public int MaximumCount { get; }
        /// <summary>
        /// Gets and returns the number of blocks in the list.
        /// </summary>
        public int Count => blockList.Count;

        private readonly List<TBlock> blockList;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlockList{TBlock}"/> class.
        /// </summary>
        /// <param name="maximumElementCount">The maximum number of blocks allowed in the list.</param>
        public BlockList(int maximumElementCount)
        {
            //Setup
            MaximumCount = maximumElementCount;
            blockList = new List<TBlock>();
        }
        /// <summary>
        /// Attempts to add a block to the list.
        /// </summary>
        /// <param name="block">The block to add.</param>
        public void Add(TBlock block)
        {
            //Add
            Add(block, out bool success);
        }
        /// <summary>
        /// Attempts to add a block to the list.
        /// </summary>
        /// <param name="block">The block to add.</param>
        /// <param name="success">When this method returns, the boolean value will be <see langword="true"/> if the block was successfully added; otherwise, the boolean value will be <see langword="false"/>.</param>
        public void Add(TBlock block, out bool success)
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
        /// Determines whether a block exists in the <see cref="BlockList{TBlock}"/>.
        /// </summary>
        /// <param name="block">The block to locate in the <see cref="BlockField{T}"/>. The value can be <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if <paramref name="block"/> is found in the <see cref="BlockList{TBlock}"/>; otherwise, <see langword="false"/></returns>
        public bool Contains(TBlock block)
        {
            return blockList.Contains(block);
        }
        /// <summary>
        /// Removes the first occurance of a specific block from the <see cref="BlockList{TBlock}"/>.
        /// </summary>
        /// <param name="block">The block to remove from the <see cref="BlockList{TBlock}"/>. The value can be <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if <paramref name="block"/> is successfully removed; otherwise, <see langword="false"/> if <paramref name="block"/> was not found in the <see cref="BlockList{TBlock}"/>.</returns>
        public bool Remove(TBlock block)
        {
            return blockList.Remove(block);
        }
        /// <summary>
        /// Removes all blocks from the <see cref="BlockList{TBlock}"/>.
        /// </summary>
        public void Clear()
        {
            blockList.Clear();
        }
        /// <summary>
        /// Copies the entire <see cref="BlockList{TBlock}"/> to a compatible one-dimensionsal array, starting at the specified index of the target array.
        /// </summary>
        /// <param name="array">the one-dimensional <see cref="Array"/> that is the destination of the elements copied from <see cref="BlockField{T}"/>. The <see cref="Array"/> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        public void CopyTo(TBlock[] array, int arrayIndex)
        {
            blockList.CopyTo(array, arrayIndex);
        }
        /// <summary>
        /// Searches for the specified block and returns the zero-based index of the first occurance within the entire <see cref="BlockList{TBlock}"/>.
        /// </summary>
        /// <param name="block">The block to locate in the <see cref="BlockList{TBlock}"/>. The value can be <see langword="null"/>.</param>
        /// <returns>The zero-based index of the first occurance of <paramref name="block"/> within the entire <see cref="BlockList{TBlock}"/>, if found; otherwise, -1.</returns>
        public int IndexOf(TBlock block)
        {
            return blockList.IndexOf(block);
        }
        /// <summary>
        /// Attempts to insert a block into the <see cref="BlockList{TBlock}"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="block"/> should be inserted.</param>
        /// <param name="block">The block to insert. The value can be <see langword="null"/>.</param>
        public void Insert(int index, TBlock block)
        {
            Insert(index, block, out bool success);
        }
        /// <summary>
        /// Attempts to insert a block into the <see cref="BlockList{TBlock}"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="block"/> should be inserted.</param>
        /// <param name="block">The block to insert. The value can be <see langword="null"/>.</param>
        /// <param name="success">When this method returns, the boolean value will be <see langword="true"/> if the block was successfully inserted; otherwise, the boolean value will be <see langword="false"/>.</param>
        public void Insert(int index, TBlock block, out bool success)
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
        /// Removes the block at the specified index of the <see cref="BlockList{TBlock}"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the block to remove.</param>
        public void RemoveAt(int index)
        {
            blockList.RemoveAt(index);
        }
        /// <summary>
        /// Creates and attempts to add a new block to the block list.
        /// </summary>
        /// <param name="success">When this method returns, the boolean value will be <see langword="true"/> if the new block was successfully add; otherwise, the boolean value will be <see langword="false"/>.</param>
        /// <returns>A new <see cref="ITagBlock"/> instance.</returns>
        public TBlock New(out bool success)
        {
            //Prepare
            TBlock block = new TBlock();

            //Add
            Add(block, out success);

            //Return
            return block;
        }
        /// <summary>
        /// Converts this list to an array of tag block elements.
        /// </summary>
        /// <returns></returns>
        public TBlock[] ToArray()
        {
            return blockList.ToArray();
        }
        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="BlockList{TBlock}"/>.
        /// </summary>
        /// <returns>An enumerator.</returns>
        public IEnumerator<TBlock> GetEnumerator()
        {
            return blockList.GetEnumerator();
        }

        bool ICollection<TBlock>.IsReadOnly => false;
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
