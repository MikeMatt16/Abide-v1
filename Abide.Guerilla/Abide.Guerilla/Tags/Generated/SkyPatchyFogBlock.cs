using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(88, 4)]
	public unsafe struct SkyPatchyFogBlock
	{
		[Field("Color", null)]
		public ColorRgbF Color0;
		[Field("", null)]
		public fixed byte _1[12];
		[Field("Density:[0,1]", null)]
		public FloatBounds Density2;
		[Field("Distance:world units", null)]
		public FloatBounds Distance3;
		[Field("", null)]
		public fixed byte _4[32];
		[Field("Patchy Fog", null)]
		public TagReference PatchyFog5;
	}
}
#pragma warning restore CS1591
