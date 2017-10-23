using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(12, 4)]
	public unsafe struct LensFlareColorAnimationBlock
	{
		[Field("function", typeof(ColorFunctionStructBlock))]
		[Block("Color Function Struct", 1, typeof(ColorFunctionStructBlock))]
		public ColorFunctionStructBlock Function0;
	}
}
#pragma warning restore CS1591
