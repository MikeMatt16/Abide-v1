using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(27, 4)]
	public unsafe struct ShaderPostprocessBitmapTransformOverlayBlock
	{
		[Field("parameter index", null)]
		public int ParameterIndex0;
		[Field("transform index", null)]
		public int TransformIndex1;
		[Field("animation property type", null)]
		public int AnimationPropertyType2;
		[Field("input name", null)]
		public StringId InputName3;
		[Field("range name", null)]
		public StringId RangeName4;
		[Field("time period in seconds", null)]
		public float TimePeriodInSeconds5;
		[Field("function", typeof(ScalarFunctionStructBlock))]
		[Block("Scalar Function Struct", 1, typeof(ScalarFunctionStructBlock))]
		public ScalarFunctionStructBlock Function6;
	}
}
#pragma warning restore CS1591
