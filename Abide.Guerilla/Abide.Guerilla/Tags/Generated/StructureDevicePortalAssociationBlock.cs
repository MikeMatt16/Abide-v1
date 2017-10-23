using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(12, 4)]
	public unsafe struct StructureDevicePortalAssociationBlock
	{
		[Field("device id", typeof(ScenarioObjectIdStructBlock))]
		[Block("Scenario Object Id Struct", 1, typeof(ScenarioObjectIdStructBlock))]
		public ScenarioObjectIdStructBlock DeviceId0;
		[Field("first game portal index", null)]
		public short FirstGamePortalIndex1;
		[Field("game portal count", null)]
		public short GamePortalCount2;
	}
}
#pragma warning restore CS1591
