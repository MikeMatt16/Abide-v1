using Abide.Builder.Tags.TagDefinition;
namespace Abide.Builder.Tags
{
	[TagDefinition("MGS2", 16)]
	internal sealed class _MGS2
	{
		[TagBlock("Volumes", 8, 152, 16, 4)]
		public sealed class _8_Volumes
		{
			[Tag("Bitmap", 4)]
			public sealed class _4_Bitmap
			{
			}
			[TagIdentifier("Bitmap", 8)]
			public sealed class _8_Bitmap
			{
			}
			[TagBlock("Data", 16, 1, 1024, 4)]
			public sealed class _16_Data
			{
			}
			[TagBlock("Data", 24, 1, 1024, 4)]
			public sealed class _24_Data
			{
			}
			[TagBlock("Data", 32, 1, 1024, 4)]
			public sealed class _32_Data
			{
			}
			[TagBlock("Data", 40, 1, 1024, 4)]
			public sealed class _40_Data
			{
			}
			[TagBlock("Data", 48, 1, 1024, 4)]
			public sealed class _48_Data
			{
			}
			[TagBlock("Aspect", 56, 28, 1, 4)]
			public sealed class _56_Aspect
			{
				[TagBlock("Data", 0, 1, 1024, 4)]
				public sealed class _0_Data
				{
				}
				[TagBlock("Data", 8, 1, 1024, 4)]
				public sealed class _8_Data
				{
				}
			}
			[TagBlock("*", 96, 8, 256, 4)]
			public sealed class _96__
			{
			}
		}
	}
}
