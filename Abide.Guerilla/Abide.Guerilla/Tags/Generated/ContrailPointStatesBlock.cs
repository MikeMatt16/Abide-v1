using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(72, 4)]
	public unsafe struct ContrailPointStatesBlock
	{
		public enum ScaleFlagsTheseFlagsDetermineWhichFieldsAreScaledByTheContrailDensity9Options
		{
			Duration_0 = 1,
			DurationDelta_1 = 2,
			TransitionDuration_2 = 4,
			TransitionDurationDelta_3 = 8,
			Width_4 = 16,
			Color_5 = 32,
		}
		[Field("duration:seconds:seconds#the time a point spends in this state", null)]
		public FloatBounds Duration1;
		[Field("transition duration:seconds#the time a point takes to transition to the next state", null)]
		public FloatBounds TransitionDuration2;
		[Field("physics", null)]
		public TagReference Physics4;
		[Field("", null)]
		public fixed byte _5[32];
		[Field("width:world units#contrail width at this point", null)]
		public float Width6;
		[Field("color lower bound#contrail color at this point", null)]
		public ColorArgbF ColorLowerBound7;
		[Field("color upper bound#contrail color at this point", null)]
		public ColorArgbF ColorUpperBound8;
		[Field("scale flags#these flags determine which fields are scaled by the contrail density", typeof(ScaleFlagsTheseFlagsDetermineWhichFieldsAreScaledByTheContrailDensity9Options))]
		public int ScaleFlags9;
	}
}
#pragma warning restore CS1591
