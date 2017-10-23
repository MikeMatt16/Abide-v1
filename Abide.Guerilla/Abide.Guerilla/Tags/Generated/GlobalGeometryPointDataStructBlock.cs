using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(56, 4)]
	public unsafe struct GlobalGeometryPointDataStructBlock
	{
		[Field("Raw Points*", null)]
		[Block("Point", 32767, typeof(GlobalGeometryRawPointBlock))]
		public TagBlock RawPoints0;
		[Field("Runtime Point Data*", null)]
		[Data(1048544)]
		public TagBlock RuntimePointData1;
		[Field("Rigid Point Groups*", null)]
		[Block("Rigid Point Group", 32767, typeof(GlobalGeometryRigidPointGroupBlock))]
		public TagBlock RigidPointGroups2;
		[Field("Vertex-Point Indices*", null)]
		[Block("Index", 32767, typeof(GlobalGeometryPointDataIndexBlock))]
		public TagBlock VertexPointIndices3;
	}
}
#pragma warning restore CS1591
