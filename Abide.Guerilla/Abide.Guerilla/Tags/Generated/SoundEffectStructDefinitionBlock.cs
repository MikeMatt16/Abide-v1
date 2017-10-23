using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(72, 4)]
	public unsafe struct SoundEffectStructDefinitionBlock
	{
		[Field("", null)]
		public TagReference _1;
		[Field("components", null)]
		[Block("Sound Effect Component Block", 16, typeof(SoundEffectComponentBlock))]
		public TagBlock Components2;
		[Field("", null)]
		[Block("Sound Effect Overrides Block", 128, typeof(SoundEffectOverridesBlock))]
		public TagBlock _3;
		[Field("", null)]
		[Data(1024)]
		public TagBlock _4;
		[Field("", null)]
		[Block("Platform Sound Effect Collection Block", 1, typeof(PlatformSoundEffectCollectionBlock))]
		public TagBlock _5;
	}
}
#pragma warning restore CS1591
