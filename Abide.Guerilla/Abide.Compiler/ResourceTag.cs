using Abide.HaloLibrary;
using System;

namespace Abide.Compiler
{
    public sealed class TagResourceInfo : IEquatable<TagResourceInfo>
    {
        public bool Merge { get; set; } = false;
        public TagId DestinationId { get; set; } = TagId.Null;
        public string FileName { get; set; } = string.Empty;

        public string TagPath { get; } = string.Empty;
        public TagFourCc Root { get; } = "____";
        public TagId Id { get; } = TagId.Null;

        public TagResourceInfo(string fileName, TagFourCc root, TagId id)
        {
            TagPath = fileName;
            Root = root;
            Id = id;
        }
        public override string ToString()
        {
            return $"{TagPath}.{Root.FourCc} 0x{Id}";
        }
        public bool Equals(TagResourceInfo other)
        {
            return TagPath.Equals(other.TagPath) && Root.Equals(other.Root) && Id.Equals(other.Id);
        }
    }
}
