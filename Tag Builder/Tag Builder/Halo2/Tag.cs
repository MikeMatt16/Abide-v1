using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using Abide.Tag;
using Abide.Tag.Cache.Generated;

namespace Abide.TagBuilder.Halo2
{
    public sealed class ScenarioStructureTag : TagBase
    {
        public int Index { get; }

        public ScenarioStructureTag(int index)
        {
            Index = index;
        }
    }
    public sealed class Tag : TagBase
    {
        public Tag() { }
    }
    public abstract class TagBase
    {
        public string Name { get; set; }
        public TagId Id { get; set; }
        public IndexEntry SourceEntry { get; set; }
        public ITagGroup TagGroup { get; set; }

        public TagBase()
        {
        }
        public override string ToString()
        {
            return $"{Name}.{TagGroup.Name}";
        }
    }
}
