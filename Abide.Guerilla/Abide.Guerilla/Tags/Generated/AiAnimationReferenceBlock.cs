using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(60, 4)]
	public unsafe struct AiAnimationReferenceBlock
	{
		[Field("animation name^", null)]
		public String AnimationName0;
		[Field("animation graph#leave this blank to use the unit's normal animation graph", null)]
		public TagReference AnimationGraph1;
		[Field("", null)]
		public fixed byte _2[12];
	}
}
#pragma warning restore CS1591
