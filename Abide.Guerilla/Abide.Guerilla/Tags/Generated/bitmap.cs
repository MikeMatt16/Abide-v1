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
			HeightMapG8B8WAlpha_10 = 10,
		}
		public enum FlagsOptions
		{
			EnableDiffusionDithering_0 = 1,
			DisableHeightMapCompression_1 = 2,
			UniformSpriteSequences_2 = 4,
			FilthySpriteBugFix_3 = 8,
			UseSharpBumpFilter_4 = 16,
			UNUSED_5 = 32,
			UseClampedMirroredBumpFilter_6 = 64,
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
			BlendAddSubtractMax_0 = 0,
			MultiplyMin_1 = 1,
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
				public short BitmapIndex0;
				[Field("", null)]
				public fixed byte _1[2];
				[Field("", null)]
				public fixed byte _2[4];
				[Field("Left*", null)]
				public float Left3;
				[Field("Right*", null)]
				public float Right4;
				[Field("Top*", null)]
				public float Top5;
				[Field("Bottom*", null)]
				public float Bottom6;
				[Field("Registration Point*", null)]
				public Vector2 RegistrationPoint7;
			}
			[Field("Name^", null)]
			public String Name0;
			[Field("First Bitmap Index*", null)]
			public short FirstBitmapIndex1;
			[Field("Bitmap Count*", null)]
			public short BitmapCount2;
			[Field("", null)]
			public fixed byte _3[16];
			[Field("Sprites*", null)]
			[Block("Bitmap Group Sprite Block", 64, typeof(BitmapGroupSpriteBlock))]
			public TagBlock Sprites4;
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
			public enum TypeDeterminesBitmapGeometryOptions
			{
				_2DTexture_0 = 0,
				_3DTexture_1 = 1,
				CubeMap_2 = 2,
			}
			public enum FormatDeterminesHowPixelsAreRepresentedInternallyOptions
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
			public enum FlagsOptions
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
			public Tag Signature0;
			[Field("Width*:pixels", null)]
			public short Width1;
			[Field("Height*:pixels", null)]
			public short Height2;
			[Field("Depth*:pixels#Depth is 1 for 2D textures and cube maps.", null)]
			public int Depth3;
			[Field("More Flags", typeof(MoreFlagsOptions))]
			public byte MoreFlags4;
			[Field("Type*#Determines bitmap \"geometry.\"", typeof(TypeDeterminesBitmapGeometryOptions))]
			public short Type5;
			[Field("Format*#Determines how pixels are represented internally.", typeof(FormatDeterminesHowPixelsAreRepresentedInternallyOptions))]
			public short Format6;
			[Field("Flags*", typeof(FlagsOptions))]
			public short Flags7;
			[Field("Registration Point*", null)]
			public Vector2 RegistrationPoint8;
			[Field("mipmap Count*", null)]
			public short MipmapCount9;
			[Field("Low-Detail mipmap Count*", null)]
			public short LowDetailMipmapCount10;
			[Field("Pixels Offset*", null)]
			public int PixelsOffset11;
			[Field("", null)]
			public fixed byte _12[12];
			[Field("", null)]
			public fixed byte _13[12];
			[Field("", null)]
			public fixed byte _14[12];
			[Field("", null)]
			public fixed byte _15[12];
			[Field("", null)]
			public fixed byte _16[4];
			[Field("", null)]
			public fixed byte _17[4];
			[Field("", null)]
			public fixed byte _18[4];
			[Field("", null)]
			public fixed byte _19[4];
			[Field("", null)]
			public fixed byte _20[20];
			[Field("", null)]
			public fixed byte _21[4];
		}
		public enum ColorSubsamplingOptions
		{
			_400_0 = 0,
			_420_1 = 1,
			_422_2 = 2,
			_444_3 = 3,
		}
		[Field("Type", typeof(TypeOptions))]
		public short Type2;
		[Field("Format", typeof(FormatOptions))]
		public short Format4;
		[Field("Usage", typeof(UsageOptions))]
		public short Usage6;
		[Field("Flags", typeof(FlagsOptions))]
		public short Flags7;
		[Field("Detail Fade Factor:[0,1]#0 means fade to gray by last mipmap; 1 means fade to gray by first mipmap.", null)]
		public float DetailFadeFactor9;
		[Field("Sharpen Amount:[0,1]#Sharpens mipmap after downsampling.", null)]
		public float SharpenAmount10;
		[Field("Bump Height:repeats#tApparent height of the bump map above the triangle onto which it is textured, in texture repeats (i.e., 1.0 would be as high as the texture is wide).", null)]
		public float BumpHeight11;
		[Field("EMPTY STRING", typeof(EMPTYSTRINGOptions))]
		public short EMPTYSTRING12;
		[Field("EMPTY STRING", null)]
		public short EMPTYSTRING13;
		[Field("Color Plate Width*:pixels", null)]
		public short ColorPlateWidth15;
		[Field("Color Plate Height*:pixels", null)]
		public short ColorPlateHeight16;
		[Field("Compressed Color Plate Data*", null)]
		[Data(1073741824)]
		public TagBlock CompressedColorPlateData17;
		[Field("Processed Pixel Data*", null)]
		[Data(1073741824)]
		public TagBlock ProcessedPixelData19;
		[Field("Blur Filter Size:[0,10] pixels#Blurs the bitmap before generating mipmaps.", null)]
		public float BlurFilterSize21;
		[Field("Alpha Bias:[-1,1]#Affects alpha mipmap generation.", null)]
		public float AlphaBias22;
		[Field("Mipmap Count:levels#0 Defaults to all levels.", null)]
		public short MipmapCount23;
		[Field("Sprite Usage", typeof(SpriteUsageOptions))]
		public short SpriteUsage25;
		[Field("Sprite Spacing*", null)]
		public short SpriteSpacing26;
		[Field("Force Format", typeof(ForceFormatOptions))]
		public short ForceFormat28;
		[Field("Sequences*", null)]
		[Block("Bitmap Group Sequence Block", 256, typeof(BitmapGroupSequenceBlock))]
		public TagBlock Sequences29;
		[Field("Bitmaps*", null)]
		[Block("Bitmap Data Block", 65536, typeof(BitmapDataBlock))]
		public TagBlock Bitmaps30;
		[Field("color compression quality:[1,127]#1 means lossless, 127 means crappy", null)]
		public int ColorCompressionQuality32;
		[Field("alpha compression quality:[1,127]#1 means lossless, 127 means crappy", null)]
		public int AlphaCompressionQuality33;
		[Field("overlap*", null)]
		public int Overlap34;
		[Field("color subsampling*", typeof(ColorSubsamplingOptions))]
		public byte ColorSubsampling35;
	}
}
#pragma warning restore CS1591
