using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(4, 4)]
	public unsafe struct SpecialMovementBlock
	{
		public enum SpecialMovement10Options
		{
			Jump_0 = 1,
			Climb_1 = 2,
			Vault_2 = 4,
			Mount_3 = 8,
			Hoist_4 = 16,
			WallJump_5 = 32,
			NA_6 = 64,
		}
		[Field("Special movement 1", typeof(SpecialMovement10Options))]
		public int SpecialMovement10;
	}
}
#pragma warning restore CS1591
