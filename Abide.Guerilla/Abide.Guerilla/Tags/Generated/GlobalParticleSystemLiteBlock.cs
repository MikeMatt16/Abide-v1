using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(156, 4)]
	public unsafe struct GlobalParticleSystemLiteBlock
	{
		public enum Type15Options
		{
			Generic_0 = 0,
			Snow_1 = 1,
			Rain_2 = 2,
			RainSplash_3 = 3,
			Bugs_4 = 4,
			SandStorm_5 = 5,
			Debris_6 = 6,
			Bubbles_7 = 7,
		}
		[Field("sprites", null)]
		public TagReference Sprites0;
		[Field("view box width", null)]
		public float ViewBoxWidth1;
		[Field("view box height", null)]
		public float ViewBoxHeight2;
		[Field("view box depth", null)]
		public float ViewBoxDepth3;
		[Field("exclusion radius", null)]
		public float ExclusionRadius4;
		[Field("max velocity", null)]
		public float MaxVelocity5;
		[Field("min mass", null)]
		public float MinMass6;
		[Field("max mass", null)]
		public float MaxMass7;
		[Field("min size", null)]
		public float MinSize8;
		[Field("max size", null)]
		public float MaxSize9;
		[Field("maximum number of particles", null)]
		public int MaximumNumberOfParticles10;
		[Field("initial velocity", null)]
		public Vector3 InitialVelocity11;
		[Field("bitmap animation speed", null)]
		public float BitmapAnimationSpeed12;
		[Field("geometry block info*", typeof(GlobalGeometryBlockInfoStructBlock))]
		[Block("Global Geometry Block Info Struct", 1, typeof(GlobalGeometryBlockInfoStructBlock))]
		public GlobalGeometryBlockInfoStructBlock GeometryBlockInfo13;
		[Field("particle system data", null)]
		[Block("Particle System Lite Data Block", 1, typeof(ParticleSystemLiteDataBlock))]
		public TagBlock ParticleSystemData14;
		[Field("type", typeof(Type15Options))]
		public short Type15;
		[Field("", null)]
		public fixed byte _16[2];
		[Field("mininum opacity", null)]
		public float MininumOpacity17;
		[Field("maxinum opacity", null)]
		public float MaxinumOpacity18;
		[Field("rain streak scale", null)]
		public float RainStreakScale19;
		[Field("rain line width", null)]
		public float RainLineWidth20;
		[Field("", null)]
		public fixed byte _21[4];
		[Field("", null)]
		public fixed byte _22[4];
		[Field("", null)]
		public fixed byte _23[4];
	}
}
#pragma warning restore CS1591
