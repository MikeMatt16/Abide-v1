using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(76, 4)]
	public unsafe struct LiquidCoreBlock
	{
		[Field("", null)]
		public fixed byte _0[12];
		[Field("bitmap index", null)]
		public short BitmapIndex1;
		[Field("", null)]
		public fixed byte _2[2];
		[Field("thickness", typeof(ScalarFunctionStructBlock))]
		[Block("Scalar Function Struct", 1, typeof(ScalarFunctionStructBlock))]
		public ScalarFunctionStructBlock Thickness4;
		[Field("color", typeof(ColorFunctionStructBlock))]
		[Block("Color Function Struct", 1, typeof(ColorFunctionStructBlock))]
		public ColorFunctionStructBlock Color6;
		[Field("brightness-time", typeof(ScalarFunctionStructBlock))]
		[Block("Scalar Function Struct", 1, typeof(ScalarFunctionStructBlock))]
		public ScalarFunctionStructBlock BrightnessTime8;
		[Field("brightness-facing", typeof(ScalarFunctionStructBlock))]
		[Block("Scalar Function Struct", 1, typeof(ScalarFunctionStructBlock))]
		public ScalarFunctionStructBlock BrightnessFacing10;
		[Field("along-axis scale", typeof(ScalarFunctionStructBlock))]
		[Block("Scalar Function Struct", 1, typeof(ScalarFunctionStructBlock))]
		public ScalarFunctionStructBlock AlongAxisScale12;
	}
}
#pragma warning restore CS1591
