using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(2, 4)]
	public unsafe struct StructureBspSkyOwnerClusterBlock
	{
		[Field("Cluster Owner*", null)]
		public short ClusterOwner0;
	}
}
#pragma warning restore CS1591
