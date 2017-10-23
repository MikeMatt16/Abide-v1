using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(32, 4)]
	public unsafe struct SoundEncodedDialogueSectionBlock
	{
		[Field("encoded data", null)]
		[Data(301989888)]
		public TagBlock EncodedData0;
		[Field("sound dialogue info", null)]
		[Block("Sound Permutation Dialogue Info Block", 288, typeof(SoundPermutationDialogueInfoBlock))]
		public TagBlock SoundDialogueInfo1;
	}
}
#pragma warning restore CS1591
