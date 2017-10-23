using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct PathfindingHintsBlock
	{
		public enum HintType0Options
		{
			IntersectionLink_0 = 0,
			JumpLink_1 = 1,
			ClimbLink_2 = 2,
			VaultLink_3 = 3,
			MountLink_4 = 4,
			HoistLink_5 = 5,
			WallJumpLink_6 = 6,
			BreakableFloor_7 = 7,
		}
		[Field("hint type", typeof(HintType0Options))]
		public short HintType0;
		[Field("Next hint index", null)]
		public short NextHintIndex1;
		[Field("hint data 0*", null)]
		public short HintData02;
		[Field("hint data 1*", null)]
		public short HintData13;
		[Field("hint data 2*", null)]
		public short HintData24;
		[Field("hint data 3*", null)]
		public short HintData35;
		[Field("hint data 4*", null)]
		public short HintData46;
		[Field("hint data 5*", null)]
		public short HintData57;
		[Field("hint data 6*", null)]
		public short HintData68;
		[Field("hint data 7*", null)]
		public short HintData79;
	}
}
#pragma warning restore CS1591
