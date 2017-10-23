using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("ai_dialogue_globals", "adlg", "����", typeof(AiDialogueGlobalsBlock))]
	[FieldSet(60, 4)]
	public unsafe struct AiDialogueGlobalsBlock
	{
		[Field("vocalizations", null)]
		[Block("Vocalization Definitions Block 0", 500, typeof(VocalizationDefinitionsBlock0))]
		public TagBlock Vocalizations0;
		[Field("patterns", null)]
		[Block("Vocalization Patterns Block", 1000, typeof(VocalizationPatternsBlock))]
		public TagBlock Patterns1;
		[Field("", null)]
		public fixed byte _2[12];
		[Field("dialogue data", null)]
		[Block("Dialogue Data Block", 200, typeof(DialogueDataBlock))]
		public TagBlock DialogueData3;
		[Field("involuntary data", null)]
		[Block("Involuntary Data Block", 100, typeof(InvoluntaryDataBlock))]
		public TagBlock InvoluntaryData4;
	}
}
#pragma warning restore CS1591
