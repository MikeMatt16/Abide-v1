using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("device", "devi", "obje", typeof(DeviceBlock))]
	[FieldSet(152, 4)]
	public unsafe struct DeviceBlock
	{
		public enum Flags1Options
		{
			PositionLoops_0 = 1,
			Unused_1 = 2,
			AllowInterpolation_2 = 4,
		}
		public enum LightmapFlags8Options
		{
			DonTUseInLightmap_0 = 1,
			DonTUseInLightprobe_1 = 2,
		}
		[Field("flags", typeof(Flags1Options))]
		public int Flags1;
		[Field("power transition time:seconds", null)]
		public float PowerTransitionTime2;
		[Field("power acceleration time:seconds", null)]
		public float PowerAccelerationTime3;
		[Field("position transition time:seconds", null)]
		public float PositionTransitionTime4;
		[Field("position acceleration time:seconds", null)]
		public float PositionAccelerationTime5;
		[Field("depowered position transition time:seconds", null)]
		public float DepoweredPositionTransitionTime6;
		[Field("depowered position acceleration time:seconds", null)]
		public float DepoweredPositionAccelerationTime7;
		[Field("lightmap flags", typeof(LightmapFlags8Options))]
		public short LightmapFlags8;
		[Field("", null)]
		public fixed byte _9[2];
		[Field("", null)]
		public fixed byte _10[4];
		[Field("open (up)", null)]
		public TagReference OpenUp11;
		[Field("close (down)", null)]
		public TagReference CloseDown12;
		[Field("opened", null)]
		public TagReference Opened13;
		[Field("closed", null)]
		public TagReference Closed14;
		[Field("depowered", null)]
		public TagReference Depowered15;
		[Field("repowered", null)]
		public TagReference Repowered16;
		[Field("delay time:seconds", null)]
		public float DelayTime17;
		[Field("", null)]
		public fixed byte _18[8];
		[Field("delay effect", null)]
		public TagReference DelayEffect19;
		[Field("automatic activation radius:world units", null)]
		public float AutomaticActivationRadius20;
		[Field("", null)]
		public fixed byte _21[112];
	}
}
#pragma warning restore CS1591
