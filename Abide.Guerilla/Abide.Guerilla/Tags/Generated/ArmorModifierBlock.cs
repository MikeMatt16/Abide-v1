using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct ArmorModifierBlock
	{
		[Field("name^", null)]
		public StringId Name0;
		[Field("damage multiplier", null)]
		public float DamageMultiplier1;
	}
}
#pragma warning restore CS1591
