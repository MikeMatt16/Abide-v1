using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(28, 4)]
	public unsafe struct ScenarioInterpolatorBlock
	{
		[Field("Name^", null)]
		public StringId Name0;
		[Field("Accelerator Name:Interpolator", null)]
		public StringId AcceleratorName1;
		[Field("Multiplier Name:Interpolator", null)]
		public StringId MultiplierName2;
		[Field("Function", typeof(ScalarFunctionStructBlock))]
		[Block("Scalar Function Struct", 1, typeof(ScalarFunctionStructBlock))]
		public ScalarFunctionStructBlock Function3;
		[Field("", null)]
		public fixed byte _4[2];
		[Field("", null)]
		public fixed byte _5[2];
	}
}
#pragma warning restore CS1591
