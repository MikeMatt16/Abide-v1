using System;
using System.Collections;
using System.Collections.Generic;

namespace Abide.HaloLibrary.Halo2
{
    /// <summary>
    /// Represents a Halo 2 map string list.
    /// </summary>
    [Serializable]
    public sealed class StringList : IEnumerable<string>, ICollection<string>
    {
        /// <summary>
        /// Gets and returns the number of strings in the string list.
        /// </summary>
        public int Count
        {
            get { return strings.Count; }
        }
        /// <summary>
        /// Gets and returns a a string's ID from the given string within the string list.
        /// </summary>
        /// <param name="sid">The string whose ID is to be retrieved.</param>
        /// <returns><see cref="StringId.Zero"/> if the string does not exist, otherwise returns a valid <see cref="StringId"/> value.</returns>
        public StringId this[string sid]
        {
            get
            {
                //Check
                if (sid == null) throw new ArgumentNullException(nameof(sid));

                //Return zero for non-existing strings.
                if (!Contains(sid)) return StringId.Zero;
                else return StringId.FromString(sid, strings.IndexOf(sid));
            }
        }
        /// <summary>
        /// Gets or sets a string's value at a given index within the string list.
        /// </summary>
        /// <param name="index">The index of the string.</param>
        /// <returns>A string value.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is outside of the valid range.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        public string this[int index]
        {
            get
            {
                //Check
                if (index < 0 || index >= strings.Count) throw new ArgumentOutOfRangeException(nameof(index));

                //Return
                return strings[index];
            }
            set
            {
                //Check
                if (index < 0 || index >= strings.Count) throw new ArgumentOutOfRangeException(nameof(index));
                if (value == null) throw new ArgumentNullException(nameof(value));

                //Set
                strings[index] = value;
            }
        }

        private readonly List<string> strings;

        /// <summary>
        /// Initializes a new <see cref="StringList"/> instance.
        /// </summary>
        public StringList() : this(DefaultStrings.GetDefaultStrings().ToArray()) { }
        /// <summary>
        /// Initializes a new <see cref="StringList"/> instance using the supplied string array.
        /// </summary>
        /// <param name="strings">The string array to populate the string list with.</param>
        /// <exception cref="ArgumentNullException"><paramref name="strings"/> is null.</exception>
        public StringList(string[] strings)
        {
            //Check
            if (strings == null) throw new ArgumentNullException(nameof(strings));

            //Intialize
            this.strings = new List<string>(strings);
        }
        /// <summary>
        /// Attempts to add a string value to the string list.
        /// </summary>
        /// <param name="value">The string to add.</param>
        /// <returns>true if the string does not exist, and the string is added successfully; othewise false and the string was not added.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        public bool Add(string value)
        {
            //Check
            if (value == null) throw new ArgumentNullException(nameof(value));

            //Add?
            bool contains = strings.Contains(value);
            if (!contains) strings.Add(value);

            //Return
            return contains;
        }
        /// <summary>
        /// Attempts to add a string value to the string list, and retrieve its identifier.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="id">The target string id.</param>
        /// <returns>true if the string does not exist, and the string is added successfully; othewise false and the string was not added.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        public bool Add(string value, out StringId id)
        {
            //Check
            if (value == null) throw new ArgumentNullException(nameof(value));

            //Add?
            bool contains = strings.Contains(value);
            if (!contains) strings.Add(value);

            //Get ID
            id = StringId.FromString(value, strings.IndexOf(value));

            //Return
            return contains;
        }
        /// <summary>
        /// Determines if a string value exists within the string list.
        /// </summary>
        /// <param name="value">The string to check for.</param>
        /// <returns>true if the string exists in the string list; otherwise false.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        public bool Contains(string value)
        {
            //Check
            if (value == null) throw new ArgumentNullException(nameof(value));

            //Return
            return strings.Contains(value);
        }
        /// <summary>
        /// Attempts to remove a string value from the string list.
        /// </summary>
        /// <param name="value">The string to remove.</param>
        /// <returns>true if the string is found and removed; otherwise false.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        public bool Remove(string value)
        {
            //Check
            if (value == null) throw new ArgumentNullException(nameof(value));

            //Return
            return strings.Remove(value);
        }
        /// <summary>
        /// Attempts to remove a string value from this list at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the string to remove.</param>
        public void RemoveAt(int index)
        {
            //Check
            if (index >= 0 && index < strings.Count)
                strings.RemoveAt(index);
        }
        /// <summary>
        /// Reset's the string list, leaving only the zero-string.
        /// </summary>
        public void Reset()
        {
            //Clear
            strings.Clear();
            strings.Add(string.Empty);
        }
        /// <summary>
        /// Removes all strings from the string list.
        /// </summary>
        public void Clear()
        {
            strings.Clear();
        }
        /// <summary>
        /// Gets the zero-based index of the string within the string list.
        /// </summary>
        /// <param name="value">The string to retrieve the index of.</param>
        /// <returns>-1 if the specified string was not found, otherwise returns a valid index.</returns>
        public int IndexOf(string value)
        {
            //Check
            if (value == null) throw new ArgumentNullException(nameof(value));

            //Return
            return strings.IndexOf(value);
        }
        /// <summary>
        /// Gets and returns an enumerator that iterates the instance.
        /// </summary>
        /// <returns>An enumerator.</returns>
        public IEnumerator<string> GetEnumerator()
        {
            return strings.GetEnumerator();
        }
        /// <summary>
        /// Returns a string representation of this list.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"Count: {strings.Count}";
        }

        bool ICollection<string>.IsReadOnly
        {
            get { return false; }
        }
        void ICollection<string>.Add(string item)
        {
            if (!Add(item)) throw new InvalidOperationException("Unable to add string to list.");
        }
        void ICollection<string>.Clear()
        {
            Clear();
        }
        void ICollection<string>.CopyTo(string[] array, int arrayIndex)
        {
            strings.CopyTo(array, arrayIndex);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
