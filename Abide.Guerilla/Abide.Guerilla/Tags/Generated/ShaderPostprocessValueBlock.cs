using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(5, 4)]
	public unsafe struct ShaderPostprocessValueBlock
	{
		[Field("parameter index", null)]
		public int ParameterIndex0;
		[Field("value", null)]
		public float Value1;
	}
}
#pragma warning restore CS1591
