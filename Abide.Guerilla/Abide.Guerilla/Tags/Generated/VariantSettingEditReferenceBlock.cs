using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(32, 4)]
	public unsafe struct VariantSettingEditReferenceBlock
	{
		public enum SettingCategory0Options
		{
			MatchCtf_0 = 0,
			MatchSlayer_1 = 1,
			MatchOddball_2 = 2,
			MatchKing_3 = 3,
			MatchRace_4 = 4,
			MatchHeadhunter_5 = 5,
			MatchJuggernaut_6 = 6,
			MatchTerritories_7 = 7,
			MatchAssault_8 = 8,
			Players_9 = 9,
			OBSOLETE_10 = 10,
			Vehicles_11 = 11,
			Equipment_12 = 12,
			GameCtf_13 = 13,
			GameSlayer_14 = 14,
			GameOddball_15 = 15,
			GameKing_16 = 16,
			GameRace_17 = 17,
			GameHeadhunter_18 = 18,
			GameJuggernaut_19 = 19,
			GameTerritories_20 = 20,
			GameAssault_21 = 21,
			QuickOptionsCtf_22 = 22,
			QuickOptionsSlayer_23 = 23,
			QuickOptionsOddball_24 = 24,
			QuickOptionsKing_25 = 25,
			QuickOptionsRace_26 = 26,
			QuickOptionsHeadhunter_27 = 27,
			QuickOptionsJuggernaut_28 = 28,
			QuickOptionsTerritories_29 = 29,
			QuickOptionsAssault_30 = 30,
			TeamCtf_31 = 31,
			TeamSlayer_32 = 32,
			TeamOddball_33 = 33,
			TeamKing_34 = 34,
			TeamRace_35 = 35,
			TeamHeadhunter_36 = 36,
			TeamJuggernaut_37 = 37,
			TeamTerritories_38 = 38,
			TeamAssault_39 = 39,
		}
		[Field("setting category^", typeof(SettingCategory0Options))]
		public int SettingCategory0;
		[Field("", null)]
		public fixed byte _1[4];
		[Field("options", null)]
		[Block("Text Value Pair Block", 32, typeof(TextValuePairBlock))]
		public TagBlock Options2;
		[Field("", null)]
		[Block("Null Block", 0, typeof(NullBlock))]
		public TagBlock _3;
	}
}
#pragma warning restore CS1591
