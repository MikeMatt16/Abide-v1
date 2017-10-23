using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("collision_model", "coll", "����", typeof(CollisionModelBlock))]
	[FieldSet(76, 4)]
	public unsafe struct CollisionModelBlock
	{
		public enum Flags2Options
		{
			ContainsOpenEdges_0 = 1,
		}
		[Field("import info*", null)]
		[Block("Import Info", 1, typeof(GlobalTagImportInfoBlock))]
		public TagBlock ImportInfo0;
		[Field("errors*", null)]
		[Block("Error Report Category", 64, typeof(GlobalErrorReportCategoriesBlock))]
		public TagBlock Errors1;
		[Field("flags*", typeof(Flags2Options))]
		public int Flags2;
		[Field("", null)]
		public fixed byte _3[124];
		[Field("materials*", null)]
		[Block("Material", 32, typeof(CollisionModelMaterialBlock))]
		public TagBlock Materials4;
		[Field("regions*", null)]
		[Block("Region", 16, typeof(CollisionModelRegionBlock))]
		public TagBlock Regions5;
		[Field("pathfinding spheres*", null)]
		[Block("Sphere", 32, typeof(CollisionModelPathfindingSphereBlock))]
		public TagBlock PathfindingSpheres6;
		[Field("nodes*", null)]
		[Block("Collision Model Node Block", 255, typeof(CollisionModelNodeBlock))]
		public TagBlock Nodes7;
	}
}
#pragma warning restore CS1591
