using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(2, 4)]
	public unsafe struct LightmapInstanceBucketSectionOffsetBlock
	{
		[Field("section offset*", null)]
		public short SectionOffset0;
	}
}
#pragma warning restore CS1591
