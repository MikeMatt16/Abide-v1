using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(1024, 64)]
	public unsafe struct StructureLightmapPaletteColorBlock
	{
		[Field("FIRST palette color*", null)]
		public int FIRSTPaletteColor0;
		[Field("", null)]
		public fixed byte _1[1020];
	}
}
#pragma warning restore CS1591
