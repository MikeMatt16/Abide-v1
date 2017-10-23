using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(64, 4)]
	public unsafe struct PlatformSoundFilterLfoBlock
	{
		[Field("delay", typeof(SoundPlaybackParameterDefinitionBlock))]
		[Block("Sound Playback Parameter Definition", 1, typeof(SoundPlaybackParameterDefinitionBlock))]
		public SoundPlaybackParameterDefinitionBlock Delay1;
		[Field("frequency", typeof(SoundPlaybackParameterDefinitionBlock))]
		[Block("Sound Playback Parameter Definition", 1, typeof(SoundPlaybackParameterDefinitionBlock))]
		public SoundPlaybackParameterDefinitionBlock Frequency3;
		[Field("cutoff modulation", typeof(SoundPlaybackParameterDefinitionBlock))]
		[Block("Sound Playback Parameter Definition", 1, typeof(SoundPlaybackParameterDefinitionBlock))]
		public SoundPlaybackParameterDefinitionBlock CutoffModulation5;
		[Field("gain modulation", typeof(SoundPlaybackParameterDefinitionBlock))]
		[Block("Sound Playback Parameter Definition", 1, typeof(SoundPlaybackParameterDefinitionBlock))]
		public SoundPlaybackParameterDefinitionBlock GainModulation7;
	}
}
#pragma warning restore CS1591
