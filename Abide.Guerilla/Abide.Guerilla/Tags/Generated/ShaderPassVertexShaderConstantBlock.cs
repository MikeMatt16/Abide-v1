using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(12, 4)]
	public unsafe struct ShaderPassVertexShaderConstantBlock
	{
		public enum ScaleByTextureStage1Options
		{
			None_0 = 0,
			Stage0_1 = 1,
			Stage1_2 = 2,
			Stage2_3 = 3,
			Stage3_4 = 4,
		}
		public enum RegisterBank2Options
		{
			Vn015_0 = 0,
			Cn012_1 = 1,
		}
		public enum ComponentMask4Options
		{
			XValue_0 = 0,
			YValue_1 = 1,
			ZValue_2 = 2,
			WValue_3 = 3,
			XyzRgbColor_4 = 4,
			XUniformScale_5 = 5,
			YUniformScale_6 = 6,
			ZUniformScale_7 = 7,
			WUniformScale_8 = 8,
			Xy2DScale_9 = 9,
			Zw2DScale_10 = 10,
			Xy2DTranslation_11 = 11,
			Zw2DTranslation_12 = 12,
			Xyzw2DSimpleXform_13 = 13,
			XywRow12DAffineXform_14 = 14,
			XywRow22DAffineXform_15 = 15,
			Xyz3DScale_16 = 16,
			Xyz3DTranslation_17 = 17,
			XyzwRow13DAffineXform_18 = 18,
			XyzwRow23DAffineXform_19 = 19,
			XyzwRow33DAffineXform_20 = 20,
		}
		[Field("Source Parameter", null)]
		public StringId SourceParameter0;
		[Field("Scale by Texture Stage", typeof(ScaleByTextureStage1Options))]
		public short ScaleByTextureStage1;
		[Field("Register Bank", typeof(RegisterBank2Options))]
		public short RegisterBank2;
		[Field("Register Index", null)]
		public short RegisterIndex3;
		[Field("Component Mask", typeof(ComponentMask4Options))]
		public short ComponentMask4;
	}
}
#pragma warning restore CS1591
