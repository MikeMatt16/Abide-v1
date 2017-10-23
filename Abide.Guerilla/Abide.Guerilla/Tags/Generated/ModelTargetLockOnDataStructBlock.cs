using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct ModelTargetLockOnDataStructBlock
	{
		public enum Flags1Options
		{
			LockedByHumanTracking_0 = 1,
			LockedByPlasmaTracking_1 = 2,
			Headshot_2 = 4,
			Vulnerable_3 = 8,
			AlwasLockedByPlasmaTracking_4 = 16,
		}
		[Field("flags", typeof(Flags1Options))]
		public int Flags1;
		[Field("lock on distance", null)]
		public float LockOnDistance2;
	}
}
#pragma warning restore CS1591
