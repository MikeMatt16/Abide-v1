using Abide.HaloLibrary.Halo2Map;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Abide.HaloLibrary.Halo2
{
    /// <summary>
    /// Represents a Halo 2 map tag list.
    /// </summary>
    [Serializable]
    public sealed class TagList : ICollection<TagHierarchy>, IEnumerable<TagHierarchy>
    {
        private readonly Dictionary<TagFourCc, TagHierarchy> tagHierarchies;

        /// <summary>
        /// Gets the number of tag hierarchy structures this list contains.
        /// </summary>
        public int Count
        {
            get { return tagHierarchies.Count; }
        }
        /// <summary>
        /// Gets and returns the <see cref="TagHierarchy"/> whose root is the specified <see cref="TagFourCc"/>.
        /// </summary>
        /// <param name="tag">The root of the <see cref="TagHierarchy"/> to get.</param>
        /// <returns>A <see cref="TagHierarchy"/> structure whose <see cref="TagHierarchy.Root"/> value is the specified <see cref="TagFourCc"/> value.</returns>
        /// <exception cref="ArgumentException">Occurs when the specified tag root is not found.</exception>
        public TagHierarchy this[TagFourCc tag]
        {
            get
            {
                if (tagHierarchies.ContainsKey(tag)) return tagHierarchies[tag];
                else throw new ArgumentException("Unable to find tag hierarchy for the supplied tag.", "tag");
            }
        }
        /// <summary>
        /// Initializes the <see cref="TagList"/> using that contains tag hierarchies from the specified collection.
        /// </summary>
        /// <param name="collection">The collection whose tag hierarchies are copied to the new tag list.</param>
        public TagList(IEnumerable<TagHierarchy> collection)
        {
            //Prepare
            tagHierarchies = new Dictionary<TagFourCc, TagHierarchy>();

            //Add
            foreach (TagHierarchy tagHierarchy in collection)
                tagHierarchies.Add(tagHierarchy.Root, tagHierarchy);
        }
        /// <summary>
        /// Initializes a new <see cref="TagList"/>.
        /// </summary>
        public TagList()
        {
            tagHierarchies = new Dictionary<TagFourCc, TagHierarchy>();
        }
        /// <summary>
        /// Checks if the list contains an tag hierarchy whose root matches the specified tag.
        /// </summary>
        /// <param name="tag">The ID to check.</param>
        /// <returns>True if the list contains an tag hierarchy whose root matches the supplied tag, false if not.</returns>
        public bool ContainsTag(TagFourCc tag)
        {
            return tagHierarchies.ContainsKey(tag);
        }
        /// <summary>
        /// Attempts to add a specified tag hierarchy to the tag list.
        /// </summary>
        /// <param name="tagHierarchy">The tag hierarchy to be added to the tag list.</param>
        /// <returns><see langword="true"/> if the tag hierarchy does not already exist in the tag list; otherwise <see langword="false"/>.</returns>
        public bool Add(TagHierarchy tagHierarchy)
        {
            //Check
            if (tagHierarchies.ContainsKey(tagHierarchy.Root)) return false;
            tagHierarchies.Add(tagHierarchy.Root, tagHierarchy);
            return true;
        }
        /// <summary>
        /// Attempts to remove a tag hierarchy whose root matches the specified tag.
        /// </summary>
        /// <param name="tagRoot">The root of the tag hierarchy to remove.</param>
        /// <returns></returns>
        public bool Remove(TagFourCc tagRoot)
        {
            //Check and remove
            if (tagHierarchies.ContainsKey(tagRoot))
                return tagHierarchies.Remove(tagRoot);
            return false;
        }
        /// <summary>
        /// Clears the tag list.
        /// </summary>
        public void Clear()
        {
            //Clear
            tagHierarchies.Clear();
        }
        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="TagList"/>.
        /// </summary>
        /// <returns>An enumerator.</returns>
        public IEnumerator<TagHierarchy> GetEnumerator()
        {
            return tagHierarchies.Values.GetEnumerator();
        }
        /// <summary>
        /// Gets a string representation of this list.
        /// </summary>
        /// <returns>A string representation of this list.</returns>
        public override string ToString()
        {
            return string.Format("Count = {0}", tagHierarchies.Count);
        }

        bool ICollection<TagHierarchy>.IsReadOnly => false;
        void ICollection<TagHierarchy>.Add(TagHierarchy item)
        {
            Add(item);
        }
        bool ICollection<TagHierarchy>.Contains(TagHierarchy item)
        {
            return tagHierarchies.ContainsValue(item);
        }
        void ICollection<TagHierarchy>.CopyTo(TagHierarchy[] array, int arrayIndex)
        {
            tagHierarchies.Values.CopyTo(array, arrayIndex);
        }
        bool ICollection<TagHierarchy>.Remove(TagHierarchy item)
        {
            return Remove(item.Root);
        }
        IEnumerator<TagHierarchy> IEnumerable<TagHierarchy>.GetEnumerator()
        {
            return GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
