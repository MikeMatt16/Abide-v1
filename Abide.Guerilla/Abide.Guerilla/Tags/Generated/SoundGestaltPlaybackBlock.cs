using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(56, 4)]
	public unsafe struct SoundGestaltPlaybackBlock
	{
		[Field("", typeof(SoundPlaybackParametersStructBlock))]
		[Block("Sound Playback Parameters Struct", 1, typeof(SoundPlaybackParametersStructBlock))]
		public SoundPlaybackParametersStructBlock _0;
	}
}
#pragma warning restore CS1591
