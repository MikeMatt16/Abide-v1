using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(72, 4)]
	public unsafe struct PlatformSoundFilterBlock
	{
		public enum FilterType1Options
		{
			ParametricEQ_0 = 0,
			DLS2_1 = 1,
			BothOnlyValidForMono_2 = 2,
		}
		[Field("filter type", typeof(FilterType1Options))]
		public int FilterType1;
		[Field("filter width:[0,7]", null)]
		public int FilterWidth2;
		[Field("left filter frequency", typeof(SoundPlaybackParameterDefinitionBlock))]
		[Block("Sound Playback Parameter Definition", 1, typeof(SoundPlaybackParameterDefinitionBlock))]
		public SoundPlaybackParameterDefinitionBlock LeftFilterFrequency4;
		[Field("left filter gain", typeof(SoundPlaybackParameterDefinitionBlock))]
		[Block("Sound Playback Parameter Definition", 1, typeof(SoundPlaybackParameterDefinitionBlock))]
		public SoundPlaybackParameterDefinitionBlock LeftFilterGain6;
		[Field("right filter frequency", typeof(SoundPlaybackParameterDefinitionBlock))]
		[Block("Sound Playback Parameter Definition", 1, typeof(SoundPlaybackParameterDefinitionBlock))]
		public SoundPlaybackParameterDefinitionBlock RightFilterFrequency8;
		[Field("right filter gain", typeof(SoundPlaybackParameterDefinitionBlock))]
		[Block("Sound Playback Parameter Definition", 1, typeof(SoundPlaybackParameterDefinitionBlock))]
		public SoundPlaybackParameterDefinitionBlock RightFilterGain10;
	}
}
#pragma warning restore CS1591
