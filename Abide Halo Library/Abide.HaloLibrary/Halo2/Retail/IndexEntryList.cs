using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Abide.HaloLibrary.Halo2.Retail
{
    /// <summary>
    /// Represents a Halo 2 retail map index entry list.
    /// </summary>
    [Serializable]
    public sealed class IndexEntryList : ICollection<IndexEntry>, IEnumerable<IndexEntry>, IDisposable
    {
        private readonly Dictionary<uint, IndexEntry> entries;
        private readonly Dictionary<int, uint> indexLookup;

        /// <summary>
        /// 
        /// </summary>
        public bool IsDisposed { get; private set; } = false;
        /// <summary>
        /// Gets the number of index entries this list contains.
        /// </summary>
        public int Count
        {
            get { return entries.Count; }
        }
        /// <summary>
        /// Gets and returns the first object entry in the list.
        /// </summary>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public IndexEntry First
        {
            get
            {
                if (entries.Count > 0) return entries[indexLookup[0]];
                else throw new IndexOutOfRangeException();
            }
        }
        /// <summary>
        /// Gets and returns the last object entry in the list.
        /// </summary>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public IndexEntry Last
        {
            get
            {
                int index = entries.Count - 1;
                if (index >= 0) return entries[indexLookup[index]];
                else throw new IndexOutOfRangeException();
            }
        }
        /// <summary>
        /// Gets and returns the index entry object by its <see cref="TagId"/>.
        /// </summary>
        /// <param name="id">The <see cref="TagId"/> of the index entry object.</param>
        /// <returns>An <see cref="IndexEntry"/> whose ID matches the supplied <see cref="TagId"/>.</returns>
        public IndexEntry this[TagId id]
        {
            get
            {
                if (entries.ContainsKey(id)) return entries[id];
                else return null;
            }
        }
        /// <summary>
        /// Gets and returns the index entry object at a given index.
        /// </summary>
        /// <param name="index">The index of the index entry object.</param>
        /// <returns>An <see cref="IndexEntry"/> at the given index.</returns>
        public IndexEntry this[int index]
        {
            get
            {
                if (indexLookup.ContainsKey(index)) return entries[indexLookup[index]];
                else return null;
            }
        }
        /// <summary>
        /// Intializes a new <see cref="IndexEntryList"/> copying the supplied <see cref="IndexEntry"/> array into the list.
        /// </summary>
        /// <param name="indexEntries">The array to copy into the new list.</param>
        public IndexEntryList(IndexEntry[] indexEntries) : this()
        {
            //Check
            if (indexEntries == null) throw new ArgumentNullException(nameof(indexEntries));

            //Loop through entries and add
            for (int i = 0; i < indexEntries.Length; i++)
            {
                entries.Add(indexEntries[i].Id, indexEntries[i]);
                indexLookup.Add(i, indexEntries[i].Id);
            }
        }
        /// <summary>
        /// Initializes a new <see cref="IndexEntryList"/>.
        /// </summary>
        public IndexEntryList()
        {
            //Setup
            entries = new Dictionary<uint, IndexEntry>();
            indexLookup = new Dictionary<int, uint>();
        }
        /// <summary>
        /// Checks if the list contains an index entry whose ID matches the supplied ID.
        /// </summary>
        /// <param name="id">The ID to check.</param>
        /// <returns>True if the list contains an index entry whose ID is the supplied ID, false if not.</returns>
        public bool ContainsId(TagId id)
        {
            return entries.ContainsKey(id.Dword);
        }
        /// <summary>
        /// Gets a string representation of this list.
        /// </summary>
        /// <returns>A string representation of this list.</returns>
        public override string ToString()
        {
            return string.Format("Count = {0}", entries.Count);
        }
        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="IndexEntryList"/>.
        /// </summary>
        /// <returns>An enumerator.</returns>
        public IEnumerator<IndexEntry> GetEnumerator()
        {
            return entries.Values.GetEnumerator();
        }
        /// <summary>
        /// Attempts to insert an index entry to the list.
        /// </summary>
        /// <param name="index">The zero-based index in the list at which <paramref name="entry"/> is to be inserted.</param>
        /// <param name="entry">The index entry to be inserted into the list.</param>
        /// <returns><see langword="true"/> if the index entry was successfully inserted; otherwise, <see langword="false"/>.</returns>
        public bool Insert(int index, IndexEntry entry)
        {
            //Check
            if (entries.ContainsKey(entry.Id)) return false;
            var indexEntries = entries.Values.ToList();
            indexEntries.Insert(index, entry);

            //Rebuild dictionaries
            entries.Clear();
            indexLookup.Clear();
            foreach (IndexEntry indexEntry in indexEntries)
            {
                indexLookup.Add(entries.Count, indexEntry.Id);
                entries.Add(indexEntry.Id, indexEntry);
            }
            return true;
        }
        /// <summary>
        /// Attempts to add an index entry to the end of the list.
        /// </summary>
        /// <param name="entry">The entry to be added to the list.</param>
        /// <returns><see langword="true"/> if the index entry was successfully added; otherwise, <see langword="false"/>.</returns>
        public bool Add(IndexEntry entry)
        {
            //Check
            if (entries.ContainsKey(entry.Id.Dword)) return false;
            indexLookup.Add(entries.Count, entry.Id.Dword);
            entries.Add(entry.Id.Dword, entry);
            return true;
        }
        /// <summary>
        /// Attempts to remove an index entry from the list.
        /// </summary>
        /// <param name="entry">The entry to be removed from the list.</param>
        /// <returns><see langword="true"/> if the index entry was successfully removed; otherwise, <see langword="false"/>.</returns>
        public bool Remove(IndexEntry entry)
        {
            //Check
            if (!entries.ContainsKey(entry.Id)) return false;
            var indexEntries = entries.Values.ToList();
            indexEntries.Remove(entry);

            //Rebuild dictionary
            entries.Clear();
            indexLookup.Clear();
            foreach (IndexEntry indexEntry in indexEntries)
            {
                indexLookup.Add(entries.Count, indexEntry.Id);
                entries.Add(indexEntry.Id.Dword, indexEntry);
            }
            return true;
        }
        /// <summary>
        /// Clears the list.
        /// </summary>
        public void Clear()
        {
            indexLookup.Clear();
            entries.Clear();
        }
        /// <summary>
        /// Releases all resources used by this instance.
        /// </summary>
        public void Dispose()
        {
            if (IsDisposed) return;
            IsDisposed = true;

            foreach (var item in entries.Values)
                item.Dispose();

            Clear();
        }

        bool ICollection<IndexEntry>.IsReadOnly => false;
        void ICollection<IndexEntry>.Add(IndexEntry item)
        {
            Add(item);
        }
        bool ICollection<IndexEntry>.Contains(IndexEntry item)
        {
            return entries.ContainsValue(item);
        }
        void ICollection<IndexEntry>.CopyTo(IndexEntry[] array, int arrayIndex)
        {
            entries.Values.CopyTo(array, arrayIndex);
        }
        bool ICollection<IndexEntry>.Remove(IndexEntry item)
        {
            return Remove(item);
        }
        IEnumerator<IndexEntry> IEnumerable<IndexEntry>.GetEnumerator()
        {
            return GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
