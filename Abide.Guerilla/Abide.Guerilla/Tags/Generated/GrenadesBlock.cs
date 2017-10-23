using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(68, 4)]
	public unsafe struct GrenadesBlock
	{
		[Field("maximum count", null)]
		public short MaximumCount0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("throwing effect", null)]
		public TagReference ThrowingEffect2;
		[Field("", null)]
		public fixed byte _3[16];
		[Field("equipment", null)]
		public TagReference Equipment4;
		[Field("projectile", null)]
		public TagReference Projectile5;
	}
}
#pragma warning restore CS1591
