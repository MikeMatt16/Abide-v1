using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(68, 4)]
	public unsafe struct ScenarioTriggerVolumeBlock
	{
		[Field("Name^", null)]
		public StringId Name0;
		[Field("Object Name", null)]
		public short ObjectName1;
		[Field("", null)]
		public fixed byte _2[2];
		[Field("Node Name", null)]
		public StringId NodeName3;
		[Field("EMPTY STRING", null)]
		public float EMPTYSTRING5;
		public Vector3 Position7;
		public Vector3 Extents8;
		[Field("", null)]
		public fixed byte _9[4];
		[Field("~Kill Trigger Volume*", null)]
		public short KillTriggerVolume10;
		[Field("", null)]
		public fixed byte _11[2];
	}
}
#pragma warning restore CS1591
