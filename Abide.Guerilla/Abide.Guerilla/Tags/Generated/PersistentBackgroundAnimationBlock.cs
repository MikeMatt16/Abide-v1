using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct PersistentBackgroundAnimationBlock
	{
		[Field("", null)]
		public fixed byte _0[4];
		[Field("animation period:milliseconds", null)]
		public int AnimationPeriod1;
		[Field("interpolated keyframes", null)]
		[Block("Background Animation Keyframe Reference Block", 64, typeof(BackgroundAnimationKeyframeReferenceBlock))]
		public TagBlock InterpolatedKeyframes2;
	}
}
#pragma warning restore CS1591
