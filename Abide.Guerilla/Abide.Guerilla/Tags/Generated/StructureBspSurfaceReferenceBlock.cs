using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct StructureBspSurfaceReferenceBlock
	{
		[Field("Strip Index*", null)]
		public short StripIndex0;
		[Field("Lightmap Triangle Index*", null)]
		public short LightmapTriangleIndex1;
		[Field("BSP Node Index*", null)]
		public int BSPNodeIndex2;
	}
}
#pragma warning restore CS1591
