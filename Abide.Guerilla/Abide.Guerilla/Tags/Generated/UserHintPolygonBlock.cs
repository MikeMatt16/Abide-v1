using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct UserHintPolygonBlock
	{
		public enum Flags0Options
		{
			Bidirectional_0 = 1,
			Closed_1 = 2,
		}
		[Field("Flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("Points", null)]
		[Block("User Hint Point Block", 200, typeof(UserHintPointBlock))]
		public TagBlock Points1;
	}
}
#pragma warning restore CS1591
