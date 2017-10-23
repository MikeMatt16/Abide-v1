using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("text_value_pair_definition", "sily", "����", typeof(TextValuePairDefinitionBlock))]
	[FieldSet(48, 4)]
	public unsafe struct TextValuePairDefinitionBlock
	{
		public enum Parameter0Options
		{
			MatchRoundSetting_0 = 0,
			MatchCtfScoreToWin_1 = 1,
			MatchSlayerScoreToWinRound_2 = 2,
			MatchOddballScoreToWinRound_3 = 3,
			MatchKingScoreToWinRound_4 = 4,
			MatchRaceScoreToWinRound_5 = 5,
			MatchHeadhunterScoreToWinRound_6 = 6,
			MatchJuggernautScoreToWinRound_7 = 7,
			MatchTerritoriesScoreToWinRound_8 = 8,
			MatchAssaultScoreToWinRound_9 = 9,
			MatchRoundTimeLimit_10 = 10,
			MatchRoundsResetMap_11 = 11,
			MatchTieResolution_12 = 12,
			MatchObservers_13 = 13,
			MatchJoinInProgress_14 = 14,
			MaximumPlayers_15 = 15,
			LivesPerRound_16 = 16,
			RespawnTime_17 = 17,
			SuicidePenalty_18 = 18,
			Shields_19 = 19,
			MotionSensor_20 = 20,
			Invisibility_21 = 21,
			TeamChanging_22 = 22,
			TeamScoring_23 = 23,
			FriendlyFire_24 = 24,
			TeamRespawnSetting_25 = 25,
			BetrayalRespawnPenalty_26 = 26,
			TeamKillerManagement_27 = 27,
			SlayerBonusPoints_28 = 28,
			SlayerSuicidePointLoss_29 = 29,
			SlayerDeathPointLoss_30 = 30,
			HeadhunterMovingHeadBin_31 = 31,
			HeadhunterPointMultiplier_32 = 32,
			HeadhunterSuicidePointLoss_33 = 33,
			HeadhunterDeathPointLoss_34 = 34,
			HeadhunterUncontestedBin_35 = 35,
			HeadhunterSpeedWithHeads_36 = 36,
			HeadhunterMaxHeadsCarried_37 = 37,
			KingUncontestedHill_38 = 38,
			KingTeamTimeMultiplier_39 = 39,
			KingMovingHill_40 = 40,
			KingExtraDamageOnHill_41 = 41,
			KingDmgResistanceOnHill_42 = 42,
			OddballBallSpawnCount_43 = 43,
			OddballBallHitDamage_44 = 44,
			OddballSpeedWithBall_45 = 45,
			OddballDrivingGunningWithBall_46 = 46,
			OddballWaypointToBall_47 = 47,
			RaceRandomTrack_48 = 48,
			RaceUncontestedFlag_49 = 49,
			CtfGameType_50 = 50,
			CtfSuddenDeath_51 = 51,
			CtfFlagMayBeReturned_52 = 52,
			CtfFlagAtHomeToScore_53 = 53,
			CtfFlagResetTime_54 = 54,
			CtfSpeedWithFlag_55 = 55,
			CtfFlagHitDamage_56 = 56,
			CtfDrivingGunningWithFlag_57 = 57,
			CtfWaypointToOwnFlag_58 = 58,
			AssaultGameType_59 = 59,
			AssaultSuddenDeath_60 = 60,
			AssaultDetonationTime_61 = 61,
			AssaultBombAtHomeToScore_62 = 62,
			AssaultArmingTime_63 = 63,
			AssaultSpeedWithBomb_64 = 64,
			AssaultBombHitDamage_65 = 65,
			AssaultDrivingGunningWithBomb_66 = 66,
			AssaultWaypointToOwnBomb_67 = 67,
			JuggernautBetrayalPointLoss_68 = 68,
			JuggernautJuggyExtraDamage_69 = 69,
			JuggernautJuggyInfiniteAmmo_70 = 70,
			JuggernautJuggyOvershields_71 = 71,
			JuggernautJuggyActiveCamo_72 = 72,
			JuggernautJuggyMotionSensor_73 = 73,
			TerritoriesTerritoryCount_74 = 74,
			VehRespawn_75 = 75,
			VehPrimaryLightLand_76 = 76,
			VehSecondaryLightLand_77 = 77,
			VehPrimaryHeavyLand_78 = 78,
			VehPrimaryFlying_79 = 79,
			VehSecondaryHeavyLand_80 = 80,
			VehPrimaryTurret_81 = 81,
			VehSecondaryTurret_82 = 82,
			EquipWeaponsOnMap_83 = 83,
			EquipOvershieldsOnMap_84 = 84,
			EquipActiveCamoOnMap_85 = 85,
			EquipGrenadesOnMap_86 = 86,
			EquipWeaponRespawnTimes_87 = 87,
			EquipStartingGrenades_88 = 88,
			EquipPrimaryStartingEquipment_89 = 89,
			UNSMaxLivingPlayers_90 = 90,
			UNSTeamsEnabled_91 = 91,
			UNSAssaultBombMayBeReturned_92 = 92,
			UNSMaxTeams_93 = 93,
			UNSEquipSecondaryStartingEquipment_94 = 94,
			UNSAssaultFuseTime_95 = 95,
			UNSJuggyMovement_96 = 96,
			UNSStickyFuse_97 = 97,
			UNSTerrContestTime_98 = 98,
			UNSTerrControlTime_99 = 99,
			UNSOddbCarrInvis_100 = 100,
			UNSKingInvisInHill_101 = 101,
			UNSBallCarrDmgResis_102 = 102,
			UNSKingDmgResInHill_103 = 103,
			UNSPlayersExDmg_104 = 104,
			UNSPlayersDmgResis_105 = 105,
			UNSCtfCarrDmgResis_106 = 106,
			UNSCtfCarrInvis_107 = 107,
			UNSJuggyDmgResis_108 = 108,
			UNSBombCarrDmgResis_109 = 109,
			UNSBombCarrInvis_110 = 110,
			UNSForceEvenTeams_111 = 111,
		}
		[Field("parameter", typeof(Parameter0Options))]
		public int Parameter0;
		[Field("", null)]
		public fixed byte _1[4];
		[Field("string list", null)]
		public TagReference StringList2;
		[Field("title text", null)]
		public StringId TitleText3;
		[Field("header text", null)]
		public StringId HeaderText4;
		[Field("description text", null)]
		public StringId DescriptionText5;
		[Field("text value pairs", null)]
		[Block("Text Value Pair Reference Block", 32, typeof(TextValuePairReferenceBlock))]
		public TagBlock TextValuePairs6;
	}
}
#pragma warning restore CS1591
