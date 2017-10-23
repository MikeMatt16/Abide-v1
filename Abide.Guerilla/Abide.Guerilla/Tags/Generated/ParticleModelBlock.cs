using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("particle_model", "PRTM", "����", typeof(ParticleModelBlock))]
	[FieldSet(292, 4)]
	public unsafe struct ParticleModelBlock
	{
		public enum Flags0Options
		{
			Spins_0 = 1,
			RandomUMirror_1 = 2,
			RandomVMirror_2 = 4,
			FrameAnimationOneShot_3 = 8,
			SelectRandomSequence_4 = 16,
			DisableFrameBlending_5 = 32,
			CanAnimateBackwards_6 = 64,
			ReceiveLightmapLighting_7 = 128,
			TintFromDiffuseTexture_8 = 256,
			DiesAtRest_9 = 512,
			DiesOnStructureCollision_10 = 1024,
			DiesInMedia_11 = 2048,
			DiesInAir_12 = 4096,
			BitmapAuthoredVertically_13 = 8192,
			HasSweetener_14 = 16384,
		}
		public enum Orientation1Options
		{
			ScreenFacing_0 = 0,
			ParallelToDirection_1 = 1,
			PerpendicularToDirection_2 = 2,
			Vertical_3 = 3,
			Horizontal_4 = 4,
		}
		[Field("flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("orientation", typeof(Orientation1Options))]
		public int Orientation1;
		[Field("", null)]
		public fixed byte _2[16];
		[Field("shader", null)]
		public TagReference Shader3;
		[Field("scale x", typeof(ParticlePropertyScalarStructNewBlock))]
		[Block("Particle Property Scalar Struct New", 1, typeof(ParticlePropertyScalarStructNewBlock))]
		public ParticlePropertyScalarStructNewBlock ScaleX5;
		[Field("scale y", typeof(ParticlePropertyScalarStructNewBlock))]
		[Block("Particle Property Scalar Struct New", 1, typeof(ParticlePropertyScalarStructNewBlock))]
		public ParticlePropertyScalarStructNewBlock ScaleY7;
		[Field("scale z", typeof(ParticlePropertyScalarStructNewBlock))]
		[Block("Particle Property Scalar Struct New", 1, typeof(ParticlePropertyScalarStructNewBlock))]
		public ParticlePropertyScalarStructNewBlock ScaleZ9;
		[Field("rotation", typeof(ParticlePropertyScalarStructNewBlock))]
		[Block("Particle Property Scalar Struct New", 1, typeof(ParticlePropertyScalarStructNewBlock))]
		public ParticlePropertyScalarStructNewBlock Rotation11;
		[Field("collision effect#effect, material effect or sound spawned when this particle collides with something", null)]
		public TagReference CollisionEffect13;
		[Field("death effect#effect, material effect or sound spawned when this particle dies", null)]
		public TagReference DeathEffect14;
		[Field("locations", null)]
		[Block("Effect Locations Block", 32, typeof(EffectLocationsBlock))]
		public TagBlock Locations16;
		[Field("attached particle systems", null)]
		[Block("Particle System Definition Block New", 32, typeof(ParticleSystemDefinitionBlockNew))]
		public TagBlock AttachedParticleSystems17;
		[Field("models*", null)]
		[Block("Particle Models Block", 256, typeof(ParticleModelsBlock))]
		public TagBlock Models18;
		[Field("raw vertices*", null)]
		[Block("Particle Model Vertices Block", 32768, typeof(ParticleModelVerticesBlock))]
		public TagBlock RawVertices19;
		[Field("indices*", null)]
		[Block("Particle Model Indices Block", 32768, typeof(ParticleModelIndicesBlock))]
		public TagBlock Indices20;
		[Field("cached data", null)]
		[Block("Cached Data Block", 1, typeof(CachedDataBlock))]
		public TagBlock CachedData21;
		[Field("geometry section info", typeof(GlobalGeometryBlockInfoStructBlock))]
		[Block("Global Geometry Block Info Struct", 1, typeof(GlobalGeometryBlockInfoStructBlock))]
		public GlobalGeometryBlockInfoStructBlock GeometrySectionInfo22;
		[Field("", null)]
		public fixed byte _23[16];
		[Field("", null)]
		public fixed byte _24[8];
		[Field("", null)]
		public fixed byte _25[4];
	}
}
#pragma warning restore CS1591
