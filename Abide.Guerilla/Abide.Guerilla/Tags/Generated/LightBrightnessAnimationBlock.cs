using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(12, 4)]
	public unsafe struct LightBrightnessAnimationBlock
	{
		[Field("function", typeof(MappingFunctionBlock))]
		[Block("Mapping Function", 1, typeof(MappingFunctionBlock))]
		public MappingFunctionBlock Function1;
	}
}
#pragma warning restore CS1591
