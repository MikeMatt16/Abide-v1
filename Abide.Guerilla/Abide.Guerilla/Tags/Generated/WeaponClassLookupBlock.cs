using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct WeaponClassLookupBlock
	{
		[Field("weapon name^", null)]
		public StringId WeaponName0;
		[Field("weapon class", null)]
		public StringId WeaponClass1;
	}
}
#pragma warning restore CS1591
