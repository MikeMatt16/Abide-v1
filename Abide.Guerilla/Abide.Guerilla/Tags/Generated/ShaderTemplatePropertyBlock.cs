using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct ShaderTemplatePropertyBlock
	{
		public enum Property0Options
		{
			Unused_0 = 0,
			DiffuseMap_1 = 1,
			LightmapEmissiveMap_2 = 2,
			LightmapEmissiveColor_3 = 3,
			LightmapEmissivePower_4 = 4,
			LightmapResolutionScale_5 = 5,
			LightmapHalfLife_6 = 6,
			LightmapDiffuseScale_7 = 7,
			LightmapAlphaTestMap_8 = 8,
			LightmapTranslucentMap_9 = 9,
			LightmapTranslucentColor_10 = 10,
			LightmapTranslucentAlpha_11 = 11,
			ActiveCamoMap_12 = 12,
			LightmapFoliageScale_13 = 13,
		}
		[Field("Property^", typeof(Property0Options))]
		public short Property0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("Parameter Name", null)]
		public StringId ParameterName2;
	}
}
#pragma warning restore CS1591
