using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(32, 4)]
	public unsafe struct WeaponFirstPersonInterfaceBlock
	{
		[Field("first person model", null)]
		public TagReference FirstPersonModel0;
		[Field("first person animations", null)]
		public TagReference FirstPersonAnimations1;
	}
}
#pragma warning restore CS1591
