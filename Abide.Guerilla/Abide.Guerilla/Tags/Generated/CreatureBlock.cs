using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("creature", "crea", "obje", typeof(CreatureBlock))]
	[FieldSet(224, 4)]
	public unsafe struct CreatureBlock
	{
		public enum Flags1Options
		{
			Unused_0 = 1,
			InfectionForm_1 = 2,
			ImmuneToFallingDamage_2 = 4,
			RotateWhileAirborne_3 = 8,
			ZappedByShields_4 = 16,
			AttachUponImpact_5 = 32,
			NotOnMotionSensor_6 = 64,
		}
		public enum DefaultTeam2Options
		{
			Default_0 = 0,
			Player_1 = 1,
			Human_2 = 2,
			Covenant_3 = 3,
			Flood_4 = 4,
			Sentinel_5 = 5,
			Heretic_6 = 6,
			Prophet_7 = 7,
			Unused8_8 = 8,
			Unused9_9 = 9,
			Unused10_10 = 10,
			Unused11_11 = 11,
			Unused12_12 = 12,
			Unused13_13 = 13,
			Unused14_14 = 14,
			Unused15_15 = 15,
		}
		public enum MotionSensorBlipSize3Options
		{
			Medium_0 = 0,
			Small_1 = 1,
			Large_2 = 2,
		}
		[Field("flags", typeof(Flags1Options))]
		public int Flags1;
		[Field("default team", typeof(DefaultTeam2Options))]
		public short DefaultTeam2;
		[Field("motion sensor blip size", typeof(MotionSensorBlipSize3Options))]
		public short MotionSensorBlipSize3;
		[Field("turning velocity maximum:degrees per second", null)]
		public float TurningVelocityMaximum4;
		[Field("turning acceleration maximum:degrees per second squared", null)]
		public float TurningAccelerationMaximum5;
		[Field("casual turning modifier:[0,1]", null)]
		public float CasualTurningModifier6;
		[Field("", null)]
		public fixed byte _7[4];
		[Field("autoaim width:world units", null)]
		public float AutoaimWidth8;
		[Field("physics", typeof(CharacterPhysicsStructBlock))]
		[Block("Character Physics Struct", 1, typeof(CharacterPhysicsStructBlock))]
		public CharacterPhysicsStructBlock Physics9;
		[Field("", null)]
		public fixed byte _10[64];
		[Field("impact damage", null)]
		public TagReference ImpactDamage11;
		[Field("impact shield damage#if not specified, uses 'impact damage'", null)]
		public TagReference ImpactShieldDamage12;
		[Field("", null)]
		public fixed byte _13[32];
		[Field("destroy after death time:seconds#if non-zero, the creature will destroy itself upon death after this much time", null)]
		public FloatBounds DestroyAfterDeathTime15;
	}
}
#pragma warning restore CS1591
