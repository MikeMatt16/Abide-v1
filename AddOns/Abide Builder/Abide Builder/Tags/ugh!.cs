using Abide.Builder.Tags.TagDefinition;
namespace Abide.Builder.Tags
{
	[TagDefinition("ugh!", 88)]
	internal sealed class _ugh_
	{
		[TagBlock("Playbacks", 0, 56, 32767, 4)]
		public sealed class _0_Playbacks
		{
		}
		[TagBlock("Scales", 8, 20, 32767, 4)]
		public sealed class _8_Scales
		{
		}
		[TagBlock("Import Names", 16, 4, 32767, 4)]
		public sealed class _16_Import_Names
		{
		}
		[TagBlock("Pitch Range Parameters", 24, 10, 32767, 4)]
		public sealed class _24_Pitch_Range_Parameters
		{
		}
		[TagBlock("Pitch Ranges", 32, 12, 32767, 4)]
		public sealed class _32_Pitch_Ranges
		{
		}
		[TagBlock("Permutations", 40, 16, 32767, 4)]
		public sealed class _40_Permutations
		{
		}
		[TagBlock("Custom Playbacks", 48, 52, 32767, 4)]
		public sealed class _48_Custom_Playbacks
		{
			[TagBlock("Platform Sound Override Mixbins Block", 0, 8, 8, 4)]
			public sealed class _0_Platform_Sound_Override_Mixbins_Block
			{
			}
			[TagBlock("Filter", 20, 72, 1, 4)]
			public sealed class _20_Filter
			{
			}
			[TagBlock("Pitch Lfo", 28, 48, 1, 4)]
			public sealed class _28_Pitch_Lfo
			{
			}
			[TagBlock("Filter Lfo", 36, 64, 1, 4)]
			public sealed class _36_Filter_Lfo
			{
			}
			[TagBlock("Sound Effect", 44, 40, 1, 4)]
			public sealed class _44_Sound_Effect
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
		[TagBlock("Runtime Permutation Flags", 56, 1, 32767, 4)]
		public sealed class _56_Runtime_Permutation_Flags
		{
		}
		[TagBlock("Chunks", 64, 12, 32767, 4)]
		public sealed class _64_Chunks
		{
		}
		[TagBlock("Promotions", 72, 20, 32767, 4)]
		public sealed class _72_Promotions
		{
			[Tag("Promotion Sound", 0)]
			public sealed class _0_Promotion_Sound
			{
			}
			[TagIdentifier("Promotion Sound", 4)]
			public sealed class _4_Promotion_Sound
			{
			}
		}
		[TagBlock("Extra Infos", 80, 44, 32767, 4)]
		public sealed class _80_Extra_Infos
		{
			[TagBlock("Encoded Permutation Section", 0, 16, 1, 4)]
			public sealed class _0_Encoded_Permutation_Section
			{
				[TagBlock("encoded data", 0, 1, 301989888, 4)]
				public sealed class _0_encoded_data
				{
				}
				[TagBlock("Sound Dialogue Info", 8, 16, 288, 4)]
				public sealed class _8_Sound_Dialogue_Info
				{
				}
			}
			[TagBlock("Resources*", 24, 16, 1024, 4)]
			public sealed class _24_Resources_
			{
			}
		}
	}
}
