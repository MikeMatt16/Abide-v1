using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("scenario_trigger_volumes_resource", "trg*", "����", typeof(ScenarioTriggerVolumesResourceBlock))]
	[FieldSet(24, 4)]
	public unsafe struct ScenarioTriggerVolumesResourceBlock
	{
		[Field("Kill Trigger Volumes", null)]
		[Block("Scenario Trigger Volume Block", 256, typeof(ScenarioTriggerVolumeBlock))]
		public TagBlock KillTriggerVolumes0;
		[Field("Object Names", null)]
		[Block("Scenario Object Names Block", 640, typeof(ScenarioObjectNamesBlock))]
		public TagBlock ObjectNames1;
	}
}
#pragma warning restore CS1591
