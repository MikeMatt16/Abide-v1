using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(116, 4)]
	public unsafe struct BitmapDataBlock
	{
		public enum MoreFlags4Options
		{
			DeleteFromCacheFile_0 = 1,
			BitmapCreateAttempted_1 = 2,
			__2 = 4,
		}
		public enum TypeDeterminesBitmapGeometry5Options
		{
			_2DTexture_0 = 0,
			_3DTexture_1 = 1,
			CubeMap_2 = 2,
		}
		public enum FormatDeterminesHowPixelsAreRepresentedInternally6Options
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
		public enum Flags7Options
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
		[Field("More Flags", typeof(MoreFlags4Options))]
		public byte MoreFlags4;
		[Field("Type*#Determines bitmap \"geometry.\"", typeof(TypeDeterminesBitmapGeometry5Options))]
		public short Type5;
		[Field("Format*#Determines how pixels are represented internally.", typeof(FormatDeterminesHowPixelsAreRepresentedInternally6Options))]
		public short Format6;
		[Field("Flags*", typeof(Flags7Options))]
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
}
#pragma warning restore CS1591
