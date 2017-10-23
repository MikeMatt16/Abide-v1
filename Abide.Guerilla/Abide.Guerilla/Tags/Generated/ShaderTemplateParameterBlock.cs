using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(72, 4)]
	public unsafe struct ShaderTemplateParameterBlock
	{
		public enum Type2Options
		{
			Bitmap_0 = 0,
			Value_1 = 1,
			Color_2 = 2,
			Switch_3 = 3,
		}
		public enum Flags3Options
		{
			Animated_0 = 1,
			HideBitmapReference_1 = 2,
		}
		public enum BitmapType7Options
		{
			_2D_0 = 0,
			_3D_1 = 1,
			CubeMap_2 = 2,
		}
		public enum BitmapAnimationFlags9Options
		{
			ScaleUniform_0 = 1,
			Scale_1 = 2,
			Translation_2 = 4,
			Rotation_3 = 8,
			Index_4 = 16,
		}
		[Field("Name^", null)]
		public StringId Name0;
		[Field("Explanation", null)]
		[Data(65535)]
		public TagBlock Explanation1;
		[Field("Type", typeof(Type2Options))]
		public short Type2;
		[Field("Flags", typeof(Flags3Options))]
		public short Flags3;
		[Field("Default Bitmap", null)]
		public TagReference DefaultBitmap4;
		[Field("Default Const Value", null)]
		public float DefaultConstValue5;
		[Field("Default Const Color", null)]
		public ColorRgbF DefaultConstColor6;
		[Field("Bitmap Type", typeof(BitmapType7Options))]
		public short BitmapType7;
		[Field("", null)]
		public fixed byte _8[2];
		[Field("Bitmap Animation Flags", typeof(BitmapAnimationFlags9Options))]
		public short BitmapAnimationFlags9;
		[Field("", null)]
		public fixed byte _10[2];
		[Field("Bitmap Scale", null)]
		public float BitmapScale11;
	}
}
#pragma warning restore CS1591
