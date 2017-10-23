using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct UserHintWellBlock
	{
		public enum Flags0Options
		{
			Bidirectional_0 = 1,
		}
		[Field("flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("points", null)]
		[Block("User Hint Well Point Block", 200, typeof(UserHintWellPointBlock))]
		public TagBlock Points1;
	}
}
#pragma warning restore CS1591
