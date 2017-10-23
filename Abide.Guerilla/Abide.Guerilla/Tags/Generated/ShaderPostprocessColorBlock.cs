using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(13, 4)]
	public unsafe struct ShaderPostprocessColorBlock
	{
		[Field("parameter index", null)]
		public int ParameterIndex0;
		[Field("color", null)]
		public ColorRgbF Color1;
	}
}
#pragma warning restore CS1591
