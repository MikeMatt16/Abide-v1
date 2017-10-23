using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("particle_physics", "pmov", "����", typeof(ParticlePhysicsBlock))]
	[FieldSet(32, 4)]
	public unsafe struct ParticlePhysicsBlock
	{
		public enum Flags1Options
		{
			Physics_0 = 1,
			CollideWithStructure_1 = 2,
			CollideWithMedia_2 = 4,
			CollideWithScenery_3 = 8,
			CollideWithVehicles_4 = 16,
			CollideWithBipeds_5 = 32,
			Swarm_6 = 64,
			Wind_7 = 128,
		}
		[Field("template", null)]
		public TagReference Template0;
		[Field("flags", typeof(Flags1Options))]
		public int Flags1;
		[Field("movements", null)]
		[Block("Particle Controller", 4, typeof(ParticleController))]
		public TagBlock Movements2;
	}
}
#pragma warning restore CS1591
