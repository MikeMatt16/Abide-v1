using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(4, 4)]
	public unsafe struct WeaponTrackingStructBlock
	{
		public enum TrackingType0Options
		{
			NoTracking_0 = 0,
			HumanTracking_1 = 1,
			PlasmaTracking_2 = 2,
		}
		[Field("tracking type", typeof(TrackingType0Options))]
		public short TrackingType0;
		[Field("", null)]
		public fixed byte _1[2];
	}
}
#pragma warning restore CS1591
