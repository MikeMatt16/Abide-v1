using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct PermutationsBlock
	{
		[Field("name^*", null)]
		public StringId Name0;
		[Field("rigid bodies", null)]
		[Block("Rigid Body Indices Block", 64, typeof(RigidBodyIndicesBlock))]
		public TagBlock RigidBodies1;
	}
}
#pragma warning restore CS1591
