using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(72, 4)]
	public unsafe struct SoundGestaltCustomPlaybackBlock
	{
		[Field("playback definition", typeof(SimplePlatformSoundPlaybackStructBlock))]
		[Block("Simple Platform Sound Playback Struct", 1, typeof(SimplePlatformSoundPlaybackStructBlock))]
		public SimplePlatformSoundPlaybackStructBlock PlaybackDefinition0;
	}
}
#pragma warning restore CS1591
