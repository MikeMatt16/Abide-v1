using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(52, 4)]
	public unsafe struct GlobalShaderParameterBlock
	{
		public enum Type1Options
		{
			Bitmap_0 = 0,
			Value_1 = 1,
			Color_2 = 2,
			Switch_3 = 3,
		}
		[Field("Name^", null)]
		public StringId Name0;
		[Field("Type", typeof(Type1Options))]
		public short Type1;
		[Field("", null)]
		public fixed byte _2[2];
		[Field("Bitmap", null)]
		public TagReference Bitmap3;
		[Field("Const Value", null)]
		public float ConstValue4;
		[Field("Const Color", null)]
		public ColorRgbF ConstColor5;
		[Field("Animation Properties", null)]
		[Block("Animation Property", 14, typeof(ShaderAnimationPropertyBlock))]
		public TagBlock AnimationProperties6;
	}
}
#pragma warning restore CS1591
