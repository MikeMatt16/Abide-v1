using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct TableViewListRowReferenceBlock
	{
		public enum Flags0Options
		{
			Unused_0 = 1,
		}
		[Field("flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("row height", null)]
		public short RowHeight1;
		[Field("", null)]
		public fixed byte _2[2];
		[Field("row cells", null)]
		[Block("Table View List Item Reference Block", 8, typeof(TableViewListItemReferenceBlock))]
		public TagBlock RowCells3;
	}
}
#pragma warning restore CS1591
