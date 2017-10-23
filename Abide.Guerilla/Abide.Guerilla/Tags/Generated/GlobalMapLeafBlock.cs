using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct GlobalMapLeafBlock
	{
		[Field("faces*", null)]
		[Block("Map Leaf Face Block", 512, typeof(MapLeafFaceBlock))]
		public TagBlock Faces0;
		[Field("connection indices*", null)]
		[Block("Map Leaf Connection Index Block", 512, typeof(MapLeafConnectionIndexBlock))]
		public TagBlock ConnectionIndices1;
	}
}
#pragma warning restore CS1591
