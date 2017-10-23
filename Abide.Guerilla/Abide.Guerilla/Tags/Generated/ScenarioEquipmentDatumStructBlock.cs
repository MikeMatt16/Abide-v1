using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(4, 4)]
	public unsafe struct ScenarioEquipmentDatumStructBlock
	{
		public enum EquipmentFlags0Options
		{
			InitiallyAtRestDoesNotFall_0 = 1,
			Obsolete_1 = 2,
			DoesAccelerateMovesDueToExplosions_2 = 4,
		}
		[Field("Equipment Flags", typeof(EquipmentFlags0Options))]
		public int EquipmentFlags0;
	}
}
#pragma warning restore CS1591
