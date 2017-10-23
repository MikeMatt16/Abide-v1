using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(32, 4)]
	public unsafe struct PhantomsBlock
	{
		[Field("", null)]
		public fixed byte _0[4];
		[Field("size*", null)]
		public short Size1;
		[Field("count*", null)]
		public short Count2;
		[Field("", null)]
		public fixed byte _3[4];
		[Field("", null)]
		public fixed byte _4[4];
		[Field("", null)]
		public fixed byte _5[4];
		[Field("", null)]
		public fixed byte _6[4];
		[Field("size*", null)]
		public short Size7;
		[Field("count*", null)]
		public short Count8;
		[Field("", null)]
		public fixed byte _9[4];
	}
}
#pragma warning restore CS1591
