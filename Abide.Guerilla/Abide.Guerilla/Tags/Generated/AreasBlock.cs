using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(140, 4)]
	public unsafe struct AreasBlock
	{
		public enum AreaFlags1Options
		{
			VehicleArea_0 = 1,
			Perch_1 = 2,
			ManualReferenceFrame_2 = 4,
		}
		[Field("name^`", null)]
		public String Name0;
		[Field("area flags", typeof(AreaFlags1Options))]
		public int AreaFlags1;
		[Field("", null)]
		public fixed byte _2[20];
		[Field("", null)]
		public fixed byte _3[4];
		[Field("", null)]
		public fixed byte _4[64];
		[Field("manual reference frame", null)]
		public short ManualReferenceFrame5;
		[Field("", null)]
		public fixed byte _6[2];
		[Field("flight_hints", null)]
		[Block("Flight Reference Block", 10, typeof(FlightReferenceBlock))]
		public TagBlock FlightHints7;
	}
}
#pragma warning restore CS1591
