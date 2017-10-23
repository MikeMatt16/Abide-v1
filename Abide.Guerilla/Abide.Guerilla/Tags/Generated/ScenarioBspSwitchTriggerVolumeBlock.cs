using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(14, 4)]
	public unsafe struct ScenarioBspSwitchTriggerVolumeBlock
	{
		[Field("Trigger Volume", null)]
		public short TriggerVolume0;
		[Field("Source", null)]
		public short Source1;
		[Field("Destination", null)]
		public short Destination2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("", null)]
		public fixed byte _4[2];
		[Field("", null)]
		public fixed byte _5[2];
		[Field("", null)]
		public fixed byte _6[2];
	}
}
#pragma warning restore CS1591
