using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(32, 4)]
	public unsafe struct ShaderTemplatePassReferenceBlock
	{
		public enum Layer0Options
		{
			Texaccum_0 = 0,
			EnvironmentMap_1 = 1,
			SelfIllumination_2 = 2,
			Overlay_3 = 3,
			Transparent_4 = 4,
			LightmapIndirect_5 = 5,
			Diffuse_6 = 6,
			Specular_7 = 7,
			ShadowGenerate_8 = 8,
			ShadowApply_9 = 9,
			Boom_10 = 10,
			Fog_11 = 11,
			ShPrt_12 = 12,
			ActiveCamo_13 = 13,
			WaterEdgeBlend_14 = 14,
			Decal_15 = 15,
			ActiveCamoStencilModulate_16 = 16,
			Hologram_17 = 17,
			LightAlbedo_18 = 18,
		}
		[Field("Layer", typeof(Layer0Options))]
		public short Layer0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("Pass^", null)]
		public TagReference Pass2;
		[Field("", null)]
		public fixed byte _3[12];
	}
}
#pragma warning restore CS1591
