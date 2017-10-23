using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct DamageGroupBlock
	{
		[Field("name^", null)]
		public StringId Name0;
		[Field("armor modifiers", null)]
		[Block("Armor Modifier Block", 2147483647, typeof(ArmorModifierBlock))]
		public TagBlock ArmorModifiers1;
	}
}
#pragma warning restore CS1591
