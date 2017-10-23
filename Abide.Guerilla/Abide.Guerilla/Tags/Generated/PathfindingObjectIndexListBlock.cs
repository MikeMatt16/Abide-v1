using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(4, 4)]
	public unsafe struct PathfindingObjectIndexListBlock
	{
		[Field("BSP Index", null)]
		public short BSPIndex0;
		[Field("Pathfinding Object Index", null)]
		public short PathfindingObjectIndex1;
	}
}
#pragma warning restore CS1591
