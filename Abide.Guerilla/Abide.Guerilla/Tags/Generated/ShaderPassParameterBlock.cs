using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(64, 4)]
	public unsafe struct ShaderPassParameterBlock
	{
		public enum Type2Options
		{
			Bitmap_0 = 0,
			Value_1 = 1,
			Color_2 = 2,
			Switch_3 = 3,
		}
		public enum Flags3Options
		{
			NoBitmapLOD_0 = 1,
			RequiredParameter_1 = 2,
		}
		public enum SourceExtern7Options
		{
			None_0 = 0,
			GLOBALEyeForwardVectorZ_1 = 1,
			GLOBALEyeRightVectorX_2 = 2,
			GLOBALEyeUpVectorY_3 = 3,
			OBJECTPrimaryColor_4 = 4,
			OBJECTSecondaryColor_5 = 5,
			OBJECTFunctionValue_6 = 6,
			LIGHTDiffuseColor_7 = 7,
			LIGHTSpecularColor_8 = 8,
			LIGHTForwardVectorZ_9 = 9,
			LIGHTRightVectorX_10 = 10,
			LIGHTUpVectorY_11 = 11,
			LIGHTObjectRelativeForwardVectorZ_12 = 12,
			LIGHTObjectRelativeRightVectorX_13 = 13,
			LIGHTObjectRelativeUpVectorY_14 = 14,
			LIGHTObjectFalloffValue_15 = 15,
			LIGHTObjectGelColor_16 = 16,
			LIGHTMAPObjectAmbientFactor_17 = 17,
			LIGHTMAPObjectDirectVector_18 = 18,
			LIGHTMAPObjectDirectColor_19 = 19,
			LIGHTMAPObjectIndirectVector_20 = 20,
			LIGHTMAPObjectIndirectColor_21 = 21,
			OLDFOGAtmosphericColor_22 = 22,
			OLDFOGAtmosphericMaxDensity_23 = 23,
			OLDFOGPlanarColor_24 = 24,
			OLDFOGPlanarMaxDensity_25 = 25,
			OLDFOGAtmosphericPlanarBlendValue_26 = 26,
			OLDFOGObjectAtmosphericDensity_27 = 27,
			OLDFOGObjectPlanarDensity_28 = 28,
			OLDFOGObjectColor_29 = 29,
			OLDFOGObjectDensity_30 = 30,
			OBJECTModelAlpha_31 = 31,
			OBJECTShadowAlpha_32 = 32,
			LIGHTOverbrightenDiffuseShift_33 = 33,
			LIGHTOverbrightenSpecularShift_34 = 34,
			LIGHTDiffuseContrast_35 = 35,
			LIGHTSpecularGel_36 = 36,
			SHADERSpecularType_37 = 37,
			Pad3_38 = 38,
			Pad3Scale_39 = 39,
			PadThai_40 = 40,
			TacoSalad_41 = 41,
			AnisotropicBinormal_42 = 42,
			OBJECTLIGHTShadowFade_43 = 43,
			LIGHTShadowFade_44 = 44,
			OLDFOGAtmosphericDensity_45 = 45,
			OLDFOGPlanarDensity_46 = 46,
			OLDFOGPlanarDensityInvert_47 = 47,
			OBJECTChangeColorTertiary_48 = 48,
			OBJECTChangeColorQuaternary_49 = 49,
			LIGHTMAPObjectSpecularColor_50 = 50,
			SHADERLightmapType_51 = 51,
			LIGHTMAPObjectAmbientColor_52 = 52,
			SHADERLightmapSpecularBrightness_53 = 53,
			GLOBALLightmapShadowDarkening_54 = 54,
			LIGHTMAPObjectEnvBrightness_55 = 55,
			FOGAtmosphericMaxDensity_56 = 56,
			FOGAtmosphericColor_57 = 57,
			FOGAtmosphericColorAdj_58 = 58,
			FOGAtmosphericPlanarBlend_59 = 59,
			FOGAtmosphericPlanarBlendAdjInv_60 = 60,
			FOGAtmosphericPlanarBlendAdj_61 = 61,
			FOGSecondaryMaxDensity_62 = 62,
			FOGSecondaryColor_63 = 63,
			FOGSecondaryColorAdj_64 = 64,
			FOGAtmosphericSecondaryBlend_65 = 65,
			FOGAtmosphericSecondaryBlendAdjInv_66 = 66,
			FOGAtmosphericSecondaryBlendAdj_67 = 67,
			FOGSkyDensity_68 = 68,
			FOGSkyColor_69 = 69,
			FOGSkyColorAdj_70 = 70,
			FOGPlanarMaxDensity_71 = 71,
			FOGPlanarColor_72 = 72,
			FOGPlanarColorAdj_73 = 73,
			FOGPlanarEyeDensity_74 = 74,
			FOGPlanarEyeDensityAdjInv_75 = 75,
			FOGPlanarEyeDensityAdj_76 = 76,
			HUDWaypointPrimaryColor_77 = 77,
			HUDWaypointSecondaryColor_78 = 78,
			LIGHTMAPObjectSpecularColorTimesOneHalf_79 = 79,
			LIGHTSpecularEnabled_80 = 80,
			LIGHTDefinitionSpecularEnabled_81 = 81,
			OBJECTActiveCamoAmount_82 = 82,
			OBJECTSuperCamoAmount_83 = 83,
			HUDCustomColor1_84 = 84,
			HUDCustomColor2_85 = 85,
			HUDCustomColor3_86 = 86,
			HUDCustomColor4_87 = 87,
			OBJECTActiveCamoRGB_88 = 88,
			FOGPatchyPlaneNXyz_89 = 89,
			FOGPatchyPlaneDW_90 = 90,
			HUDGlobalFade_91 = 91,
			SCREENEFFECTPrimary_92 = 92,
			SCREENEFFECTSecondary_93 = 93,
		}
		[Field("Name^", null)]
		public StringId Name0;
		[Field("Explanation", null)]
		[Data(65535)]
		public TagBlock Explanation1;
		[Field("Type", typeof(Type2Options))]
		public short Type2;
		[Field("Flags", typeof(Flags3Options))]
		public short Flags3;
		[Field("Default Bitmap", null)]
		public TagReference DefaultBitmap4;
		[Field("Default Const Value", null)]
		public float DefaultConstValue5;
		[Field("Default Const Color", null)]
		public ColorRgbF DefaultConstColor6;
		[Field("Source Extern", typeof(SourceExtern7Options))]
		public short SourceExtern7;
		[Field("", null)]
		public fixed byte _8[2];
	}
}
#pragma warning restore CS1591
