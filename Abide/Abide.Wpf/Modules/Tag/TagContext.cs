using Abide.HaloLibrary.Halo2.Retail;
using Abide.Tag;
using System;

namespace Abide.Wpf.Modules.Tag
{
    public abstract class TagContext
    {
        public abstract TagContextMode Mode { get; }
        public abstract Block CreateBlock(string blockName);
        public virtual string ResolveName(AbideTagObject tagObject, string parameter)
        {
            return tagObject.Name;
        }
    }

    public enum TagContextMode
    {
        Cache,
        Guerilla,
        Other
    }

    public sealed class HaloMapTagContext : TagContext
    {
        public HaloMapFile Map { get; }
        public override TagContextMode Mode => TagContextMode.Cache;

        public HaloMapTagContext(HaloMapFile map)
        {
            Map = map ?? throw new ArgumentNullException(nameof(map));
        }
        public override Block CreateBlock(string blockName)
        {
            var b = Abide.Tag.Cache.Generated.TagLookup.CreateTagBlock(blockName);
            b.Initialize();

            return b;
        }
        public override string ResolveName(AbideTagObject tagObject, string parameter)
        {
            return base.ResolveName(tagObject, parameter);
        }
    }
    public sealed class GuerillaTagContext : TagContext
    {
        public override TagContextMode Mode => TagContextMode.Guerilla;
        public override Block CreateBlock(string blockName)
        {
            var b = Abide.Tag.Guerilla.Generated.TagLookup.CreateTagBlock(blockName);
            b.Initialize();

            return b;
        }
        public override string ResolveName(AbideTagObject tagObject, string parameter)
        {
            return base.ResolveName(tagObject, parameter);
        }
    }
}
