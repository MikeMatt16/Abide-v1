using Abide.Guerilla.Types;
using Abide.HaloLibrary;
namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("bitmap", "bitm", "����", typeof(BitmapBlock))]
	[FieldSet(112, 4)]
	public unsafe struct BitmapBlock
	{
		public enum TypeOptions
		{
			_2DTextures_0 = 0,
			_3DTextures_1 = 1,
			CubeMaps_2 = 2,
			Sprites_3 = 3,
			InterfaceBitmaps_4 = 4,
		}
		public enum FormatOptions
		{
			CompressedWithColorKeyTransparency_0 = 0,
			CompressedWithExplicitAlpha_1 = 1,
			CompressedWithInterpolatedAlpha_2 = 2,
			_16BitColor_3 = 3,
			_32BitColor_4 = 4,
			Monochrome_5 = 5,
		}
		public enum UsageOptions
		{
			AlphaBlend_0 = 0,
			Default_1 = 1,
			HeightMap_2 = 2,
			DetailMap_3 = 3,
			LightMap_4 = 4,
			VectorMap_5 = 5,
			HeightMapBLUE255_6 = 6,
			Embm_7 = 7,
			HeightMapA8L8_8 = 8,
			HeightMapG8B8_9 = 9,
			HeightMapG8B8W_Alpha_10 = 10,
		}
		public enum FlagsOptions
		{
			EnableDiffusionDithering_0 = 1,
			DisableHeightMapCompression_1 = 2,
			UniformSpriteSequences_2 = 4,
			FilthySpriteBugFix_3 = 8,
			UseSharpBumpFilter_4 = 16,
			UNUSED_5 = 32,
			UseClamped_MirroredBumpFilter_6 = 64,
			InvertDetailFade_7 = 128,
			SwapXYVectorComponents_8 = 256,
			ConvertFromSigned_9 = 512,
			ConvertToSigned_10 = 1024,
			ImportMipmapChains_11 = 2048,
			IntentionallyTrueColor_12 = 4096,
		}
		public enum EMPTYSTRINGOptions
		{
			_32X32_0 = 0,
			_64X64_1 = 1,
			_128X128_2 = 2,
			_256X256_3 = 3,
			_512X512_4 = 4,
			_1024X1024_5 = 5,
		}
		public enum SpriteUsageOptions
		{
			Blend_Add_Subtract_Max_0 = 0,
			Multiply_Min_1 = 1,
			DoubleMultiply_2 = 2,
		}
		public enum ForceFormatOptions
		{
			Default_0 = 0,
			ForceG8B8_1 = 1,
			ForceDXT1_2 = 2,
			ForceDXT3_3 = 3,
			ForceDXT5_4 = 4,
			ForceALPHALUMINANCE8_5 = 5,
			ForceA4R4G4B4_6 = 6,
		}
		[FieldSet(64, 4)]
		public unsafe struct BitmapGroupSequenceBlock
		{
			[FieldSet(32, 4)]
			public unsafe struct BitmapGroupSpriteBlock
			{
				[Field("Bitmap Index*", null)]
				public short Bitmap_Index__0;
				[Field("", null)]
				public fixed byte Empty_1[2];
				[Field("", null)]
				public fixed byte Empty_2[4];
				[Field("Left*", null)]
				public float Left__3;
				[Field("Right*", null)]
				public float Right__4;
				[Field("Top*", null)]
				public float Top__5;
				[Field("Bottom*", null)]
				public float Bottom__6;
				[Field("Registration Point*", null)]
				public Vector2 Registration_Point__7;
			}
			[Field("Name^", null)]
			public String Name__0;
			[Field("First Bitmap Index*", null)]
			public short First_Bitmap_Index__1;
			[Field("Bitmap Count*", null)]
			public short Bitmap_Count__2;
			[Field("", null)]
			public fixed byte Empty_3[16];
			[Field("Sprites*", null)]
			[Block("Bitmap Group Sprite Block", 64, typeof(BitmapGroupSpriteBlock))]
			public TagBlock Sprites__4;
		}
		[FieldSet(116, 4)]
		public unsafe struct BitmapDataBlock
		{
			public enum MoreFlagsOptions
			{
				DeleteFromCacheFile_0 = 1,
				BitmapCreateAttempted_1 = 2,
				__2 = 4,
			}
			public enum Type__DeterminesBitmap_geometry_Options
			{
				_2DTexture_0 = 0,
				_3DTexture_1 = 1,
				CubeMap_2 = 2,
			}
			public enum Format__DeterminesHowPixelsAreRepresentedInternallyOptions
			{
				A8_0 = 0,
				Y8_1 = 1,
				Ay8_2 = 2,
				A8y8_3 = 3,
				Unused1_4 = 4,
				Unused2_5 = 5,
				R5g6b5_6 = 6,
				Unused3_7 = 7,
				A1r5g5b5_8 = 8,
				A4r4g4b4_9 = 9,
				X8r8g8b8_10 = 10,
				A8r8g8b8_11 = 11,
				Unused4_12 = 12,
				Unused5_13 = 13,
				Dxt1_14 = 14,
				Dxt3_15 = 15,
				Dxt5_16 = 16,
				P8Bump_17 = 17,
				P8_18 = 18,
				Argbfp32_19 = 19,
				Rgbfp32_20 = 20,
				Rgbfp16_21 = 21,
				V8u8_22 = 22,
				G8b8_23 = 23,
			}
			public enum Flags_Options
			{
				PowerOfTwoDimensions_0 = 1,
				Compressed_1 = 2,
				Palettized_2 = 4,
				Swizzled_3 = 8,
				Linear_4 = 16,
				V16u16_5 = 32,
				MIPMapDebugLevel_6 = 64,
				PreferStutterPreferLowDetail_7 = 128,
			}
			[Field("Signature*", null)]
			public Tag Signature__0;
			[Field("Width*:pixels", null)]
			public short Width__pixels_1;
			[Field("Height*:pixels", null)]
			public short Height__pixels_2;
			[Field("Depth*:pixels#Depth is 1 for 2D textures and cube maps.", null)]
			public int Depth__pixels_Depth_is_1_for_2D_textures_and_cube_maps__3;
			[Field("More Flags", typeof(MoreFlagsOptions))]
			public byte More_Flags_4;
			[Field("Type*#Determines bitmap \"geometry.\"", typeof(Type__DeterminesBitmap_geometry_Options))]
			public short Type__Determines_bitmap__geometry___5;
			[Field("Format*#Determines how pixels are represented internally.", typeof(Format__DeterminesHowPixelsAreRepresentedInternallyOptions))]
			public short Format__Determines_how_pixels_are_represented_internally__6;
			[Field("Flags*", typeof(Flags_Options))]
			public short Flags__7;
			[Field("Registration Point*", null)]
			public Vector2 Registration_Point__8;
			[Field("mipmap Count*", null)]
			public short mipmap_Count__9;
			[Field("Low-Detail mipmap Count*", null)]
			public short Low_Detail_mipmap_Count__10;
			[Field("Pixels Offset*", null)]
			public int Pixels_Offset__11;
			[Field("", null)]
			public fixed byte Empty_12[12];
			[Field("", null)]
			public fixed byte Empty_13[12];
			[Field("", null)]
			public fixed byte Empty_14[12];
			[Field("", null)]
			public fixed byte Empty_15[12];
			[Field("", null)]
			public fixed byte Empty_16[4];
			[Field("", null)]
			public fixed byte Empty_17[4];
			[Field("", null)]
			public fixed byte Empty_18[4];
			[Field("", null)]
			public fixed byte Empty_19[4];
			[Field("", null)]
			public fixed byte Empty_20[20];
			[Field("", null)]
			public fixed byte Empty_21[4];
		}
		public enum ColorSubsampling_Options
		{
			_4_0_0_0 = 0,
			_4_2_0_1 = 1,
			_4_2_2_2 = 2,
			_4_4_4_3 = 3,
		}
		[Field("Type", typeof(TypeOptions))]
		public short Type_2;
		[Field("Format", typeof(FormatOptions))]
		public short Format_4;
		[Field("Usage", typeof(UsageOptions))]
		public short Usage_6;
		[Field("Flags", typeof(FlagsOptions))]
		public short Flags_7;
		[Field("Detail Fade Factor:[0,1]#0 means fade to gray by last mipmap; 1 means fade to gray by first mipmap.", null)]
		public float Detail_Fade_Factor__0_1__0_means_fade_to_gray_by_last_mipmap__1_means_fade_to_gray_by_first_mipmap__9;
		[Field("Sharpen Amount:[0,1]#Sharpens mipmap after downsampling.", null)]
		public float Sharpen_Amount__0_1__Sharpens_mipmap_after_downsampling__10;
		[Field("Bump Height:repeats#tApparent height of the bump map above the triangle onto which it is textured, in texture repeats (i.e., 1.0 would be as high as the texture is wide).", null)]
		public float Bump_Height_repeats_tApparent_height_of_the_bump_map_above_the_triangle_onto_which_it_is_textured__in_texture_repeats__i_e___1_0_would_be_as_high_as_the_texture_is_wide___11;
		[Field("EMPTY STRING", typeof(EMPTYSTRINGOptions))]
		public short EMPTY_STRING_12;
		[Field("EMPTY STRING", null)]
		public short EMPTY_STRING_13;
		[Field("Color Plate Width*:pixels", null)]
		public short Color_Plate_Width__pixels_15;
		[Field("Color Plate Height*:pixels", null)]
		public short Color_Plate_Height__pixels_16;
		[Field("Compressed Color Plate Data*", null)]
		[Data(1073741824)]
		public TagBlock Compressed_Color_Plate_Data__17;
		[Field("Processed Pixel Data*", null)]
		[Data(1073741824)]
		public TagBlock Processed_Pixel_Data__19;
		[Field("Blur Filter Size:[0,10] pixels#Blurs the bitmap before generating mipmaps.", null)]
		public float Blur_Filter_Size__0_10__pixels_Blurs_the_bitmap_before_generating_mipmaps__21;
		[Field("Alpha Bias:[-1,1]#Affects alpha mipmap generation.", null)]
		public float Alpha_Bias___1_1__Affects_alpha_mipmap_generation__22;
		[Field("Mipmap Count:levels#0 Defaults to all levels.", null)]
		public short Mipmap_Count_levels_0_Defaults_to_all_levels__23;
		[Field("Sprite Usage", typeof(SpriteUsageOptions))]
		public short Sprite_Usage_25;
		[Field("Sprite Spacing*", null)]
		public short Sprite_Spacing__26;
		[Field("Force Format", typeof(ForceFormatOptions))]
		public short Force_Format_28;
		[Field("Sequences*", null)]
		[Block("Bitmap Group Sequence Block", 256, typeof(BitmapGroupSequenceBlock))]
		public TagBlock Sequences__29;
		[Field("Bitmaps*", null)]
		[Block("Bitmap Data Block", 65536, typeof(BitmapDataBlock))]
		public TagBlock Bitmaps__30;
		[Field("color compression quality:[1,127]#1 means lossless, 127 means crappy", null)]
		public int color_compression_quality__1_127__1_means_lossless__127_means_crappy_32;
		[Field("alpha compression quality:[1,127]#1 means lossless, 127 means crappy", null)]
		public int alpha_compression_quality__1_127__1_means_lossless__127_means_crappy_33;
		[Field("overlap*", null)]
		public int overlap__34;
		[Field("color subsampling*", typeof(ColorSubsampling_Options))]
		public byte color_subsampling__35;
	}
}
#pragma warning restore CS1591
