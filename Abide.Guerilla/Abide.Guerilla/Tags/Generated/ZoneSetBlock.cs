using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct ZoneSetBlock
	{
		public enum AreaType0Options
		{
			Deault_0 = 0,
			Search_1 = 1,
			Goal_2 = 2,
		}
		[Field("area type", typeof(AreaType0Options))]
		public short AreaType0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("zone^", null)]
		public short Zone2;
		[Field("area^", null)]
		public short Area3;
	}
}
#pragma warning restore CS1591
