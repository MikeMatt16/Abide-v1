using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct ParticleController
	{
		public enum Type0Options
		{
			Physics_0 = 0,
			Collider_1 = 1,
			Swarm_2 = 2,
			Wind_3 = 3,
		}
		[Field("type", typeof(Type0Options))]
		public short Type0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("parameters", null)]
		[Block("Particle Controller Parameters", 9, typeof(ParticleControllerParameters))]
		public TagBlock Parameters2;
		[Field("", null)]
		public fixed byte _3[8];
	}
}
#pragma warning restore CS1591
