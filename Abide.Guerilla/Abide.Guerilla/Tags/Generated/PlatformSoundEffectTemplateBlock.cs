using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(28, 4)]
	public unsafe struct PlatformSoundEffectTemplateBlock
	{
		[Field("input dsp effect name", null)]
		public StringId InputDspEffectName0;
		[Field("", null)]
		public fixed byte _1[12];
		[Field("components", null)]
		[Block("Platform Sound Effect Template Component Block", 16, typeof(PlatformSoundEffectTemplateComponentBlock))]
		public TagBlock Components2;
	}
}
#pragma warning restore CS1591
