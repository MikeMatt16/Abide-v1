using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(184, 4)]
	public unsafe struct ShaderPostprocessDefinitionNewBlock
	{
		[Field("shader template index", null)]
		public int ShaderTemplateIndex0;
		[Field("bitmaps", null)]
		[Block("Shader Postprocess Bitmap New Block", 1024, typeof(ShaderPostprocessBitmapNewBlock))]
		public TagBlock Bitmaps1;
		[Field("pixel constants", null)]
		[Block("Pixel32 Block", 1024, typeof(Pixel32Block))]
		public TagBlock PixelConstants2;
		[Field("vertex constants", null)]
		[Block("Real Vector4d Block", 1024, typeof(RealVector4dBlock))]
		public TagBlock VertexConstants3;
		[Field("levels of detail", null)]
		[Block("Shader Postprocess Level Of Detail New Block", 1024, typeof(ShaderPostprocessLevelOfDetailNewBlock))]
		public TagBlock LevelsOfDetail4;
		[Field("layers", null)]
		[Block("Tag Block Index Block", 1024, typeof(TagBlockIndexBlock))]
		public TagBlock Layers5;
		[Field("passes", null)]
		[Block("Tag Block Index Block", 1024, typeof(TagBlockIndexBlock))]
		public TagBlock Passes6;
		[Field("implementations", null)]
		[Block("Shader Postprocess Implementation New Block", 1024, typeof(ShaderPostprocessImplementationNewBlock))]
		public TagBlock Implementations7;
		[Field("overlays", null)]
		[Block("Shader Postprocess Overlay New Block", 1024, typeof(ShaderPostprocessOverlayNewBlock))]
		public TagBlock Overlays8;
		[Field("overlay references", null)]
		[Block("Shader Postprocess Overlay Reference New Block", 1024, typeof(ShaderPostprocessOverlayReferenceNewBlock))]
		public TagBlock OverlayReferences9;
		[Field("animated parameters", null)]
		[Block("Shader Postprocess Animated Parameter New Block", 1024, typeof(ShaderPostprocessAnimatedParameterNewBlock))]
		public TagBlock AnimatedParameters10;
		[Field("animated parameter references", null)]
		[Block("Shader Postprocess Animated Parameter Reference New Block", 1024, typeof(ShaderPostprocessAnimatedParameterReferenceNewBlock))]
		public TagBlock AnimatedParameterReferences11;
		[Field("bitmap properties", null)]
		[Block("Shader Postprocess Bitmap Property Block", 5, typeof(ShaderPostprocessBitmapPropertyBlock))]
		public TagBlock BitmapProperties12;
		[Field("color properties", null)]
		[Block("Shader Postprocess Color Property Block", 2, typeof(ShaderPostprocessColorPropertyBlock))]
		public TagBlock ColorProperties13;
		[Field("value properties", null)]
		[Block("Shader Postprocess Value Property Block", 6, typeof(ShaderPostprocessValuePropertyBlock))]
		public TagBlock ValueProperties14;
		[Field("old levels of detail", null)]
		[Block("Shader Postprocess Level Of Detail Block", 1024, typeof(ShaderPostprocessLevelOfDetailBlock))]
		public TagBlock OldLevelsOfDetail15;
	}
}
#pragma warning restore CS1591
