using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(80, 4)]
	public unsafe struct ShaderPassTextureBlock
	{
		public enum SourceExtern1Options
		{
			None_0 = 0,
			GLOBALVectorNormalization_1 = 1,
			UNUSED_2 = 2,
			GLOBALTargetTexaccum_3 = 3,
			UNUSED_4 = 4,
			GLOBALTargetFrameBuffer_5 = 5,
			GLOBATargetZ_6 = 6,
			UNUSED_7 = 7,
			GLOBALTargetShadow_8 = 8,
			LIGHTFalloff_9 = 9,
			LIGHTGel_10 = 10,
			LIGHTMAP_11 = 11,
			UNUSED_12 = 12,
			GLOBALShadowBuffer_13 = 13,
			GLOBALGradientSeparate_14 = 14,
			GLOBALGradientProduct_15 = 15,
			HUDBitmap_16 = 16,
			GLOBALActiveCamo_17 = 17,
			GLOBALTextureCamera_18 = 18,
			GLOBALWaterReflection_19 = 19,
			GLOBALWaterRefraction_20 = 20,
			GLOBALAux1_21 = 21,
			GLOBALAux2_22 = 22,
			GLOBALParticleDistortion_23 = 23,
			GLOBALConvolution1_24 = 24,
			GLOBALConvolution2_25 = 25,
			SHADERActiveCamoBump_26 = 26,
			FIRSTPERSONScope_27 = 27,
		}
		public enum Mode4Options
		{
			_2D_0 = 0,
			_3D_1 = 1,
			CubeMap_2 = 2,
			Passthrough_3 = 3,
			Texkill_4 = 4,
			_2DDependentAR_5 = 5,
			_2DDependentGB_6 = 6,
			_2DBumpenv_7 = 7,
			_2DBumpenvLuminance_8 = 8,
			_3DBRDF_9 = 9,
			DotProduct_10 = 10,
			DotProduct2D_11 = 11,
			DotProduct3D_12 = 12,
			DotProductCubeMap_13 = 13,
			DotProductZW_14 = 14,
			DotReflectDiffuse_15 = 15,
			DotReflectSpecular_16 = 16,
			DotReflectSpecularConst_17 = 17,
			None_18 = 18,
		}
		public enum DotMapping6Options
		{
			_0To1_0 = 0,
			SignedD3D_1 = 1,
			SignedGL_2 = 2,
			SignedNV_3 = 3,
			HILO0To1_4 = 4,
			HILOSignedHemisphereD3D_5 = 5,
			HILOSignedHemisphereGL_6 = 6,
			HILOSignedHemisphereNV_7 = 7,
		}
		[Field("Source Parameter", null)]
		public StringId SourceParameter0;
		[Field("Source Extern", typeof(SourceExtern1Options))]
		public short SourceExtern1;
		[Field("", null)]
		public fixed byte _2[2];
		[Field("", null)]
		public fixed byte _3[2];
		[Field("Mode", typeof(Mode4Options))]
		public short Mode4;
		[Field("", null)]
		public fixed byte _5[2];
		[Field("Dot Mapping", typeof(DotMapping6Options))]
		public short DotMapping6;
		[Field("Input Stage:[0,3]", null)]
		public short InputStage7;
		[Field("", null)]
		public fixed byte _8[2];
		[Field("address state", null)]
		[Block("Address State", 1, typeof(ShaderTextureStateAddressStateBlock))]
		public TagBlock AddressState9;
		[Field("filter state", null)]
		[Block("Filter State", 1, typeof(ShaderTextureStateFilterStateBlock))]
		public TagBlock FilterState10;
		[Field("kill state", null)]
		[Block("Kill State", 1, typeof(ShaderTextureStateKillStateBlock))]
		public TagBlock KillState11;
		[Field("misc state", null)]
		[Block("Misc State", 1, typeof(ShaderTextureStateMiscStateBlock))]
		public TagBlock MiscState12;
		[Field("constants", null)]
		[Block("Texture Constant", 10, typeof(ShaderTextureStateConstantBlock))]
		public TagBlock Constants13;
	}
}
#pragma warning restore CS1591
