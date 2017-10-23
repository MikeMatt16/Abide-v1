using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(72, 4)]
	public unsafe struct SoundEffectPlaybackBlock
	{
		[Field("sound effect struct", typeof(SoundEffectStructDefinitionBlock))]
		[Block("Sound Effect Struct Definition", 1, typeof(SoundEffectStructDefinitionBlock))]
		public SoundEffectStructDefinitionBlock SoundEffectStruct0;
	}
}
#pragma warning restore CS1591
