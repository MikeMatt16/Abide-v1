using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(76, 4)]
	public unsafe struct ScenarioMachineBlock
	{
		[Field("Type", null)]
		public short Type1;
		[Field("Name^", null)]
		public short Name3;
		[Field("Object Data", typeof(ScenarioObjectDatumStructBlock))]
		[Block("Scenario Object Datum Struct", 1, typeof(ScenarioObjectDatumStructBlock))]
		public ScenarioObjectDatumStructBlock ObjectData4;
		[Field("Device Data", typeof(ScenarioDeviceStructBlock))]
		[Block("Scenario Device Struct", 1, typeof(ScenarioDeviceStructBlock))]
		public ScenarioDeviceStructBlock DeviceData5;
		[Field("Machine Data", typeof(ScenarioMachineStructV3Block))]
		[Block("Scenario Machine Struct V3", 1, typeof(ScenarioMachineStructV3Block))]
		public ScenarioMachineStructV3Block MachineData6;
	}
}
#pragma warning restore CS1591
