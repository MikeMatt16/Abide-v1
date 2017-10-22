using Abide.Builder.Tags.TagDefinition;
namespace Abide.Builder.Tags
{
	[TagDefinition("sfx+", 8)]
	internal sealed class _sfx_
	{
		[TagBlock("Sound Effects", 0, 56, 128, 4)]
		public sealed class _0_Sound_Effects
		{
			[TagBlock("Platform Sound Override Mixbins Block", 4, 8, 8, 4)]
			public sealed class _4_Platform_Sound_Override_Mixbins_Block
			{
			}
			[TagBlock("Filter", 24, 72, 1, 4)]
			public sealed class _24_Filter
			{
			}
			[TagBlock("Pitch Lfo", 32, 48, 1, 4)]
			public sealed class _32_Pitch_Lfo
			{
			}
			[TagBlock("Filter Lfo", 40, 64, 1, 4)]
			public sealed class _40_Filter_Lfo
			{
			}
			[TagBlock("Sound Effect", 48, 40, 1, 4)]
			public sealed class _48_Sound_Effect
			{
				[Tag("", 0)]
				public sealed class _0_
				{
				}
				[TagIdentifier("", 4)]
				public sealed class _4_
				{
				}
				[TagBlock("Components", 8, 16, 16, 4)]
				public sealed class _8_Components
				{
					[Tag("Sound^", 0)]
					public sealed class _0_Sound_
					{
					}
					[TagIdentifier("Sound^", 4)]
					public sealed class _4_Sound_
					{
					}
				}
				[TagBlock("Sound Effect Overrides Block", 16, 12, 128, 4)]
				public sealed class _16_Sound_Effect_Overrides_Block
				{
					[TagBlock("Overrides", 4, 32, 128, 4)]
					public sealed class _4_Overrides
					{
						[TagBlock("Data", 24, 1, 1024, 4)]
						public sealed class _24_Data
						{
						}
					}
				}
				[TagBlock("", 24, 1, 1024, 4)]
				public sealed class _24_
				{
				}
				[TagBlock("Platform Sound Effect Collection Block", 32, 20, 1, 4)]
				public sealed class _32_Platform_Sound_Effect_Collection_Block
				{
					[TagBlock("Sound Effects*", 0, 28, 8, 4)]
					public sealed class _0_Sound_Effects_
					{
						[TagBlock("Function Inputs", 0, 16, 16, 4)]
						public sealed class _0_Function_Inputs
						{
							[TagBlock("Data", 4, 1, 1024, 4)]
							public sealed class _4_Data
							{
							}
						}
						[TagBlock("Constant Inputs", 8, 4, 16, 4)]
						public sealed class _8_Constant_Inputs
						{
						}
						[TagBlock("Template Override Descriptors", 16, 1, 16, 4)]
						public sealed class _16_Template_Override_Descriptors
						{
						}
					}
					[TagBlock("Low Frequency Input*", 8, 16, 16, 4)]
					public sealed class _8_Low_Frequency_Input_
					{
						[TagBlock("Data", 4, 1, 1024, 4)]
						public sealed class _4_Data
						{
						}
					}
				}
			}
		}
	}
}
