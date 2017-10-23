using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(36, 4)]
	public unsafe struct EnvironmentObjectRefs
	{
		public enum Flags0Options
		{
			Mobile_0 = 1,
		}
		[Field("flags", typeof(Flags0Options))]
		public short Flags0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("first sector*", null)]
		public int FirstSector2;
		[Field("last sector*", null)]
		public int LastSector3;
		[Field("bsps*", null)]
		[Block("Environment Object Bsp Refs", 1024, typeof(EnvironmentObjectBspRefs))]
		public TagBlock Bsps4;
		[Field("nodes*", null)]
		[Block("Environment Object Nodes", 255, typeof(EnvironmentObjectNodes))]
		public TagBlock Nodes5;
	}
}
#pragma warning restore CS1591
