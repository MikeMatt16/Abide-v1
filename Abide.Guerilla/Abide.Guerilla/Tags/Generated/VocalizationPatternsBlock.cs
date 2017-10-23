using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(64, 4)]
	public unsafe struct VocalizationPatternsBlock
	{
		public enum DialogueType0Options
		{
			Death_0 = 0,
			Unused_1 = 1,
			Unused_2 = 2,
			Damage_3 = 3,
			DamageUnused1_4 = 4,
			DamageUnused2_5 = 5,
			SightedNew_6 = 6,
			SightedNewMajor_7 = 7,
			Unused_8 = 8,
			SightedOld_9 = 9,
			SightedFirst_10 = 10,
			SightedSpecial_11 = 11,
			Unused_12 = 12,
			HeardNew_13 = 13,
			Unused_14 = 14,
			HeardOld_15 = 15,
			Unused_16 = 16,
			Unused_17 = 17,
			Unused_18 = 18,
			AcknowledgeMultiple_19 = 19,
			Unused_20 = 20,
			Unused_21 = 21,
			Unused_22 = 22,
			FoundUnit_23 = 23,
			FoundUnitPresearch_24 = 24,
			FoundUnitPursuit_25 = 25,
			FoundUnitSelfPreserving_26 = 26,
			FoundUnitRetreating_27 = 27,
			ThrowingGrenade_28 = 28,
			NoticedGrenade_29 = 29,
			Fighting_30 = 30,
			Charging_31 = 31,
			SuppressingFire_32 = 32,
			GrenadeUncover_33 = 33,
			Unused_34 = 34,
			Unused_35 = 35,
			Dive_36 = 36,
			Evade_37 = 37,
			Avoid_38 = 38,
			Surprised_39 = 39,
			Unused_40 = 40,
			Unused_41 = 41,
			Presearch_42 = 42,
			PresearchStart_43 = 43,
			Search_44 = 44,
			SearchStart_45 = 45,
			InvestigateFailed_46 = 46,
			UncoverFailed_47 = 47,
			PursuitFailed_48 = 48,
			InvestigateStart_49 = 49,
			AbandonedSearchSpace_50 = 50,
			AbandonedSearchTime_51 = 51,
			PresearchFailed_52 = 52,
			AbandonedSearchRestricted_53 = 53,
			InvestigatePursuitStart_54 = 54,
			PostcombatInspectBody_55 = 55,
			VehicleSlowDown_56 = 56,
			VehicleGetIn_57 = 57,
			Idle_58 = 58,
			Taunt_59 = 59,
			TauntReply_60 = 60,
			Retreat_61 = 61,
			RetreatFromScaryTarget_62 = 62,
			RetreatFromDeadLeader_63 = 63,
			RetreatFromProximity_64 = 64,
			RetreatFromLowShield_65 = 65,
			Flee_66 = 66,
			Cowering_67 = 67,
			Unused_68 = 68,
			Unused_69 = 69,
			Unused_70 = 70,
			Cover_71 = 71,
			Covered_72 = 72,
			Unused_73 = 73,
			Unused_74 = 74,
			Unused_75 = 75,
			PursuitStart_76 = 76,
			PursuitSyncStart_77 = 77,
			PursuitSyncJoin_78 = 78,
			PursuitSyncQuorum_79 = 79,
			Melee_80 = 80,
			Unused_81 = 81,
			Unused_82 = 82,
			Unused_83 = 83,
			VehicleFalling_84 = 84,
			VehicleWoohoo_85 = 85,
			VehicleScared_86 = 86,
			VehicleCrazy_87 = 87,
			Unused_88 = 88,
			Unused_89 = 89,
			Leap_90 = 90,
			Unused_91 = 91,
			Unused_92 = 92,
			PostcombatWin_93 = 93,
			PostcombatLose_94 = 94,
			PostcombatNeutral_95 = 95,
			ShootCorpse_96 = 96,
			PostcombatStart_97 = 97,
			InspectBodyStart_98 = 98,
			PostcombatStatus_99 = 99,
			Unused_100 = 100,
			VehicleEntryStartDriver_101 = 101,
			VehicleEnter_102 = 102,
			VehicleEntryStartGun_103 = 103,
			VehicleEntryStartPassenger_104 = 104,
			VehicleExit_105 = 105,
			EvictDriver_106 = 106,
			EvictGunner_107 = 107,
			EvictPassenger_108 = 108,
			Unused_109 = 109,
			Unused_110 = 110,
			NewOrderAdvance_111 = 111,
			NewOrderCharge_112 = 112,
			NewOrderFallback_113 = 113,
			NewOrderRetreat_114 = 114,
			NewOrderMoveon_115 = 115,
			NewOrderArrival_116 = 116,
			NewOrderEntervcl_117 = 117,
			NewOrderExitvcl_118 = 118,
			NewOrderFllplr_119 = 119,
			NewOrderLeaveplr_120 = 120,
			NewOrderSupport_121 = 121,
			Unused_122 = 122,
			Unused_123 = 123,
			Unused_124 = 124,
			Unused_125 = 125,
			Unused_126 = 126,
			Unused_127 = 127,
			Unused_128 = 128,
			Unused_129 = 129,
			Unused_130 = 130,
			Unused_131 = 131,
			Unused_132 = 132,
			Unused_133 = 133,
			Emerge_134 = 134,
			Unused_135 = 135,
			Unused_136 = 136,
			Unused_137 = 137,
			Curse_138 = 138,
			Unused_139 = 139,
			Unused_140 = 140,
			Unused_141 = 141,
			Threaten_142 = 142,
			Unused_143 = 143,
			Unused_144 = 144,
			Unused_145 = 145,
			CoverFriend_146 = 146,
			Unused_147 = 147,
			Unused_148 = 148,
			Unused_149 = 149,
			Strike_150 = 150,
			Unused_151 = 151,
			Unused_152 = 152,
			Unused_153 = 153,
			Unused_154 = 154,
			Unused_155 = 155,
			Unused_156 = 156,
			Unused_157 = 157,
			Unused_158 = 158,
			Gloat_159 = 159,
			Unused_160 = 160,
			Unused_161 = 161,
			Unused_162 = 162,
			Greet_163 = 163,
			Unused_164 = 164,
			Unused_165 = 165,
			Unused_166 = 166,
			Unused_167 = 167,
			PlayerLook_168 = 168,
			PlayerLookLongtime_169 = 169,
			Unused_170 = 170,
			Unused_171 = 171,
			Unused_172 = 172,
			Unused_173 = 173,
			PanicGrenadeAttached_174 = 174,
			Unused_175 = 175,
			Unused_176 = 176,
			Unused_177 = 177,
			Unused_178 = 178,
			HelpResponse_179 = 179,
			Unused_180 = 180,
			Unused_181 = 181,
			Unused_182 = 182,
			Remind_183 = 183,
			Unused_184 = 184,
			Unused_185 = 185,
			Unused_186 = 186,
			Unused_187 = 187,
			WeaponTradeBetter_188 = 188,
			WeaponTradeWorse_189 = 189,
			WeaponReadeEqual_190 = 190,
			Unused_191 = 191,
			Unused_192 = 192,
			Unused_193 = 193,
			Betray_194 = 194,
			Unused_195 = 195,
			Forgive_196 = 196,
			Unused_197 = 197,
			Reanimate_198 = 198,
			Unused_199 = 199,
		}
		public enum SpeakerType3Options
		{
			Subject_0 = 0,
			Cause_1 = 1,
			Friend_2 = 2,
			Target_3 = 3,
			Enemy_4 = 4,
			Vehicle_5 = 5,
			Joint_6 = 6,
			Squad_7 = 7,
			Leader_8 = 8,
			JointLeader_9 = 9,
			Clump_10 = 10,
			Peer_11 = 11,
		}
		public enum Flags4Options
		{
			SubjectVisible_0 = 1,
			CauseVisible_1 = 2,
			FriendsPresent_2 = 4,
			SubjectIsSpeakerSTarget_3 = 8,
			CauseIsSpeakerSTarget_4 = 16,
			CauseIsPlayerOrSpeakerIsPlayerAlly_5 = 32,
			SpeakerIsSearching_6 = 64,
			SpeakerIsFollowingPlayer_7 = 128,
			CauseIsPrimaryPlayerAlly_8 = 256,
		}
		public enum ListenerTargetWhoWhatAmISpeakingToOf5Options
		{
			Subject_0 = 0,
			Cause_1 = 1,
			Friend_2 = 2,
			Target_3 = 3,
			Enemy_4 = 4,
			Vehicle_5 = 5,
			Joint_6 = 6,
			Squad_7 = 7,
			Leader_8 = 8,
			JointLeader_9 = 9,
			Clump_10 = 10,
			Peer_11 = 11,
		}
		public enum HostilityTheRelationshipBetweenTheSubjectAndTheCause8Options
		{
			NONE_0 = 0,
			Self_1 = 1,
			Neutral_2 = 2,
			Friend_3 = 3,
			Enemy_4 = 4,
			Traitor_5 = 5,
		}
		public enum DamageType9Options
		{
			NONE_0 = 0,
			Falling_1 = 1,
			Bullet_2 = 2,
			Grenade_3 = 3,
			Explosive_4 = 4,
			Sniper_5 = 5,
			Melee_6 = 6,
			Flame_7 = 7,
			MountedWeapon_8 = 8,
			Vehicle_9 = 9,
			Plasma_10 = 10,
			Needle_11 = 11,
			Shotgun_12 = 12,
		}
		public enum DangerLevelSpeakerMustHaveDangerLevelOfAtLeastThisMuch10Options
		{
			NONE_0 = 0,
			BroadlyFacing_1 = 1,
			ShootingNear_2 = 2,
			ShootingAt_3 = 3,
			ExtremelyClose_4 = 4,
			ShieldDamage_5 = 5,
			ShieldExtendedDamage_6 = 6,
			BodyDamage_7 = 7,
			BodyExtendedDamage_8 = 8,
		}
		public enum Attitude11Options
		{
			Normal_0 = 0,
			Timid_1 = 1,
			Aggressive_2 = 2,
		}
		public enum SubjectActorType13Options
		{
			NONE_0 = 0,
			Elite_1 = 1,
			Jackal_2 = 2,
			Grunt_3 = 3,
			Hunter_4 = 4,
			Engineer_5 = 5,
			Assassin_6 = 6,
			Player_7 = 7,
			Marine_8 = 8,
			Crew_9 = 9,
			CombatForm_10 = 10,
			InfectionForm_11 = 11,
			CarrierForm_12 = 12,
			Monitor_13 = 13,
			Sentinel_14 = 14,
			None_15 = 15,
			MountedWeapon_16 = 16,
			Brute_17 = 17,
			Prophet_18 = 18,
			Bugger_19 = 19,
			Juggernaut_20 = 20,
		}
		public enum CauseActorType14Options
		{
			NONE_0 = 0,
			Elite_1 = 1,
			Jackal_2 = 2,
			Grunt_3 = 3,
			Hunter_4 = 4,
			Engineer_5 = 5,
			Assassin_6 = 6,
			Player_7 = 7,
			Marine_8 = 8,
			Crew_9 = 9,
			CombatForm_10 = 10,
			InfectionForm_11 = 11,
			CarrierForm_12 = 12,
			Monitor_13 = 13,
			Sentinel_14 = 14,
			None_15 = 15,
			MountedWeapon_16 = 16,
			Brute_17 = 17,
			Prophet_18 = 18,
			Bugger_19 = 19,
			Juggernaut_20 = 20,
		}
		public enum CauseType15Options
		{
			NONE_0 = 0,
			Player_1 = 1,
			Actor_2 = 2,
			Biped_3 = 3,
			Body_4 = 4,
			Vehicle_5 = 5,
			Projectile_6 = 6,
			ActorOrPlayer_7 = 7,
			Turret_8 = 8,
			UnitInVehicle_9 = 9,
			UnitInTurret_10 = 10,
			Driver_11 = 11,
			Gunner_12 = 12,
			Passenger_13 = 13,
			Postcombat_14 = 14,
			PostcombatWon_15 = 15,
			PostcombatLost_16 = 16,
			PlayerMasterchief_17 = 17,
			PlayerDervish_18 = 18,
			Heretic_19 = 19,
			MajorlyScary_20 = 20,
			LastManInVehicle_21 = 21,
			Male_22 = 22,
			Female_23 = 23,
			Grenade_24 = 24,
		}
		public enum SubjectType16Options
		{
			NONE_0 = 0,
			Player_1 = 1,
			Actor_2 = 2,
			Biped_3 = 3,
			Body_4 = 4,
			Vehicle_5 = 5,
			Projectile_6 = 6,
			ActorOrPlayer_7 = 7,
			Turret_8 = 8,
			UnitInVehicle_9 = 9,
			UnitInTurret_10 = 10,
			Driver_11 = 11,
			Gunner_12 = 12,
			Passenger_13 = 13,
			Postcombat_14 = 14,
			PostcombatWon_15 = 15,
			PostcombatLost_16 = 16,
			PlayerMasterchief_17 = 17,
			PlayerDervish_18 = 18,
			Heretic_19 = 19,
			MajorlyScary_20 = 20,
			LastManInVehicle_21 = 21,
			Male_22 = 22,
			Female_23 = 23,
			Grenade_24 = 24,
		}
		public enum SpatialRelationWithRespectToTheSubjectTheCauseIs18Options
		{
			None_0 = 0,
			VeryNear1wu_1 = 1,
			Near25wus_2 = 2,
			MediumRange5wus_3 = 3,
			Far10wus_4 = 4,
			VeryFar10wus_5 = 5,
			InFrontOf_6 = 6,
			Behind_7 = 7,
			AboveDelta1Wu_8 = 8,
			BelowDelta1Wu_9 = 9,
		}
		public enum Conditions22Options
		{
			Asleep_0 = 1,
			Idle_1 = 2,
			Alert_2 = 4,
			Active_3 = 8,
			UninspectedOrphan_4 = 16,
			DefiniteOrphan_5 = 32,
			CertainOrphan_6 = 64,
			VisibleEnemy_7 = 128,
			ClearLosEnemy_8 = 256,
			DangerousEnemy_9 = 512,
			NoVehicle_10 = 1024,
			VehicleDriver_11 = 2048,
			VehiclePassenger_12 = 4096,
		}
		[Field("dialogue type", typeof(DialogueType0Options))]
		public short DialogueType0;
		[Field("vocalization index", null)]
		public short VocalizationIndex1;
		[Field("vocalization name", null)]
		public StringId VocalizationName2;
		[Field("speaker type", typeof(SpeakerType3Options))]
		public short SpeakerType3;
		[Field("flags", typeof(Flags4Options))]
		public short Flags4;
		[Field("listener/target#who/what am I speaking to/of?", typeof(ListenerTargetWhoWhatAmISpeakingToOf5Options))]
		public short ListenerTarget5;
		[Field("", null)]
		public fixed byte _6[2];
		[Field("", null)]
		public fixed byte _7[4];
		[Field("hostility#The relationship between the subject and the cause", typeof(HostilityTheRelationshipBetweenTheSubjectAndTheCause8Options))]
		public short Hostility8;
		[Field("damage type", typeof(DamageType9Options))]
		public short DamageType9;
		[Field("danger level#Speaker must have danger level of at least this much", typeof(DangerLevelSpeakerMustHaveDangerLevelOfAtLeastThisMuch10Options))]
		public short DangerLevel10;
		[Field("attitude", typeof(Attitude11Options))]
		public short Attitude11;
		[Field("", null)]
		public fixed byte _12[4];
		[Field("subject actor type", typeof(SubjectActorType13Options))]
		public short SubjectActorType13;
		[Field("cause actor type", typeof(CauseActorType14Options))]
		public short CauseActorType14;
		[Field("cause type", typeof(CauseType15Options))]
		public short CauseType15;
		[Field("subject type", typeof(SubjectType16Options))]
		public short SubjectType16;
		[Field("cause ai type name", null)]
		public StringId CauseAiTypeName17;
		[Field("spatial relation#with respect to the subject, the cause is ...", typeof(SpatialRelationWithRespectToTheSubjectTheCauseIs18Options))]
		public short SpatialRelation18;
		[Field("", null)]
		public fixed byte _19[2];
		[Field("subject ai type name", null)]
		public StringId SubjectAiTypeName20;
		[Field("", null)]
		public fixed byte _21[8];
		[Field("Conditions", typeof(Conditions22Options))]
		public int Conditions22;
	}
}
#pragma warning restore CS1591
