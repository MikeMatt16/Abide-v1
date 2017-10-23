using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct LightmapInstanceBucketReferenceBlock
	{
		[Field("flags", null)]
		public short Flags0;
		[Field("bucket index", null)]
		public short BucketIndex1;
		[Field("section offsets", null)]
		[Block("Lightmap Instance Bucket Section Offset Block", 255, typeof(LightmapInstanceBucketSectionOffsetBlock))]
		public TagBlock SectionOffsets2;
	}
}
#pragma warning restore CS1591
