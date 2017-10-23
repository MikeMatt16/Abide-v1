using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(40, 4)]
	public unsafe struct GlobalGeometryBlockInfoStructBlock
	{
		[Field("Block Offset*", null)]
		public int BlockOffset1;
		[Field("Block Size*", null)]
		public int BlockSize2;
		[Field("Section Data Size*", null)]
		public int SectionDataSize3;
		[Field("Resource Data Size*", null)]
		public int ResourceDataSize4;
		[Field("Resources*", null)]
		[Block("Block Resources", 1024, typeof(GlobalGeometryBlockResourceBlock))]
		public TagBlock Resources5;
		[Field("", null)]
		public fixed byte _6[4];
		[Field("Owner Tag Section Offset*", null)]
		public short OwnerTagSectionOffset7;
		[Field("", null)]
		public fixed byte _8[2];
		[Field("", null)]
		public fixed byte _9[4];
	}
}
#pragma warning restore CS1591
