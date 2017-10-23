using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct RenderModelMarkerGroupBlock
	{
		[Field("markers*", null)]
		[Block("Marker", 256, typeof(RenderModelMarkerBlock))]
		public TagBlock Markers1;
	}
}
#pragma warning restore CS1591
