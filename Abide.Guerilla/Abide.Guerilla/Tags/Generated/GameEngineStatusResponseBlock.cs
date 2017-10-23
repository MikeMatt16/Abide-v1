using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(36, 4)]
	public unsafe struct GameEngineStatusResponseBlock
	{
		public enum Flags0Options
		{
			Unused_0 = 1,
		}
		public enum State2Options
		{
			WaitingForSpaceToClear_0 = 0,
			Observing_1 = 1,
			RespawningSoon_2 = 2,
			SittingOut_3 = 3,
			OutOfLives_4 = 4,
			PlayingWinning_5 = 5,
			PlayingTied_6 = 6,
			PlayingLosing_7 = 7,
			GameOverWon_8 = 8,
			GameOverTied_9 = 9,
			GameOverLost_10 = 10,
			YouHaveFlag_11 = 11,
			EnemyHasFlag_12 = 12,
			FlagNotHome_13 = 13,
			CarryingOddball_14 = 14,
			YouAreJuggy_15 = 15,
			YouControlHill_16 = 16,
			SwitchingSidesSoon_17 = 17,
			PlayerRecentlyStarted_18 = 18,
			YouHaveBomb_19 = 19,
			FlagContested_20 = 20,
			BombContested_21 = 21,
			LimitedLivesLeftMultiple_22 = 22,
			LimitedLivesLeftSingle_23 = 23,
			LimitedLivesLeftFinal_24 = 24,
			PlayingWinningUnlimited_25 = 25,
			PlayingTiedUnlimited_26 = 26,
			PlayingLosingUnlimited_27 = 27,
		}
		[Field("flags", typeof(Flags0Options))]
		public short Flags0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("state^", typeof(State2Options))]
		public short State2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("ffa message", null)]
		public StringId FfaMessage4;
		[Field("team message", null)]
		public StringId TeamMessage5;
		[Field("", null)]
		public TagReference _6;
		[Field("", null)]
		public fixed byte _7[4];
	}
}
#pragma warning restore CS1591
