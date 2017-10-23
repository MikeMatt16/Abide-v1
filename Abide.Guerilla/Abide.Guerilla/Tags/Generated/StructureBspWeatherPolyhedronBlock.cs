using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(28, 4)]
	public unsafe struct StructureBspWeatherPolyhedronBlock
	{
		public Vector3 BoundingSphereCenter0;
		[Field("Bounding Sphere Radius*", null)]
		public float BoundingSphereRadius1;
		[Field("Planes*", null)]
		[Block("Structure Bsp Weather Polyhedron Plane Block", 16, typeof(StructureBspWeatherPolyhedronPlaneBlock))]
		public TagBlock Planes2;
	}
}
#pragma warning restore CS1591
