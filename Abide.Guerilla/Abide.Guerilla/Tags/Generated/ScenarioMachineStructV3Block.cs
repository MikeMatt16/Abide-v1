using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct ScenarioMachineStructV3Block
	{
		public enum Flags0Options
		{
			DoesNotOperateAutomatically_0 = 1,
			OneSided_1 = 2,
			NeverAppearsLocked_2 = 4,
			OpenedByMeleeAttack_3 = 8,
			OneSidedForPlayer_4 = 16,
			DoesNotCloseAutomatically_5 = 32,
		}
		[Field("Flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("Pathfinding References*", null)]
		[Block("Pathfinding Object Index List Block", 16, typeof(PathfindingObjectIndexListBlock))]
		public TagBlock PathfindingReferences1;
	}
}
#pragma warning restore CS1591
