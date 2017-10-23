using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(28, 4)]
	public unsafe struct ShaderAnimationPropertyBlock
	{
		public enum Type0Options
		{
			BitmapScaleUniform_0 = 0,
			BitmapScaleX_1 = 1,
			BitmapScaleY_2 = 2,
			BitmapScaleZ_3 = 3,
			BitmapTranslationX_4 = 4,
			BitmapTranslationY_5 = 5,
			BitmapTranslationZ_6 = 6,
			BitmapRotationAngle_7 = 7,
			BitmapRotationAxisX_8 = 8,
			BitmapRotationAxisY_9 = 9,
			BitmapRotationAxisZ_10 = 10,
			Value_11 = 11,
			Color_12 = 12,
			BitmapIndex_13 = 13,
		}
		[Field("Type^", typeof(Type0Options))]
		public short Type0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("Input Name", null)]
		public StringId InputName2;
		[Field("Range Name", null)]
		public StringId RangeName3;
		[Field("Time Period:sec", null)]
		public float TimePeriod4;
		[Field("Function", typeof(MappingFunctionBlock))]
		[Block("Mapping Function", 1, typeof(MappingFunctionBlock))]
		public MappingFunctionBlock Function7;
	}
}
#pragma warning restore CS1591
