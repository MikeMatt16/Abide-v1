using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct AnimationEntryBlock
	{
		[Field("label^", null)]
		public StringId Label0;
		[Field("animation*", typeof(AnimationIndexStructBlock))]
		[Block("Animation Index Struct", 1, typeof(AnimationIndexStructBlock))]
		public AnimationIndexStructBlock Animation1;
	}
}
#pragma warning restore CS1591
