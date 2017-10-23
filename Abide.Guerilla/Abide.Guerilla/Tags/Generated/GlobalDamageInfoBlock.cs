using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(320, 4)]
	public unsafe struct GlobalDamageInfoBlock
	{
		public enum Flags0Options
		{
			TakesShieldDamageForChildren_0 = 1,
			TakesBodyDamageForChildren_1 = 2,
			AlwaysShieldsFriendlyDamage_2 = 4,
			PassesAreaDamageToChildren_3 = 8,
			ParentNeverTakesBodyDamageForUs_4 = 16,
			OnlyDamagedByExplosives_5 = 32,
			ParentNeverTakesShieldDamageForUs_6 = 64,
			CannotDieFromDamage_7 = 128,
			PassesAttachedDamageToRiders_8 = 256,
		}
		public enum CollisionDamageReportingType5Options
		{
			TehGuardians11_0 = 0,
			FallingDamage_1 = 1,
			GenericCollisionDamage_2 = 2,
			GenericMeleeDamage_3 = 3,
			GenericExplosion_4 = 4,
			MagnumPistol_5 = 5,
			PlasmaPistol_6 = 6,
			Needler_7 = 7,
			Smg_8 = 8,
			PlasmaRifle_9 = 9,
			BattleRifle_10 = 10,
			Carbine_11 = 11,
			Shotgun_12 = 12,
			SniperRifle_13 = 13,
			BeamRifle_14 = 14,
			RocketLauncher_15 = 15,
			FlakCannon_16 = 16,
			BruteShot_17 = 17,
			Disintegrator_18 = 18,
			BrutePlasmaRifle_19 = 19,
			EnergySword_20 = 20,
			FragGrenade_21 = 21,
			PlasmaGrenade_22 = 22,
			FlagMeleeDamage_23 = 23,
			BombMeleeDamage_24 = 24,
			BombExplosionDamage_25 = 25,
			BallMeleeDamage_26 = 26,
			HumanTurret_27 = 27,
			PlasmaTurret_28 = 28,
			Banshee_29 = 29,
			Ghost_30 = 30,
			Mongoose_31 = 31,
			Scorpion_32 = 32,
			SpectreDriver_33 = 33,
			SpectreGunner_34 = 34,
			WarthogDriver_35 = 35,
			WarthogGunner_36 = 36,
			Wraith_37 = 37,
			Tank_38 = 38,
			SentinelBeam_39 = 39,
			SentinelRpg_40 = 40,
			Teleporter_41 = 41,
		}
		public enum ResponseDamageReportingType6Options
		{
			TehGuardians11_0 = 0,
			FallingDamage_1 = 1,
			GenericCollisionDamage_2 = 2,
			GenericMeleeDamage_3 = 3,
			GenericExplosion_4 = 4,
			MagnumPistol_5 = 5,
			PlasmaPistol_6 = 6,
			Needler_7 = 7,
			Smg_8 = 8,
			PlasmaRifle_9 = 9,
			BattleRifle_10 = 10,
			Carbine_11 = 11,
			Shotgun_12 = 12,
			SniperRifle_13 = 13,
			BeamRifle_14 = 14,
			RocketLauncher_15 = 15,
			FlakCannon_16 = 16,
			BruteShot_17 = 17,
			Disintegrator_18 = 18,
			BrutePlasmaRifle_19 = 19,
			EnergySword_20 = 20,
			FragGrenade_21 = 21,
			PlasmaGrenade_22 = 22,
			FlagMeleeDamage_23 = 23,
			BombMeleeDamage_24 = 24,
			BombExplosionDamage_25 = 25,
			BallMeleeDamage_26 = 26,
			HumanTurret_27 = 27,
			PlasmaTurret_28 = 28,
			Banshee_29 = 29,
			Ghost_30 = 30,
			Mongoose_31 = 31,
			Scorpion_32 = 32,
			SpectreDriver_33 = 33,
			SpectreGunner_34 = 34,
			WarthogDriver_35 = 35,
			WarthogGunner_36 = 36,
			Wraith_37 = 37,
			Tank_38 = 38,
			SentinelBeam_39 = 39,
			SentinelRpg_40 = 40,
			Teleporter_41 = 41,
		}
		[Field("flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("global indirect material name#absorbes AOE or child damage", null)]
		public StringId GlobalIndirectMaterialName1;
		[Field("indirect damage section#absorbes AOE or child damage", null)]
		public short IndirectDamageSection2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("", null)]
		public fixed byte _4[4];
		[Field("collision damage reporting type", typeof(CollisionDamageReportingType5Options))]
		public byte CollisionDamageReportingType5;
		[Field("response damage reporting type", typeof(ResponseDamageReportingType6Options))]
		public byte ResponseDamageReportingType6;
		[Field("", null)]
		public fixed byte _7[2];
		[Field("", null)]
		public fixed byte _8[20];
		[Field("maximum vitality", null)]
		public float MaximumVitality10;
		[Field("minimum stun damage#the minimum damage required to stun this object's health", null)]
		public float MinimumStunDamage11;
		[Field("stun time:seconds#the length of time the health stay stunned (do not recharge) after taking damage", null)]
		public float StunTime12;
		[Field("recharge time:seconds#the length of time it would take for the shields to fully recharge after being completely depleted", null)]
		public float RechargeTime13;
		[Field("recharge fraction#0 defaults to 1 - to what maximum level the body health will be allowed to recharge", null)]
		public float RechargeFraction14;
		[Field("", null)]
		public fixed byte _15[64];
		[Field("shield damaged first person shader", null)]
		public TagReference ShieldDamagedFirstPersonShader17;
		[Field("shield damaged shader", null)]
		public TagReference ShieldDamagedShader18;
		[Field("maximum shield vitality#the default initial and maximum shield vitality of this object", null)]
		public float MaximumShieldVitality19;
		[Field("global shield material name", null)]
		public StringId GlobalShieldMaterialName20;
		[Field("minimum stun damage#the minimum damage required to stun this object's shields", null)]
		public float MinimumStunDamage21;
		[Field("stun time:seconds#the length of time the shields stay stunned (do not recharge) after taking damage", null)]
		public float StunTime22;
		[Field("recharge time:seconds#the length of time it would take for the shields to fully recharge after being completely depleted", null)]
		public float RechargeTime23;
		[Field("shield damaged threshold", null)]
		public float ShieldDamagedThreshold24;
		[Field("shield damaged effect", null)]
		public TagReference ShieldDamagedEffect25;
		[Field("shield depleted effect", null)]
		public TagReference ShieldDepletedEffect26;
		[Field("shield recharging effect", null)]
		public TagReference ShieldRechargingEffect27;
		[Field("damage sections", null)]
		[Block("Global Damage Section Block", 16, typeof(GlobalDamageSectionBlock))]
		public TagBlock DamageSections28;
		[Field("nodes*", null)]
		[Block("Global Damage Nodes Block", 255, typeof(GlobalDamageNodesBlock))]
		public TagBlock Nodes29;
		[Field("", null)]
		public fixed byte _30[2];
		[Field("", null)]
		public fixed byte _31[2];
		[Field("", null)]
		public fixed byte _32[4];
		[Field("", null)]
		public fixed byte _33[4];
		[Field("damage seats", null)]
		[Block("Damage Seat Info Block", 16, typeof(DamageSeatInfoBlock))]
		public TagBlock DamageSeats34;
		[Field("damage constraints", null)]
		[Block("Damage Constraint Info Block", 16, typeof(DamageConstraintInfoBlock))]
		public TagBlock DamageConstraints35;
		[Field("overshield first person shader", null)]
		public TagReference OvershieldFirstPersonShader37;
		[Field("overshield shader", null)]
		public TagReference OvershieldShader38;
	}
}
#pragma warning restore CS1591
