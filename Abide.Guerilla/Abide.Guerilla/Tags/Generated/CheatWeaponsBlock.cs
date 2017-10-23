using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct CheatWeaponsBlock
	{
		[Field("weapon^", null)]
		public TagReference Weapon0;
	}
}
#pragma warning restore CS1591
