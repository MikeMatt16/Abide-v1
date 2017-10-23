using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("scenario_devices_resource", "dgr*", "����", typeof(ScenarioDevicesResourceBlock))]
	[FieldSet(144, 4)]
	public unsafe struct ScenarioDevicesResourceBlock
	{
		[Field("Names", null)]
		[Block("Scenario Object Names Block", 640, typeof(ScenarioObjectNamesBlock))]
		public TagBlock Names0;
		[Field("*", null)]
		[Block("Dont Use Me Scenario Environment Object Block", 4096, typeof(DontUseMeScenarioEnvironmentObjectBlock))]
		public TagBlock Empty1;
		[Field("Structure References", null)]
		[Block("Scenario Structure Bsp Reference Block", 16, typeof(ScenarioStructureBspReferenceBlock))]
		public TagBlock StructureReferences2;
		[Field("Device Groups", null)]
		[Block("Device Group Block", 128, typeof(DeviceGroupBlock))]
		public TagBlock DeviceGroups3;
		[Field("Machines", null)]
		[Block("Scenario Machine Block", 400, typeof(ScenarioMachineBlock))]
		public TagBlock Machines4;
		[Field("Machines Palette", null)]
		[Block("Scenario Machine Palette Block", 256, typeof(ScenarioMachinePaletteBlock))]
		public TagBlock MachinesPalette5;
		[Field("Controls", null)]
		[Block("Scenario Control Block", 100, typeof(ScenarioControlBlock))]
		public TagBlock Controls6;
		[Field("Controls Palette", null)]
		[Block("Scenario Control Palette Block", 256, typeof(ScenarioControlPaletteBlock))]
		public TagBlock ControlsPalette7;
		[Field("Light Fixtures", null)]
		[Block("Scenario Light Fixture Block", 500, typeof(ScenarioLightFixtureBlock))]
		public TagBlock LightFixtures8;
		[Field("Light Fixtures Palette", null)]
		[Block("Scenario Light Fixture Palette Block", 256, typeof(ScenarioLightFixturePaletteBlock))]
		public TagBlock LightFixturesPalette9;
		[Field("next machine id salt*", null)]
		public int NextMachineIdSalt10;
		[Field("Next Control ID Salt*", null)]
		public int NextControlIDSalt11;
		[Field("Next Light Fixture ID Salt*", null)]
		public int NextLightFixtureIDSalt12;
		[Field("Editor Folders*", null)]
		[Block("G Scenario Editor Folder Block", 32767, typeof(GScenarioEditorFolderBlock))]
		public TagBlock EditorFolders13;
	}
}
#pragma warning restore CS1591
