using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct SectorBlock
	{
		public enum PathFindingSectorFlags0Options
		{
			SectorWalkable_0 = 1,
			SectorBreakable_1 = 2,
			SectorMobile_2 = 4,
			SectorBspSource_3 = 8,
			Floor_4 = 16,
			Ceiling_5 = 32,
			WallNorth_6 = 64,
			WallSouth_7 = 128,
			WallEast_8 = 256,
			WallWest_9 = 512,
			Crouchable_10 = 1024,
			Aligned_11 = 2048,
			SectorStep_12 = 4096,
			SectorInterior_13 = 8192,
		}
		[Field("Path-finding sector flags", typeof(PathFindingSectorFlags0Options))]
		public short PathFindingSectorFlags0;
		[Field("hint index", null)]
		public short HintIndex1;
		[Field("first link (do not set manually)", null)]
		public int FirstLinkDoNotSetManually2;
	}
}
#pragma warning restore CS1591
