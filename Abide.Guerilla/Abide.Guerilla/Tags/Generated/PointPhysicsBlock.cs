using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("point_physics", "pphy", "����", typeof(PointPhysicsBlock))]
	[FieldSet(64, 4)]
	public unsafe struct PointPhysicsBlock
	{
		public enum Flags0Options
		{
			UNUSED_0 = 1,
			CollidesWithStructures_1 = 2,
			CollidesWithWaterSurface_2 = 4,
			UsesSimpleWindTheWindOnThisPointWonTHaveHighFrequencyVariations_3 = 8,
			UsesDampedWindTheWindOnThisPointWillBeArtificiallySlow_4 = 16,
			NoGravityThePointIsNotAffectedByGravity_5 = 32,
		}
		[Field("flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("", null)]
		public fixed byte _1[28];
		[Field("density:g/mL", null)]
		public float Density2;
		[Field("air friction", null)]
		public float AirFriction3;
		[Field("water friction", null)]
		public float WaterFriction4;
		[Field("surface friction#when hitting the ground or interior, percentage of velocity lost in one collision", null)]
		public float SurfaceFriction5;
		[Field("elasticity#0.0 is inelastic collisions (no bounce) 1.0 is perfectly elastic (reflected velocity equals incoming velocity)", null)]
		public float Elasticity6;
		[Field("", null)]
		public fixed byte _7[12];
	}
}
#pragma warning restore CS1591
