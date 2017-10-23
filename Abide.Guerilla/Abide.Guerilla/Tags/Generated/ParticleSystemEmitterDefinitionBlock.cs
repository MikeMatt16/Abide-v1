using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(228, 4)]
	public unsafe struct ParticleSystemEmitterDefinitionBlock
	{
		public enum EmissionShape16Options
		{
			Sprayer_0 = 0,
			Disc_1 = 1,
			Globe_2 = 2,
			Implode_3 = 3,
			Tube_4 = 4,
			Halo_5 = 5,
			ImpactContour_6 = 6,
			ImpactArea_7 = 7,
			Debris_8 = 8,
			Line_9 = 9,
		}
		[Field("particle physics", null)]
		public TagReference ParticlePhysics0;
		[Field("particle emission rate", typeof(ParticlePropertyScalarStructNewBlock))]
		[Block("Particle Property Scalar Struct New", 1, typeof(ParticlePropertyScalarStructNewBlock))]
		public ParticlePropertyScalarStructNewBlock ParticleEmissionRate2;
		[Field("particle lifespan", typeof(ParticlePropertyScalarStructNewBlock))]
		[Block("Particle Property Scalar Struct New", 1, typeof(ParticlePropertyScalarStructNewBlock))]
		public ParticlePropertyScalarStructNewBlock ParticleLifespan4;
		[Field("particle velocity", typeof(ParticlePropertyScalarStructNewBlock))]
		[Block("Particle Property Scalar Struct New", 1, typeof(ParticlePropertyScalarStructNewBlock))]
		public ParticlePropertyScalarStructNewBlock ParticleVelocity6;
		[Field("particle angular velocity", typeof(ParticlePropertyScalarStructNewBlock))]
		[Block("Particle Property Scalar Struct New", 1, typeof(ParticlePropertyScalarStructNewBlock))]
		public ParticlePropertyScalarStructNewBlock ParticleAngularVelocity8;
		[Field("particle size", typeof(ParticlePropertyScalarStructNewBlock))]
		[Block("Particle Property Scalar Struct New", 1, typeof(ParticlePropertyScalarStructNewBlock))]
		public ParticlePropertyScalarStructNewBlock ParticleSize10;
		[Field("particle tint", typeof(ParticlePropertyColorStructNewBlock))]
		[Block("Particle Property Color Struct New", 1, typeof(ParticlePropertyColorStructNewBlock))]
		public ParticlePropertyColorStructNewBlock ParticleTint12;
		[Field("particle alpha", typeof(ParticlePropertyScalarStructNewBlock))]
		[Block("Particle Property Scalar Struct New", 1, typeof(ParticlePropertyScalarStructNewBlock))]
		public ParticlePropertyScalarStructNewBlock ParticleAlpha14;
		[Field("emission shape", typeof(EmissionShape16Options))]
		public int EmissionShape16;
		[Field("emission radius", typeof(ParticlePropertyScalarStructNewBlock))]
		[Block("Particle Property Scalar Struct New", 1, typeof(ParticlePropertyScalarStructNewBlock))]
		public ParticlePropertyScalarStructNewBlock EmissionRadius18;
		[Field("emission angle", typeof(ParticlePropertyScalarStructNewBlock))]
		[Block("Particle Property Scalar Struct New", 1, typeof(ParticlePropertyScalarStructNewBlock))]
		public ParticlePropertyScalarStructNewBlock EmissionAngle20;
		public Vector3 TranslationalOffset21;
		[Field("relative direction#particle initial velocity direction relative to the location's forward", null)]
		public Vector2 RelativeDirection22;
		[Field("", null)]
		public fixed byte _23[8];
	}
}
#pragma warning restore CS1591
