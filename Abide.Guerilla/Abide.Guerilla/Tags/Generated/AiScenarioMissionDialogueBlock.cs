using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct AiScenarioMissionDialogueBlock
	{
		[Field("mission dialogue", null)]
		public TagReference MissionDialogue0;
	}
}
#pragma warning restore CS1591
