using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(44, 4)]
	public unsafe struct ShaderPostprocessImplementationBlock
	{
		[Field("GPU Constant State", typeof(ShaderGpuStateReferenceStructBlock))]
		[Block("Shader Gpu State Reference Struct", 1, typeof(ShaderGpuStateReferenceStructBlock))]
		public ShaderGpuStateReferenceStructBlock GPUConstantState0;
		[Field("GPU Volatile State", typeof(ShaderGpuStateReferenceStructBlock))]
		[Block("Shader Gpu State Reference Struct", 1, typeof(ShaderGpuStateReferenceStructBlock))]
		public ShaderGpuStateReferenceStructBlock GPUVolatileState1;
		[Field("bitmap parameters", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock BitmapParameters2;
		[Field("bitmap transforms", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock BitmapTransforms3;
		[Field("value parameters", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock ValueParameters4;
		[Field("color parameters", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock ColorParameters5;
		[Field("bitmap transform overlays", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock BitmapTransformOverlays6;
		[Field("value overlays", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock ValueOverlays7;
		[Field("color overlays", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock ColorOverlays8;
		[Field("vertex shader constants", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock VertexShaderConstants9;
	}
}
#pragma warning restore CS1591
