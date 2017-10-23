using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(4, 4)]
	public unsafe struct UnitAdditionalNodeNamesStructBlock
	{
		[Field("preferred_gun_node#if found, use this gun marker", null)]
		public StringId PreferredGunNode0;
	}
}
#pragma warning restore CS1591
