using Abide.HaloLibrary;

namespace Abide.Guerilla.Managed
{
    /// <summary>
    /// Represents a tag group definition.
    /// </summary>
    public sealed class TagGroupDefinition
    {
        /// <summary>
        /// Gets and returns the tag group's name.
        /// </summary>
        public string Name
        {
            get { return name; }
        }
        /// <summary>
        /// Gets and returns the tag group's group tag.
        /// </summary>
        public Tag GroupTag
        {
            get { return groupTag; }
        }
        /// <summary>
        /// Gets and returns the tag group's parent group tag.
        /// </summary>
        public Tag ParentGroupTag
        {
            get { return parentGroupTag; }
        }
        /// <summary>
        /// Gets and returns the tag group's version address.
        /// </summary>
        public short Version
        {
            get { return version; }
        }
        /// <summary>
        /// Gets and returns the tag group's definition address.
        /// </summary>
        public int DefinitionAddress
        {
            get { return definitionAddress; }
        }
        
        private int flags;
        private Tag groupTag;
        private Tag parentGroupTag;
        private short version;
        private byte initialized;
        private int definitionAddress;
        private int[] childGroupTags;
        private short childsCount;
        private string name;
        private string defaultTagPath;

        /// <summary>
        /// Intializes a new instance of the <see cref="TagGroupDefinition"/> class.
        /// </summary>
        public TagGroupDefinition() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="TagGroupDefinition"/> class using the supplied guerilla reader.
        /// </summary>
        /// <param name="reader">The guerilla binary reader.</param>
        internal TagGroupDefinition(H2Guerilla.GuerillaBinaryReader reader) : this()
        {
            //Read tag group
            H2Guerilla.TagGroup tagGroup = reader.ReadTagGroup();
            
            //Setup
            flags = tagGroup.Flags;
            groupTag = tagGroup.GroupTag;
            parentGroupTag = tagGroup.ParentGroupTag;
            version = tagGroup.Version;
            initialized = tagGroup.Initialized;
            definitionAddress = tagGroup.DefinitionAddress;
            childGroupTags = tagGroup.ChildGroupTags;
            childsCount = tagGroup.ChildsCount;
            defaultTagPath = reader.ReadLocalizedString(tagGroup.DefaultTagPathAddress);
            name = reader.ReadLocalizedString(tagGroup.NameAddress);
        }
        /// <summary>
        /// Returns a string that represents this tag group definition.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"{parentGroupTag}.{groupTag} {name}";
        }
    }
}
