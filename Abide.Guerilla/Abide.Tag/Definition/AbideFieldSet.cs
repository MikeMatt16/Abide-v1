using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Abide.Tag.Definition
{
    /// <summary>
    /// Represents an Abide field set definition.
    /// </summary>
    public sealed class AbideFieldSet : IList<AbideTagField>, ICloneable
    {
        private readonly List<AbideTagField> fieldList;

        /// <summary>
        /// Gets or sets the field in the field set at a given index.
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <returns>A <see cref="AbideTagField"/> class instance.</returns>
        public AbideTagField this[int index]
        {
            get { return fieldList[index]; }
            set { fieldList[index] = value; }
        }
        /// <summary>
        /// Gets and returns the number of fields in the field set.
        /// </summary>
        public int Count => fieldList.Count;
        /// <summary>
        /// Gets and returns the tag block that owns this <see cref="AbideFieldSet"/>.
        /// </summary>
        public AbideTagBlock Owner { get; set; }
        /// <summary>
        /// Gets or sets the alignment of the field set.
        /// </summary>
        public int Alignment { get; set; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="AbideFieldSet"/> class.
        /// </summary>
        /// <param name="owner">The tag block that owns this <see cref="AbideFieldSet"/>.</param>
        public AbideFieldSet(AbideTagBlock owner)
        {
            //Setup
            Alignment = 4;
            fieldList = new List<AbideTagField>();
            this.Owner = owner;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AbideFieldSet"/> class using the specified alignment.
        /// </summary>
        /// <param name="owner">The tag block that owns this <see cref="AbideFieldSet"/>.</param>
        /// <param name="alignment">The alignment of the field set.</param>
        public AbideFieldSet(AbideTagBlock owner, int alignment) : this(owner)
        {
            //Setup
            this.Alignment = alignment;
        }
        /// <summary>
        /// Returns a copy of the <see cref="AbideFieldSet"/>.
        /// </summary>
        /// <returns>A copy of the current <see cref="AbideFieldSet"/> object.</returns>
        public object Clone()
        {
            //Create
            AbideFieldSet fieldSet = new AbideFieldSet(Owner)
            {
                Alignment = Alignment,
            };

            //Copy fields
            fieldSet.fieldList.AddRange(fieldList.Select(f => (AbideTagField)f.Clone()));

            //Return
            return fieldSet;
        }
        /// <summary>
        /// Removes all fields from the <see cref="AbideFieldSet"/>.
        /// </summary>
        public void Clear()
        {
            fieldList.Clear();
        }
        /// <summary>
        /// Adds a field to the end of the <see cref="AbideFieldSet"/>.
        /// </summary>
        /// <param name="field">The field to be added to the end of the <see cref="AbideFieldSet"/>.</param>
        public void Add(AbideTagField field)
        {
            fieldList.Add(field);
        }
        /// <summary>
        /// Removes the first occurance of a specifiec field from the <see cref="AbideFieldSet"/>.
        /// </summary>
        /// <param name="field">The field to remove from the <see cref="AbideFieldSet"/>.</param>
        /// <returns></returns>
        public bool Remove(AbideTagField field)
        {
            return fieldList.Remove(field);
        }
        /// <summary>
        /// Removes the field at the specified index of the <see cref="AbideFieldSet"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the field to remove from the field set.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is outside valid range.</exception>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= fieldList.Count) throw new ArgumentOutOfRangeException(nameof(index));
            fieldList.RemoveAt(index);
        }
        /// <summary>
        /// Searches for the specified field and returns the zero-based index of the first occurance within the entire <see cref="AbideFieldSet"/>.
        /// </summary>
        /// <param name="field">The field to locate in the <see cref="AbideFieldSet"/>.</param>
        /// <returns>The zero-based index of the first occurance of a <paramref name="field"/> in the <see cref="AbideFieldSet"/>, if found; otherwise, <see langword="-1"/>.</returns>
        public int IndexOf(AbideTagField field)
        {
            return fieldList.IndexOf(field);
        }
        /// <summary>
        /// Inserts a field into the <see cref="AbideFieldSet"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="field"/> should be inserted.</param>
        /// <param name="field">The field to insert.</param>
        public void Insert(int index, AbideTagField field)
        {
            fieldList.Insert(index, field);
        }
        /// <summary>
        /// Determines whether a field is in the <see cref="AbideFieldSet"/>.
        /// </summary>
        /// <param name="field">The field to locate in the <see cref="AbideFieldSet"/>.</param>
        /// <returns><see langword="true"/> if <paramref name="field"/> is found in the <see cref="AbideFieldSet"/>; otherwise, <see langword="false"/>.</returns>
        public bool Contains(AbideTagField field)
        {
            return fieldList.Contains(field);
        }
        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="AbideFieldSet"/>.
        /// </summary>
        /// <returns>An enumerator.</returns>
        
        public IEnumerator<AbideTagField> GetEnumerator()
        {
            return fieldList.GetEnumerator();
        }
        bool ICollection<AbideTagField>.IsReadOnly => false;
        int IList<AbideTagField>.IndexOf(AbideTagField item)
        {
            return IndexOf(item);
        }
        void IList<AbideTagField>.Insert(int index, AbideTagField item)
        {
            Insert(index, item);
        }
        void IList<AbideTagField>.RemoveAt(int index)
        {
            RemoveAt(index);
        }
        void ICollection<AbideTagField>.Add(AbideTagField item)
        {
            Add(item);
        }
        void ICollection<AbideTagField>.Clear()
        {
            Clear();
        }
        bool ICollection<AbideTagField>.Contains(AbideTagField item)
        {
            return Contains(item);
        }
        void ICollection<AbideTagField>.CopyTo(AbideTagField[] array, int arrayIndex)
        {
            fieldList.CopyTo(array, arrayIndex);
        }
        bool ICollection<AbideTagField>.Remove(AbideTagField item)
        {
            return Remove(item);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
