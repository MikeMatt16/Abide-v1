using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct MoppsBlock
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
		public fixed byte _4[2];
		[Field("list*", null)]
		public short List5;
		[Field("code offset", null)]
		public int CodeOffset6;
	}
}
#pragma warning restore CS1591
