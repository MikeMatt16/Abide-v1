using System;
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
            get { return fieldSetLatestAddress; }
            internal set { fieldSetLatestAddress = value; }
        }
        /// <summary>
        /// Gets and returns this tag block definition's name.
        /// </summary>
        public string Name
        {
            get { return name; }
            internal set { name = value; }
        }
        /// <summary>
        /// Gets and returns this tag block definition's display name.
        /// </summary>
        public string DisplayName
        {
            get { return displayName; }
            internal set { displayName = value; }
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
        /// Gets and returns this tag block definition's maximum element count.
        /// </summary>
        public int MaximumElementCount
        {
            get { return maximumElementCount; }
        }
        /// <summary>
        /// Gets and returns this tag block definition's field sets address.
        /// </summary>
        public int FieldSetsAddress
        {
            get { return fieldSetsAddress; }
            internal set { fieldSetsAddress = value; }
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
        internal List<TagFieldDefinition> tagFields;

        private readonly List<TagBlockDefinition> references;
        private int tagFieldSetLatestIndex;
        private bool isTagGroup;
        private int address;
        private int flags;
        private int maximumElementCount;
        private int fieldSetsAddress;
        private int fieldSetCount;
        private int fieldSetLatestAddress;
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
        /// Initializes a new instance of the <see cref="TagBlockDefinition"/> using the supplied guerilla reader.
        /// </summary>
        /// <param name="reader">The guerilla binary reader.</param>
        internal TagBlockDefinition(H2Guerilla.GuerillaBinaryReader reader) : this()
        {
            //Read tag block definition
            H2Guerilla.TagBlockDefinition tagBlockDefinition = reader.ReadTagBlockDefinition();

            //Initialize
            flags = tagBlockDefinition.Flags;
            maximumElementCount = tagBlockDefinition.MaximumElementCount;
            fieldSetsAddress = tagBlockDefinition.FieldSetsAddress;
            fieldSetCount = tagBlockDefinition.FieldSetCount;
            fieldSetLatestAddress = tagBlockDefinition.FieldSetLatestAddress;
            displayName = reader.ReadLocalizedString(tagBlockDefinition.DisplayNameAddress);
            name = reader.ReadLocalizedString(tagBlockDefinition.NameAddress);
            maximumElementCountString = reader.ReadLocalizedString(tagBlockDefinition.MaximumElementCountStringAddress);
        }
        /// <summary>
        /// Returns a string that represents this tag block definition.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"{name} \"{displayName}\" Max Count: {maximumElementCountString} Field Sets: {fieldSetCount}";
        }
        /// <summary>
        /// Gets the field set of this block at a given index.
        /// </summary>
        /// <param name="index">The zero-based index of the field set.</param>
        /// <returns>A <see cref="TagFieldSet"/> instance.</returns>
        public TagFieldSet GetFieldSet(int index)
        {
            //Check
            if (index < 0 || index > fieldSetCount) throw new ArgumentOutOfRangeException(nameof(index));
            return tagFieldSets[index];
        }
        /// <summary>
        /// Gets the latest field set of this block.
        /// </summary>
        /// <returns>A <see cref="TagFieldSet"/> instance.</returns>
        public TagFieldSet GetLatestFieldSet()
        {
            return tagFieldSets[tagFieldSetLatestIndex];
        }
        /// <summary>
        /// Gets an array of fields for the latest field set of this block.
        /// </summary>
        /// <returns>A <see cref="TagFieldDefinition"/> array.</returns>
        public TagFieldDefinition[] GetLatestFieldDefinitions()
        {
            return tagFields.ToArray();
        }
    }
}
