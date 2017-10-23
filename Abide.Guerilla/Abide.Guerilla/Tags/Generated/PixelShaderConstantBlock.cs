using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(6, 4)]
	public unsafe struct PixelShaderConstantBlock
	{
		public enum ParameterType0Options
		{
			Bitmap_0 = 0,
			Value_1 = 1,
			Color_2 = 2,
			Switch_3 = 3,
		}
		public enum ComponentMask3Options
		{
			XValue_0 = 0,
			YValue_1 = 1,
			ZValue_2 = 2,
			WValue_3 = 3,
			XyzRgbColor_4 = 4,
		}
		[Field("parameter type", typeof(ParameterType0Options))]
		public byte ParameterType0;
		[Field("combiner index", null)]
		public int CombinerIndex1;
		[Field("register index", null)]
		public int RegisterIndex2;
		[Field("component mask", typeof(ComponentMask3Options))]
		public byte ComponentMask3;
		[Field("", null)]
		public fixed byte _4[1];
		[Field("", null)]
		public fixed byte _5[1];
	}
}
#pragma warning restore CS1591
