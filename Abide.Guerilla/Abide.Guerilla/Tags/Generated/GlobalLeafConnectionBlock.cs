using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(28, 4)]
	public unsafe struct GlobalLeafConnectionBlock
	{
		[Field("plane index*", null)]
		public int PlaneIndex0;
		[Field("back leaf index*", null)]
		public int BackLeafIndex1;
		[Field("front leaf index*", null)]
		public int FrontLeafIndex2;
		[Field("vertices*", null)]
		[Block("Leaf Connection Vertex Block", 64, typeof(LeafConnectionVertexBlock))]
		public TagBlock Vertices3;
		[Field("area*", null)]
		public float Area4;
	}
}
#pragma warning restore CS1591
