using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(12, 4)]
	public unsafe struct SectionRenderLeavesBlock
	{
		[Field("node render leaves*", null)]
		[Block("Node Render Leaves Block", 64, typeof(NodeRenderLeavesBlock))]
		public TagBlock NodeRenderLeaves0;
	}
}
#pragma warning restore CS1591
