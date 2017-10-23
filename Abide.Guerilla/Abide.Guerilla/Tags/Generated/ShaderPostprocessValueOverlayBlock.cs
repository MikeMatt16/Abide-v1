using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(25, 4)]
	public unsafe struct ShaderPostprocessValueOverlayBlock
	{
		[Field("parameter index", null)]
		public int ParameterIndex0;
		[Field("input name", null)]
		public StringId InputName1;
		[Field("range name", null)]
		public StringId RangeName2;
		[Field("time period in seconds", null)]
		public float TimePeriodInSeconds3;
		[Field("function", typeof(ScalarFunctionStructBlock))]
		[Block("Scalar Function Struct", 1, typeof(ScalarFunctionStructBlock))]
		public ScalarFunctionStructBlock Function4;
	}
}
#pragma warning restore CS1591
