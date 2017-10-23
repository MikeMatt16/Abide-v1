using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(4, 4)]
	public unsafe struct DamageRegionBlock
	{
		[Field("animation*", typeof(AnimationIndexStructBlock))]
		[Block("Animation Index Struct", 1, typeof(AnimationIndexStructBlock))]
		public AnimationIndexStructBlock Animation0;
	}
}
#pragma warning restore CS1591
