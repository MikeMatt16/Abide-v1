using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(128, 16)]
	public unsafe struct CollisionBspPhysicsBlock
	{
		[Field("", null)]
		public fixed byte _0[4];
		[Field("Size*", null)]
		public short Size1;
		[Field("Count*", null)]
		public short Count2;
		[Field("", null)]
		public fixed byte _3[4];
		[Field("", null)]
		public fixed byte _4[4];
		[Field("", null)]
		public fixed byte _5[32];
		[Field("", null)]
		public fixed byte _6[16];
		[Field("", null)]
		public fixed byte _7[4];
		[Field("Size*", null)]
		public short Size8;
		[Field("Count*", null)]
		public short Count9;
		[Field("", null)]
		public fixed byte _10[4];
		[Field("", null)]
		public fixed byte _11[4];
		[Field("", null)]
		public fixed byte _12[4];
		[Field("Size*", null)]
		public short Size13;
		[Field("Count*", null)]
		public short Count14;
		[Field("", null)]
		public fixed byte _15[4];
		[Field("", null)]
		public fixed byte _16[8];
		[Field("mopp Code Data*", null)]
		[Data(1048576)]
		public TagBlock MoppCodeData17;
		[Field("", null)]
		public fixed byte _18[8];
	}
}
#pragma warning restore CS1591
