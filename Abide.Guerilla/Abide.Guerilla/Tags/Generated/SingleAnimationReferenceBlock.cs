using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct SingleAnimationReferenceBlock
	{
		public enum Flags0Options
		{
			Unused_0 = 1,
		}
		[Field("flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("animation period:milliseconds", null)]
		public int AnimationPeriod1;
		[Field("keyframes", null)]
		[Block("Screen Animation Keyframe Reference Block", 64, typeof(ScreenAnimationKeyframeReferenceBlock))]
		public TagBlock Keyframes2;
	}
}
#pragma warning restore CS1591
