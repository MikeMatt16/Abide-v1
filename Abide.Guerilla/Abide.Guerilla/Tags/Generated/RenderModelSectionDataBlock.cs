using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(180, 4)]
	public unsafe struct RenderModelSectionDataBlock
	{
		[Field("section*", typeof(GlobalGeometrySectionStructBlock))]
		[Block("Global Geometry Section Struct", 1, typeof(GlobalGeometrySectionStructBlock))]
		public GlobalGeometrySectionStructBlock Section0;
		[Field("point data*", typeof(GlobalGeometryPointDataStructBlock))]
		[Block("Global Geometry Point Data Struct", 1, typeof(GlobalGeometryPointDataStructBlock))]
		public GlobalGeometryPointDataStructBlock PointData1;
		[Field("node map*", null)]
		[Block("Index", 40, typeof(RenderModelNodeMapBlock))]
		public TagBlock NodeMap2;
		[Field("", null)]
		public fixed byte _3[4];
	}
}
#pragma warning restore CS1591
