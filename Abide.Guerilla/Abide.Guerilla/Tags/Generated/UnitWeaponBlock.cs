using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct UnitWeaponBlock
	{
		[Field("weapon^", null)]
		public TagReference Weapon0;
		[Field("", null)]
		public fixed byte _1[20];
	}
}
#pragma warning restore CS1591
