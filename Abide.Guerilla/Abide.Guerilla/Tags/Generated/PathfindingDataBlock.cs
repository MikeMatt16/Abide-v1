using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(156, 4)]
	public unsafe struct PathfindingDataBlock
	{
		[Field("sectors", null)]
		[Block("Sector Block", 65534, typeof(SectorBlock))]
		public TagBlock Sectors0;
		[Field("links", null)]
		[Block("Sector Link Block", 262144, typeof(SectorLinkBlock))]
		public TagBlock Links1;
		[Field("refs", null)]
		[Block("Ref Block", 131072, typeof(RefBlock))]
		public TagBlock Refs2;
		[Field("bsp2d nodes", null)]
		[Block("Sector Bsp2d Nodes Block", 131072, typeof(SectorBsp2dNodesBlock))]
		public TagBlock Bsp2dNodes3;
		[Field("surface flags", null)]
		[Block("Surface Flags Block", 4096, typeof(SurfaceFlagsBlock))]
		public TagBlock SurfaceFlags4;
		[Field("vertices", null)]
		[Block("Sector Vertex Block", 65535, typeof(SectorVertexBlock))]
		public TagBlock Vertices5;
		[Field("object refs", null)]
		[Block("Environment Object Refs", 2000, typeof(EnvironmentObjectRefs))]
		public TagBlock ObjectRefs6;
		[Field("pathfinding hints", null)]
		[Block("Pathfinding Hints Block", 32767, typeof(PathfindingHintsBlock))]
		public TagBlock PathfindingHints7;
		[Field("instanced geometry refs", null)]
		[Block("Instanced Geometry Reference Block", 1024, typeof(InstancedGeometryReferenceBlock))]
		public TagBlock InstancedGeometryRefs8;
		[Field("structure checksum*", null)]
		public int StructureChecksum9;
		[Field("", null)]
		public fixed byte _10[32];
		[Field("user-placed hints", null)]
		[Block("User Hint Block", 1, typeof(UserHintBlock))]
		public TagBlock UserPlacedHints11;
	}
}
#pragma warning restore CS1591
