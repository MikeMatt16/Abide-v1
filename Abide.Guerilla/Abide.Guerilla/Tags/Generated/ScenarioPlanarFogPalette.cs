using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct ScenarioPlanarFogPalette
	{
		[Field("Name^", null)]
		public StringId Name0;
		[Field("Planar Fog", null)]
		public TagReference PlanarFog1;
		[Field("", null)]
		public fixed byte _2[2];
		[Field("", null)]
		public fixed byte _3[2];
	}
}
#pragma warning restore CS1591
