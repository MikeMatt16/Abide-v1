using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct ObjectAnimationBlock
	{
		public enum FunctionControls3Options
		{
			Frame_0 = 0,
			Scale_1 = 1,
		}
		[Field("label^", null)]
		public StringId Label0;
		[Field("animation*", typeof(AnimationIndexStructBlock))]
		[Block("Animation Index Struct", 1, typeof(AnimationIndexStructBlock))]
		public AnimationIndexStructBlock Animation1;
		[Field("", null)]
		public fixed byte _2[2];
		[Field("function controls", typeof(FunctionControls3Options))]
		public short FunctionControls3;
		[Field("function", null)]
		public StringId Function4;
		[Field("", null)]
		public fixed byte _5[4];
	}
}
#pragma warning restore CS1591
