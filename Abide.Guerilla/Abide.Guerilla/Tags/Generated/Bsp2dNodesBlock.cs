using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 16)]
	public unsafe struct Bsp2dNodesBlock
	{
		[Field("Plane*", null)]
		public Plane2 Plane0;
		[Field("Left Child*", null)]
		public short LeftChild1;
		[Field("Right Child*", null)]
		public short RightChild2;
	}
}
#pragma warning restore CS1591
