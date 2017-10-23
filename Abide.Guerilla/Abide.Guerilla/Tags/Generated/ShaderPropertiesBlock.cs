using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(112, 4)]
	public unsafe struct ShaderPropertiesBlock
	{
		[Field("Diffuse Map*", null)]
		public TagReference DiffuseMap0;
		[Field("Lightmap Emissive Map*", null)]
		public TagReference LightmapEmissiveMap1;
		[Field("Lightmap Emissive Color*", null)]
		public ColorRgbF LightmapEmissiveColor2;
		[Field("Lightmap Emissive Power*", null)]
		public float LightmapEmissivePower3;
		[Field("Lightmap Resolution Scale*", null)]
		public float LightmapResolutionScale4;
		[Field("Lightmap Half Life*", null)]
		public float LightmapHalfLife5;
		[Field("Lightmap Diffuse Scale*", null)]
		public float LightmapDiffuseScale6;
		[Field("Alpha Test Map*", null)]
		public TagReference AlphaTestMap7;
		[Field("Translucent Map*", null)]
		public TagReference TranslucentMap8;
		[Field("Lightmap Transparent Color*", null)]
		public ColorRgbF LightmapTransparentColor9;
		[Field("Lightmap Transparent Alpha*", null)]
		public float LightmapTransparentAlpha10;
		[Field("Lightmap Foliage Scale*", null)]
		public float LightmapFoliageScale11;
	}
}
#pragma warning restore CS1591
