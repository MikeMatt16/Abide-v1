using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct ScenarioBspSwitchTransitionVolumeBlock
	{
		[Field("BSP Index Key", null)]
		public int BSPIndexKey0;
		[Field("Trigger Volume^", null)]
		public short TriggerVolume1;
		[Field("", null)]
		public fixed byte _2[2];
	}
}
#pragma warning restore CS1591
