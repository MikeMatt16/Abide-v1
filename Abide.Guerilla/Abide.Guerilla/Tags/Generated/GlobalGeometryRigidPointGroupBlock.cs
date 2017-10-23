using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(4, 4)]
	public unsafe struct GlobalGeometryRigidPointGroupBlock
	{
		[Field("Rigid Node Index*", null)]
		public int RigidNodeIndex0;
		[Field("Nodes/Point*", null)]
		public int NodesPoint1;
		[Field("Point Count*", null)]
		public short PointCount2;
	}
}
#pragma warning restore CS1591
