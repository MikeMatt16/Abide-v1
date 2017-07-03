using System;
using System.Runtime.InteropServices;

namespace Abide.HaloLibrary.Halo2Map
{
    /// <summary>
    /// Represents a 12-byte length <see cref="TagHierarchy"/> structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = Length), Serializable]
    public struct TagHierarchy : IEquatable<TagHierarchy>
    {
        /// <summary>
        /// Represents the length of a <see cref="TagHierarchy"/> structure in bytes.
        /// This value is constant.
        /// </summary>
        public const int Length = 12;
        /// <summary>
        /// Represents an empty <see cref="TagHierarchy"/> structure.
        /// </summary>
        public static readonly TagHierarchy Empty = new TagHierarchy();

        /// <summary>
        /// The root of the tag hierarchy.
        /// </summary>
        public Tag Root
        {
            get { return root; }
            set { root = value; }
        }
        /// <summary>
        /// The parent of the tag hierarchy.
        /// </summary>
        public Tag Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        /// <summary>
        /// The class of the tag hierarchy.
        /// </summary>
        public Tag Class
        {
            get { return @class; }
            set { @class = value; }
        }

        /// <summary>
        /// Initializes a <see cref="TagHierarchy"/> structure with the supplied class, root, and parent values.
        /// </summary>
        /// <param name="class">The tag's class.</param>
        /// <param name="root">The tag's root.</param>
        /// <param name="parent">The tag's parent.</param>
        public TagHierarchy(string @class, string root, string parent)
        {
            //Setup
            this.root = root;
            this.@class = @class;
            this.parent = parent;
        }
        
        private Tag root;
        private Tag parent;
        private Tag @class;

        /// <summary>
        /// Returns the string representation of the tag hierarchy.
        /// </summary>
        /// <returns>A string representation of the structure.</returns>
        public override string ToString()
        {
            return string.Concat(@class, " > ", parent, " > ", root);
        }

        /// <summary>
        /// Determines whether the specified <see cref="TagHierarchy"/> is equal to the current <see cref="TagHierarchy"/>.  
        /// </summary>
        /// <param name="other">The <see cref="TagHierarchy"/> to compare the current instance to.</param>
        /// <returns>True if the tags are identical, false if not.</returns>
        public bool Equals(TagHierarchy other)
        {
            bool equal = root.Equals(other.root) && parent.Equals(other.parent) && @class.Equals(other.@class);
            return equal;
        }
    }
}