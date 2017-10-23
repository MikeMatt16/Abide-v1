using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct ParticleControllerParameters
	{
		[Field("parameter id", null)]
		public int ParameterId0;
		[Field("property", typeof(ParticlePropertyScalarStructNewBlock))]
		[Block("Particle Property Scalar Struct New", 1, typeof(ParticlePropertyScalarStructNewBlock))]
		public ParticlePropertyScalarStructNewBlock Property1;
	}
}
#pragma warning restore CS1591
