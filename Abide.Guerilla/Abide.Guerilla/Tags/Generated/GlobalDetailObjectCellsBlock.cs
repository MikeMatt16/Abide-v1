using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(32, 4)]
	public unsafe struct GlobalDetailObjectCellsBlock
	{
		[Field("*", null)]
		public short Empty0;
		[Field("*", null)]
		public short Empty1;
		[Field("*", null)]
		public short Empty2;
		[Field("*", null)]
		public short Empty3;
		[Field("*", null)]
		public int Empty4;
		[Field("*", null)]
		public int Empty5;
		[Field("*", null)]
		public int Empty6;
		[Field("", null)]
		public fixed byte _7[12];
	}
}
#pragma warning restore CS1591
