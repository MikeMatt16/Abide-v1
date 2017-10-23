using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(4, 4)]
	public unsafe struct InstancedGeometryReferenceBlock
	{
		[Field("pathfinding object_index", null)]
		public short PathfindingObjectIndex0;
		[Field("", null)]
		public fixed byte _1[2];
	}
}
#pragma warning restore CS1591
