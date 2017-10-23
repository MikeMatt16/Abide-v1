using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(56, 4)]
	public unsafe struct ScenarioEquipmentBlock
	{
		[Field("Type", null)]
		public short Type1;
		[Field("Name^", null)]
		public short Name3;
		[Field("Object Data", typeof(ScenarioObjectDatumStructBlock))]
		[Block("Scenario Object Datum Struct", 1, typeof(ScenarioObjectDatumStructBlock))]
		public ScenarioObjectDatumStructBlock ObjectData4;
		[Field("Equipment Data", typeof(ScenarioEquipmentDatumStructBlock))]
		[Block("Scenario Equipment Datum Struct", 1, typeof(ScenarioEquipmentDatumStructBlock))]
		public ScenarioEquipmentDatumStructBlock EquipmentData5;
	}
}
#pragma warning restore CS1591
