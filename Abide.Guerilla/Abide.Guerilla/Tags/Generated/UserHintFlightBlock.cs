using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(12, 4)]
	public unsafe struct UserHintFlightBlock
	{
		[Field("", null)]
		public fixed byte _0[4];
		[Field("points", null)]
		[Block("User Hint Flight Point Block", 10, typeof(UserHintFlightPointBlock))]
		public TagBlock Points1;
	}
}
#pragma warning restore CS1591
