using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct TorqueCurveStructBlock
	{
		[Field("min torque", null)]
		public float MinTorque0;
		[Field("max torque", null)]
		public float MaxTorque1;
		[Field("peak torque scale", null)]
		public float PeakTorqueScale2;
		[Field("past peak torque exponent", null)]
		public float PastPeakTorqueExponent3;
		[Field("torque at max angular velocity#generally 0 for loading torque and something less than max torque for cruising torque", null)]
		public float TorqueAtMaxAngularVelocity4;
		[Field("torque at 2x max angular velocity", null)]
		public float TorqueAt2xMaxAngularVelocity5;
		[Field("", null)]
		public fixed byte _6[8];
	}
}
#pragma warning restore CS1591
