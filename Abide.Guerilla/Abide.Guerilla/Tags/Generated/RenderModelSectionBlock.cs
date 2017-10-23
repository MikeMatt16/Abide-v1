using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(104, 4)]
	public unsafe struct RenderModelSectionBlock
	{
		public enum GlobalGeometryClassificationEnumDefinition0Options
		{
			Worldspace_0 = 0,
			Rigid_1 = 1,
			RigidBoned_2 = 2,
			Skinned_3 = 3,
			UnsupportedReimport_4 = 4,
		}
		public enum Flags4Options
		{
			GeometryPostprocessed_0 = 1,
		}
		[Field("global_geometry_classification_enum_definition", typeof(GlobalGeometryClassificationEnumDefinition0Options))]
		public short GlobalGeometryClassificationEnumDefinition0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("section info*", typeof(GlobalGeometrySectionInfoStructBlock))]
		[Block("Global Geometry Section Info Struct", 1, typeof(GlobalGeometrySectionInfoStructBlock))]
		public GlobalGeometrySectionInfoStructBlock SectionInfo2;
		[Field("rigid node*", null)]
		public short RigidNode3;
		[Field("flags", typeof(Flags4Options))]
		public short Flags4;
		[Field("section data*", null)]
		[Block("Render Model Section Data Block", 1, typeof(RenderModelSectionDataBlock))]
		public TagBlock SectionData5;
		[Field("geometry block info*", typeof(GlobalGeometryBlockInfoStructBlock))]
		[Block("Global Geometry Block Info Struct", 1, typeof(GlobalGeometryBlockInfoStructBlock))]
		public GlobalGeometryBlockInfoStructBlock GeometryBlockInfo6;
	}
}
#pragma warning restore CS1591
