using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(276, 4)]
	public unsafe struct LiquidArcBlock
	{
		public enum Flags1Options
		{
			BasisMarkerRelative_0 = 1,
			SpreadByExternalInput_1 = 2,
			CollideWithStuff_2 = 4,
			NoPerspectiveMidpoints_3 = 8,
		}
		public enum SpriteCount2Options
		{
			_4Sprites_0 = 0,
			_8Sprites_1 = 1,
			_16Sprites_2 = 2,
			_32Sprites_3 = 3,
			_64Sprites_4 = 4,
			_128Sprites_5 = 5,
			_256Sprites_6 = 6,
		}
		public enum OctaveFlags31Options
		{
			Octave1_0 = 1,
			Octave2_1 = 2,
			Octave3_2 = 4,
			Octave4_3 = 8,
			Octave5_4 = 16,
			Octave6_5 = 32,
			Octave7_6 = 64,
			Octave8_7 = 128,
			Octave9_8 = 256,
		}
		[Field("flags", typeof(Flags1Options))]
		public short Flags1;
		[Field("sprite count", typeof(SpriteCount2Options))]
		public short SpriteCount2;
		[Field("natural length:world units", null)]
		public float NaturalLength3;
		[Field("instances", null)]
		public short Instances4;
		[Field("", null)]
		public fixed byte _5[2];
		[Field("instance spread angle:degrees", null)]
		public float InstanceSpreadAngle6;
		[Field("instance rotation period:seconds", null)]
		public float InstanceRotationPeriod7;
		[Field("", null)]
		public fixed byte _8[8];
		[Field("material effects", null)]
		public TagReference MaterialEffects9;
		[Field("bitmap", null)]
		public TagReference Bitmap10;
		[Field("", null)]
		public fixed byte _11[8];
		[Field("horizontal range", typeof(ScalarFunctionStructBlock))]
		[Block("Scalar Function Struct", 1, typeof(ScalarFunctionStructBlock))]
		public ScalarFunctionStructBlock HorizontalRange13;
		[Field("vertical range", typeof(ScalarFunctionStructBlock))]
		[Block("Scalar Function Struct", 1, typeof(ScalarFunctionStructBlock))]
		public ScalarFunctionStructBlock VerticalRange15;
		[Field("vertical negative scale:[0,1]", null)]
		public float VerticalNegativeScale16;
		[Field("roughness", typeof(ScalarFunctionStructBlock))]
		[Block("Scalar Function Struct", 1, typeof(ScalarFunctionStructBlock))]
		public ScalarFunctionStructBlock Roughness18;
		[Field("", null)]
		public fixed byte _19[64];
		[Field("octave 1 frequency:cycles/second", null)]
		public float Octave1Frequency21;
		[Field("octave 2 frequency:cycles/second", null)]
		public float Octave2Frequency22;
		[Field("octave 3 frequency:cycles/second", null)]
		public float Octave3Frequency23;
		[Field("octave 4 frequency:cycles/second", null)]
		public float Octave4Frequency24;
		[Field("octave 5 frequency:cycles/second", null)]
		public float Octave5Frequency25;
		[Field("octave 6 frequency:cycles/second", null)]
		public float Octave6Frequency26;
		[Field("octave 7 frequency:cycles/second", null)]
		public float Octave7Frequency27;
		[Field("octave 8 frequency:cycles/second", null)]
		public float Octave8Frequency28;
		[Field("octave 9 frequency:cycles/second", null)]
		public float Octave9Frequency29;
		[Field("", null)]
		public fixed byte _30[28];
		[Field("octave flags", typeof(OctaveFlags31Options))]
		public short OctaveFlags31;
		[Field("", null)]
		public fixed byte _32[2];
		[Field("cores", null)]
		[Block("Core", 4, typeof(LiquidCoreBlock))]
		public TagBlock Cores33;
		[Field("range-scale", typeof(ScalarFunctionStructBlock))]
		[Block("Scalar Function Struct", 1, typeof(ScalarFunctionStructBlock))]
		public ScalarFunctionStructBlock RangeScale35;
		[Field("brightness-scale", typeof(ScalarFunctionStructBlock))]
		[Block("Scalar Function Struct", 1, typeof(ScalarFunctionStructBlock))]
		public ScalarFunctionStructBlock BrightnessScale37;
	}
}
#pragma warning restore CS1591
