using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(12, 4)]
	public unsafe struct DamageDirectionBlock
	{
		[Field("regions*|AABBCC", null)]
		[Block("Damage Region Block", 11, typeof(DamageRegionBlock))]
		public TagBlock RegionsAABBCC0;
	}
}
#pragma warning restore CS1591
