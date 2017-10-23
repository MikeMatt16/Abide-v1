using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(3, 4)]
	public unsafe struct RenderStateParameterBlock
	{
		[Field("parameter index", null)]
		public int ParameterIndex0;
		[Field("parameter type", null)]
		public int ParameterType1;
		[Field("state index", null)]
		public int StateIndex2;
	}
}
#pragma warning restore CS1591
