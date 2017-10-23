using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(68, 4)]
	public unsafe struct LightmapVertexBufferBucketBlock
	{
		public enum Flags0Options
		{
			IncidentDirection_0 = 1,
			Color_1 = 2,
		}
		[Field("flags", typeof(Flags0Options))]
		public short Flags0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("raw vertices", null)]
		[Block("Vertex", 32767, typeof(LightmapBucketRawVertexBlock))]
		public TagBlock RawVertices2;
		[Field("geometry block info", typeof(GlobalGeometryBlockInfoStructBlock))]
		[Block("Global Geometry Block Info Struct", 1, typeof(GlobalGeometryBlockInfoStructBlock))]
		public GlobalGeometryBlockInfoStructBlock GeometryBlockInfo3;
		[Field("cache data", null)]
		[Block("Lightmap Vertex Buffer Bucket Cache Data Block", 1, typeof(LightmapVertexBufferBucketCacheDataBlock))]
		public TagBlock CacheData4;
	}
}
#pragma warning restore CS1591
