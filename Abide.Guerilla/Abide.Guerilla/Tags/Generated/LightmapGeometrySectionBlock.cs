using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(96, 4)]
	public unsafe struct LightmapGeometrySectionBlock
	{
		[Field("geometry info", typeof(GlobalGeometrySectionInfoStructBlock))]
		[Block("Global Geometry Section Info Struct", 1, typeof(GlobalGeometrySectionInfoStructBlock))]
		public GlobalGeometrySectionInfoStructBlock GeometryInfo0;
		[Field("geometry block info", typeof(GlobalGeometryBlockInfoStructBlock))]
		[Block("Global Geometry Block Info Struct", 1, typeof(GlobalGeometryBlockInfoStructBlock))]
		public GlobalGeometryBlockInfoStructBlock GeometryBlockInfo1;
		[Field("cache data", null)]
		[Block("Lightmap Geometry Section Cache Data Block", 1, typeof(LightmapGeometrySectionCacheDataBlock))]
		public TagBlock CacheData2;
	}
}
#pragma warning restore CS1591
