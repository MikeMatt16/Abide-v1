using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(36, 4)]
	public unsafe struct CharacterMovementBlock
	{
		public enum MovementFlags0Options
		{
			DangerCrouchAllowMovement_0 = 1,
			NoSideStep_1 = 2,
			PreferToCombarNearFriends_2 = 4,
			HopToCoverPathSegements_3 = 8,
			Perch_4 = 16,
			HasFlyingMode_5 = 32,
			DisallowCrouch_6 = 64,
		}
		public enum ObstacleLeapMinSize6Options
		{
			None_0 = 0,
			Tiny_1 = 1,
			Small_2 = 2,
			Medium_3 = 3,
			Large_4 = 4,
			Huge_5 = 5,
			Immobile_6 = 6,
		}
		public enum ObstacleLeapMaxSize7Options
		{
			None_0 = 0,
			Tiny_1 = 1,
			Small_2 = 2,
			Medium_3 = 3,
			Large_4 = 4,
			Huge_5 = 5,
			Immobile_6 = 6,
		}
		public enum ObstacleIgnoreSize8Options
		{
			None_0 = 0,
			Tiny_1 = 1,
			Small_2 = 2,
			Medium_3 = 3,
			Large_4 = 4,
			Huge_5 = 5,
			Immobile_6 = 6,
		}
		public enum ObstacleSmashableSize9Options
		{
			None_0 = 0,
			Tiny_1 = 1,
			Small_2 = 2,
			Medium_3 = 3,
			Large_4 = 4,
			Huge_5 = 5,
			Immobile_6 = 6,
		}
		public enum JumpHeight11Options
		{
			NONE_0 = 0,
			Down_1 = 1,
			Step_2 = 2,
			Crouch_3 = 3,
			Stand_4 = 4,
			Storey_5 = 5,
			Tower_6 = 6,
			Infinite_7 = 7,
		}
		public enum MovementHints12Options
		{
			VaultStep_0 = 1,
			VaultCrouch_1 = 2,
			__2 = 4,
			__3 = 8,
			__4 = 16,
			MountStep_5 = 32,
			MountCrouch_6 = 64,
			MountStand_7 = 128,
			__8 = 256,
			__9 = 512,
			__10 = 1024,
			HoistCrouch_11 = 2048,
			HoistStand_12 = 4096,
			__13 = 8192,
			__14 = 16384,
			__15 = 32768,
		}
		[Field("movement flags", typeof(MovementFlags0Options))]
		public int MovementFlags0;
		[Field("pathfinding radius", null)]
		public float PathfindingRadius1;
		[Field("destination radius", null)]
		public float DestinationRadius2;
		[Field("", null)]
		public fixed byte _3[20];
		[Field("dive grenade chance", null)]
		public float DiveGrenadeChance4;
		[Field("", null)]
		public fixed byte _5[8];
		[Field("obstacle leap min size", typeof(ObstacleLeapMinSize6Options))]
		public short ObstacleLeapMinSize6;
		[Field("obstacle leap max size", typeof(ObstacleLeapMaxSize7Options))]
		public short ObstacleLeapMaxSize7;
		[Field("obstacle ignore size", typeof(ObstacleIgnoreSize8Options))]
		public short ObstacleIgnoreSize8;
		[Field("obstacle smashable size", typeof(ObstacleSmashableSize9Options))]
		public short ObstacleSmashableSize9;
		[Field("", null)]
		public fixed byte _10[2];
		[Field("jump height", typeof(JumpHeight11Options))]
		public short JumpHeight11;
		[Field("movement hints", typeof(MovementHints12Options))]
		public int MovementHints12;
		[Field("throttle scale", null)]
		public float ThrottleScale13;
	}
}
#pragma warning restore CS1591
