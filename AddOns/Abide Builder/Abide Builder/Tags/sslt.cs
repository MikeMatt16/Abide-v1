using Abide.Builder.Tags.TagDefinition;
namespace Abide.Builder.Tags
{
	[TagDefinition("sslt", 8)]
	internal sealed class _sslt
	{
		[TagBlock("Structure Lighting", 0, 16, 16, 4)]
		public sealed class _0_Structure_Lighting
		{
			[Tag("BSP*", 0)]
			public sealed class _0_BSP_
			{
			}
			[TagIdentifier("BSP*", 4)]
			public sealed class _4_BSP_
			{
			}
			[TagBlock("Lighting Points", 8, 12, 32768, 4)]
			public sealed class _8_Lighting_Points
			{
			}
		}
	}
}
