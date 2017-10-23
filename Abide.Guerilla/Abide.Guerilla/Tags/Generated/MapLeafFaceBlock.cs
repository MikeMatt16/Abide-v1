using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct MapLeafFaceBlock
	{
		[Field("node index*", null)]
		public int NodeIndex0;
		[Field("vertices*", null)]
		[Block("Map Leaf Face Vertex Block", 64, typeof(MapLeafFaceVertexBlock))]
		public TagBlock Vertices1;
	}
}
#pragma warning restore CS1591
