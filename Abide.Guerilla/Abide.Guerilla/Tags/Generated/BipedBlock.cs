using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("biped", "bipd", "unit", typeof(BipedBlock))]
	[FieldSet(336, 4)]
	public unsafe struct BipedBlock
	{
		public enum Flags2Options
		{
			TurnsWithoutAnimating_0 = 1,
			PassesThroughOtherBipeds_1 = 2,
			ImmuneToFallingDamage_2 = 4,
			RotateWhileAirborne_3 = 8,
			UsesLimpBodyPhysics_4 = 16,
			Unused_5 = 32,
			RandomSpeedIncrease_6 = 64,
			Unused_7 = 128,
			SpawnDeathChildrenOnDestroy_8 = 256,
			StunnedByEmpDamage_9 = 512,
			DeadPhysicsWhenStunned_10 = 1024,
			AlwaysRagdollWhenDead_11 = 2048,
		}
		[Field("moving turning speed:degrees per second", null)]
		public float MovingTurningSpeed1;
		[Field("flags", typeof(Flags2Options))]
		public int Flags2;
		[Field("stationary turning threshold", null)]
		public float StationaryTurningThreshold3;
		[Field("", null)]
		public fixed byte _4[16];
		[Field("", null)]
		public fixed byte _5[32];
		[Field("jump velocity:world units per second", null)]
		public float JumpVelocity7;
		[Field("", null)]
		public fixed byte _8[28];
		[Field("maximum soft landing time:seconds#the longest amount of time the biped can take to recover from a soft landing", null)]
		public float MaximumSoftLandingTime9;
		[Field("maximum hard landing time:seconds#the longest amount of time the biped can take to recover from a hard landing", null)]
		public float MaximumHardLandingTime10;
		[Field("minimum soft landing velocity:world units per second#below this velocity the biped does not react when landing", null)]
		public float MinimumSoftLandingVelocity11;
		[Field("minimum hard landing velocity:world units per second#below this velocity the biped will not do a soft landing when returning to the ground", null)]
		public float MinimumHardLandingVelocity12;
		[Field("maximum hard landing velocity:world units per second#the velocity corresponding to the maximum landing time", null)]
		public float MaximumHardLandingVelocity13;
		[Field("death hard landing velocity:world units per second#the maximum velocity with which a character can strike the ground and live", null)]
		public float DeathHardLandingVelocity14;
		[Field("", null)]
		public fixed byte _15[16];
		[Field("stun duration#0 is the default.  Bipeds are stuned when damaged by vehicle collisions, also some are when they take emp damage", null)]
		public float StunDuration16;
		[Field("standing camera height:world units", null)]
		public float StandingCameraHeight18;
		[Field("crouching camera height:world units", null)]
		public float CrouchingCameraHeight19;
		[Field("crouch transition time:seconds", null)]
		public float CrouchTransitionTime20;
		[Field("camera interpolation start:degrees#looking-downward angle that starts camera interpolation to fp position", null)]
		public float CameraInterpolationStart21;
		[Field("camera interpolation end:degrees#looking-downward angle at which camera interpolation to fp position is complete", null)]
		public float CameraInterpolationEnd22;
		[Field("camera forward movement scale#amount of fp camera movement forward and back (1.0 is full)", null)]
		public float CameraForwardMovementScale23;
		[Field("camera side movement scale#amount of fp camera movement side-to-side (1.0 is full)", null)]
		public float CameraSideMovementScale24;
		[Field("camera vertical movement scale#amount of fp camera movement vertically (1.0 is full)", null)]
		public float CameraVerticalMovementScale25;
		[Field("camera exclusion distance:world units#fp camera must always be at least this far out from root node", null)]
		public float CameraExclusionDistance26;
		[Field("autoaim width:world units", null)]
		public float AutoaimWidth27;
		[Field("lock-on data", typeof(BipedLockOnDataStructBlock))]
		[Block("Biped Lock On Data Struct", 1, typeof(BipedLockOnDataStructBlock))]
		public BipedLockOnDataStructBlock LockOnData28;
		[Field("", null)]
		public fixed byte _29[16];
		[Field("", null)]
		public fixed byte _30[12];
		[Field("head shot acc scale#when the biped ragdolls from a head shot it acceleartes based on this value.  0 defaults to the standard acceleration scale", null)]
		public float HeadShotAccScale31;
		[Field("area damage effect", null)]
		public TagReference AreaDamageEffect32;
		[Field("physics", typeof(CharacterPhysicsStructBlock))]
		[Block("Character Physics Struct", 1, typeof(CharacterPhysicsStructBlock))]
		public CharacterPhysicsStructBlock Physics33;
		[Field("contact points#these are the points where the biped touches the ground", null)]
		[Block("Contact Point Block", 3, typeof(ContactPointBlock))]
		public TagBlock ContactPoints34;
		[Field("reanimation character#when the flood reanimate this guy, he turns into a ...", null)]
		public TagReference ReanimationCharacter35;
		[Field("death spawn character#when I die, out of the ashes of my death crawls a ...", null)]
		public TagReference DeathSpawnCharacter36;
		[Field("death spawn count", null)]
		public short DeathSpawnCount37;
		[Field("", null)]
		public fixed byte _38[2];
	}
}
#pragma warning restore CS1591
