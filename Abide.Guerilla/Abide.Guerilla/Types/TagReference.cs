using Abide.HaloLibrary;
using System;
using System.Runtime.InteropServices;

namespace Abide.Guerilla.Types
{
    /// <summary>
    /// Represents a tag reference.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = 8)]
    public struct TagReference : IEquatable<TagReference>
    {
        /// <summary>
        /// Gets or sets the tag group.
        /// </summary>
        public Tag Tag
        {
            get { return tag; }
            set { tag = value; }
        }
        /// <summary>
        /// Gets or sets the tag ID.
        /// </summary>
        public TagId Id
        {
            get { return id; }
            set { id = value; }
        }

        private Tag tag;
        private TagId id;

        /// <summary>
        /// Determines whether this instance and another specified <see cref="TagReference"/> have the same value.
        /// </summary>
        /// <param name="reference">The object to compare with the current instance.</param>
        /// <returns></returns>
        public bool Equals(TagReference reference)
        {
            return tag.Equals(reference.tag) && id.Equals(reference.Id);
        }
        /// <summary>
        /// Gets a string representation of this tag reference.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"{tag} {id}";
        }
    }
}
