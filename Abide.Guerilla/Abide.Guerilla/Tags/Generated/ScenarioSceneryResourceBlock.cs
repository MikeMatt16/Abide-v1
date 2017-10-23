using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("scenario_scenery_resource", "*cen", "����", typeof(ScenarioSceneryResourceBlock))]
	[FieldSet(104, 4)]
	public unsafe struct ScenarioSceneryResourceBlock
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
		[Block("Scenario Scenery Palette Block", 256, typeof(ScenarioSceneryPaletteBlock))]
		public TagBlock Palette3;
		[Field("Objects", null)]
		[Block("Scenario Scenery Block", 2000, typeof(ScenarioSceneryBlock))]
		public TagBlock Objects4;
		[Field("Next Scenery Object ID Salt*", null)]
		public int NextSceneryObjectIDSalt5;
		[Field("Palette", null)]
		[Block("Scenario Crate Palette Block", 256, typeof(ScenarioCratePaletteBlock))]
		public TagBlock Palette6;
		[Field("Objects", null)]
		[Block("Scenario Crate Block", 1024, typeof(ScenarioCrateBlock))]
		public TagBlock Objects7;
		[Field("Next Block Object ID Salt*", null)]
		public int NextBlockObjectIDSalt8;
		[Field("Editor Folders*", null)]
		[Block("G Scenario Editor Folder Block", 32767, typeof(GScenarioEditorFolderBlock))]
		public TagBlock EditorFolders9;
	}
}
#pragma warning restore CS1591
