using Abide.Builder.Tags.TagDefinition;
namespace Abide.Builder.Tags
{
	[TagDefinition("stem", 96)]
	internal sealed class _stem
	{
		[TagBlock("Documentation", 0, 1, 65535, 4)]
		public sealed class _0_Documentation
		{
		}
		[TagBlock("Properties", 16, 8, 14, 4)]
		public sealed class _16_Properties
		{
		}
		[TagBlock("Categories", 24, 12, 16, 4)]
		public sealed class _24_Categories
		{
			[TagBlock("Parameters", 4, 52, 64, 4)]
			public sealed class _4_Parameters
			{
				[TagBlock("Explanation", 4, 1, 65535, 4)]
				public sealed class _4_Explanation
				{
				}
				[Tag("Default Bitmap", 16)]
				public sealed class _16_Default_Bitmap
				{
				}
				[TagIdentifier("Default Bitmap", 20)]
				public sealed class _20_Default_Bitmap
				{
				}
			}
		}
		[Tag("Light Response", 32)]
		public sealed class _32_Light_Response
		{
		}
		[TagIdentifier("Light Response", 36)]
		public sealed class _36_Light_Response
		{
		}
		[TagBlock("Lods", 40, 12, 8, 4)]
		public sealed class _40_Lods
		{
			[TagBlock("Pass", 4, 24, 16, 4)]
			public sealed class _4_Pass
			{
				[Tag("Pass^", 4)]
				public sealed class _4_Pass_
				{
				}
				[TagIdentifier("Pass^", 8)]
				public sealed class _8_Pass_
				{
				}
			}
		}
		[TagBlock("EMPTY STRING", 48, 4, 65535, 4)]
		public sealed class _48_EMPTY_STRING
		{
		}
		[TagBlock("EMPTY STRING", 56, 4, 65535, 4)]
		public sealed class _56_EMPTY_STRING
		{
		}
		[Tag("Aux 1 Shader", 64)]
		public sealed class _64_Aux_1_Shader
		{
		}
		[TagIdentifier("Aux 1 Shader", 68)]
		public sealed class _68_Aux_1_Shader
		{
		}
		[Tag("Aux 2 Shader", 76)]
		public sealed class _76_Aux_2_Shader
		{
		}
		[TagIdentifier("Aux 2 Shader", 80)]
		public sealed class _80_Aux_2_Shader
		{
		}
		[TagBlock("Postprocess Definition*", 88, 40, 1, 4)]
		public sealed class _88_Postprocess_Definition_
		{
			[TagBlock("Levels Of Detail", 0, 10, 1024, 4)]
			public sealed class _0_Levels_Of_Detail
			{
			}
			[TagBlock("Layers", 8, 2, 1024, 4)]
			public sealed class _8_Layers
			{
			}
			[TagBlock("Passes", 16, 10, 1024, 4)]
			public sealed class _16_Passes
			{
				[Tag("Pass", 0)]
				public sealed class _0_Pass
				{
				}
				[TagIdentifier("Pass", 4)]
				public sealed class _4_Pass
				{
				}
			}
			[TagBlock("Implementations", 24, 6, 1024, 4)]
			public sealed class _24_Implementations
			{
			}
			[TagBlock("Remappings", 32, 4, 1024, 4)]
			public sealed class _32_Remappings
			{
			}
		}
	}
}
