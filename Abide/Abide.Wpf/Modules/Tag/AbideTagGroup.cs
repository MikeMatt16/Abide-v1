using Abide.Tag;
using System;
using System.Collections.ObjectModel;

namespace Abide.Wpf.Modules.Tag
{
    public sealed class AbideTagGroup : AbideTagObject
    {
        private Group group;

        public ObservableCollection<AbideTagBlock> TagBlocks { get; } = new ObservableCollection<AbideTagBlock>();
        public override string Name => group.Name ?? base.Name;
        public string GroupTag => group.Tag;

        public AbideTagGroup(Group group, TagContext context) : base(context)
        {
            this.group = group ?? throw new ArgumentNullException(nameof(group));
            foreach (var block in group.TagBlocks)
            {
                var b = new AbideTagBlock(context, this, block);
                TagBlocks.Add(b);
                b.PostprocessTagBlock();
            }
        }
    }
}
