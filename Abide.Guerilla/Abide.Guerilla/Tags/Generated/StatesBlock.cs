using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(96, 4)]
	public unsafe struct StatesBlock
	{
		[Field("name^", null)]
		public String Name0;
		[Field("color", null)]
		public ColorRgbF Color1;
		[Field("counts as:neighbors", null)]
		public short CountsAs2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("initial placement weight", null)]
		public float InitialPlacementWeight4;
		[Field("", null)]
		public fixed byte _5[24];
		[Field("zero", null)]
		public short Zero6;
		[Field("one", null)]
		public short One7;
		[Field("two", null)]
		public short Two8;
		[Field("three", null)]
		public short Three9;
		[Field("four", null)]
		public short Four10;
		[Field("five", null)]
		public short Five11;
		[Field("six", null)]
		public short Six12;
		[Field("seven", null)]
		public short Seven13;
		[Field("eight", null)]
		public short Eight14;
		[Field("", null)]
		public fixed byte _15[2];
	}
}
#pragma warning restore CS1591
