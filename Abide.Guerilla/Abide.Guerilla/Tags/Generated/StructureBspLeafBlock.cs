using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct StructureBspLeafBlock
	{
		[Field("Cluster*", null)]
		public short Cluster0;
		[Field("Surface Reference Count*", null)]
		public short SurfaceReferenceCount1;
		[Field("First Surface Reference Index*", null)]
		public int FirstSurfaceReferenceIndex2;
	}
}
#pragma warning restore CS1591
