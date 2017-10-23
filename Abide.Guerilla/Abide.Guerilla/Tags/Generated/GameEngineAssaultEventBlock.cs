using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(244, 4)]
	public unsafe struct GameEngineAssaultEventBlock
	{
		public enum Flags0Options
		{
			QuantityMessage_0 = 1,
		}
		public enum Event2Options
		{
			GameStart_0 = 0,
			BombTaken_1 = 1,
			BombDropped_2 = 2,
			BombReturnedByPlayer_3 = 3,
			BombReturnedByTimeout_4 = 4,
			BombCaptured_5 = 5,
			BombNewDefensiveTeam_6 = 6,
			BombReturnFaliure_7 = 7,
			SideSwitchTick_8 = 8,
			SideSwitchFinalTick_9 = 9,
			SideSwitch30Seconds_10 = 10,
			SideSwitch10Seconds_11 = 11,
			BombReturnedByDefusing_12 = 12,
			BombPlacedOnEnemyPost_13 = 13,
			BombArmingStarted_14 = 14,
			BombArmingCompleted_15 = 15,
			BombContested_16 = 16,
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
