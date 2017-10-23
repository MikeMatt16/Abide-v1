using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("sound_effect_collection", "sfx+", "����", typeof(SoundEffectCollectionBlock))]
	[FieldSet(12, 4)]
	public unsafe struct SoundEffectCollectionBlock
	{
		[Field("sound effects", null)]
		[Block("Platform Sound Playback Block", 128, typeof(PlatformSoundPlaybackBlock))]
		public TagBlock SoundEffects0;
	}
}
#pragma warning restore CS1591
