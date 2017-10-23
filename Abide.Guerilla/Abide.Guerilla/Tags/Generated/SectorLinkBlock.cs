using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct SectorLinkBlock
	{
		public enum LinkFlags2Options
		{
			SectorLinkFromCollisionEdge_0 = 1,
			SectorIntersectionLink_1 = 2,
			SectorLinkBsp2dCreationError_2 = 4,
			SectorLinkTopologyError_3 = 8,
			SectorLinkChainError_4 = 16,
			SectorLinkBothSectorsWalkable_5 = 32,
			SectorLinkMagicHangingLink_6 = 64,
			SectorLinkThreshold_7 = 128,
			SectorLinkCrouchable_8 = 256,
			SectorLinkWallBase_9 = 512,
			SectorLinkLedge_10 = 1024,
			SectorLinkLeanable_11 = 2048,
			SectorLinkStartCorner_12 = 4096,
			SectorLinkEndCorner_13 = 8192,
		}
		[Field("vertex 1", null)]
		public short Vertex10;
		[Field("vertex 2", null)]
		public short Vertex21;
		[Field("link flags", typeof(LinkFlags2Options))]
		public short LinkFlags2;
		[Field("hint index", null)]
		public short HintIndex3;
		[Field("forward link", null)]
		public short ForwardLink4;
		[Field("reverse link", null)]
		public short ReverseLink5;
		[Field("left sector", null)]
		public short LeftSector6;
		[Field("right sector", null)]
		public short RightSector7;
	}
}
#pragma warning restore CS1591
