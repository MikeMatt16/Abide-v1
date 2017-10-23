using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct DamageAnimationBlock
	{
		[Field("label^", null)]
		public StringId Label0;
		[Field("directions*|AABBCC", null)]
		[Block("Damage Direction Block", 4, typeof(DamageDirectionBlock))]
		public TagBlock DirectionsAABBCC1;
	}
}
#pragma warning restore CS1591
