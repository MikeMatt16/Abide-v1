using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("scenario_equipment_resource", "*qip", "����", typeof(ScenarioEquipmentResourceBlock))]
	[FieldSet(76, 4)]
	public unsafe struct ScenarioEquipmentResourceBlock
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
		[Block("Scenario Equipment Palette Block", 256, typeof(ScenarioEquipmentPaletteBlock))]
		public TagBlock Palette3;
		[Field("Objects", null)]
		[Block("Scenario Equipment Block", 256, typeof(ScenarioEquipmentBlock))]
		public TagBlock Objects4;
		[Field("Next Object ID Salt*", null)]
		public int NextObjectIDSalt5;
		[Field("Editor Folders*", null)]
		[Block("G Scenario Editor Folder Block", 32767, typeof(GScenarioEditorFolderBlock))]
		public TagBlock EditorFolders6;
	}
}
#pragma warning restore CS1591
