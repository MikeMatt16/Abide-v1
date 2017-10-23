using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(72, 4)]
	public unsafe struct SimplePlatformSoundPlaybackStructBlock
	{
		public enum Flags1Options
		{
			Use3dRadioHack_0 = 1,
		}
		[Field("", null)]
		[Block("Platform Sound Override Mixbins Block", 8, typeof(PlatformSoundOverrideMixbinsBlock))]
		public TagBlock _0;
		[Field("flags", typeof(Flags1Options))]
		public int Flags1;
		[Field("", null)]
		public fixed byte _2[8];
		[Field("filter", null)]
		[Block("Platform Sound Filter Block", 1, typeof(PlatformSoundFilterBlock))]
		public TagBlock Filter3;
		[Field("pitch lfo", null)]
		[Block("Platform Sound Pitch Lfo Block", 1, typeof(PlatformSoundPitchLfoBlock))]
		public TagBlock PitchLfo4;
		[Field("filter lfo", null)]
		[Block("Platform Sound Filter Lfo Block", 1, typeof(PlatformSoundFilterLfoBlock))]
		public TagBlock FilterLfo5;
		[Field("sound effect", null)]
		[Block("Sound Effect Playback Block", 1, typeof(SoundEffectPlaybackBlock))]
		public TagBlock SoundEffect6;
	}
}
#pragma warning restore CS1591
