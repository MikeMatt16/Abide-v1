using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(108, 4)]
	public unsafe struct StructureBspClusterDataBlockNew
	{
		[Field("Section*", typeof(GlobalGeometrySectionStructBlock))]
		[Block("Global Geometry Section Struct", 1, typeof(GlobalGeometrySectionStructBlock))]
		public GlobalGeometrySectionStructBlock Section0;
	}
}
#pragma warning restore CS1591
