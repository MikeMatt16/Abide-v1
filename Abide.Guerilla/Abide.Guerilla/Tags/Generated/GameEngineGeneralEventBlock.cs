using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(244, 4)]
	public unsafe struct GameEngineGeneralEventBlock
	{
		public enum Flags0Options
		{
			QuantityMessage_0 = 1,
		}
		public enum Event2Options
		{
			Kill_0 = 0,
			Suicide_1 = 1,
			KillTeammate_2 = 2,
			Victory_3 = 3,
			TeamVictory_4 = 4,
			Unused1_5 = 5,
			Unused2_6 = 6,
			_1MinToWin_7 = 7,
			Team1MinToWin_8 = 8,
			_30SecsToWin_9 = 9,
			Team30SecsToWin_10 = 10,
			PlayerQuit_11 = 11,
			PlayerJoined_12 = 12,
			KilledByUnknown_13 = 13,
			_30MinutesLeft_14 = 14,
			_15MinutesLeft_15 = 15,
			_5MinutesLeft_16 = 16,
			_1MinuteLeft_17 = 17,
			TimeExpired_18 = 18,
			GameOver_19 = 19,
			RespawnTick_20 = 20,
			LastRespawnTick_21 = 21,
			TeleporterUsed_22 = 22,
			PlayerChangedTeam_23 = 23,
			PlayerRejoined_24 = 24,
			GainedLead_25 = 25,
			GainedTeamLead_26 = 26,
			LostLead_27 = 27,
			LostTeamLead_28 = 28,
			TiedLeader_29 = 29,
			TiedTeamLeader_30 = 30,
			RoundOver_31 = 31,
			_30SecondsLeft_32 = 32,
			_10SecondsLeft_33 = 33,
			KillFalling_34 = 34,
			KillCollision_35 = 35,
			KillMelee_36 = 36,
			SuddenDeath_37 = 37,
			PlayerBootedPlayer_38 = 38,
			KillFlagCarrier_39 = 39,
			KillBombCarrier_40 = 40,
			KillStickyGrenade_41 = 41,
			KillSniper_42 = 42,
			KillStMelee_43 = 43,
			BoardedVehicle_44 = 44,
			StartTeamNoti_45 = 45,
			Telefrag_46 = 46,
			_10SecsToWin_47 = 47,
			Team10SecsToWin_48 = 48,
		}
		public enum Audience3Options
		{
			CausePlayer_0 = 0,
			CauseTeam_1 = 1,
			EffectPlayer_2 = 2,
			EffectTeam_3 = 3,
			All_4 = 4,
		}
		public enum RequiredField7Options
		{
			NONE_0 = 0,
			CausePlayer_1 = 1,
			CauseTeam_2 = 2,
			EffectPlayer_3 = 3,
			EffectTeam_4 = 4,
		}
		public enum ExcludedAudience8Options
		{
			NONE_0 = 0,
			CausePlayer_1 = 1,
			CauseTeam_2 = 2,
			EffectPlayer_3 = 3,
			EffectTeam_4 = 4,
		}
		public enum SoundFlags14Options
		{
			AnnouncerSound_0 = 1,
		}
		[Field("flags", typeof(Flags0Options))]
		public short Flags0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("event^", typeof(Event2Options))]
		public short Event2;
		[Field("audience^", typeof(Audience3Options))]
		public short Audience3;
		[Field("", null)]
		public fixed byte _4[2];
		[Field("", null)]
		public fixed byte _5[2];
		[Field("display string", null)]
		public StringId DisplayString6;
		[Field("required field", typeof(RequiredField7Options))]
		public short RequiredField7;
		[Field("excluded audience", typeof(ExcludedAudience8Options))]
		public short ExcludedAudience8;
		[Field("primary string", null)]
		public StringId PrimaryString9;
		[Field("primary string duration:seconds", null)]
		public int PrimaryStringDuration10;
		[Field("plural display string", null)]
		public StringId PluralDisplayString11;
		[Field("", null)]
		public fixed byte _12[28];
		[Field("sound delay (announcer only)", null)]
		public float SoundDelayAnnouncerOnly13;
		[Field("sound flags", typeof(SoundFlags14Options))]
		public short SoundFlags14;
		[Field("", null)]
		public fixed byte _15[2];
		[Field("sound^", null)]
		public TagReference Sound16;
		[Field("extra sounds", typeof(SoundResponseExtraSoundsStructBlock))]
		[Block("Sound Response Extra Sounds Struct", 1, typeof(SoundResponseExtraSoundsStructBlock))]
		public SoundResponseExtraSoundsStructBlock ExtraSounds17;
		[Field("", null)]
		public fixed byte _18[4];
		[Field("", null)]
		public fixed byte _19[16];
		[Field("sound permutations", null)]
		[Block("Sound Response Definition Block", 10, typeof(SoundResponseDefinitionBlock))]
		public TagBlock SoundPermutations20;
	}
}
#pragma warning restore CS1591
