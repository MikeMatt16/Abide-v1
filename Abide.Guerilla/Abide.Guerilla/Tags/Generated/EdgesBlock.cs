using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(12, 4)]
	public unsafe struct EdgesBlock
	{
		[Field("Start Vertex*", null)]
		public short StartVertex0;
		[Field("End Vertex*", null)]
		public short EndVertex1;
		[Field("Forward Edge*", null)]
		public short ForwardEdge2;
		[Field("Reverse Edge*", null)]
		public short ReverseEdge3;
		[Field("Left Surface*", null)]
		public short LeftSurface4;
		[Field("Right Surface*", null)]
		public short RightSurface5;
	}
}
#pragma warning restore CS1591
