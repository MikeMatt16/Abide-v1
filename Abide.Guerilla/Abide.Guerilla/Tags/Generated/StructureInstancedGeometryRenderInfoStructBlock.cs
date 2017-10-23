using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(108, 4)]
	public unsafe struct StructureInstancedGeometryRenderInfoStructBlock
	{
		[Field("Section Info*", typeof(GlobalGeometrySectionInfoStructBlock))]
		[Block("Global Geometry Section Info Struct", 1, typeof(GlobalGeometrySectionInfoStructBlock))]
		public GlobalGeometrySectionInfoStructBlock SectionInfo0;
		[Field("Geometry Block Info*", typeof(GlobalGeometryBlockInfoStructBlock))]
		[Block("Global Geometry Block Info Struct", 1, typeof(GlobalGeometryBlockInfoStructBlock))]
		public GlobalGeometryBlockInfoStructBlock GeometryBlockInfo1;
		[Field("Render Data*", null)]
		[Block("Structure Bsp Cluster Data Block New", 1, typeof(StructureBspClusterDataBlockNew))]
		public TagBlock RenderData2;
		[Field("Index Reorder Table*", null)]
		[Block("Index", 65535, typeof(GlobalGeometrySectionStripIndexBlock))]
		public TagBlock IndexReorderTable3;
	}
}
#pragma warning restore CS1591
