using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct SoundEffectOverridesBlock
	{
		[Field("name", null)]
		public StringId Name0;
		[Field("overrides", null)]
		[Block("Sound Effect Override Parameters Block", 128, typeof(SoundEffectOverrideParametersBlock))]
		public TagBlock Overrides1;
	}
}
#pragma warning restore CS1591
