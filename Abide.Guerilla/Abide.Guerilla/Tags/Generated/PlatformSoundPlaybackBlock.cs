using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(76, 4)]
	public unsafe struct PlatformSoundPlaybackBlock
	{
		[Field("name^", null)]
		public StringId Name0;
		[Field("playback", typeof(PlatformSoundPlaybackStructBlock))]
		[Block("Platform Sound Playback Struct", 1, typeof(PlatformSoundPlaybackStructBlock))]
		public PlatformSoundPlaybackStructBlock Playback1;
	}
}
#pragma warning restore CS1591
