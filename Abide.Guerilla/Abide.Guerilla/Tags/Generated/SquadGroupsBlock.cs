using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(36, 4)]
	public unsafe struct SquadGroupsBlock
	{
		[Field("name^", null)]
		public String Name0;
		[Field("", null)]
		public fixed byte _1[48];
		[Field("parent", null)]
		public short Parent2;
		[Field("initial orders", null)]
		public short InitialOrders3;
		[Field("", null)]
		public fixed byte _4[48];
	}
}
#pragma warning restore CS1591
