using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(32, 4)]
	public unsafe struct PlayerBlockReferenceBlock
	{
		public enum TableOrder3Options
		{
			RowMajor_0 = 0,
			ColumnMajor_1 = 1,
		}
		[Field("", null)]
		public fixed byte _0[4];
		[Field("skin", null)]
		public TagReference Skin1;
		[Field("bottom-left", null)]
		public Vector2 BottomLeft2;
		[Field("table order", typeof(TableOrder3Options))]
		public byte TableOrder3;
		[Field("maximum player count", null)]
		public int MaximumPlayerCount4;
		[Field("row count", null)]
		public int RowCount5;
		[Field("column count", null)]
		public int ColumnCount6;
		[Field("row height", null)]
		public short RowHeight7;
		[Field("column width", null)]
		public short ColumnWidth8;
	}
}
#pragma warning restore CS1591
