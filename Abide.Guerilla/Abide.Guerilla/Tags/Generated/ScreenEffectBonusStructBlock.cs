using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(32, 4)]
	public unsafe struct ScreenEffectBonusStructBlock
	{
		[Field("halfscreen screen effect", null)]
		public TagReference HalfscreenScreenEffect0;
		[Field("quarterscreen screen effect", null)]
		public TagReference QuarterscreenScreenEffect1;
	}
}
#pragma warning restore CS1591
