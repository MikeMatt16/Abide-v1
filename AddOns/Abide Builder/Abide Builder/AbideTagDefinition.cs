using Abide.Builder.Tags.TagDefinition;
using Abide.HaloLibrary;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Abide.Builder
{
    /// <summary>
    /// Represents a tag definition.
    /// </summary>
    public sealed class AbideTagDefinition
    {
        /// <summary>
        /// Gets and returns the tag definition's tag.
        /// </summary>
        private Tag Tag
        {
            get { return tag; }
        }
        /// <summary>
        /// Gets and returns the tag definition's size in bytes.
        /// </summary>
        public uint Size
        {
            get { return size; }
        }

        private readonly Tag tag = "null";
        private readonly uint size = 0;
        private readonly AbideTagDefinitionMember[] members;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbideTagDefinition"/> class using the supplied tag root.
        /// </summary>
        /// <param name="root">The tag root.</param>
        public AbideTagDefinition(Tag root) : this(Tags.AbideTags.GetTagDefinition(root)) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="AbideTagDefinition"/> class using the supplied type.
        /// </summary>
        /// <param name="tagType">The tag definition type.</param>
        /// <exception cref="NullReferenceException"><paramref name="tagType"/>is null.</exception>
        public AbideTagDefinition(Type tagType)
        {
            //Check
            if (tagType == null) throw new ArgumentNullException(nameof(tagType));

            //Get Tag Definition attribute
            TagDefinitionAttribute tagDefinition = tagType.GetCustomAttribute<TagDefinitionAttribute>();
            List<AbideTagDefinitionMember> members = new List<AbideTagDefinitionMember>();

            //Check
            if (tagDefinition != null)
            {
                //Setup
                tag = tagDefinition.Tag;
                size = tagDefinition.Size;

                //Loop
                foreach (Type child in tagType.GetNestedTypes())
                {
                    //Get Member
                    AbideTagDefinitionMember member = new AbideTagDefinitionMember(child);

                    //Check
                    if (member.MemberType != TagMemberTypes.None) members.Add(member);
                }
            }

            //Set
            this.members = members.ToArray();
        }
        /// <summary>
        /// Returns a string representation if this tag definition.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"Abide Tag \'{tag}\' Size: {size}";
        }
        /// <summary>
        /// Returns an array of tag members.
        /// </summary>
        /// <returns>An array of <see cref="AbideTagDefinitionMember"/> elements.</returns>
        public AbideTagDefinitionMember[] GetTagMembers()
        {
            return (AbideTagDefinitionMember[])members.Clone();
        }
    }

    /// <summary>
    /// Represents a tag definition member.
    /// </summary>
    public sealed class AbideTagDefinitionMember
    {
        /// <summary>
        /// Gets and returns the name of the member.
        /// </summary>
        public string Name
        {
            get { return name; }
        }
        /// <summary>
        /// Gets and returns the tag member type.
        /// </summary>
        public TagMemberTypes MemberType
        {
            get { return memberType; }
        }
        /// <summary>
        /// Gets and returns the offset of the member.
        /// </summary>
        public uint Offset
        {
            get { return offset; }
        }
        /// <summary>
        /// Gets and returns the size of a block.
        /// This value is only valid if <see cref="MemberType"/> is <see cref="TagMemberTypes.TagBlock"/>.
        /// </summary>
        public uint Size
        {
            get { return size; }
        }
        /// <summary>
        /// Gets and returns the block alignment.
        /// This value is only valid if <see cref="MemberType"/> is <see cref="TagMemberTypes.TagBlock"/>.
        /// </summary>
        public uint Alignment
        {
            get { return alignment; }
        }
        /// <summary>
        /// Gets and returns the maximum number of blocks.
        /// This value is only valid if <see cref="MemberType"/> is <see cref="TagMemberTypes.TagBlock"/>.
        /// </summary>
        public uint MaxBlockCount
        {
            get { return maxBlockCount; }
        }

        private readonly AbideTagDefinitionMember[] members;
        private readonly TagMemberTypes memberType = TagMemberTypes.None;
        private readonly string name;
        private readonly uint offset;
        private readonly uint size;
        private readonly uint alignment;
        private readonly uint maxBlockCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbideTagDefinitionMember"/> class using the supplied type.
        /// </summary>
        /// <param name="memberType">The member type.</param>
        public AbideTagDefinitionMember(Type memberType)
        {
            //Prepare
            TagBlockAttribute tagBlockAttribute = memberType.GetCustomAttribute<TagBlockAttribute>();
            DefinitionAttribute baseAttribute = memberType.GetCustomAttribute<DefinitionAttribute>();
            StringIdentifierAttribute stringIdAttribute = memberType.GetCustomAttribute<StringIdentifierAttribute>();
            TagIdentifierAttribute tagIdAttribute = memberType.GetCustomAttribute<TagIdentifierAttribute>();
            TagAttribute tagAttribute = memberType.GetCustomAttribute<TagAttribute>();
            List<AbideTagDefinitionMember> members = new List<AbideTagDefinitionMember>();
            bool search = true;

            name = baseAttribute.Name;
            offset = baseAttribute.Offset;
            if (tagBlockAttribute != null)
            {
                this.memberType = TagMemberTypes.TagBlock;
                size = tagBlockAttribute.Size;
                alignment = tagBlockAttribute.Alignment;
                maxBlockCount = tagBlockAttribute.MaxBlockCount;
            }
            else if (stringIdAttribute != null)
                this.memberType = TagMemberTypes.StringId;
            else if (tagIdAttribute != null)
                this.memberType = TagMemberTypes.TagId;
            else if (tagAttribute != null)
                this.memberType = TagMemberTypes.Tag;
            else search = false;

            //Loop?
            if (search)
                foreach (Type child in memberType.GetNestedTypes())
                {
                    //Get Member
                    AbideTagDefinitionMember member = new AbideTagDefinitionMember(child);

                    //Check
                    if (member.MemberType != TagMemberTypes.None) members.Add(member);
                }

            //Set
            this.members = members.ToArray();
        }
        /// <summary>
        /// Returns a string representation of this tag member.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"{name} {Enum.GetName(typeof(TagMemberTypes), memberType)} Offset: {offset}";
        }
        /// <summary>
        /// Returns an array of tag members.
        /// </summary>
        /// <returns>An array of <see cref="AbideTagDefinitionMember"/> elements.</returns>
        public AbideTagDefinitionMember[] GetTagMembers()
        {
            return (AbideTagDefinitionMember[])members.Clone();
        }
    }

    /// <summary>
    /// Represents an enumeration containing tag member types.
    /// </summary>
    public enum TagMemberTypes
    {
        /// <summary>
        /// No specified type.
        /// </summary>
        None,
        /// <summary>
        /// A tag type.
        /// </summary>
        Tag,
        /// <summary>
        /// A tag identifer type.
        /// </summary>
        TagId,
        /// <summary>
        /// A string identifier type.
        /// </summary>
        StringId,
        /// <summary>
        /// A tag block type.
        /// </summary>
        TagBlock
    };
}
