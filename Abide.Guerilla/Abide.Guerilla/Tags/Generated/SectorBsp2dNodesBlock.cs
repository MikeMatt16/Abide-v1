using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct SectorBsp2dNodesBlock
	{
		[Field("plane*", null)]
		public Plane2 Plane0;
		[Field("left child*", null)]
		public int LeftChild1;
		[Field("right child*", null)]
		public int RightChild2;
	}
}
#pragma warning restore CS1591
