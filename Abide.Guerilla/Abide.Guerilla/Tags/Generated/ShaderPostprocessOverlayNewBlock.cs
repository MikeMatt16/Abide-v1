using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct ShaderPostprocessOverlayNewBlock
	{
		[Field("input name", null)]
		public StringId InputName0;
		[Field("range name", null)]
		public StringId RangeName1;
		[Field("time period in seconds", null)]
		public float TimePeriodInSeconds2;
		[Field("function", typeof(ScalarFunctionStructBlock))]
		[Block("Scalar Function Struct", 1, typeof(ScalarFunctionStructBlock))]
		public ScalarFunctionStructBlock Function3;
	}
}
#pragma warning restore CS1591
