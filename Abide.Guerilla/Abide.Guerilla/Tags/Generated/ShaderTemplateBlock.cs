using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("shader_template", "stem", "����", typeof(ShaderTemplateBlock))]
	[FieldSet(156, 4)]
	public unsafe struct ShaderTemplateBlock
	{
		public enum Flags4Options
		{
			ForceActiveCamo_0 = 1,
			Water_1 = 2,
			Foliage_2 = 4,
			HideStandardParameters_3 = 8,
		}
		public enum Aux1Layer14Options
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
		public enum Aux2Layer17Options
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
		[Field("Documentation", null)]
		[Data(65535)]
		public TagBlock Documentation0;
		[Field("Default Material Name", null)]
		public StringId DefaultMaterialName1;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("Flags", typeof(Flags4Options))]
		public short Flags4;
		[Field("Properties", null)]
		[Block("Property", 14, typeof(ShaderTemplatePropertyBlock))]
		public TagBlock Properties5;
		[Field("Categories", null)]
		[Block("Category", 16, typeof(ShaderTemplateCategoryBlock))]
		public TagBlock Categories6;
		[Field("Light Response", null)]
		public TagReference LightResponse8;
		[Field("LODs", null)]
		[Block("Shader Template Level Of Detail Block", 8, typeof(ShaderTemplateLevelOfDetailBlock))]
		public TagBlock LODs9;
		[Field("EMPTY STRING", null)]
		[Block("Shader Template Runtime External Light Response Index Block", 65535, typeof(ShaderTemplateRuntimeExternalLightResponseIndexBlock))]
		public TagBlock EMPTYSTRING10;
		[Field("EMPTY STRING", null)]
		[Block("Shader Template Runtime External Light Response Index Block", 65535, typeof(ShaderTemplateRuntimeExternalLightResponseIndexBlock))]
		public TagBlock EMPTYSTRING11;
		[Field("Aux 1 Shader", null)]
		public TagReference Aux1Shader13;
		[Field("Aux 1 Layer", typeof(Aux1Layer14Options))]
		public short Aux1Layer14;
		[Field("", null)]
		public fixed byte _15[2];
		[Field("Aux 2 Shader", null)]
		public TagReference Aux2Shader16;
		[Field("Aux 2 Layer", typeof(Aux2Layer17Options))]
		public short Aux2Layer17;
		[Field("", null)]
		public fixed byte _18[2];
		[Field("Postprocess Definition*", null)]
		[Block("Shader Template Postprocess Definition New Block", 1, typeof(ShaderTemplatePostprocessDefinitionNewBlock))]
		public TagBlock PostprocessDefinition19;
	}
}
#pragma warning restore CS1591
