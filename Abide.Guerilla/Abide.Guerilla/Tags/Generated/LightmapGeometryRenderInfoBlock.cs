using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(4, 4)]
	public unsafe struct LightmapGeometryRenderInfoBlock
	{
		[Field("bitmap index", null)]
		public short BitmapIndex0;
		[Field("palette index", null)]
		public int PaletteIndex1;
		[Field("", null)]
		public fixed byte _2[1];
	}
}
#pragma warning restore CS1591
