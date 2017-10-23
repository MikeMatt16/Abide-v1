using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("device_machine", "mach", "devi", typeof(DeviceMachineBlock))]
	[FieldSet(24, 4)]
	public unsafe struct DeviceMachineBlock
	{
		public enum Type1Options
		{
			Door_0 = 0,
			Platform_1 = 1,
			Gear_2 = 2,
		}
		public enum Flags2Options
		{
			PathfindingObstacle_0 = 1,
			ButNotWhenOpen_1 = 2,
			ElevatorLightingBasedOnWhatSAroundRatherThanWhatSBelow_2 = 4,
		}
		public enum CollisionResponse6Options
		{
			PauseUntilCrushed_0 = 0,
			ReverseDirections_1 = 1,
		}
		public enum PathfindingPolicy9Options
		{
			Discs_0 = 0,
			Sectors_1 = 1,
			CutOut_2 = 2,
			None_3 = 3,
		}
		[Field("type", typeof(Type1Options))]
		public short Type1;
		[Field("flags", typeof(Flags2Options))]
		public short Flags2;
		[Field("door open time:seconds", null)]
		public float DoorOpenTime3;
		[Field("door occlusion bounds#maps position [0,1] to occlusion", null)]
		public FloatBounds DoorOcclusionBounds4;
		[Field("", null)]
		public fixed byte _5[72];
		[Field("collision response", typeof(CollisionResponse6Options))]
		public short CollisionResponse6;
		[Field("elevator node", null)]
		public short ElevatorNode7;
		[Field("", null)]
		public fixed byte _8[68];
		[Field("pathfinding policy", typeof(PathfindingPolicy9Options))]
		public short PathfindingPolicy9;
		[Field("", null)]
		public fixed byte _10[2];
	}
}
#pragma warning restore CS1591
