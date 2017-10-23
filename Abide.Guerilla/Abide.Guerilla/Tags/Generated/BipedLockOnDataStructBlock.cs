using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct BipedLockOnDataStructBlock
	{
		public enum Flags1Options
		{
			LockedByHumanTargeting_0 = 1,
			LockedByPlasmaTargeting_1 = 2,
			AlwaysLockedByPlasmaTargeting_2 = 4,
		}
		[Field("flags", typeof(Flags1Options))]
		public int Flags1;
		[Field("lock on distance", null)]
		public float LockOnDistance2;
	}
}
#pragma warning restore CS1591
