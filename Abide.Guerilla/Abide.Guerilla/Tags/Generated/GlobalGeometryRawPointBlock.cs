using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(68, 4)]
	public unsafe struct GlobalGeometryRawPointBlock
	{
		public Vector3 Position0;
		[Field("Node Index (OLD)*", null)]
		public int NodeIndexOLD2;
		[Field("node_weight*", null)]
		public float NodeWeight5;
		[Field("Node Index (NEW)*", null)]
		public int NodeIndexNEW8;
		[Field("Use New Node Indices*", null)]
		public int UseNewNodeIndices10;
		[Field("Adjusted Compound Node Index*", null)]
		public int AdjustedCompoundNodeIndex11;
	}
}
#pragma warning restore CS1591
