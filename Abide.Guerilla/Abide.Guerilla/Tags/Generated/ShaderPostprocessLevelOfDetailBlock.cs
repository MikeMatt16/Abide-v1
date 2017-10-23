using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(224, 4)]
	public unsafe struct ShaderPostprocessLevelOfDetailBlock
	{
		[Field("projected height percentage", null)]
		public float ProjectedHeightPercentage0;
		[Field("available layers", null)]
		public int AvailableLayers1;
		[Field("layers", null)]
		[Block("Shader Postprocess Layer Block", 25, typeof(ShaderPostprocessLayerBlock))]
		public TagBlock Layers2;
		[Field("passes", null)]
		[Block("Shader Postprocess Pass Block", 1024, typeof(ShaderPostprocessPassBlock))]
		public TagBlock Passes3;
		[Field("implementations", null)]
		[Block("Shader Postprocess Implementation Block", 1024, typeof(ShaderPostprocessImplementationBlock))]
		public TagBlock Implementations4;
		[Field("bitmaps", null)]
		[Block("Shader Postprocess Bitmap Block", 1024, typeof(ShaderPostprocessBitmapBlock))]
		public TagBlock Bitmaps5;
		[Field("bitmap transforms", null)]
		[Block("Shader Postprocess Bitmap Transform Block", 1024, typeof(ShaderPostprocessBitmapTransformBlock))]
		public TagBlock BitmapTransforms6;
		[Field("values", null)]
		[Block("Shader Postprocess Value Block", 1024, typeof(ShaderPostprocessValueBlock))]
		public TagBlock Values7;
		[Field("colors", null)]
		[Block("Shader Postprocess Color Block", 1024, typeof(ShaderPostprocessColorBlock))]
		public TagBlock Colors8;
		[Field("bitmap transform overlays", null)]
		[Block("Shader Postprocess Bitmap Transform Overlay Block", 1024, typeof(ShaderPostprocessBitmapTransformOverlayBlock))]
		public TagBlock BitmapTransformOverlays9;
		[Field("value overlays", null)]
		[Block("Shader Postprocess Value Overlay Block", 1024, typeof(ShaderPostprocessValueOverlayBlock))]
		public TagBlock ValueOverlays10;
		[Field("color overlays", null)]
		[Block("Shader Postprocess Color Overlay Block", 1024, typeof(ShaderPostprocessColorOverlayBlock))]
		public TagBlock ColorOverlays11;
		[Field("vertex shader constants", null)]
		[Block("Shader Postprocess Vertex Shader Constant Block", 1024, typeof(ShaderPostprocessVertexShaderConstantBlock))]
		public TagBlock VertexShaderConstants12;
		[Field("GPU State", typeof(ShaderGpuStateStructBlock))]
		[Block("Shader Gpu State Struct", 1, typeof(ShaderGpuStateStructBlock))]
		public ShaderGpuStateStructBlock GPUState13;
	}
}
#pragma warning restore CS1591
