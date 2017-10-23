using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(104, 4)]
	public unsafe struct StructureBspEnvironmentObjectBlock
	{
		[Field("Name^", null)]
		public String Name0;
		[Field("Rotation", null)]
		public Quaternion Rotation1;
		public Vector3 Translation2;
		[Field("palette_index", null)]
		public short PaletteIndex3;
		[Field("", null)]
		public fixed byte _4[2];
		[Field("Unique ID*", null)]
		public int UniqueID5;
		[Field("Exported Object Type", null)]
		public Tag ExportedObjectType6;
		[Field("Scenario Object Name", null)]
		public String ScenarioObjectName7;
	}
}
#pragma warning restore CS1591
