using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(36, 4)]
	public unsafe struct LightVolumeAspectBlock
	{
		[Field("along axis", typeof(ScalarFunctionStructBlock))]
		[Block("Scalar Function Struct", 1, typeof(ScalarFunctionStructBlock))]
		public ScalarFunctionStructBlock AlongAxis2;
		[Field("away from axis", typeof(ScalarFunctionStructBlock))]
		[Block("Scalar Function Struct", 1, typeof(ScalarFunctionStructBlock))]
		public ScalarFunctionStructBlock AwayFromAxis4;
		[Field("parallel scale", null)]
		public float ParallelScale6;
		[Field("parallel threshold angle:degrees", null)]
		public float ParallelThresholdAngle7;
		[Field("parallel exponent", null)]
		public float ParallelExponent8;
	}
}
#pragma warning restore CS1591
