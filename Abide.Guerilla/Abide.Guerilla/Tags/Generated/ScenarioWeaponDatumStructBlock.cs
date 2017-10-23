using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct ScenarioWeaponDatumStructBlock
	{
		public enum Flags2Options
		{
			InitiallyAtRestDoesNotFall_0 = 1,
			Obsolete_1 = 2,
			DoesAccelerateMovesDueToExplosions_2 = 4,
		}
		[Field("Rounds Left", null)]
		public short RoundsLeft0;
		[Field("Rounds Loaded", null)]
		public short RoundsLoaded1;
		[Field("Flags", typeof(Flags2Options))]
		public int Flags2;
	}
}
#pragma warning restore CS1591
