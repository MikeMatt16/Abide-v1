using Abide.Builder.Tags.TagDefinition;
namespace Abide.Builder.Tags
{
	[TagDefinition("DECP", 48)]
	internal sealed class _DECP
	{
		[TagBlock("Cache Blocks", 16, 44, 4096, 4)]
		public sealed class _16_Cache_Blocks
		{
			[TagBlock("Resources*", 16, 16, 1024, 4)]
			public sealed class _16_Resources_
			{
			}
			[TagBlock("Cache Block Data*", 36, 136, 1, 4)]
			public sealed class _36_Cache_Block_Data_
			{
				[TagBlock("Placements*", 0, 22, 32768, 4)]
				public sealed class _0_Placements_
				{
				}
				[TagBlock("Decal Vertices*", 8, 31, 65536, 4)]
				public sealed class _8_Decal_Vertices_
				{
				}
				[TagBlock("Decal Indices*", 16, 2, 65536, 4)]
				public sealed class _16_Decal_Indices_
				{
				}
				[TagBlock("Sprite Vertices*", 72, 47, 65536, 4)]
				public sealed class _72_Sprite_Vertices_
				{
				}
				[TagBlock("Sprite Indices*", 80, 2, 65536, 4)]
				public sealed class _80_Sprite_Indices_
				{
				}
			}
		}
		[TagBlock("Groups", 24, 24, 131072, 4)]
		public sealed class _24_Groups
		{
		}
		[TagBlock("Cells", 32, 24, 65535, 4)]
		public sealed class _32_Cells
		{
		}
		[TagBlock("Decals", 40, 64, 32768, 4)]
		public sealed class _40_Decals
		{
		}
	}
}
