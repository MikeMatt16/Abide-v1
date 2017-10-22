using Abide.Builder.Tags.TagDefinition;
namespace Abide.Builder.Tags
{
	[TagDefinition("slit", 20)]
	internal sealed class _slit
	{
		[TagBlock("Categories", 0, 12, 16, 4)]
		public sealed class _0_Categories
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
		[TagBlock("Shader Lods", 8, 12, 8, 4)]
		public sealed class _8_Shader_Lods
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
	}
}
