using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(4, 4)]
	public unsafe struct AnimationFrameEventBlock
	{
		public enum Type0Options
		{
			PrimaryKeyframe_0 = 0,
			SecondaryKeyframe_1 = 1,
			LeftFoot_2 = 2,
			RightFoot_3 = 3,
			AllowInterruption_4 = 4,
			TransitionA_5 = 5,
			TransitionB_6 = 6,
			TransitionC_7 = 7,
			TransitionD_8 = 8,
			BothFeetShuffle_9 = 9,
			BodyImpact_10 = 10,
		}
		[Field("type", typeof(Type0Options))]
		public short Type0;
		[Field("frame", null)]
		public short Frame1;
	}
}
#pragma warning restore CS1591
