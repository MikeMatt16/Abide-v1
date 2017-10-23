using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(48, 4)]
	public unsafe struct PlatformSoundPitchLfoBlock
	{
		[Field("delay", typeof(SoundPlaybackParameterDefinitionBlock))]
		[Block("Sound Playback Parameter Definition", 1, typeof(SoundPlaybackParameterDefinitionBlock))]
		public SoundPlaybackParameterDefinitionBlock Delay1;
		[Field("frequency", typeof(SoundPlaybackParameterDefinitionBlock))]
		[Block("Sound Playback Parameter Definition", 1, typeof(SoundPlaybackParameterDefinitionBlock))]
		public SoundPlaybackParameterDefinitionBlock Frequency3;
		[Field("pitch modulation", typeof(SoundPlaybackParameterDefinitionBlock))]
		[Block("Sound Playback Parameter Definition", 1, typeof(SoundPlaybackParameterDefinitionBlock))]
		public SoundPlaybackParameterDefinitionBlock PitchModulation5;
	}
}
#pragma warning restore CS1591
