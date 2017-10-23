using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("sound_dialogue_constants", "spk!", "����", typeof(SoundDialogueConstantsBlock))]
	[FieldSet(40, 4)]
	public unsafe struct SoundDialogueConstantsBlock
	{
		[Field("almost never", null)]
		public float AlmostNever1;
		[Field("rarely", null)]
		public float Rarely2;
		[Field("somewhat", null)]
		public float Somewhat3;
		[Field("often", null)]
		public float Often4;
		[Field("", null)]
		public fixed byte _5[24];
	}
}
#pragma warning restore CS1591
