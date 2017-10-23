using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(100, 4)]
	public unsafe struct CollisionModelBspBlock
	{
		[Field("node index*", null)]
		public short NodeIndex0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("", null)]
		public fixed byte _2[16];
		[Field("bsp*", typeof(GlobalCollisionBspStructBlock))]
		[Block("Global Collision Bsp Struct", 1, typeof(GlobalCollisionBspStructBlock))]
		public GlobalCollisionBspStructBlock Bsp3;
	}
}
#pragma warning restore CS1591
