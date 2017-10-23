using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(12, 4)]
	public unsafe struct ShaderPostprocessColorPropertyBlock
	{
		[Field("color", null)]
		public ColorRgbF Color0;
	}
}
#pragma warning restore CS1591
