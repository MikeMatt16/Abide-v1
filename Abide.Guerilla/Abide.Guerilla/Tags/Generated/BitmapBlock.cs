using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("bitmap", "bitm", "����", typeof(BitmapBlock))]
	[FieldSet(112, 4)]
	public unsafe struct BitmapBlock
	{
		public enum Type2Options
		{
			_2DTextures_0 = 0,
			_3DTextures_1 = 1,
			CubeMaps_2 = 2,
			Sprites_3 = 3,
			InterfaceBitmaps_4 = 4,
		}
		public enum Format4Options
		{
			CompressedWithColorKeyTransparency_0 = 0,
			CompressedWithExplicitAlpha_1 = 1,
			CompressedWithInterpolatedAlpha_2 = 2,
			_16BitColor_3 = 3,
			_32BitColor_4 = 4,
			Monochrome_5 = 5,
		}
		public enum Usage6Options
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
		public enum Flags7Options
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
		public enum EMPTYSTRING12Options
		{
			_32X32_0 = 0,
			_64X64_1 = 1,
			_128X128_2 = 2,
			_256X256_3 = 3,
			_512X512_4 = 4,
			_1024X1024_5 = 5,
		}
		public enum SpriteUsage25Options
		{
			BlendAddSubtractMax_0 = 0,
			MultiplyMin_1 = 1,
			DoubleMultiply_2 = 2,
		}
		public enum ForceFormat28Options
		{
			Default_0 = 0,
			ForceG8B8_1 = 1,
			ForceDXT1_2 = 2,
			ForceDXT3_3 = 3,
			ForceDXT5_4 = 4,
			ForceALPHALUMINANCE8_5 = 5,
			ForceA4R4G4B4_6 = 6,
		}
		public enum ColorSubsampling35Options
		{
			_400_0 = 0,
			_420_1 = 1,
			_422_2 = 2,
			_444_3 = 3,
		}
		[Field("Type", typeof(Type2Options))]
		public short Type2;
		[Field("Format", typeof(Format4Options))]
		public short Format4;
		[Field("Usage", typeof(Usage6Options))]
		public short Usage6;
		[Field("Flags", typeof(Flags7Options))]
		public short Flags7;
		[Field("Detail Fade Factor:[0,1]#0 means fade to gray by last mipmap; 1 means fade to gray by first mipmap.", null)]
		public float DetailFadeFactor9;
		[Field("Sharpen Amount:[0,1]#Sharpens mipmap after downsampling.", null)]
		public float SharpenAmount10;
		[Field("Bump Height:repeats#tApparent height of the bump map above the triangle onto which it is textured, in texture repeats (i.e., 1.0 would be as high as the texture is wide).", null)]
		public float BumpHeight11;
		[Field("EMPTY STRING", typeof(EMPTYSTRING12Options))]
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
		[Field("Sprite Usage", typeof(SpriteUsage25Options))]
		public short SpriteUsage25;
		[Field("Sprite Spacing*", null)]
		public short SpriteSpacing26;
		[Field("Force Format", typeof(ForceFormat28Options))]
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
		[Field("color subsampling*", typeof(ColorSubsampling35Options))]
		public byte ColorSubsampling35;
	}
}
#pragma warning restore CS1591
