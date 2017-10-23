using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(108, 4)]
	public unsafe struct UserHintBlock
	{
		[Field("", null)]
		public fixed byte _0[48];
		[Field("point geometry", null)]
		[Block("User Hint Point Block", 200, typeof(UserHintPointBlock))]
		public TagBlock PointGeometry1;
		[Field("ray geometry", null)]
		[Block("User Hint Ray Block", 200, typeof(UserHintRayBlock))]
		public TagBlock RayGeometry2;
		[Field("line segment geometry", null)]
		[Block("User Hint Line Segment Block", 200, typeof(UserHintLineSegmentBlock))]
		public TagBlock LineSegmentGeometry3;
		[Field("parallelogram geometry", null)]
		[Block("User Hint Parallelogram Block", 200, typeof(UserHintParallelogramBlock))]
		public TagBlock ParallelogramGeometry4;
		[Field("polygon geometry", null)]
		[Block("User Hint Polygon Block", 200, typeof(UserHintPolygonBlock))]
		public TagBlock PolygonGeometry5;
		[Field("", null)]
		public fixed byte _6[48];
		[Field("jump hints", null)]
		[Block("User Hint Jump Block", 200, typeof(UserHintJumpBlock))]
		public TagBlock JumpHints7;
		[Field("climb hints", null)]
		[Block("User Hint Climb Block", 200, typeof(UserHintClimbBlock))]
		public TagBlock ClimbHints8;
		[Field("well hints", null)]
		[Block("User Hint Well Block", 200, typeof(UserHintWellBlock))]
		public TagBlock WellHints9;
		[Field("flight hints", null)]
		[Block("User Hint Flight Block", 50, typeof(UserHintFlightBlock))]
		public TagBlock FlightHints10;
	}
}
#pragma warning restore CS1591
