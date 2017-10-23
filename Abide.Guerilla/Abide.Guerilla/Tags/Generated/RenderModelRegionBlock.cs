using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct RenderModelRegionBlock
	{
		[Field("node map offset (OLD)*", null)]
		public short NodeMapOffsetOLD1;
		[Field("node map size (OLD)*", null)]
		public short NodeMapSizeOLD2;
		[Field("permutations*", null)]
		[Block("Permutation", 32, typeof(RenderModelPermutationBlock))]
		public TagBlock Permutations3;
	}
}
#pragma warning restore CS1591
