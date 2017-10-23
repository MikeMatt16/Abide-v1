using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("particle", "prt3", "����", typeof(ParticleBlock))]
	[FieldSet(248, 4)]
	public unsafe struct ParticleBlock
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
		public enum ParticleBillboardStyle1Options
		{
			ScreenFacing_0 = 0,
			ParallelToDirection_1 = 1,
			PerpendicularToDirection_2 = 2,
			Vertical_3 = 3,
			Horizontal_4 = 4,
		}
		[Field("flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("particle billboard style", typeof(ParticleBillboardStyle1Options))]
		public short ParticleBillboardStyle1;
		[Field("", null)]
		public fixed byte _2[2];
		[Field("first sequence index", null)]
		public short FirstSequenceIndex3;
		[Field("sequence count", null)]
		public short SequenceCount4;
		[Field("shader template", null)]
		public TagReference ShaderTemplate8;
		[Field("shader parameters", null)]
		[Block("Parameter", 64, typeof(GlobalShaderParameterBlock))]
		public TagBlock ShaderParameters9;
		[Field("color", typeof(ParticlePropertyColorStructNewBlock))]
		[Block("Particle Property Color Struct New", 1, typeof(ParticlePropertyColorStructNewBlock))]
		public ParticlePropertyColorStructNewBlock Color12;
		[Field("alpha", typeof(ParticlePropertyScalarStructNewBlock))]
		[Block("Particle Property Scalar Struct New", 1, typeof(ParticlePropertyScalarStructNewBlock))]
		public ParticlePropertyScalarStructNewBlock Alpha14;
		[Field("scale", typeof(ParticlePropertyScalarStructNewBlock))]
		[Block("Particle Property Scalar Struct New", 1, typeof(ParticlePropertyScalarStructNewBlock))]
		public ParticlePropertyScalarStructNewBlock Scale16;
		[Field("rotation", typeof(ParticlePropertyScalarStructNewBlock))]
		[Block("Particle Property Scalar Struct New", 1, typeof(ParticlePropertyScalarStructNewBlock))]
		public ParticlePropertyScalarStructNewBlock Rotation18;
		[Field("frame index", typeof(ParticlePropertyScalarStructNewBlock))]
		[Block("Particle Property Scalar Struct New", 1, typeof(ParticlePropertyScalarStructNewBlock))]
		public ParticlePropertyScalarStructNewBlock FrameIndex20;
		[Field("collision effect#effect, material effect or sound spawned when this particle collides with something", null)]
		public TagReference CollisionEffect22;
		[Field("death effect#effect, material effect or sound spawned when this particle dies", null)]
		public TagReference DeathEffect23;
		[Field("locations", null)]
		[Block("Effect Locations Block", 32, typeof(EffectLocationsBlock))]
		public TagBlock Locations25;
		[Field("attached particle systems", null)]
		[Block("Particle System Definition Block New", 32, typeof(ParticleSystemDefinitionBlockNew))]
		public TagBlock AttachedParticleSystems26;
		[Field("", null)]
		[Block("Shader Postprocess Definition New Block", 1, typeof(ShaderPostprocessDefinitionNewBlock))]
		public TagBlock _27;
		[Field("", null)]
		public fixed byte _28[8];
		[Field("", null)]
		public fixed byte _29[16];
		[Field("", null)]
		public fixed byte _30[16];
	}
}
#pragma warning restore CS1591
