using Abide.Builder.Tags.TagDefinition;
namespace Abide.Builder.Tags
{
	[TagDefinition("DECR", 112)]
	internal sealed class _DECR
	{
		[TagBlock("Shaders", 0, 8, 8, 4)]
		public sealed class _0_Shaders
		{
			[Tag("Shader", 0)]
			public sealed class _0_Shader
			{
			}
			[TagIdentifier("Shader", 4)]
			public sealed class _4_Shader
			{
			}
		}
		[TagBlock("Classes", 16, 20, 8, 4)]
		public sealed class _16_Classes
		{
			[TagBlock("Permutations", 12, 38, 64, 4)]
			public sealed class _12_Permutations
			{
			}
		}
		[TagBlock("Models*", 24, 8, 256, 4)]
		public sealed class _24_Models_
		{
		}
		[TagBlock("Raw Vertices*", 32, 56, 32768, 4)]
		public sealed class _32_Raw_Vertices_
		{
		}
		[TagBlock("Indices*", 40, 2, 32768, 4)]
		public sealed class _40_Indices_
		{
		}
		[TagBlock("Cached Data", 48, 32, 1, 4)]
		public sealed class _48_Cached_Data
		{
		}
		[TagBlock("Resources*", 72, 16, 1024, 4)]
		public sealed class _72_Resources_
		{
		}
	}
}
