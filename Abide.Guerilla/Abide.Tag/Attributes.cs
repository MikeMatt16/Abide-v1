using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abide.Tag
{
    public abstract class TagSerializableAttribute : Attribute
    {
        public abstract object Deserialize(Stream serializedStream);
        public abstract void Serialize(Stream serializationStream);
    }

    public abstract class TagGroupDefinitionAttribute  : TagSerializableAttribute
    {
        public string Name { get; }
        public string GroupTag { get; }
        public TagGroupDefinitionAttribute(string name, string groupTag)
        {
            Name = name;
            GroupTag = groupTag;
        }
    }

    public abstract class TagBlockDefinitionAttribute : TagSerializableAttribute
    {
        public string Name { get; }
        public string DisplayName { get; }
        public int MaximumElementCount { get; }
        public int Alignment { get; }
    }

    public abstract class TagFieldDefinitionAttribute : TagSerializableAttribute
    {
        public bool IsVisible => string.IsNullOrEmpty(Name);
        public string Name { get; }
    }
}
