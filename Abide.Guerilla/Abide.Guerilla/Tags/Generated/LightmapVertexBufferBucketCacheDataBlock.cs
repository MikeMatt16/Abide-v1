using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(12, 4)]
	public unsafe struct LightmapVertexBufferBucketCacheDataBlock
	{
		[Field("vertex buffers*", null)]
		[Block("Vertex Buffer", 512, typeof(GlobalGeometrySectionVertexBufferBlock))]
		public TagBlock VertexBuffers0;
	}
}
#pragma warning restore CS1591
