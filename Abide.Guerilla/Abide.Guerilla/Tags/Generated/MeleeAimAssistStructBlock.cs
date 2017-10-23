using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct MeleeAimAssistStructBlock
	{
		[Field("magnetism angle:degrees#the maximum angle that magnetism works at full strength", null)]
		public float MagnetismAngle0;
		[Field("magnetism range:world units#the maximum distance that magnetism works at full strength", null)]
		public float MagnetismRange1;
		[Field("", null)]
		public fixed byte _2[8];
		[Field("throttle magnitude", null)]
		public float ThrottleMagnitude3;
		[Field("throttle minimum distance", null)]
		public float ThrottleMinimumDistance4;
		[Field("throttle maximum adjustment angle:degrees", null)]
		public float ThrottleMaximumAdjustmentAngle5;
		[Field("", null)]
		public fixed byte _6[4];
	}
}
#pragma warning restore CS1591
