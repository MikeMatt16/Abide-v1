using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(52, 4)]
	public unsafe struct StructureBspDetailObjectDataBlock
	{
		[Field("Cells", null)]
		[Block("Global Detail Object Cells Block", 262144, typeof(GlobalDetailObjectCellsBlock))]
		public TagBlock Cells0;
		[Field("Instances", null)]
		[Block("Global Detail Object Block", 2097152, typeof(GlobalDetailObjectBlock))]
		public TagBlock Instances1;
		[Field("Counts", null)]
		[Block("Global Detail Object Counts Block", 8388608, typeof(GlobalDetailObjectCountsBlock))]
		public TagBlock Counts2;
		[Field("z Reference Vectors", null)]
		[Block("Global Z Reference Vector Block", 262144, typeof(GlobalZReferenceVectorBlock))]
		public TagBlock ZReferenceVectors3;
		[Field("", null)]
		public fixed byte _4[1];
		[Field("", null)]
		public fixed byte _5[3];
	}
}
#pragma warning restore CS1591
