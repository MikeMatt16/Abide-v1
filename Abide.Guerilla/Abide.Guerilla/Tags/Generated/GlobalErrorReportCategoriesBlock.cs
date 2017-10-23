using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(680, 4)]
	public unsafe struct GlobalErrorReportCategoriesBlock
	{
		public enum ReportType1Options
		{
			Silent_0 = 0,
			Comment_1 = 1,
			Warning_2 = 2,
			Error_3 = 3,
		}
		public enum Flags2Options
		{
			Rendered_0 = 1,
			TangentSpace_1 = 2,
			Noncritical_2 = 4,
			LightmapLight_3 = 8,
			ReportKeyIsValid_4 = 16,
		}
		[Field("Name^*", null)]
		public LongString Name0;
		[Field("Report Type*", typeof(ReportType1Options))]
		public short ReportType1;
		[Field("Flags*", typeof(Flags2Options))]
		public short Flags2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("", null)]
		public fixed byte _4[2];
		[Field("", null)]
		public fixed byte _5[404];
		[Field("Reports*", null)]
		[Block("Error Report", 1024, typeof(ErrorReportsBlock))]
		public TagBlock Reports6;
	}
}
#pragma warning restore CS1591
