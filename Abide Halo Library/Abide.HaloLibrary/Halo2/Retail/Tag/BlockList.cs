using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Abide.HaloLibrary.Halo2.Retail.Tag
{
    /// <summary>
    /// Represents a tag block list.
    /// </summary>
    public sealed class BlockList : IList<Block>, ICollection<Block>, IEnumerable<Block>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly List<Block> list = new List<Block>();
        private int maximumCount = 1;
        private int count = 0;

        public Block this[int index]
        {
            get { return list[index]; }
            set { list[index] = value ?? throw new ArgumentNullException(nameof(value)); }
        }
        public int MaximumCount
        {
            get => maximumCount;
            set
            {
                if (maximumCount != value)
                {
                    maximumCount = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int Count
        {
            get => count;
            set
            {
                if (count != value)
                {
                    count = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public BlockList(int maximumElementCount)
        {
            MaximumCount = maximumElementCount;
        }
        public void Move(int sourceIndex, int destinationIndex)
        {
            if (sourceIndex < 0 || sourceIndex >= list.Count) throw new ArgumentOutOfRangeException(nameof(sourceIndex));
            if (destinationIndex < 0 || destinationIndex >= list.Count) throw new ArgumentOutOfRangeException(nameof(destinationIndex));

            Block block = list[sourceIndex];
            list.RemoveAt(sourceIndex);

            list.Insert(destinationIndex, block);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Move, block));
        }
        public void Add(Block block)
        {
            Add(block, out bool success);
        }
        public void Add(Block block, out bool success)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));

            success = false;
            if (Count < MaximumCount)
            {
                success = true;
                list.Add(block);
                Count = list.Count;
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, block));
            }
        }
        public bool Contains(Block block)
        {
            return list.Contains(block);
        }
        public bool Remove(Block block)
        {
            if (list.Remove(block))
            {
                Count = list.Count;
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, block));
                return true;
            }

            return false;
        }
        public void Clear()
        {
            list.Clear();
            Count = list.Count;
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
        public void CopyTo(Block[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }
        public int IndexOf(Block block)
        {
            return list.IndexOf(block);
        }
        public void Insert(int index, Block block)
        {
            Insert(index, block, out bool success);
        }
        public void Insert(int index, Block block, out bool success)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (index < 0 || index > Count) throw new ArgumentOutOfRangeException(nameof(index));
            success = false;

            if (Count < MaximumCount)
            {
                success = true;
                list.Insert(index, block);
                Count = list.Count;
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, block));
            }
        }
        public void RemoveAt(int index)
        {
            if (index < 0 || index > Count) throw new ArgumentOutOfRangeException(nameof(index));
            var block = list[index];
            list.RemoveAt(index);
            Count = list.Count;
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, block));
        }
        public Block[] ToArray()
        {
            return list.ToArray();
        }
        public IEnumerator<Block> GetEnumerator()
        {
            return list.GetEnumerator();
        }
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        bool ICollection<Block>.IsReadOnly => false;
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
