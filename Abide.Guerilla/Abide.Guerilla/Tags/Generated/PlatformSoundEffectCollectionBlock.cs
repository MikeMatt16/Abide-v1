using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(28, 4)]
	public unsafe struct PlatformSoundEffectCollectionBlock
	{
		[Field("sound effects*", null)]
		[Block("Platform Sound Effect Block", 8, typeof(PlatformSoundEffectBlock))]
		public TagBlock SoundEffects0;
		[Field("low frequency input*", null)]
		[Block("Platform Sound Effect Function Block", 16, typeof(PlatformSoundEffectFunctionBlock))]
		public TagBlock LowFrequencyInput1;
		[Field("sound effect overrides", null)]
		public int SoundEffectOverrides2;
	}
}
#pragma warning restore CS1591
