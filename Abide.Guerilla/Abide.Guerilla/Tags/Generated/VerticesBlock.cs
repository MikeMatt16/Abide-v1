using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 16)]
	public unsafe struct VerticesBlock
	{
		public Vector3 Point0;
		[Field("First Edge*", null)]
		public short FirstEdge1;
		[Field("", null)]
		public fixed byte _2[2];
	}
}
#pragma warning restore CS1591
