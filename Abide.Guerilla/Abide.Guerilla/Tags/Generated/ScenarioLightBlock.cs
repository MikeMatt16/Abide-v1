using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(108, 4)]
	public unsafe struct ScenarioLightBlock
	{
		[Field("Type", null)]
		public short Type4;
		[Field("Name^", null)]
		public short Name6;
		[Field("Object Data", typeof(ScenarioObjectDatumStructBlock))]
		[Block("Scenario Object Datum Struct", 1, typeof(ScenarioObjectDatumStructBlock))]
		public ScenarioObjectDatumStructBlock ObjectData7;
		[Field("Device Data", typeof(ScenarioDeviceStructBlock))]
		[Block("Scenario Device Struct", 1, typeof(ScenarioDeviceStructBlock))]
		public ScenarioDeviceStructBlock DeviceData8;
		[Field("Light Data", typeof(ScenarioLightStructBlock))]
		[Block("Scenario Light Struct", 1, typeof(ScenarioLightStructBlock))]
		public ScenarioLightStructBlock LightData9;
	}
}
#pragma warning restore CS1591
