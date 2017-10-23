using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(64, 4)]
	public unsafe struct ZoneBlock
	{
		public enum Flags1Options
		{
			ManualBspIndex_0 = 1,
		}
		[Field("name^", null)]
		public String Name0;
		[Field("flags", typeof(Flags1Options))]
		public int Flags1;
		[Field("manual bsp", null)]
		public short ManualBsp2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("", null)]
		public fixed byte _4[24];
		[Field("firing positions", null)]
		[Block("Firing Positions Block", 512, typeof(FiringPositionsBlock))]
		public TagBlock FiringPositions5;
		[Field("areas", null)]
		[Block("Areas Block", 64, typeof(AreasBlock))]
		public TagBlock Areas6;
	}
}
#pragma warning restore CS1591
