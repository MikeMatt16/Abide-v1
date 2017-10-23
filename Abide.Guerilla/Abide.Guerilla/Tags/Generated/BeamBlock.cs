using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(92, 4)]
	public unsafe struct BeamBlock
	{
		[Field("shader", null)]
		public TagReference Shader0;
		[Field("location", null)]
		public short Location1;
		[Field("", null)]
		public fixed byte _2[2];
		[Field("color", typeof(ColorFunctionStructBlock))]
		[Block("Color Function Struct", 1, typeof(ColorFunctionStructBlock))]
		public ColorFunctionStructBlock Color4;
		[Field("alpha", typeof(ScalarFunctionStructBlock))]
		[Block("Scalar Function Struct", 1, typeof(ScalarFunctionStructBlock))]
		public ScalarFunctionStructBlock Alpha6;
		[Field("width", typeof(ScalarFunctionStructBlock))]
		[Block("Scalar Function Struct", 1, typeof(ScalarFunctionStructBlock))]
		public ScalarFunctionStructBlock Width8;
		[Field("length", typeof(ScalarFunctionStructBlock))]
		[Block("Scalar Function Struct", 1, typeof(ScalarFunctionStructBlock))]
		public ScalarFunctionStructBlock Length10;
		[Field("yaw", typeof(ScalarFunctionStructBlock))]
		[Block("Scalar Function Struct", 1, typeof(ScalarFunctionStructBlock))]
		public ScalarFunctionStructBlock Yaw12;
		[Field("pitch", typeof(ScalarFunctionStructBlock))]
		[Block("Scalar Function Struct", 1, typeof(ScalarFunctionStructBlock))]
		public ScalarFunctionStructBlock Pitch14;
	}
}
#pragma warning restore CS1591
