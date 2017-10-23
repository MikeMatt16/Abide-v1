using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("style", "styl", "����", typeof(StyleBlock))]
	[FieldSet(100, 4)]
	public unsafe struct StyleBlock
	{
		public enum CombatStatusDecayOptions2Options
		{
			LatchAtIdle_0 = 0,
			LatchAtAlert_1 = 1,
			LatchAtCombat_2 = 2,
		}
		public enum Attitude5Options
		{
			Normal_0 = 0,
			Timid_1 = 1,
			Aggressive_2 = 2,
		}
		public enum EngageAttitude7Options
		{
			Default_0 = 0,
			Normal_1 = 1,
			Timid_2 = 2,
			Aggressive_3 = 3,
		}
		public enum EvasionAttitude8Options
		{
			Default_0 = 0,
			Normal_1 = 1,
			Timid_2 = 2,
			Aggressive_3 = 3,
		}
		public enum CoverAttitude9Options
		{
			Default_0 = 0,
			Normal_1 = 1,
			Timid_2 = 2,
			Aggressive_3 = 3,
		}
		public enum SearchAttitude10Options
		{
			Default_0 = 0,
			Normal_1 = 1,
			Timid_2 = 2,
			Aggressive_3 = 3,
		}
		public enum PresearchAttitude11Options
		{
			Default_0 = 0,
			Normal_1 = 1,
			Timid_2 = 2,
			Aggressive_3 = 3,
		}
		public enum RetreatAttitude12Options
		{
			Default_0 = 0,
			Normal_1 = 1,
			Timid_2 = 2,
			Aggressive_3 = 3,
		}
		public enum ChargeAttitude13Options
		{
			Default_0 = 0,
			Normal_1 = 1,
			Timid_2 = 2,
			Aggressive_3 = 3,
		}
		public enum ReadyAttitude14Options
		{
			Default_0 = 0,
			Normal_1 = 1,
			Timid_2 = 2,
			Aggressive_3 = 3,
		}
		public enum IdleAttitude15Options
		{
			Default_0 = 0,
			Normal_1 = 1,
			Timid_2 = 2,
			Aggressive_3 = 3,
		}
		public enum WeaponAttitude16Options
		{
			Default_0 = 0,
			Normal_1 = 1,
			Timid_2 = 2,
			Aggressive_3 = 3,
		}
		public enum SwarmAttitude17Options
		{
			Default_0 = 0,
			Normal_1 = 1,
			Timid_2 = 2,
			Aggressive_3 = 3,
		}
		public enum StyleControl21Options
		{
			NewBehaviorsDefaultToON_0 = 1,
		}
		public enum Behaviors122Options
		{
			GENERAL_0 = 1,
			Root_1 = 2,
			Null_2 = 4,
			NullDiscrete_3 = 8,
			Obey_4 = 16,
			Guard_5 = 32,
			FollowBehavior_6 = 64,
			Ready_7 = 128,
			SmashObstacle_8 = 256,
			DestroyObstacle_9 = 512,
			Perch_10 = 1024,
			CoverFriend_11 = 2048,
			BlindPanic_12 = 4096,
			ENGAGE_13 = 8192,
			Engage_14 = 16384,
			Fight_15 = 32768,
			MeleeCharge_16 = 65536,
			MeleeLeapingCharge_17 = 131072,
			Surprise_18 = 262144,
			GrenadeImpulse_19 = 524288,
			AntiVehicleGrenade_20 = 1048576,
			Stalk_21 = 2097152,
			BerserkWanderImpulse_22 = 4194304,
			BERSERK_23 = 8388608,
			LastManBerserk_24 = 16777216,
			StuckWithGrenadeBerserk_25 = 33554432,
			PRESEARCH_26 = 67108864,
			Presearch_27 = 134217728,
			PresearchUncover_28 = 268435456,
			DestroyCover_29 = 536870912,
			SuppressingFire_30 = 1073741824,
			GrenadeUncover_31 = -2147483648,
		}
		public enum Behaviors223Options
		{
			LeapOnCover_0 = 1,
			SEARCH_1 = 2,
			Search_2 = 4,
			Uncover_3 = 8,
			Investigate_4 = 16,
			PursuitSync_5 = 32,
			Pursuit_6 = 64,
			Postsearch_7 = 128,
			CovermeInvestigate_8 = 256,
			SELFDEFENSE_9 = 512,
			SelfPreservation_10 = 1024,
			Cover_11 = 2048,
			CoverPeek_12 = 4096,
			Avoid_13 = 8192,
			EvasionImpulse_14 = 16384,
			DiveImpulse_15 = 32768,
			DangerCoverImpulse_16 = 65536,
			DangerCrouchImpulse_17 = 131072,
			ProximityMelee_18 = 262144,
			ProximitySelfPreservation_19 = 524288,
			UnreachableEnemyCover_20 = 1048576,
			ScaryTargetCover_21 = 2097152,
			GroupEmerge_22 = 4194304,
			RETREAT_23 = 8388608,
			Retreat_24 = 16777216,
			RetreatGrenade_25 = 33554432,
			Flee_26 = 67108864,
			Cower_27 = 134217728,
			LowShieldRetreat_28 = 268435456,
			ScaryTargetRetreat_29 = 536870912,
			LeaderDeadRetreat_30 = 1073741824,
			PeerDeadRetreat_31 = -2147483648,
		}
		public enum Behaviors324Options
		{
			DangerRetreat_0 = 1,
			ProximityRetreat_1 = 2,
			ChargeWhenCornered_2 = 4,
			SurpriseRetreat_3 = 8,
			OverheatedWeaponRetreat_4 = 16,
			AMBUSH_5 = 32,
			Ambush_6 = 64,
			CoordinatedAmbush_7 = 128,
			ProximityAmbush_8 = 256,
			VulnerableEnemyAmbush_9 = 512,
			NowhereToRunAmbush_10 = 1024,
			VEHICLE_11 = 2048,
			Vehicle_12 = 4096,
			EnterFriendlyVehicle_13 = 8192,
			ReEnterFlippedVehicle_14 = 16384,
			VehicleEntryEngageImpulse_15 = 32768,
			VehicleBoard_16 = 65536,
			VehicleFight_17 = 131072,
			VehicleCharge_18 = 262144,
			VehicleRamBehavior_19 = 524288,
			VehicleCover_20 = 1048576,
			DamageVehicleCover_21 = 2097152,
			ExposedRearCoverImpulse_22 = 4194304,
			PlayerEndageredCoverImpulse_23 = 8388608,
			VehicleAvoid_24 = 16777216,
			VehiclePickup_25 = 33554432,
			VehiclePlayerPickup_26 = 67108864,
			VehicleExitImpulse_27 = 134217728,
			DangerVehicleExitImpulse_28 = 268435456,
			VehicleFlip_29 = 536870912,
			VehicleTurtle_30 = 1073741824,
			VehicleEngagePatrolImpulse_31 = -2147483648,
		}
		public enum Behaviors425Options
		{
			VehicleEngageWanderImpulse_0 = 1,
			POSTCOMBAT_1 = 2,
			Postcombat_2 = 4,
			PostPostcombat_3 = 8,
			CheckFriend_4 = 16,
			ShootCorpse_5 = 32,
			PostcombatApproach_6 = 64,
			ALERT_7 = 128,
			Alert_8 = 256,
			IDLE_9 = 512,
			Idle_10 = 1024,
			WanderBehavior_11 = 2048,
			FlightWander_12 = 4096,
			Patrol_13 = 8192,
			FallSleep_14 = 16384,
			BUGGERS_15 = 32768,
			BuggerGroundUncover_16 = 65536,
			SWARMS_17 = 131072,
			SwarmRoot_18 = 262144,
			SwarmAttack_19 = 524288,
			SupportAttack_20 = 1048576,
			Infect_21 = 2097152,
			Scatter_22 = 4194304,
			EjectParasite_23 = 8388608,
			FloodSelfPreservation_24 = 16777216,
			JuggernautFlurry_25 = 33554432,
			SENTINELS_26 = 67108864,
			EnforcerWeaponControl_27 = 134217728,
			Grapple_28 = 268435456,
			SPECIAL_29 = 536870912,
			Formation_30 = 1073741824,
			GruntScaredByElite_31 = -2147483648,
		}
		public enum Behaviors526Options
		{
			Stunned_0 = 1,
			CureIsolation_1 = 2,
			DeployTurret_2 = 4,
			__3 = 8,
			__4 = 16,
			__5 = 32,
			__6 = 64,
			__7 = 128,
			__8 = 256,
			__9 = 512,
			__10 = 1024,
			__11 = 2048,
			__12 = 4096,
			__13 = 8192,
			__14 = 16384,
			__15 = 32768,
			__16 = 65536,
			__17 = 131072,
			__18 = 262144,
			__19 = 524288,
			__20 = 1048576,
			__21 = 2097152,
			__22 = 4194304,
			__23 = 8388608,
			__24 = 16777216,
			__25 = 33554432,
			__26 = 67108864,
			__27 = 134217728,
			__28 = 268435456,
			__29 = 536870912,
			__30 = 1073741824,
			__31 = -2147483648,
		}
		[Field("name^", null)]
		public String Name0;
		[Field("Combat status decay options", typeof(CombatStatusDecayOptions2Options))]
		public short CombatStatusDecayOptions2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("Attitude", typeof(Attitude5Options))]
		public short Attitude5;
		[Field("", null)]
		public fixed byte _6[2];
		[Field("engage attitude", typeof(EngageAttitude7Options))]
		public byte EngageAttitude7;
		[Field("evasion attitude", typeof(EvasionAttitude8Options))]
		public byte EvasionAttitude8;
		[Field("cover attitude", typeof(CoverAttitude9Options))]
		public byte CoverAttitude9;
		[Field("search attitude", typeof(SearchAttitude10Options))]
		public byte SearchAttitude10;
		[Field("presearch attitude", typeof(PresearchAttitude11Options))]
		public byte PresearchAttitude11;
		[Field("retreat attitude", typeof(RetreatAttitude12Options))]
		public byte RetreatAttitude12;
		[Field("charge attitude", typeof(ChargeAttitude13Options))]
		public byte ChargeAttitude13;
		[Field("ready attitude", typeof(ReadyAttitude14Options))]
		public byte ReadyAttitude14;
		[Field("idle attitude", typeof(IdleAttitude15Options))]
		public byte IdleAttitude15;
		[Field("weapon attitude", typeof(WeaponAttitude16Options))]
		public byte WeaponAttitude16;
		[Field("swarm attitude", typeof(SwarmAttitude17Options))]
		public byte SwarmAttitude17;
		[Field("", null)]
		public fixed byte _18[1];
		[Field("", null)]
		public fixed byte _19[24];
		[Field("Style control", typeof(StyleControl21Options))]
		public int StyleControl21;
		[Field("Behaviors1", typeof(Behaviors122Options))]
		public int Behaviors122;
		[Field("Behaviors2", typeof(Behaviors223Options))]
		public int Behaviors223;
		[Field("Behaviors3", typeof(Behaviors324Options))]
		public int Behaviors324;
		[Field("Behaviors4", typeof(Behaviors425Options))]
		public int Behaviors425;
		[Field("Behaviors5", typeof(Behaviors526Options))]
		public int Behaviors526;
		[Field("", null)]
		public fixed byte _27[12];
		[Field("Special movement", null)]
		[Block("Special Movement Block", 1, typeof(SpecialMovementBlock))]
		public TagBlock SpecialMovement28;
		[Field("", null)]
		public fixed byte _29[60];
		[Field("Behavior list", null)]
		[Block("Behavior Names Block", 160, typeof(BehaviorNamesBlock))]
		public TagBlock BehaviorList30;
	}
}
#pragma warning restore CS1591
