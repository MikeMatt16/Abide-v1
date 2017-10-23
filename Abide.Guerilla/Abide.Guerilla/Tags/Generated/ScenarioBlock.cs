using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("scenario", "scnr", "����", typeof(ScenarioBlock))]
	[FieldSet(1476, 4)]
	public unsafe struct ScenarioBlock
	{
		public enum Type2Options
		{
			__0 = 0,
			Multiplayer_1 = 1,
			__2 = 2,
			__3 = 3,
			__4 = 4,
		}
		public enum Flags3Options
		{
			CortanaHackSortsCortanaInFrontOfOtherTransparentGeometry_0 = 1,
			AlwaysDrawSkyAlwaysDrawsSky0EvenIfNoSkyPolygonsAreVisible_1 = 2,
			DonTStripPathfindingAlwaysLeavesPathfindingInEvenForMultiplayerScenario_2 = 4,
			SymmetricMultiplayerMap_3 = 8,
			QuickLoadingCinematicOnlyScenario_4 = 16,
			CharactersUsePreviousMissionWeapons_5 = 32,
			LightmapsSmoothPalettesWithNeighbors_6 = 64,
			SnapToWhiteAtStart_7 = 128,
		}
		[Field("Do not use.", null)]
		public TagReference DoNotUse0;
		[Field("Skies", null)]
		[Block("Scenario Sky Reference Block", 32, typeof(ScenarioSkyReferenceBlock))]
		public TagBlock Skies1;
		[Field("Type", typeof(Type2Options))]
		public short Type2;
		[Field("Flags", typeof(Flags3Options))]
		public short Flags3;
		[Field("@child scenarios", null)]
		[Block("Scenario Child Scenario Block", 16, typeof(ScenarioChildScenarioBlock))]
		public TagBlock ChildScenarios4;
		[Field("Local North", null)]
		public float LocalNorth5;
		[Field("Predicted Resources*", null)]
		[Block("Predicted Resource Block", 2048, typeof(PredictedResourceBlock))]
		public TagBlock PredictedResources6;
		[Field("Functions", null)]
		[Block("Scenario Function Block", 32, typeof(ScenarioFunctionBlock))]
		public TagBlock Functions7;
		[Field("Editor Scenario Data", null)]
		[Data(65536)]
		public TagBlock EditorScenarioData8;
		[Field("Comments", null)]
		[Block("Editor Comment Block", 65536, typeof(EditorCommentBlock))]
		public TagBlock Comments9;
		[Field("*", null)]
		[Block("Dont Use Me Scenario Environment Object Block", 4096, typeof(DontUseMeScenarioEnvironmentObjectBlock))]
		public TagBlock Empty10;
		[Field("Object Names*", null)]
		[Block("Scenario Object Names Block", 640, typeof(ScenarioObjectNamesBlock))]
		public TagBlock ObjectNames11;
		[Field("Scenery", null)]
		[Block("Scenario Scenery Block", 2000, typeof(ScenarioSceneryBlock))]
		public TagBlock Scenery12;
		[Field("Scenery Palette", null)]
		[Block("Scenario Scenery Palette Block", 256, typeof(ScenarioSceneryPaletteBlock))]
		public TagBlock SceneryPalette13;
		[Field("Bipeds", null)]
		[Block("Scenario Biped Block", 128, typeof(ScenarioBipedBlock))]
		public TagBlock Bipeds14;
		[Field("Biped Palette", null)]
		[Block("Scenario Biped Palette Block", 256, typeof(ScenarioBipedPaletteBlock))]
		public TagBlock BipedPalette15;
		[Field("Vehicles", null)]
		[Block("Scenario Vehicle Block", 256, typeof(ScenarioVehicleBlock))]
		public TagBlock Vehicles16;
		[Field("Vehicle Palette", null)]
		[Block("Scenario Vehicle Palette Block", 256, typeof(ScenarioVehiclePaletteBlock))]
		public TagBlock VehiclePalette17;
		[Field("Equipment", null)]
		[Block("Scenario Equipment Block", 256, typeof(ScenarioEquipmentBlock))]
		public TagBlock Equipment18;
		[Field("Equipment Palette", null)]
		[Block("Scenario Equipment Palette Block", 256, typeof(ScenarioEquipmentPaletteBlock))]
		public TagBlock EquipmentPalette19;
		[Field("Weapons", null)]
		[Block("Scenario Weapon Block", 128, typeof(ScenarioWeaponBlock))]
		public TagBlock Weapons20;
		[Field("Weapon Palette", null)]
		[Block("Scenario Weapon Palette Block", 256, typeof(ScenarioWeaponPaletteBlock))]
		public TagBlock WeaponPalette21;
		[Field("Device Groups", null)]
		[Block("Device Group Block", 128, typeof(DeviceGroupBlock))]
		public TagBlock DeviceGroups22;
		[Field("Machines", null)]
		[Block("Scenario Machine Block", 400, typeof(ScenarioMachineBlock))]
		public TagBlock Machines23;
		[Field("Machine Palette", null)]
		[Block("Scenario Machine Palette Block", 256, typeof(ScenarioMachinePaletteBlock))]
		public TagBlock MachinePalette24;
		[Field("Controls", null)]
		[Block("Scenario Control Block", 100, typeof(ScenarioControlBlock))]
		public TagBlock Controls25;
		[Field("Control Palette", null)]
		[Block("Scenario Control Palette Block", 256, typeof(ScenarioControlPaletteBlock))]
		public TagBlock ControlPalette26;
		[Field("Light Fixtures", null)]
		[Block("Scenario Light Fixture Block", 500, typeof(ScenarioLightFixtureBlock))]
		public TagBlock LightFixtures27;
		[Field("Light Fixtures Palette", null)]
		[Block("Scenario Light Fixture Palette Block", 256, typeof(ScenarioLightFixturePaletteBlock))]
		public TagBlock LightFixturesPalette28;
		[Field("Sound Scenery", null)]
		[Block("Scenario Sound Scenery Block", 256, typeof(ScenarioSoundSceneryBlock))]
		public TagBlock SoundScenery29;
		[Field("Sound Scenery Palette", null)]
		[Block("Scenario Sound Scenery Palette Block", 256, typeof(ScenarioSoundSceneryPaletteBlock))]
		public TagBlock SoundSceneryPalette30;
		[Field("Light Volumes", null)]
		[Block("Scenario Light Block", 500, typeof(ScenarioLightBlock))]
		public TagBlock LightVolumes31;
		[Field("Light Volumes Palette", null)]
		[Block("Scenario Light Palette Block", 256, typeof(ScenarioLightPaletteBlock))]
		public TagBlock LightVolumesPalette32;
		[Field("Player Starting Profile", null)]
		[Block("Scenario Profiles Block", 256, typeof(ScenarioProfilesBlock))]
		public TagBlock PlayerStartingProfile33;
		[Field("Player Starting Locations", null)]
		[Block("Scenario Players Block", 256, typeof(ScenarioPlayersBlock))]
		public TagBlock PlayerStartingLocations34;
		[Field("Kill Trigger Volumes", null)]
		[Block("Scenario Trigger Volume Block", 256, typeof(ScenarioTriggerVolumeBlock))]
		public TagBlock KillTriggerVolumes35;
		[Field("Recorded Animations", null)]
		[Block("Recorded Animation Block", 1024, typeof(RecordedAnimationBlock))]
		public TagBlock RecordedAnimations36;
		[Field("Netgame Flags", null)]
		[Block("Scenario Netpoints Block", 200, typeof(ScenarioNetpointsBlock))]
		public TagBlock NetgameFlags37;
		[Field("Netgame Equipment", null)]
		[Block("Scenario Netgame Equipment Block", 100, typeof(ScenarioNetgameEquipmentBlock))]
		public TagBlock NetgameEquipment38;
		[Field("Starting Equipment", null)]
		[Block("Scenario Starting Equipment Block", 200, typeof(ScenarioStartingEquipmentBlock))]
		public TagBlock StartingEquipment39;
		[Field("BSP Switch Trigger Volumes", null)]
		[Block("Scenario Bsp Switch Trigger Volume Block", 256, typeof(ScenarioBspSwitchTriggerVolumeBlock))]
		public TagBlock BSPSwitchTriggerVolumes40;
		[Field("Decals", null)]
		[Block("Scenario Decals Block", 65536, typeof(ScenarioDecalsBlock))]
		public TagBlock Decals41;
		[Field("Decals Palette", null)]
		[Block("Scenario Decal Palette Block", 128, typeof(ScenarioDecalPaletteBlock))]
		public TagBlock DecalsPalette42;
		[Field("Detail Object Collection Palette", null)]
		[Block("Scenario Detail Object Collection Palette Block", 32, typeof(ScenarioDetailObjectCollectionPaletteBlock))]
		public TagBlock DetailObjectCollectionPalette43;
		[Field("Style Palette", null)]
		[Block("Style Palette Block", 50, typeof(StylePaletteBlock))]
		public TagBlock StylePalette44;
		[Field("Squad Groups", null)]
		[Block("Squad Groups Block", 100, typeof(SquadGroupsBlock))]
		public TagBlock SquadGroups45;
		[Field("Squads", null)]
		[Block("Squads Block", 335, typeof(SquadsBlock))]
		public TagBlock Squads46;
		[Field("Zones", null)]
		[Block("Zone Block", 128, typeof(ZoneBlock))]
		public TagBlock Zones47;
		[Field("Mission Scenes", null)]
		[Block("Ai Scene Block", 100, typeof(AiSceneBlock))]
		public TagBlock MissionScenes48;
		[Field("Character Palette", null)]
		[Block("Character Palette Block", 64, typeof(CharacterPaletteBlock))]
		public TagBlock CharacterPalette49;
		[Field("AI Pathfinding Data", null)]
		[Block("Pathfinding Data Block", 16, typeof(PathfindingDataBlock))]
		public TagBlock AIPathfindingData50;
		[Field("AI Animation References", null)]
		[Block("Ai Animation Reference Block", 128, typeof(AiAnimationReferenceBlock))]
		public TagBlock AIAnimationReferences51;
		[Field("AI Script References", null)]
		[Block("Ai Script Reference Block", 128, typeof(AiScriptReferenceBlock))]
		public TagBlock AIScriptReferences52;
		[Field("AI Recording References", null)]
		[Block("Ai Recording Reference Block", 128, typeof(AiRecordingReferenceBlock))]
		public TagBlock AIRecordingReferences53;
		[Field("AI Conversations", null)]
		[Block("Ai Conversation Block", 128, typeof(AiConversationBlock))]
		public TagBlock AIConversations54;
		[Field("Script Syntax Data", null)]
		[Data(737356)]
		public TagBlock ScriptSyntaxData55;
		[Field("Script String Data", null)]
		[Data(614400)]
		public TagBlock ScriptStringData56;
		[Field("Scripts*", null)]
		[Block("Hs Scripts Block", 1024, typeof(HsScriptsBlock))]
		public TagBlock Scripts57;
		[Field("Globals*", null)]
		[Block("Hs Globals Block", 256, typeof(HsGlobalsBlock))]
		public TagBlock Globals58;
		[Field("References*", null)]
		[Block("Hs References Block", 512, typeof(HsReferencesBlock))]
		public TagBlock References59;
		[Field("Source Files*", null)]
		[Block("Hs Source Files Block", 8, typeof(HsSourceFilesBlock))]
		public TagBlock SourceFiles60;
		[Field("Scripting Data", null)]
		[Block("Cs Script Data Block", 1, typeof(CsScriptDataBlock))]
		public TagBlock ScriptingData61;
		[Field("Cutscene Flags", null)]
		[Block("Scenario Cutscene Flag Block", 512, typeof(ScenarioCutsceneFlagBlock))]
		public TagBlock CutsceneFlags62;
		[Field("Cutscene Camera Points", null)]
		[Block("Scenario Cutscene Camera Point Block", 512, typeof(ScenarioCutsceneCameraPointBlock))]
		public TagBlock CutsceneCameraPoints63;
		[Field("Cutscene Titles", null)]
		[Block("Scenario Cutscene Title Block", 128, typeof(ScenarioCutsceneTitleBlock))]
		public TagBlock CutsceneTitles64;
		[Field("Custom Object Names", null)]
		public TagReference CustomObjectNames65;
		[Field("Chapter Title Text", null)]
		public TagReference ChapterTitleText66;
		[Field("HUD Messages", null)]
		public TagReference HUDMessages67;
		[Field("Structure BSPs", null)]
		[Block("Scenario Structure Bsp Reference Block", 16, typeof(ScenarioStructureBspReferenceBlock))]
		public TagBlock StructureBSPs68;
		[Field("Scenario Resources", null)]
		[Block("Scenario Resources Block", 1, typeof(ScenarioResourcesBlock))]
		public TagBlock ScenarioResources69;
		[Field("Scenario Resources", null)]
		[Block("Old Unused Strucure Physics Block", 16, typeof(OldUnusedStrucurePhysicsBlock))]
		public TagBlock ScenarioResources70;
		[Field(")hs Unit Seats", null)]
		[Block("Hs Unit Seat Block", 65536, typeof(HsUnitSeatBlock))]
		public TagBlock HsUnitSeats71;
		[Field("Scenario Kill Triggers", null)]
		[Block("Scenario Kill Trigger Volumes Block", 256, typeof(ScenarioKillTriggerVolumesBlock))]
		public TagBlock ScenarioKillTriggers72;
		[Field("hs Syntax Datums*", null)]
		[Block("Syntax Datum Block", 36864, typeof(SyntaxDatumBlock))]
		public TagBlock HsSyntaxDatums73;
		[Field("Orders", null)]
		[Block("Orders Block", 300, typeof(OrdersBlock))]
		public TagBlock Orders74;
		[Field("Triggers", null)]
		[Block("Triggers Block", 256, typeof(TriggersBlock))]
		public TagBlock Triggers75;
		[Field("Background Sound Palette", null)]
		[Block("Structure Bsp Background Sound Palette Block", 64, typeof(StructureBspBackgroundSoundPaletteBlock))]
		public TagBlock BackgroundSoundPalette76;
		[Field("Sound Environment Palette", null)]
		[Block("Structure Bsp Sound Environment Palette Block", 64, typeof(StructureBspSoundEnvironmentPaletteBlock))]
		public TagBlock SoundEnvironmentPalette77;
		[Field("Weather Palette", null)]
		[Block("Structure Bsp Weather Palette Block", 32, typeof(StructureBspWeatherPaletteBlock))]
		public TagBlock WeatherPalette78;
		[Field("EMPTY STRING", null)]
		[Block("G Null Block", 0, typeof(GNullBlock))]
		public TagBlock EMPTYSTRING79;
		[Field("EMPTY STRING", null)]
		[Block("G Null Block", 0, typeof(GNullBlock))]
		public TagBlock EMPTYSTRING80;
		[Field("EMPTY STRING", null)]
		[Block("G Null Block", 0, typeof(GNullBlock))]
		public TagBlock EMPTYSTRING81;
		[Field("EMPTY STRING", null)]
		[Block("G Null Block", 0, typeof(GNullBlock))]
		public TagBlock EMPTYSTRING82;
		[Field("EMPTY STRING", null)]
		[Block("G Null Block", 0, typeof(GNullBlock))]
		public TagBlock EMPTYSTRING83;
		[Field("Scenario Cluster Data", null)]
		[Block("Scenario Cluster Data Block", 16, typeof(ScenarioClusterDataBlock))]
		public TagBlock ScenarioClusterData84;
		[Field("EMPTY STRING", null)]
		public int EMPTYSTRING86;
		[Field("Spawn Data", null)]
		[Block("Scenario Spawn Data Block", 1, typeof(ScenarioSpawnDataBlock))]
		public TagBlock SpawnData88;
		[Field("Sound Effect Collection", null)]
		public TagReference SoundEffectCollection89;
		[Field("Crates", null)]
		[Block("Scenario Crate Block", 1024, typeof(ScenarioCrateBlock))]
		public TagBlock Crates90;
		[Field("Crates Palette", null)]
		[Block("Scenario Crate Palette Block", 256, typeof(ScenarioCratePaletteBlock))]
		public TagBlock CratesPalette91;
		[Field("Global Lighting", null)]
		public TagReference GlobalLighting93;
		[Field("Atmospheric Fog Palette", null)]
		[Block("Scenario Atmospheric Fog Palette", 127, typeof(ScenarioAtmosphericFogPalette))]
		public TagBlock AtmosphericFogPalette95;
		[Field("Planar Fog Palette", null)]
		[Block("Scenario Planar Fog Palette", 127, typeof(ScenarioPlanarFogPalette))]
		public TagBlock PlanarFogPalette96;
		[Field("Flocks", null)]
		[Block("Flock Definition Block", 20, typeof(FlockDefinitionBlock))]
		public TagBlock Flocks97;
		[Field("Subtitles", null)]
		public TagReference Subtitles98;
		[Field("Decorators", null)]
		[Block("Decorator Placement Definition Block", 1, typeof(DecoratorPlacementDefinitionBlock))]
		public TagBlock Decorators99;
		[Field("Creatures", null)]
		[Block("Scenario Creature Block", 128, typeof(ScenarioCreatureBlock))]
		public TagBlock Creatures100;
		[Field("Creatures Palette", null)]
		[Block("Scenario Creature Palette Block", 256, typeof(ScenarioCreaturePaletteBlock))]
		public TagBlock CreaturesPalette101;
		[Field("Decorators Palette", null)]
		[Block("Scenario Decorator Set Palette Entry Block", 32, typeof(ScenarioDecoratorSetPaletteEntryBlock))]
		public TagBlock DecoratorsPalette102;
		[Field(")BSP Transition Volumes", null)]
		[Block("Scenario Bsp Switch Transition Volume Block", 256, typeof(ScenarioBspSwitchTransitionVolumeBlock))]
		public TagBlock BSPTransitionVolumes103;
		[Field("Structure BSP Lighting", null)]
		[Block("Scenario Structure Bsp Spherical Harmonic Lighting Block", 16, typeof(ScenarioStructureBspSphericalHarmonicLightingBlock))]
		public TagBlock StructureBSPLighting104;
		[Field(")Editor Folders", null)]
		[Block("G Scenario Editor Folder Block", 32767, typeof(GScenarioEditorFolderBlock))]
		public TagBlock EditorFolders105;
		[Field("Level Data", null)]
		[Block("Scenario Level Data Block", 1, typeof(ScenarioLevelDataBlock))]
		public TagBlock LevelData106;
		[Field("Territory Location Names", null)]
		public TagReference TerritoryLocationNames107;
		[Field("", null)]
		public fixed byte _108[8];
		[Field("Mission Dialogue", null)]
		[Block("Ai Scenario Mission Dialogue Block", 1, typeof(AiScenarioMissionDialogueBlock))]
		public TagBlock MissionDialogue109;
		[Field("Objectives", null)]
		public TagReference Objectives110;
		[Field("Interpolators", null)]
		[Block("Interpolators", 16, typeof(ScenarioInterpolatorBlock))]
		public TagBlock Interpolators111;
		[Field("Shared References", null)]
		[Block("Hs References Block", 512, typeof(HsReferencesBlock))]
		public TagBlock SharedReferences112;
		[Field("Screen Effect References", null)]
		[Block("Screen Effect Reference", 16, typeof(ScenarioScreenEffectReferenceBlock))]
		public TagBlock ScreenEffectReferences113;
		[Field("Simulation Definition Table", null)]
		[Block("Simulation Definition Table Element", 512, typeof(ScenarioSimulationDefinitionTableBlock))]
		public TagBlock SimulationDefinitionTable114;
	}
}
#pragma warning restore CS1591
