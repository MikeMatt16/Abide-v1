using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct GrenadeAndPowerupStructBlock
	{
		[Field("grenades", null)]
		[Block("Grenade Block", 20, typeof(GrenadeBlock))]
		public TagBlock Grenades0;
		[Field("powerups", null)]
		[Block("Powerup Block", 20, typeof(PowerupBlock))]
		public TagBlock Powerups1;
	}
}
#pragma warning restore CS1591
