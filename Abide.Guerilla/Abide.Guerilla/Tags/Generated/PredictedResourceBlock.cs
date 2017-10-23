using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct PredictedResourceBlock
	{
		public enum Type0Options
		{
			Bitmap_0 = 0,
			Sound_1 = 1,
			RenderModelGeometry_2 = 2,
			ClusterGeometry_3 = 3,
			ClusterInstancedGeometry_4 = 4,
			LightmapGeometryObjectBuckets_5 = 5,
			LightmapGeometryInstanceBuckets_6 = 6,
			LightmapClusterBitmaps_7 = 7,
			LightmapInstanceBitmaps_8 = 8,
		}
		[Field("type", typeof(Type0Options))]
		public short Type0;
		[Field("resource index", null)]
		public short ResourceIndex1;
		[Field("tag index", null)]
		public int TagIndex2;
	}
}
#pragma warning restore CS1591
