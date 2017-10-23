using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct UserHintJumpBlock
	{
		public enum Flags0Options
		{
			Bidirectional_0 = 1,
			Closed_1 = 2,
		}
		public enum ForceJumpHeight2Options
		{
			NONE_0 = 0,
			Down_1 = 1,
			Step_2 = 2,
			Crouch_3 = 3,
			Stand_4 = 4,
			Storey_5 = 5,
			Tower_6 = 6,
			Infinite_7 = 7,
		}
		public enum ControlFlags3Options
		{
			MagicLift_0 = 1,
		}
		[Field("Flags", typeof(Flags0Options))]
		public short Flags0;
		[Field("geometry index*", null)]
		public short GeometryIndex1;
		[Field("force jump height", typeof(ForceJumpHeight2Options))]
		public short ForceJumpHeight2;
		[Field("control flags", typeof(ControlFlags3Options))]
		public short ControlFlags3;
	}
}
#pragma warning restore CS1591
