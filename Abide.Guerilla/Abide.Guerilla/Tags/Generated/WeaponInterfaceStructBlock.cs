using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(44, 4)]
	public unsafe struct WeaponInterfaceStructBlock
	{
		[Field("shared interface", typeof(WeaponSharedInterfaceStructBlock))]
		[Block("Weapon Shared Interface Struct", 1, typeof(WeaponSharedInterfaceStructBlock))]
		public WeaponSharedInterfaceStructBlock SharedInterface1;
		[Field("first person", null)]
		[Block("Weapon First Person Interface Block", 4, typeof(WeaponFirstPersonInterfaceBlock))]
		public TagBlock FirstPerson2;
		[Field("new hud interface", null)]
		public TagReference NewHudInterface3;
	}
}
#pragma warning restore CS1591
