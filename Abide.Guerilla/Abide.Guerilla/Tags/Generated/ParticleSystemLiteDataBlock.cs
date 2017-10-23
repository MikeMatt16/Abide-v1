using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(56, 4)]
	public unsafe struct ParticleSystemLiteDataBlock
	{
		[Field("particles render data*", null)]
		[Block("Particles Render Data Block", 4096, typeof(ParticlesRenderDataBlock))]
		public TagBlock ParticlesRenderData0;
		[Field("particles other data*", null)]
		[Block("Particles Update Data Block", 4096, typeof(ParticlesUpdateDataBlock))]
		public TagBlock ParticlesOtherData1;
		[Field("", null)]
		public fixed byte _2[32];
	}
}
#pragma warning restore CS1591
