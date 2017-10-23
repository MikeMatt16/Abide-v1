using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(144, 4)]
	public unsafe struct OrdersBlock
	{
		public enum Flags4Options
		{
			Locked_0 = 1,
			AlwaysActive_1 = 2,
			DebugOn_2 = 4,
			StrictAreaDef_3 = 8,
			FollowClosestPlayer_4 = 16,
			FollowSquad_5 = 32,
			ActiveCamo_6 = 64,
			SuppressCombatUntilEngaged_7 = 128,
			InhibitVehicleUse_8 = 256,
		}
		public enum ForceCombatStatus5Options
		{
			NONE_0 = 0,
			Asleep_1 = 1,
			Idle_2 = 2,
			Alert_3 = 3,
			Combat_4 = 4,
		}
		[Field("name^", null)]
		public String Name1;
		[Field("Style", null)]
		public short Style2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("flags", typeof(Flags4Options))]
		public int Flags4;
		[Field("Force combat status", typeof(ForceCombatStatus5Options))]
		public short ForceCombatStatus5;
		[Field("", null)]
		public fixed byte _6[2];
		[Field("Entry Script", null)]
		public String EntryScript7;
		[Field("", null)]
		public fixed byte _8[2];
		[Field("Follow squad", null)]
		public short FollowSquad9;
		[Field("follow radius", null)]
		public float FollowRadius10;
		[Field("Primary area set", null)]
		[Block("Zone Set Block", 32, typeof(ZoneSetBlock))]
		public TagBlock PrimaryAreaSet11;
		[Field("Secondary area set", null)]
		[Block("Secondary Zone Set Block", 32, typeof(SecondaryZoneSetBlock))]
		public TagBlock SecondaryAreaSet12;
		[Field("Secondary set trigger", null)]
		[Block("Secondary Set Trigger Block", 1, typeof(SecondarySetTriggerBlock))]
		public TagBlock SecondarySetTrigger13;
		[Field("Special movement", null)]
		[Block("Special Movement Block", 1, typeof(SpecialMovementBlock))]
		public TagBlock SpecialMovement14;
		[Field("", null)]
		public fixed byte _15[12];
		[Field("Order endings", null)]
		[Block("Order Ending Block", 12, typeof(OrderEndingBlock))]
		public TagBlock OrderEndings16;
	}
}
#pragma warning restore CS1591
