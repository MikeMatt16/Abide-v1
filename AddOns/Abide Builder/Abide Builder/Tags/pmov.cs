using Abide.Builder.Tags.TagDefinition;
namespace Abide.Builder.Tags
{
	[TagDefinition("pmov", 20)]
	internal sealed class _pmov
	{
		[Tag("Template", 0)]
		public sealed class _0_Template
		{
		}
		[TagIdentifier("Template", 4)]
		public sealed class _4_Template
		{
		}
		[TagBlock("Movements", 12, 20, 4, 4)]
		public sealed class _12_Movements
		{
			[TagBlock("Parameters", 4, 20, 9, 4)]
			public sealed class _4_Parameters
			{
				[TagBlock("Data", 12, 1, 1024, 4)]
				public sealed class _12_Data
				{
				}
			}
		}
	}
}
