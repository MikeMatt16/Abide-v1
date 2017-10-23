using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("scenario_ai_resource", "ai**", "����", typeof(ScenarioAiResourceBlock))]
	[FieldSet(228, 4)]
	public unsafe struct ScenarioAiResourceBlock
	{
		[Field("Style Palette", null)]
		[Block("Style Palette Block", 50, typeof(StylePaletteBlock))]
		public TagBlock StylePalette0;
		[Field("Squad Groups", null)]
		[Block("Squad Groups Block", 100, typeof(SquadGroupsBlock))]
		public TagBlock SquadGroups1;
		[Field("Squads", null)]
		[Block("Squads Block", 335, typeof(SquadsBlock))]
		public TagBlock Squads2;
		[Field("Zones", null)]
		[Block("Zone Block", 128, typeof(ZoneBlock))]
		public TagBlock Zones3;
		[Field("Character Palette", null)]
		[Block("Character Palette Block", 64, typeof(CharacterPaletteBlock))]
		public TagBlock CharacterPalette4;
		[Field("AI Animation References", null)]
		[Block("Ai Animation Reference Block", 128, typeof(AiAnimationReferenceBlock))]
		public TagBlock AIAnimationReferences5;
		[Field("AI Script References", null)]
		[Block("Ai Script Reference Block", 128, typeof(AiScriptReferenceBlock))]
		public TagBlock AIScriptReferences6;
		[Field("AI Recording References", null)]
		[Block("Ai Recording Reference Block", 128, typeof(AiRecordingReferenceBlock))]
		public TagBlock AIRecordingReferences7;
		[Field("AI Conversations", null)]
		[Block("Ai Conversation Block", 128, typeof(AiConversationBlock))]
		public TagBlock AIConversations8;
		[Field("Scripting Data", null)]
		[Block("Cs Script Data Block", 1, typeof(CsScriptDataBlock))]
		public TagBlock ScriptingData9;
		[Field("Orders", null)]
		[Block("Orders Block", 300, typeof(OrdersBlock))]
		public TagBlock Orders10;
		[Field("Triggers", null)]
		[Block("Triggers Block", 256, typeof(TriggersBlock))]
		public TagBlock Triggers11;
		[Field("BSP Preferences", null)]
		[Block("Scenario Structure Bsp Reference Block", 16, typeof(ScenarioStructureBspReferenceBlock))]
		public TagBlock BSPPreferences12;
		[Field("Weapon References", null)]
		[Block("Scenario Weapon Palette Block", 256, typeof(ScenarioWeaponPaletteBlock))]
		public TagBlock WeaponReferences13;
		[Field("Vehicle References", null)]
		[Block("Scenario Vehicle Palette Block", 256, typeof(ScenarioVehiclePaletteBlock))]
		public TagBlock VehicleReferences14;
		[Field("Vehicle Datum References", null)]
		[Block("Scenario Vehicle Block", 256, typeof(ScenarioVehicleBlock))]
		public TagBlock VehicleDatumReferences15;
		[Field("Mission Dialogue Scenes", null)]
		[Block("Ai Scene Block", 100, typeof(AiSceneBlock))]
		public TagBlock MissionDialogueScenes16;
		[Field("Flocks", null)]
		[Block("Flock Definition Block", 20, typeof(FlockDefinitionBlock))]
		public TagBlock Flocks17;
		[Field("Trigger Volume References", null)]
		[Block("Scenario Trigger Volume Block", 256, typeof(ScenarioTriggerVolumeBlock))]
		public TagBlock TriggerVolumeReferences18;
	}
}
#pragma warning restore CS1591
