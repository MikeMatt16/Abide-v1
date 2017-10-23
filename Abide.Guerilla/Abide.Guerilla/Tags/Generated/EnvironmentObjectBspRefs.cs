using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct EnvironmentObjectBspRefs
	{
		[Field("bsp reference*", null)]
		public int BspReference0;
		[Field("first sector*", null)]
		public int FirstSector1;
		[Field("last sector*", null)]
		public int LastSector2;
		[Field("node_index*", null)]
		public short NodeIndex3;
		[Field("", null)]
		public fixed byte _4[2];
	}
}
#pragma warning restore CS1591
