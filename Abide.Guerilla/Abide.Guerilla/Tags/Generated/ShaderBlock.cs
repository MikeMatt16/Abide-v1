using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("shader", "shad", "����", typeof(ShaderBlock))]
	[FieldSet(128, 4)]
	public unsafe struct ShaderBlock
	{
		public enum Flags4Options
		{
			Water_0 = 1,
			SortFirst_1 = 2,
			NoActiveCamo_2 = 4,
		}
		public enum ShaderLODBias11Options
		{
			None_0 = 0,
			_4xSize_1 = 1,
			_2xSize_2 = 2,
			_12Size_3 = 3,
			_14Size_4 = 4,
			Never_5 = 5,
			Cinematic_6 = 6,
		}
		public enum SpecularType12Options
		{
			None_0 = 0,
			Default_1 = 1,
			Dull_2 = 2,
			Shiny_3 = 3,
		}
		public enum LightmapType13Options
		{
			Diffuse_0 = 0,
			DefaultSpecular_1 = 1,
			DullSpecular_2 = 2,
			ShinySpecular_3 = 3,
		}
		[Field("Template", null)]
		public TagReference Template0;
		[Field("Material Name", null)]
		public StringId MaterialName1;
		[Field("Runtime Properties*", null)]
		[Block("Runtime Properties", 1, typeof(ShaderPropertiesBlock))]
		public TagBlock RuntimeProperties2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("Flags", typeof(Flags4Options))]
		public short Flags4;
		[Field("Parameters", null)]
		[Block("Parameter", 64, typeof(GlobalShaderParameterBlock))]
		public TagBlock Parameters5;
		[Field("Postprocess Definition*", null)]
		[Block("Shader Postprocess Definition New Block", 1, typeof(ShaderPostprocessDefinitionNewBlock))]
		public TagBlock PostprocessDefinition6;
		[Field("", null)]
		public fixed byte _7[4];
		[Field("", null)]
		public fixed byte _8[12];
		[Field("Predicted Resources", null)]
		[Block("Predicted Resource Block", 2048, typeof(PredictedResourceBlock))]
		public TagBlock PredictedResources9;
		[Field("Light Response", null)]
		public TagReference LightResponse10;
		[Field("Shader LOD Bias", typeof(ShaderLODBias11Options))]
		public short ShaderLODBias11;
		[Field("Specular Type", typeof(SpecularType12Options))]
		public short SpecularType12;
		[Field("Lightmap Type", typeof(LightmapType13Options))]
		public short LightmapType13;
		[Field("", null)]
		public fixed byte _14[2];
		[Field("Lightmap Specular Brightness", null)]
		public float LightmapSpecularBrightness15;
		[Field("Lightmap Ambient Bias:[-1,1]", null)]
		public float LightmapAmbientBias16;
		[Field("Postprocess Properties", null)]
		[Block("Long Block", 5, typeof(LongBlock))]
		public TagBlock PostprocessProperties17;
		[Field("Added depth bias offset", null)]
		public float AddedDepthBiasOffset18;
		[Field("Added depth bias slope scale", null)]
		public float AddedDepthBiasSlopeScale19;
	}
}
#pragma warning restore CS1591
