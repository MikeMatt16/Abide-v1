using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(68, 4)]
	public unsafe struct ScenarioControlBlock
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
		[Field("Control Data", typeof(ScenarioControlStructBlock))]
		[Block("Scenario Control Struct", 1, typeof(ScenarioControlStructBlock))]
		public ScenarioControlStructBlock ControlData6;
	}
}
#pragma warning restore CS1591
