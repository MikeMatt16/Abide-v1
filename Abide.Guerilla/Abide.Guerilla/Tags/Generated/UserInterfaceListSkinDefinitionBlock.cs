using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("user_interface_list_skin_definition", "skin", "����", typeof(UserInterfaceListSkinDefinitionBlock))]
	[FieldSet(88, 4)]
	public unsafe struct UserInterfaceListSkinDefinitionBlock
	{
		public enum ListFlags0Options
		{
			Unused_0 = 1,
		}
		[Field("list flags", typeof(ListFlags0Options))]
		public int ListFlags0;
		[Field("", null)]
		public fixed byte _1[8];
		[Field("arrows bitmap", null)]
		public TagReference ArrowsBitmap2;
		[Field("up-arrows offset:from bot-left of 1st item", null)]
		public Vector2 UpArrowsOffset3;
		[Field("down-arrows offset:from bot-left of 1st item", null)]
		public Vector2 DownArrowsOffset4;
		[Field("", null)]
		public fixed byte _5[32];
		[Field("item animations", null)]
		[Block("Single Animation Reference Block", 7, typeof(SingleAnimationReferenceBlock))]
		public TagBlock ItemAnimations7;
		[Field("text blocks", null)]
		[Block("Text Block Reference Block", 64, typeof(TextBlockReferenceBlock))]
		public TagBlock TextBlocks8;
		[Field("bitmap blocks", null)]
		[Block("Bitmap Block Reference Block", 64, typeof(BitmapBlockReferenceBlock))]
		public TagBlock BitmapBlocks10;
		[Field("hud blocks", null)]
		[Block("Hud Block Reference Block", 64, typeof(HudBlockReferenceBlock))]
		public TagBlock HudBlocks11;
		[Field("player blocks", null)]
		[Block("Player Block Reference Block", 64, typeof(PlayerBlockReferenceBlock))]
		public TagBlock PlayerBlocks12;
	}
}
#pragma warning restore CS1591
