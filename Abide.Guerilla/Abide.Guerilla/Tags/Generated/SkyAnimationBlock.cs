using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(36, 4)]
	public unsafe struct SkyAnimationBlock
	{
		[Field("Animation Index#Index of the animation in the animation graph.", null)]
		public short AnimationIndex0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("Period:sec", null)]
		public float Period2;
		[Field("", null)]
		public fixed byte _3[28];
	}
}
#pragma warning restore CS1591
