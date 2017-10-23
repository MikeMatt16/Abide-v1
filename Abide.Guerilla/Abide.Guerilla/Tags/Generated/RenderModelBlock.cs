using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("render_model", "mode", "����", typeof(RenderModelBlock))]
	[FieldSet(184, 4)]
	public unsafe struct RenderModelBlock
	{
		public enum Flags1Options
		{
			RenderModelForceThirdPersonBit_0 = 1,
			ForceCarmackReverse_1 = 2,
			ForceNodeMaps_2 = 4,
			GeometryPostprocessed_3 = 8,
		}
		[Field("flags*", typeof(Flags1Options))]
		public short Flags1;
		[Field("", null)]
		public fixed byte _2[2];
		[Field("", null)]
		public fixed byte _3[4];
		[Field("import info*", null)]
		[Block("Import Info", 1, typeof(GlobalTagImportInfoBlock))]
		public TagBlock ImportInfo4;
		[Field("compression info*", null)]
		[Block("Compression Info", 1, typeof(GlobalGeometryCompressionInfoBlock))]
		public TagBlock CompressionInfo5;
		[Field("regions*", null)]
		[Block("Region", 16, typeof(RenderModelRegionBlock))]
		public TagBlock Regions6;
		[Field("sections*", null)]
		[Block("Render Model Section Block", 255, typeof(RenderModelSectionBlock))]
		public TagBlock Sections7;
		[Field("invalid section pair bits*", null)]
		[Block("Bitvector", 1013, typeof(RenderModelInvalidSectionPairsBlock))]
		public TagBlock InvalidSectionPairBits8;
		[Field("section groups*", null)]
		[Block("Section Group", 6, typeof(RenderModelSectionGroupBlock))]
		public TagBlock SectionGroups9;
		[Field("L1 section group index*:(super low)", null)]
		public int L1SectionGroupIndex10;
		[Field("L2 section group index*:(low)", null)]
		public int L2SectionGroupIndex11;
		[Field("L3 section group index*:(medium)", null)]
		public int L3SectionGroupIndex12;
		[Field("L4 section group index*:(high)", null)]
		public int L4SectionGroupIndex13;
		[Field("L5 section group index*:(super high)", null)]
		public int L5SectionGroupIndex14;
		[Field("L6 section group index*:(hollywood)", null)]
		public int L6SectionGroupIndex15;
		[Field("", null)]
		public fixed byte _16[2];
		[Field("node list checksum*", null)]
		public int NodeListChecksum17;
		[Field("nodes*", null)]
		[Block("Node", 255, typeof(RenderModelNodeBlock))]
		public TagBlock Nodes18;
		[Field("node map (OLD)*", null)]
		[Block("Index", 640, typeof(RenderModelNodeMapBlockOLD))]
		public TagBlock NodeMapOLD19;
		[Field("marker groups*", null)]
		[Block("Marker Group", 4096, typeof(RenderModelMarkerGroupBlock))]
		public TagBlock MarkerGroups20;
		[Field("materials*", null)]
		[Block("Material", 1024, typeof(GlobalGeometryMaterialBlock))]
		public TagBlock Materials21;
		[Field("errors*", null)]
		[Block("Error Report Category", 64, typeof(GlobalErrorReportCategoriesBlock))]
		public TagBlock Errors22;
		[Field("don't draw over camera cosine angle#dont draw fp model when camera > this angle cosine (-1,1) Sugg. -0.2. 0 disables.", null)]
		public float DonTDrawOverCameraCosineAngle23;
		[Field("PRT info", null)]
		[Block("Prt Info Block", 1, typeof(PrtInfoBlock))]
		public TagBlock PRTInfo24;
		[Field("section render leaves", null)]
		[Block("Section Render Leaves Block", 255, typeof(SectionRenderLeavesBlock))]
		public TagBlock SectionRenderLeaves25;
	}
}
#pragma warning restore CS1591
