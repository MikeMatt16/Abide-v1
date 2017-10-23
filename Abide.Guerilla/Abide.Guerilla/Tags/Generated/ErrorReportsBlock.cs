using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(644, 4)]
	public unsafe struct ErrorReportsBlock
	{
		public enum Type0Options
		{
			Silent_0 = 0,
			Comment_1 = 1,
			Warning_2 = 2,
			Error_3 = 3,
		}
		public enum Flags1Options
		{
			Rendered_0 = 1,
			TangentSpace_1 = 2,
			Noncritical_2 = 4,
			LightmapLight_3 = 8,
			ReportKeyIsValid_4 = 16,
		}
		[Field("Type*", typeof(Type0Options))]
		public short Type0;
		[Field("Flags*", typeof(Flags1Options))]
		public short Flags1;
		[Field("Text*", null)]
		[Data(8192)]
		public TagBlock Text2;
		[Field("Source Filename*", null)]
		public String SourceFilename3;
		[Field("Source Line Number*", null)]
		public int SourceLineNumber4;
		[Field("Vertices*", null)]
		[Block("Error Report Vertex", 1024, typeof(ErrorReportVerticesBlock))]
		public TagBlock Vertices5;
		[Field("Vectors*", null)]
		[Block("Error Report Vector", 1024, typeof(ErrorReportVectorsBlock))]
		public TagBlock Vectors6;
		[Field("Lines*", null)]
		[Block("Error Report Line", 1024, typeof(ErrorReportLinesBlock))]
		public TagBlock Lines7;
		[Field("Triangles*", null)]
		[Block("Error Report Triangle", 1024, typeof(ErrorReportTrianglesBlock))]
		public TagBlock Triangles8;
		[Field("Quads*", null)]
		[Block("Error Report Quad", 1024, typeof(ErrorReportQuadsBlock))]
		public TagBlock Quads9;
		[Field("Comments*", null)]
		[Block("Error Report Comment", 1024, typeof(ErrorReportCommentsBlock))]
		public TagBlock Comments10;
		[Field("", null)]
		public fixed byte _11[380];
		[Field("Report Key*", null)]
		public int ReportKey12;
		[Field("Node Index*", null)]
		public int NodeIndex13;
		[Field("Bounds x*", null)]
		public FloatBounds BoundsX14;
		[Field("Bounds y*", null)]
		public FloatBounds BoundsY15;
		[Field("Bounds z*", null)]
		public FloatBounds BoundsZ16;
		[Field("Color*", null)]
		public ColorArgbF Color17;
		[Field("", null)]
		public fixed byte _18[84];
	}
}
#pragma warning restore CS1591
