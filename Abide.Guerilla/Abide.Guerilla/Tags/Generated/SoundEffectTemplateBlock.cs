using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("sound_effect_template", "<fx>", "����", typeof(SoundEffectTemplateBlock))]
	[FieldSet(40, 4)]
	public unsafe struct SoundEffectTemplateBlock
	{
		[Field("template collection", null)]
		[Block("Sound Effect Templates Block", 8, typeof(SoundEffectTemplatesBlock))]
		public TagBlock TemplateCollection0;
		[Field("input effect name", null)]
		public StringId InputEffectName1;
		[Field("additional sound inputs", null)]
		[Block("Sound Effect Template Additional Sound Input Block", 1, typeof(SoundEffectTemplateAdditionalSoundInputBlock))]
		public TagBlock AdditionalSoundInputs2;
		[Field("", null)]
		[Block("Platform Sound Effect Template Collection Block", 1, typeof(PlatformSoundEffectTemplateCollectionBlock))]
		public TagBlock _3;
	}
}
#pragma warning restore CS1591
