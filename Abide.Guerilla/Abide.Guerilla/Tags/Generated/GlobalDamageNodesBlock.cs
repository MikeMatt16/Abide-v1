using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct GlobalDamageNodesBlock
	{
		[Field("", null)]
		public fixed byte _0[2];
		[Field("", null)]
		public fixed byte _1[2];
		[Field("", null)]
		public fixed byte _2[12];
	}
}
#pragma warning restore CS1591
