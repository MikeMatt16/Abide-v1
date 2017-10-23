using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct PlatformSoundEffectTemplateCollectionBlock
	{
		[Field("platform effect templates", null)]
		[Block("Platform Sound Effect Template Block", 8, typeof(PlatformSoundEffectTemplateBlock))]
		public TagBlock PlatformEffectTemplates0;
		[Field("input dsp effect name", null)]
		public StringId InputDspEffectName1;
	}
}
#pragma warning restore CS1591
