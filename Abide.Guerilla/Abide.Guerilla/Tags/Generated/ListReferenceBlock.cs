using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(28, 4)]
	public unsafe struct ListReferenceBlock
	{
		public enum Flags0Options
		{
			ListWraps_0 = 1,
			Interactive_1 = 2,
		}
		public enum SkinIndex1Options
		{
			Default_0 = 0,
			SquadLobbyPlayerList_1 = 1,
			SettingsList_2 = 2,
			PlaylistEntryList_3 = 3,
			Variants_4 = 4,
			GameBrowser_5 = 5,
			OnlinePlayerMenu_6 = 6,
			GameSetupMenu_7 = 7,
			PlaylistContentsDisplay_8 = 8,
			PlayerProfilePicker_9 = 9,
			MpMapSelection_10 = 10,
			MainMenuList_11 = 11,
			ColorPicker_12 = 12,
			ProfilePicker_13 = 13,
			YMenuRecentList_14 = 14,
			PcrTeamStats_15 = 15,
			PcrPlayerStats_16 = 16,
			PcrKillStats_17 = 17,
			PcrPvpStats_18 = 18,
			PcrMedalStats_19 = 19,
			MatchmakingProgress_20 = 20,
			Default5_21 = 21,
			Default6_22 = 22,
			AdvancedSettingsList_23 = 23,
			LiveGameBrowser_24 = 24,
			DefaultWide_25 = 25,
			Unused26_26 = 26,
			Unused27_27 = 27,
			Unused28_28 = 28,
			Unused29_29 = 29,
			Unused30_30 = 30,
			Unused31_31 = 31,
		}
		public enum AnimationIndex4Options
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
		[Field("flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("skin index", typeof(SkinIndex1Options))]
		public short SkinIndex1;
		[Field("num. visible items", null)]
		public short NumVisibleItems2;
		[Field("bottom left", null)]
		public Vector2 BottomLeft3;
		[Field("animation index", typeof(AnimationIndex4Options))]
		public short AnimationIndex4;
		[Field("intro animation delay milliseconds", null)]
		public short IntroAnimationDelayMilliseconds5;
		[Field("UNUSED", null)]
		[Block("S Text Value Pair Reference Block UNUSED", 100, typeof(STextValuePairReferenceBlockUNUSED))]
		public TagBlock UNUSED7;
	}
}
#pragma warning restore CS1591
