using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(12, 4)]
	public unsafe struct PhysicsModelConstraintEdgeConstraintBlock
	{
		public enum Type0Options
		{
			Hinge_0 = 0,
			LimitedHinge_1 = 1,
			Ragdoll_2 = 2,
			StiffSpring_3 = 3,
			BallAndSocket_4 = 4,
			Prismatic_5 = 5,
		}
		public enum Flags2Options
		{
			IsRigidThisConstraintMakesTheEdgeRigidUntilItIsLoosenedByDamage_0 = 1,
			DisableEffectsThisConstraintWillNotGenerateImpactEffects_1 = 2,
		}
		[Field("type*", typeof(Type0Options))]
		public short Type0;
		[Field("index*", null)]
		public short Index1;
		[Field("flags", typeof(Flags2Options))]
		public int Flags2;
		[Field("friction#0 is the default (takes what it was set in max) anything else overrides that value", null)]
		public float Friction3;
	}
}
#pragma warning restore CS1591
