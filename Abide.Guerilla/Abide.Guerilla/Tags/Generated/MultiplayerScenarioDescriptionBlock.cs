using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("multiplayer_scenario_description", "mply", "����", typeof(MultiplayerScenarioDescriptionBlock))]
	[FieldSet(12, 4)]
	public unsafe struct MultiplayerScenarioDescriptionBlock
	{
		[Field("multiplayer scenarios", null)]
		[Block("Scenario Description Block", 32, typeof(ScenarioDescriptionBlock))]
		public TagBlock MultiplayerScenarios0;
	}
}
#pragma warning restore CS1591
