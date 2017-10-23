using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(60, 4)]
	public unsafe struct PlanarFogPatchyFogBlock
	{
		[Field("color", null)]
		public ColorRgbF Color0;
		[Field("", null)]
		public fixed byte _1[12];
		[Field("density:[0,1]", null)]
		public FloatBounds Density2;
		[Field("distance:world units", null)]
		public FloatBounds Distance3;
		[Field("", null)]
		public fixed byte _4[16];
		[Field("min depth fraction:[0,1]#in range (0,max_depth) world units, where patchy fog starts fading in", null)]
		public float MinDepthFraction5;
		[Field("", null)]
		public fixed byte _6[12];
		[Field("patchy fog", null)]
		public TagReference PatchyFog7;
	}
}
#pragma warning restore CS1591
