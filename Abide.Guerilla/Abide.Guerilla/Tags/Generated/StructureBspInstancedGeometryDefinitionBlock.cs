using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(260, 4)]
	public unsafe struct StructureBspInstancedGeometryDefinitionBlock
	{
		[Field("Render Info", typeof(StructureInstancedGeometryRenderInfoStructBlock))]
		[Block("Structure Instanced Geometry Render Info Struct", 1, typeof(StructureInstancedGeometryRenderInfoStructBlock))]
		public StructureInstancedGeometryRenderInfoStructBlock RenderInfo0;
		[Field("Checksum*", null)]
		public int Checksum1;
		public Vector3 BoundingSphereCenter2;
		[Field("Bounding Sphere Radius", null)]
		public float BoundingSphereRadius3;
		[Field("Collision Info", typeof(GlobalCollisionBspStructBlock))]
		[Block("Global Collision Bsp Struct", 1, typeof(GlobalCollisionBspStructBlock))]
		public GlobalCollisionBspStructBlock CollisionInfo4;
		[Field("bsp_physics*", null)]
		[Block("Collision Bsp Physics Block", 1024, typeof(CollisionBspPhysicsBlock))]
		public TagBlock BspPhysics5;
		[Field("Render Leaves*", null)]
		[Block("Structure Bsp Leaf Block", 65536, typeof(StructureBspLeafBlock))]
		public TagBlock RenderLeaves6;
		[Field("Surface References*", null)]
		[Block("Structure Bsp Surface Reference Block", 262144, typeof(StructureBspSurfaceReferenceBlock))]
		public TagBlock SurfaceReferences7;
	}
}
#pragma warning restore CS1591
