using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(112, 4)]
	public unsafe struct WindowPaneReferenceBlock
	{
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
		[Field("", null)]
		public fixed byte _0[2];
		[Field("animation index", typeof(AnimationIndex1Options))]
		public short AnimationIndex1;
		[Field("buttons", null)]
		[Block("Button Widget Reference Block", 64, typeof(ButtonWidgetReferenceBlock))]
		public TagBlock Buttons3;
		[Field("list block", null)]
		[Block("List Reference Block", 1, typeof(ListReferenceBlock))]
		public TagBlock ListBlock5;
		[Field("table view", null)]
		[Block("Table View List Reference Block", 1, typeof(TableViewListReferenceBlock))]
		public TagBlock TableView7;
		[Field("text blocks", null)]
		[Block("Text Block Reference Block", 64, typeof(TextBlockReferenceBlock))]
		public TagBlock TextBlocks9;
		[Field("bitmap blocks", null)]
		[Block("Bitmap Block Reference Block", 64, typeof(BitmapBlockReferenceBlock))]
		public TagBlock BitmapBlocks10;
		[Field("model scene blocks", null)]
		[Block("Ui Model Scene Reference Block", 32, typeof(UiModelSceneReferenceBlock))]
		public TagBlock ModelSceneBlocks11;
		[Field("text-value blocks", null)]
		[Block("S Text Value Pair Blocks Block UNUSED", 100, typeof(STextValuePairBlocksBlockUNUSED))]
		public TagBlock TextValueBlocks13;
		[Field("hud blocks", null)]
		[Block("Hud Block Reference Block", 64, typeof(HudBlockReferenceBlock))]
		public TagBlock HudBlocks14;
		[Field("player blocks", null)]
		[Block("Player Block Reference Block", 64, typeof(PlayerBlockReferenceBlock))]
		public TagBlock PlayerBlocks15;
	}
}
#pragma warning restore CS1591
