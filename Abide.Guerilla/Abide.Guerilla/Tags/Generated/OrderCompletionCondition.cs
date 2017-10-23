using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(56, 4)]
	public unsafe struct OrderCompletionCondition
	{
		public enum RuleType0Options
		{
			AOrGreaterAlive_0 = 0,
			AOrFewerAlive_1 = 1,
			XOrGreaterStrength_2 = 2,
			XOrLessStrength_3 = 3,
			IfEnemySighted_4 = 4,
			AfterATicks_5 = 5,
			IfAlertedBySquadA_6 = 6,
			ScriptRefTRUE_7 = 7,
			ScriptRefFALSE_8 = 8,
			IfPlayerInTriggerVolume_9 = 9,
			IfALLPlayersInTriggerVolume_10 = 10,
			CombatStatusAOrMore_11 = 11,
			CombatStatusAOrLess_12 = 12,
			Arrived_13 = 13,
			InVehicle_14 = 14,
			SightedPlayer_15 = 15,
			AOrGreaterFighting_16 = 16,
			AOrFewerFighting_17 = 17,
			PlayerWithinXWorldUnits_18 = 18,
			PlayerShotMoreThanXSecondsAgo_19 = 19,
			GameSafeToSave_20 = 20,
		}
		public enum Flags15Options
		{
			NOT_0 = 1,
		}
		[Field("rule type^", typeof(RuleType0Options))]
		public short RuleType0;
		[Field("squad", null)]
		public short Squad1;
		[Field("squad group", null)]
		public short SquadGroup2;
		[Field("a", null)]
		public short A3;
		[Field("", null)]
		public fixed byte _4[4];
		[Field("", null)]
		public fixed byte _5[12];
		[Field("x", null)]
		public float X6;
		[Field("", null)]
		public fixed byte _7[8];
		[Field("trigger volume", null)]
		public short TriggerVolume8;
		[Field("", null)]
		public fixed byte _9[2];
		[Field("", null)]
		public fixed byte _10[8];
		[Field("Exit condition script", null)]
		public String ExitConditionScript11;
		[Field("", null)]
		public short _12;
		[Field("", null)]
		public fixed byte _13[2];
		[Field("", null)]
		public fixed byte _14[36];
		[Field("flags", typeof(Flags15Options))]
		public int Flags15;
	}
}
#pragma warning restore CS1591
