using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct NodeRenderLeavesBlock
	{
		[Field("collision leaves*", null)]
		[Block("Bsp Leaf Block", 65536, typeof(BspLeafBlock))]
		public TagBlock CollisionLeaves0;
		[Field("surface references*", null)]
		[Block("Bsp Surface Reference Block", 262144, typeof(BspSurfaceReferenceBlock))]
		public TagBlock SurfaceReferences1;
	}
}
#pragma warning restore CS1591
