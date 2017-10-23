using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(96, 4)]
	public unsafe struct GlobalCollisionBspBlock
	{
		[Field("BSP 3D Nodes*", null)]
		[Block("Bsp3d Nodes Block", 131072, typeof(Bsp3dNodesBlock))]
		public TagBlock BSP3DNodes0;
		[Field("Planes*", null)]
		[Block("Planes Block", 65536, typeof(PlanesBlock))]
		public TagBlock Planes1;
		[Field("Leaves*", null)]
		[Block("Leaves Block", 65536, typeof(LeavesBlock))]
		public TagBlock Leaves2;
		[Field("BSP 2D References*", null)]
		[Block("Bsp2d References Block", 131072, typeof(Bsp2dReferencesBlock))]
		public TagBlock BSP2DReferences3;
		[Field("BSP 2D Nodes*", null)]
		[Block("Bsp2d Nodes Block", 131072, typeof(Bsp2dNodesBlock))]
		public TagBlock BSP2DNodes4;
		[Field("Surfaces*", null)]
		[Block("Surfaces Block", 131072, typeof(SurfacesBlock))]
		public TagBlock Surfaces5;
		[Field("Edges*", null)]
		[Block("Edges Block", 262144, typeof(EdgesBlock))]
		public TagBlock Edges6;
		[Field("Vertices*", null)]
		[Block("Vertices Block", 131072, typeof(VerticesBlock))]
		public TagBlock Vertices7;
	}
}
#pragma warning restore CS1591
