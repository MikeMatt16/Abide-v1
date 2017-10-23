using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(32, 4)]
	public unsafe struct StructureBspDebugInfoRenderLineBlock
	{
		public enum Type0Options
		{
			FogPlaneBoundaryEdge_0 = 0,
			FogPlaneInternalEdge_1 = 1,
			FogZoneFloodfill_2 = 2,
			FogZoneClusterCentroid_3 = 3,
			FogZoneClusterGeometry_4 = 4,
			FogZonePortalCentroid_5 = 5,
			FogZonePortalGeometry_6 = 6,
		}
		[Field("Type*", typeof(Type0Options))]
		public short Type0;
		[Field("Code*", null)]
		public short Code1;
		[Field("Pad Thai*", null)]
		public short PadThai2;
		[Field("", null)]
		public fixed byte _3[2];
		public Vector3 Point04;
		public Vector3 Point15;
	}
}
#pragma warning restore CS1591
