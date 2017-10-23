using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(28, 4)]
	public unsafe struct StructureBspSoundClusterBlock
	{
		[Field("", null)]
		public fixed byte _0[2];
		[Field("", null)]
		public fixed byte _1[2];
		[Field("enclosing portal designators", null)]
		[Block("Structure Sound Cluster Portal Designators", 512, typeof(StructureSoundClusterPortalDesignators))]
		public TagBlock EnclosingPortalDesignators2;
		[Field("interior cluster indices", null)]
		[Block("Structure Sound Cluster Interior Cluster Indices", 512, typeof(StructureSoundClusterInteriorClusterIndices))]
		public TagBlock InteriorClusterIndices3;
	}
}
#pragma warning restore CS1591
