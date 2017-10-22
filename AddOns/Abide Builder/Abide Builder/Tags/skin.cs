using Abide.Builder.Tags.TagDefinition;
namespace Abide.Builder.Tags
{
	[TagDefinition("skin", 60)]
	internal sealed class _skin
	{
		[Tag("Arrows Bitmap", 4)]
		public sealed class _4_Arrows_Bitmap
		{
		}
		[TagIdentifier("Arrows Bitmap", 8)]
		public sealed class _8_Arrows_Bitmap
		{
		}
		[TagBlock("Item Animations", 20, 16, 7, 4)]
		public sealed class _20_Item_Animations
		{
			[TagBlock("Keyframes", 8, 20, 64, 4)]
			public sealed class _8_Keyframes
			{
			}
		}
		[TagBlock("Text Blocks", 28, 44, 64, 4)]
		public sealed class _28_Text_Blocks
		{
		}
		[TagBlock("Bitmap Blocks", 36, 56, 64, 4)]
		public sealed class _36_Bitmap_Blocks
		{
			[Tag("Bitmap Tag", 24)]
			public sealed class _24_Bitmap_Tag
			{
			}
			[TagIdentifier("Bitmap Tag", 28)]
			public sealed class _28_Bitmap_Tag
			{
			}
		}
		[TagBlock("Hud Blocks", 44, 36, 64, 4)]
		public sealed class _44_Hud_Blocks
		{
			[Tag("Bitmap", 12)]
			public sealed class _12_Bitmap
			{
			}
			[TagIdentifier("Bitmap", 16)]
			public sealed class _16_Bitmap
			{
			}
			[Tag("Shader", 20)]
			public sealed class _20_Shader
			{
			}
			[TagIdentifier("Shader", 24)]
			public sealed class _24_Shader
			{
			}
		}
		[TagBlock("Player Blocks", 52, 24, 64, 4)]
		public sealed class _52_Player_Blocks
		{
			[Tag("Skin", 4)]
			public sealed class _4_Skin
			{
			}
			[TagIdentifier("Skin", 8)]
			public sealed class _8_Skin
			{
			}
		}
	}
}
