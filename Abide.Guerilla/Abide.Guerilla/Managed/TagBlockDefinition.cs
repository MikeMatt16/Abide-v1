using System.Collections.Generic;

namespace Abide.Guerilla.Managed
{
    /// <summary>
    /// Represents a tag block definition.
    /// </summary>
    public sealed class TagBlockDefinition
    {
        /// <summary>
        /// Gets and returns true if this tag block definition represents a tag group; otherwise false.
        /// </summary>
        public bool IsTagGroup
        {
            get { return isTagGroup; }
            internal set { isTagGroup = value; }
        }
        /// <summary>
        /// Gets and returns the latest index of the tag field set.
        /// </summary>
        public int TagFieldSetLatestIndex
        {
            get { return tagFieldSetLatestIndex; }
            internal set { tagFieldSetLatestIndex = value; }
        }
        /// <summary>
        /// Gets and returns this tag block definition's name.
        /// </summary>
        public string Name
        {
            get { return name; }
        }
        /// <summary>
        /// Gets and returns this tag block definition's display name.
        /// </summary>
        public string DisplayName
        {
            get { return displayName; }
        }
        /// <summary>
        /// Gets and returns this tag block definition's maximum element count string.
        /// </summary>
        public string MaximumElementCountString
        {
            get { return maximumElementCountString; }
        }
        /// <summary>
        /// Gets and returns this tag block definition's address.
        /// </summary>
        public int Address
        {
            get { return address; }
            internal set { address = value; }
        }
        /// <summary>
        /// Gets and returns this tag block definition's flags.
        /// </summary>
        public int Flags
        {
            get { return flags; }
        }
        /// <summary>
        /// Gets and returns this tag block definition's field sets address.
        /// </summary>
        public int FieldSetsAddress
        {
            get { return fieldSetsAddress; }
        }
        /// <summary>
        /// Gets and returns this tag block definition's field set count.
        /// </summary>
        public int FieldSetCount
        {
            get { return fieldSetCount; }
            internal set { fieldSetCount = value; }
        }
        /// <summary>
        /// Gets and returns this tag block definition's latest field set address.
        /// </summary>
        public int FieldSetLatestAddress
        {
            get { return fieldSetLatestAddress; }
            internal set { fieldSetLatestAddress = value; }
        }
        /// <summary>
        /// Gets and returns this tag block definition's list of references.
        /// </summary>
        public List<TagBlockDefinition> References
        {
            get { return references; }
        }

        internal TagFieldSet[] tagFieldSets;
        internal List<TagFieldDefinition>[] tagFields;

        private readonly List<TagBlockDefinition> references;
        private int tagFieldSetLatestIndex;
        private bool isTagGroup;
        private int address;
        private int displayNameAddress;
        private int nameAddress;
        private int flags;
        private int maximumElementCount;
        private int maximumElementCountStringAddress;
        private int fieldSetsAddress;
        private int fieldSetCount;
        private int fieldSetLatestAddress;
        private int postProcessProcedure;
        private int formatProcedure;
        private int generateDefaultProcedure;
        private int disposeElementProcedure;
        private string displayName;
        private string name;
        private string maximumElementCountString;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagBlockDefinition"/> class.
        /// </summary>
        public TagBlockDefinition()
        {
            references = new List<TagBlockDefinition>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TagBlockDefinition"/> using the supplied tag block definition structure.
        /// </summary>
        /// <param name="definition">The tag block definition structure to initialize this instance.</param>
        internal TagBlockDefinition(H2Guerilla.TagBlockDefinition definition) : this()
        {
            displayNameAddress = definition.DisplayNameAddress;
            nameAddress = definition.NameAddress;
            flags = definition.Flags;
            maximumElementCount = definition.MaximumElementCount;
            maximumElementCountStringAddress = definition.MaximumElementCountStringAddress;
            fieldSetsAddress = definition.FieldSetsAddress;
            fieldSetCount = definition.FieldSetCount;
            fieldSetLatestAddress = definition.FieldSetLatestAddress;
            postProcessProcedure = definition.PostProcessProcedure;
            formatProcedure = definition.FormatProcedure;
            generateDefaultProcedure = definition.GenerateDefaultProcedure;
            disposeElementProcedure = definition.DisposeElementProcedure;
            displayName = definition.DisplayName;
            name = definition.Name;
            maximumElementCountString = definition.MaximumElementCountString;
        }
    }
}
