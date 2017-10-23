using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(56, 4)]
	public unsafe struct AnimationReferenceBlock
	{
		public enum Flags0Options
		{
			Unused_0 = 1,
		}
		public enum AmbientAnimationLoopingStyle13Options
		{
			NONE_0 = 0,
			ReverseLoop_1 = 1,
			Loop_2 = 2,
			DonTLoop_3 = 3,
		}
		[Field("flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("animation period:milliseconds", null)]
		public int AnimationPeriod2;
		[Field("keyframes", null)]
		[Block("Screen Animation Keyframe Reference Block", 64, typeof(ScreenAnimationKeyframeReferenceBlock))]
		public TagBlock Keyframes3;
		[Field("", null)]
		public fixed byte _4[4];
		[Field("", null)]
		public fixed byte _5[12];
		[Field("animation period:milliseconds", null)]
		public int AnimationPeriod7;
		[Field("keyframes", null)]
		[Block("Screen Animation Keyframe Reference Block", 64, typeof(ScreenAnimationKeyframeReferenceBlock))]
		public TagBlock Keyframes8;
		[Field("", null)]
		public fixed byte _9[4];
		[Field("", null)]
		public fixed byte _10[12];
		[Field("animation period:milliseconds", null)]
		public int AnimationPeriod12;
		[Field("ambient animation looping style", typeof(AmbientAnimationLoopingStyle13Options))]
		public short AmbientAnimationLoopingStyle13;
		[Field("", null)]
		public fixed byte _14[2];
		[Field("keyframes", null)]
		[Block("Screen Animation Keyframe Reference Block", 64, typeof(ScreenAnimationKeyframeReferenceBlock))]
		public TagBlock Keyframes15;
		[Field("", null)]
		public fixed byte _16[16];
	}
}
#pragma warning restore CS1591
