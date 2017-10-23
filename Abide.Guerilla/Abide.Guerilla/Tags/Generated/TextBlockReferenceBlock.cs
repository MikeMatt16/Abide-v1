using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(44, 4)]
	public unsafe struct TextBlockReferenceBlock
	{
		public enum TextFlags0Options
		{
			LeftJustifyText_0 = 1,
			RightJustifyText_1 = 2,
			PulsatingText_2 = 4,
			CalloutText_3 = 8,
			Small31CharBuffer_4 = 16,
		}
		public enum AnimationIndex1Options
		{
			NONE_0 = 0,
			_00_1 = 1,
			_01_2 = 2,
			_02_3 = 3,
			_03_4 = 4,
			_04_5 = 5,
			_05_6 = 6,
			_06_7 = 7,
			_07_8 = 8,
			_08_9 = 9,
			_09_10 = 10,
			_10_11 = 11,
			_11_12 = 12,
			_12_13 = 13,
			_13_14 = 14,
			_14_15 = 15,
			_15_16 = 16,
			_16_17 = 17,
			_17_18 = 18,
			_18_19 = 19,
			_19_20 = 20,
			_20_21 = 21,
			_21_22 = 22,
			_22_23 = 23,
			_23_24 = 24,
			_24_25 = 25,
			_25_26 = 26,
			_26_27 = 27,
			_27_28 = 28,
			_28_29 = 29,
			_29_30 = 30,
			_30_31 = 31,
			_31_32 = 32,
			_32_33 = 33,
			_33_34 = 34,
			_34_35 = 35,
			_35_36 = 36,
			_36_37 = 37,
			_37_38 = 38,
			_38_39 = 39,
			_39_40 = 40,
			_40_41 = 41,
			_41_42 = 42,
			_42_43 = 43,
			_43_44 = 44,
			_44_45 = 45,
			_45_46 = 46,
			_46_47 = 47,
			_47_48 = 48,
			_48_49 = 49,
			_49_50 = 50,
			_50_51 = 51,
			_51_52 = 52,
			_52_53 = 53,
			_53_54 = 54,
			_54_55 = 55,
			_55_56 = 56,
			_56_57 = 57,
			_57_58 = 58,
			_58_59 = 59,
			_59_60 = 60,
			_60_61 = 61,
			_61_62 = 62,
			_62_63 = 63,
			_63_64 = 64,
		}
		public enum CustomFont4Options
		{
			Terminal_0 = 0,
			BodyText_1 = 1,
			Title_2 = 2,
			SuperLargeFont_3 = 3,
			LargeBodyText_4 = 4,
			SplitScreenHudMessage_5 = 5,
			FullScreenHudMessage_6 = 6,
			EnglishBodyText_7 = 7,
			HUDNumberText_8 = 8,
			SubtitleFont_9 = 9,
			MainMenuFont_10 = 10,
			TextChatFont_11 = 11,
		}
		[Field("text flags", typeof(TextFlags0Options))]
		public int TextFlags0;
		[Field("animation index", typeof(AnimationIndex1Options))]
		public short AnimationIndex1;
		[Field("intro animation delay milliseconds", null)]
		public short IntroAnimationDelayMilliseconds2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("custom font", typeof(CustomFont4Options))]
		public short CustomFont4;
		[Field("text color", null)]
		public ColorArgbF TextColor5;
		[Field("text bounds", null)]
		public Rectangle2 TextBounds6;
		[Field("string id", null)]
		public StringId StringId7;
		[Field("render depth bias", null)]
		public short RenderDepthBias8;
		[Field("", null)]
		public fixed byte _9[2];
	}
}
#pragma warning restore CS1591
