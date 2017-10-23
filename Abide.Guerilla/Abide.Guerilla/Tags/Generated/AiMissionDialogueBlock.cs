using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("ai_mission_dialogue", "mdlg", "����", typeof(AiMissionDialogueBlock))]
	[FieldSet(12, 4)]
	public unsafe struct AiMissionDialogueBlock
	{
		[Field("lines", null)]
		[Block("Mission Dialogue Lines Block", 500, typeof(MissionDialogueLinesBlock))]
		public TagBlock Lines0;
	}
}
#pragma warning restore CS1591
