using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(64, 4)]
	public unsafe struct EffectPartBlock
	{
		public enum CreateIn0Options
		{
			AnyEnvironment_0 = 0,
			AirOnly_1 = 1,
			WaterOnly_2 = 2,
			SpaceOnly_3 = 3,
		}
		public enum CreateIn1Options
		{
			EitherMode_0 = 0,
			ViolentModeOnly_1 = 1,
			NonviolentModeOnly_2 = 2,
		}
		public enum Flags3Options
		{
			FaceDownRegardlessOfLocationDecals_0 = 1,
			OffsetOriginAwayFromGeometryLights_1 = 2,
			NeverAttachedToObject_2 = 4,
			DisabledForDebugging_3 = 8,
			DrawRegardlessOfDistance_4 = 16,
		}
		public enum AScalesValues11Options
		{
			Velocity_0 = 1,
			VelocityDelta_1 = 2,
			VelocityConeAngle_2 = 4,
			AngularVelocity_3 = 8,
			AngularVelocityDelta_4 = 16,
			TypeSpecificScale_5 = 32,
		}
		public enum BScalesValues12Options
		{
			Velocity_0 = 1,
			VelocityDelta_1 = 2,
			VelocityConeAngle_2 = 4,
			AngularVelocity_3 = 8,
			AngularVelocityDelta_4 = 16,
			TypeSpecificScale_5 = 32,
		}
		[Field("create in", typeof(CreateIn0Options))]
		public short CreateIn0;
		[Field("create in", typeof(CreateIn1Options))]
		public short CreateIn1;
		[Field("location", null)]
		public short Location2;
		[Field("flags", typeof(Flags3Options))]
		public short Flags3;
		[Field("", null)]
		public fixed byte _4[4];
		[Field("type^", null)]
		public TagReference Type5;
		[Field("velocity bounds:world units per second#initial velocity along the location's forward, for decals the distance at which decal is created (defaults to 0.5)", null)]
		public FloatBounds VelocityBounds6;
		[Field("velocity cone angle:degrees#initial velocity will be inside the cone defined by this angle.", null)]
		public float VelocityConeAngle7;
		[Field("angular velocity bounds:degrees per second", null)]
		public FloatBounds AngularVelocityBounds8;
		[Field("radius modifier bounds", null)]
		public FloatBounds RadiusModifierBounds9;
		[Field("A scales values:", typeof(AScalesValues11Options))]
		public int AScalesValues11;
		[Field("B scales values:", typeof(BScalesValues12Options))]
		public int BScalesValues12;
	}
}
#pragma warning restore CS1591
