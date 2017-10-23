using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct StructureBspBreakableSurfaceBlock
	{
		[Field("Instanced Geometry Instance*", null)]
		public short InstancedGeometryInstance0;
		[Field("Breakable Surface Index*", null)]
		public short BreakableSurfaceIndex1;
		public Vector3 Centroid2;
		[Field("Radius*", null)]
		public float Radius3;
		[Field("Collision Surface Index*", null)]
		public int CollisionSurfaceIndex4;
	}
}
#pragma warning restore CS1591
