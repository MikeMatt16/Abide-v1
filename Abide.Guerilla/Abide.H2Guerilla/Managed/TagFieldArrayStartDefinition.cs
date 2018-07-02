namespace Abide.H2Guerilla.Managed
{
    /// <summary>
    /// Represents a tag field array start definition.
    /// </summary>
    public sealed class TagFieldArrayStartDefinition : TagFieldDefinition
    {
        /// <summary>
        /// Gets and returns the number of elements within this array.
        /// This value is equivalent to <see cref="TagFieldDefinition.DefinitionAddress"/>.
        /// </summary>
        public int ElementCount
        {
            get { return DefinitionAddress; }
        }
        /// <summary>
        /// Gets and returns the size of the array element.
        /// </summary>
        public int Size
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Inititalizes a new instance of the <see cref="TagFieldArrayStartDefinition"/> class.
        /// </summary>
        public TagFieldArrayStartDefinition() : base() { }
    }
}
