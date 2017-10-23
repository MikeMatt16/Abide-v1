using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct ScenarioDeviceStructBlock
	{
		public enum Flags2Options
		{
			InitiallyOpen10_0 = 1,
			InitiallyOff00_1 = 2,
			CanChangeOnlyOnce_2 = 4,
			PositionReversed_3 = 8,
			NotUsableFromAnySide_4 = 16,
		}
		[Field("Power Group", null)]
		public short PowerGroup0;
		[Field("Position Group", null)]
		public short PositionGroup1;
		[Field("Flags", typeof(Flags2Options))]
		public int Flags2;
	}
}
#pragma warning restore CS1591
