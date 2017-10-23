using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("scenario_lights_resource", "*igh", "����", typeof(ScenarioLightsResourceBlock))]
	[FieldSet(76, 4)]
	public unsafe struct ScenarioLightsResourceBlock
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
		[Field("Palette", null)]
		[Block("Scenario Light Palette Block", 256, typeof(ScenarioLightPaletteBlock))]
		public TagBlock Palette3;
		[Field("Objects", null)]
		[Block("Scenario Light Block", 500, typeof(ScenarioLightBlock))]
		public TagBlock Objects4;
		[Field("Next Object ID Salt*", null)]
		public int NextObjectIDSalt5;
		[Field("Editor Folders*", null)]
		[Block("G Scenario Editor Folder Block", 32767, typeof(GScenarioEditorFolderBlock))]
		public TagBlock EditorFolders6;
	}
}
#pragma warning restore CS1591
