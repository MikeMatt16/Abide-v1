using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(144, 16)]
	public unsafe struct RigidBodiesBlock
	{
		public enum Flags6Options
		{
			NoCollisionsWSelf_0 = 1,
			OnlyCollideWEnv_1 = 2,
			DisableEffectsThisRigidBodyWillNotGenerateImpactEffectsUnlessItHitsAnotherDynamicRigidBodyThatDoes_2 = 4,
			DoesNotInteractWEnvironmentSetThisFlagIfThisRigidBodiesWonTTouchTheEnvironmentThisAllowsUsToOpenUpSomeOptimizations_3 = 8,
			BestEarlyMoverBodyIfYouHaveEitherOfTheEarlyMoverFlagsSetInTheObjectDefinitoinThisBodyWillBeChoosenAsTheOneToMakeEveryThingLocalToOtherwiseIPick_4 = 16,
			HasNoPhantomPowerVersionDonTCheckThisFlagWithoutTalkingToEamon_5 = 32,
		}
		public enum MotionType7Options
		{
			Sphere_0 = 0,
			StabilizedSphere_1 = 1,
			Box_2 = 2,
			StabilizedBox_3 = 3,
			Keyframed_4 = 4,
			Fixed_5 = 5,
		}
		public enum Size9Options
		{
			Default_0 = 0,
			Tiny_1 = 1,
			Small_2 = 2,
			Medium_3 = 3,
			Large_4 = 4,
			Huge_5 = 5,
			ExtraHuge_6 = 6,
		}
		public enum ShapeType14Options
		{
			Sphere_0 = 0,
			Pill_1 = 1,
			Box_2 = 2,
			Triangle_3 = 3,
			Polyhedron_4 = 4,
			MultiSphere_5 = 5,
			Unused0_6 = 6,
			Unused1_7 = 7,
			Unused2_8 = 8,
			Unused3_9 = 9,
			Unused4_10 = 10,
			Unused5_11 = 11,
			Unused6_12 = 12,
			Unused7_13 = 13,
			List_14 = 14,
			Mopp_15 = 15,
		}
		[Field("node*", null)]
		public short Node0;
		[Field("region*", null)]
		public short Region1;
		[Field("permutattion*", null)]
		public short Permutattion2;
		[Field("", null)]
		public fixed byte _3[2];
		public Vector3 BoudingSphereOffset4;
		[Field("bounding sphere radius*", null)]
		public float BoundingSphereRadius5;
		[Field("flags", typeof(Flags6Options))]
		public short Flags6;
		[Field("motion type", typeof(MotionType7Options))]
		public short MotionType7;
		[Field("no phantom power alt", null)]
		public short NoPhantomPowerAlt8;
		[Field("size", typeof(Size9Options))]
		public short Size9;
		[Field("inertia tensor scale#0.0 defaults to 1.0", null)]
		public float InertiaTensorScale10;
		[Field("linear damping#this goes from 0-10 (10 is really, really high)", null)]
		public float LinearDamping11;
		[Field("angular damping#this goes from 0-10 (10 is really, really high)", null)]
		public float AngularDamping12;
		[Field("center off mass offset", null)]
		public Vector3 CenterOffMassOffset13;
		[Field("shape type*", typeof(ShapeType14Options))]
		public short ShapeType14;
		[Field("shape*", null)]
		public short Shape15;
		[Field("mass:kg*", null)]
		public float Mass16;
		[Field("center of mass*", null)]
		public Vector3 CenterOfMass17;
		[Field("", null)]
		public fixed byte _18[4];
		[Field("intertia tensor x*", null)]
		public Vector3 IntertiaTensorX19;
		[Field("", null)]
		public fixed byte _20[4];
		[Field("intertia tensor y*", null)]
		public Vector3 IntertiaTensorY21;
		[Field("", null)]
		public fixed byte _22[4];
		[Field("intertia tensor z*", null)]
		public Vector3 IntertiaTensorZ23;
		[Field("", null)]
		public fixed byte _24[4];
		[Field("bounding sphere pad#the bounding sphere for this rigid body will be outset by this much", null)]
		public float BoundingSpherePad25;
		[Field("", null)]
		public fixed byte _26[12];
	}
}
#pragma warning restore CS1591
