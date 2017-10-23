using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(144, 4)]
	public unsafe struct LoopingSoundTrackBlock
	{
		public enum Flags1Options
		{
			FadeInAtStartTheLoopSoundShouldFadeInWhileTheStartSoundIsPlaying_0 = 1,
			FadeOutAtStopTheLoopSoundShouldFadeOutWhileTheStopSoundIsPlaying_1 = 2,
			CrossfadeAltLoopWhenTheSoundChangesToTheAlternateVersion_2 = 4,
			MasterSurroundSoundTrack_3 = 8,
			FadeOutAtAltStop_4 = 16,
		}
		public enum OutputEffect11Options
		{
			None_0 = 0,
			OutputFrontSpeakers_1 = 1,
			OutputRearSpeakers_2 = 2,
			OutputCenterSpeakers_3 = 3,
		}
		[Field("name^", null)]
		public StringId Name0;
		[Field("flags", typeof(Flags1Options))]
		public int Flags1;
		[Field("gain:dB", null)]
		public float Gain2;
		[Field("fade in duration:seconds", null)]
		public float FadeInDuration3;
		[Field("fade out duration:seconds", null)]
		public float FadeOutDuration4;
		[Field("in", null)]
		public TagReference In5;
		[Field("loop", null)]
		public TagReference Loop6;
		[Field("out", null)]
		public TagReference Out7;
		[Field("alt loop", null)]
		public TagReference AltLoop8;
		[Field("alt out", null)]
		public TagReference AltOut9;
		[Field("", null)]
		public fixed byte _10[12];
		[Field("output effect", typeof(OutputEffect11Options))]
		public short OutputEffect11;
		[Field("", null)]
		public fixed byte _12[2];
		[Field("alt trans in", null)]
		public TagReference AltTransIn13;
		[Field("alt trans out", null)]
		public TagReference AltTransOut14;
		[Field("alt crossfade duration:seconds", null)]
		public float AltCrossfadeDuration15;
		[Field("alt fade out duration:seconds", null)]
		public float AltFadeOutDuration16;
	}
}
#pragma warning restore CS1591
