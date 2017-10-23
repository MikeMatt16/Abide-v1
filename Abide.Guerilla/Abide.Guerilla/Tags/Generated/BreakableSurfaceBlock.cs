using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("breakable_surface", "bsdt", "����", typeof(BreakableSurfaceBlock))]
	[FieldSet(52, 4)]
	public unsafe struct BreakableSurfaceBlock
	{
		[Field("maximum vitality", null)]
		public float MaximumVitality0;
		[Field("effect", null)]
		public TagReference Effect1;
		[Field("sound", null)]
		public TagReference Sound2;
		[Field("particle effects", null)]
		[Block("Particle System Definition Block New", 32, typeof(ParticleSystemDefinitionBlockNew))]
		public TagBlock ParticleEffects3;
		[Field("particle density", null)]
		public float ParticleDensity4;
	}
}
#pragma warning restore CS1591
