using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct BspSurfaceReferenceBlock
	{
		[Field("strip index*", null)]
		public short StripIndex0;
		[Field("lightmap triangle index*", null)]
		public short LightmapTriangleIndex1;
		[Field("bsp node index*", null)]
		public int BspNodeIndex2;
	}
}
#pragma warning restore CS1591
