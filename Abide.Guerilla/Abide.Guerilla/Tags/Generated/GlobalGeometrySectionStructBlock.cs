using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(108, 4)]
	public unsafe struct GlobalGeometrySectionStructBlock
	{
		[Field("Parts*", null)]
		[Block("Part", 255, typeof(GlobalGeometryPartBlockNew))]
		public TagBlock Parts0;
		[Field("Subparts*", null)]
		[Block("Subparts", 32768, typeof(GlobalSubpartsBlock))]
		public TagBlock Subparts1;
		[Field("Visibility Bounds*", null)]
		[Block("Visibility Bounds", 32768, typeof(GlobalVisibilityBoundsBlock))]
		public TagBlock VisibilityBounds2;
		[Field("Raw Vertices*", null)]
		[Block("Vertex", 32767, typeof(GlobalGeometrySectionRawVertexBlock))]
		public TagBlock RawVertices3;
		[Field("Strip Indices*", null)]
		[Block("Index", 65535, typeof(GlobalGeometrySectionStripIndexBlock))]
		public TagBlock StripIndices4;
		[Field("Visibility mopp Code*", null)]
		[Data(393216)]
		public TagBlock VisibilityMoppCode5;
		[Field("mopp Reorder Table*", null)]
		[Block("Index", 65535, typeof(GlobalGeometrySectionStripIndexBlock))]
		public TagBlock MoppReorderTable6;
		[Field("Vertex Buffers*", null)]
		[Block("Vertex Buffer", 512, typeof(GlobalGeometrySectionVertexBufferBlock))]
		public TagBlock VertexBuffers7;
		[Field("", null)]
		public fixed byte _8[4];
	}
}
#pragma warning restore CS1591
