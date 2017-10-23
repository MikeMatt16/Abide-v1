using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(36, 4)]
	public unsafe struct TableViewListItemReferenceBlock
	{
		public enum TextFlags0Options
		{
			LeftJustifyText_0 = 1,
			RightJustifyText_1 = 2,
			PulsatingText_2 = 4,
			CalloutText_3 = 8,
			Small31CharBuffer_4 = 16,
		}
		[Field("text flags", typeof(TextFlags0Options))]
		public int TextFlags0;
		[Field("cell width", null)]
		public short CellWidth1;
		[Field("", null)]
		public fixed byte _2[2];
		[Field("bitmap top-left:if there is a bitmap", null)]
		public Vector2 BitmapTopLeft3;
		[Field("bitmap tag", null)]
		public TagReference BitmapTag4;
		[Field("string id", null)]
		public StringId StringId5;
		[Field("render depth bias", null)]
		public short RenderDepthBias6;
		[Field("", null)]
		public fixed byte _7[2];
	}
}
#pragma warning restore CS1591
