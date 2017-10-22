using Abide.Builder.Tags.TagDefinition;
namespace Abide.Builder.Tags
{
	[TagDefinition("snd!", 144)]
	internal sealed class _snd_
	{
		[Tag("Promotion Sound", 88)]
		public sealed class _88_Promotion_Sound
		{
		}
		[TagIdentifier("Promotion Sound", 92)]
		public sealed class _92_Promotion_Sound
		{
		}
		[TagBlock("Pitch Ranges*", 120, 28, 9, 4)]
		public sealed class _120_Pitch_Ranges_
		{
			[TagBlock("Permutations*", 20, 28, 32, 4)]
			public sealed class _20_Permutations_
			{
				[TagBlock("Sound Permutation Chunk Block", 20, 12, 32767, 4)]
				public sealed class _20_Sound_Permutation_Chunk_Block
				{
				}
			}
		}
		[TagBlock("Platform Parameters", 128, 60, 1, 4)]
		public sealed class _128_Platform_Parameters
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
		[TagBlock("Sound Extra Info Block", 136, 52, 1, 4)]
		public sealed class _136_Sound_Extra_Info_Block
		{
			[TagBlock("Language Permutation Info", 0, 8, 576, 4)]
			public sealed class _0_Language_Permutation_Info
			{
				[TagBlock("Raw Info Block", 0, 40, 18, 4)]
				public sealed class _0_Raw_Info_Block
				{
					[TagBlock("", 4, 1, 16777216, 4)]
					public sealed class _4_
					{
					}
					[TagBlock("", 12, 1, 8192, 4)]
					public sealed class _12_
					{
					}
					[TagBlock("", 20, 1, 1048576, 4)]
					public sealed class _20_
					{
					}
					[TagBlock("Sound Permutation Marker Block", 28, 12, 65535, 4)]
					public sealed class _28_Sound_Permutation_Marker_Block
					{
					}
				}
			}
			[TagBlock("Encoded Permutation Section", 8, 16, 1, 4)]
			public sealed class _8_Encoded_Permutation_Section
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
			[TagBlock("Resources*", 32, 16, 1024, 4)]
			public sealed class _32_Resources_
			{
			}
		}
	}
}
