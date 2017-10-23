using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(12, 4)]
	public unsafe struct GameGlobalsDamageBlock
	{
		[Field("damage groups", null)]
		[Block("Damage Group Block", 2147483647, typeof(DamageGroupBlock))]
		public TagBlock DamageGroups0;
	}
}
#pragma warning restore CS1591
