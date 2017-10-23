using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(12, 4)]
	public unsafe struct LensFlareScalarAnimationBlock
	{
		[Field("function", typeof(ScalarFunctionStructBlock))]
		[Block("Scalar Function Struct", 1, typeof(ScalarFunctionStructBlock))]
		public ScalarFunctionStructBlock Function0;
	}
}
#pragma warning restore CS1591
