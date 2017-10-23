using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(108, 4)]
	public unsafe struct PrtInfoBlock
	{
		[Field("SH Order*", null)]
		public short SHOrder0;
		[Field("num of clusters*", null)]
		public short NumOfClusters1;
		[Field("pca vectors per cluster*", null)]
		public short PcaVectorsPerCluster2;
		[Field("number of rays*", null)]
		public short NumberOfRays3;
		[Field("number of bounces*", null)]
		public short NumberOfBounces4;
		[Field("mat index for sbsfc scattering*", null)]
		public short MatIndexForSbsfcScattering5;
		[Field("length scale*", null)]
		public float LengthScale6;
		[Field("number of lods in model*", null)]
		public short NumberOfLodsInModel7;
		[Field("", null)]
		public fixed byte _8[2];
		[Field("lod info*", null)]
		[Block("Prt Lod Info", 6, typeof(PrtLodInfoBlock))]
		public TagBlock LodInfo9;
		[Field("cluster basis*", null)]
		[Block("Cluster Basis", 34560, typeof(PrtClusterBasisBlock))]
		public TagBlock ClusterBasis10;
		[Field("raw_pca_data*", null)]
		[Block("Raw Pca Data", 150405120, typeof(PrtRawPcaDataBlock))]
		public TagBlock RawPcaData11;
		[Field("vertex buffers*", null)]
		[Block("Vertex Buffers", 255, typeof(PrtVertexBuffersBlock))]
		public TagBlock VertexBuffers12;
		[Field("geometry block info*", typeof(GlobalGeometryBlockInfoStructBlock))]
		[Block("Global Geometry Block Info Struct", 1, typeof(GlobalGeometryBlockInfoStructBlock))]
		public GlobalGeometryBlockInfoStructBlock GeometryBlockInfo13;
	}
}
#pragma warning restore CS1591
