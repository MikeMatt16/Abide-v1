using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(76, 4)]
	public unsafe struct FrictionPointDefinitionBlock
	{
		public enum Flags1Options
		{
			GetsDamageFromRegion_0 = 1,
			Powered_1 = 2,
			FrontTurning_2 = 4,
			RearTurning_3 = 8,
			AttachedToEBrake_4 = 16,
			CanBeDestroyed_5 = 32,
		}
		public enum FrictionType5Options
		{
			Point_0 = 0,
			Forward_1 = 1,
		}
		public enum ModelStateDestroyedOnlyNeedPointCanDestroyFlagSet15Options
		{
			Default_0 = 0,
			MinorDamage_1 = 1,
			MediumDamage_2 = 2,
			MajorDamage_3 = 3,
			Destroyed_4 = 4,
		}
		[Field("marker name^", null)]
		public StringId MarkerName0;
		[Field("flags", typeof(Flags1Options))]
		public int Flags1;
		[Field("fraction of total mass#(0.0-1.0) fraction of total vehicle mass", null)]
		public float FractionOfTotalMass2;
		[Field("radius", null)]
		public float Radius3;
		[Field("damaged radius#radius when the tire is blown off.", null)]
		public float DamagedRadius4;
		[Field("friction type", typeof(FrictionType5Options))]
		public short FrictionType5;
		[Field("", null)]
		public fixed byte _6[2];
		[Field("moving friction velocity diff", null)]
		public float MovingFrictionVelocityDiff7;
		[Field("e-brake moving friction", null)]
		public float EBrakeMovingFriction8;
		[Field("e-brake friction", null)]
		public float EBrakeFriction9;
		[Field("e-brake moving friction vel diff", null)]
		public float EBrakeMovingFrictionVelDiff10;
		[Field("", null)]
		public fixed byte _11[20];
		[Field("collision global material name", null)]
		public StringId CollisionGlobalMaterialName12;
		[Field("", null)]
		public fixed byte _13[2];
		[Field("model state destroyed#only need point can destroy flag set", typeof(ModelStateDestroyedOnlyNeedPointCanDestroyFlagSet15Options))]
		public short ModelStateDestroyed15;
		[Field("region name#only need point can destroy flag set", null)]
		public StringId RegionName16;
		[Field("", null)]
		public fixed byte _17[4];
	}
}
#pragma warning restore CS1591
