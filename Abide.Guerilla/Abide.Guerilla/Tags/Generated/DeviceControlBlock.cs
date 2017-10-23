using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("device_control", "ctrl", "devi", typeof(DeviceControlBlock))]
	[FieldSet(60, 4)]
	public unsafe struct DeviceControlBlock
	{
		public enum Type1Options
		{
			ToggleSwitch_0 = 0,
			OnButton_1 = 1,
			OffButton_2 = 2,
			CallButton_3 = 3,
		}
		public enum TriggersWhen2Options
		{
			TouchedByPlayer_0 = 0,
			Destroyed_1 = 1,
		}
		[Field("type", typeof(Type1Options))]
		public short Type1;
		[Field("triggers when", typeof(TriggersWhen2Options))]
		public short TriggersWhen2;
		[Field("call value:[0,1]", null)]
		public float CallValue3;
		[Field("action string", null)]
		public StringId ActionString4;
		[Field("", null)]
		public fixed byte _5[76];
		[Field("on", null)]
		public TagReference On6;
		[Field("off", null)]
		public TagReference Off7;
		[Field("deny", null)]
		public TagReference Deny8;
	}
}
#pragma warning restore CS1591
