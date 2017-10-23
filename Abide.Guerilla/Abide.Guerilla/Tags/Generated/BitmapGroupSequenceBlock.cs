using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(64, 4)]
	public unsafe struct BitmapGroupSequenceBlock
	{
		[Field("Name^", null)]
		public String Name0;
		[Field("First Bitmap Index*", null)]
		public short FirstBitmapIndex1;
		[Field("Bitmap Count*", null)]
		public short BitmapCount2;
		[Field("", null)]
		public fixed byte _3[16];
		[Field("Sprites*", null)]
		[Block("Bitmap Group Sprite Block", 64, typeof(BitmapGroupSpriteBlock))]
		public TagBlock Sprites4;
	}
}
#pragma warning restore CS1591
