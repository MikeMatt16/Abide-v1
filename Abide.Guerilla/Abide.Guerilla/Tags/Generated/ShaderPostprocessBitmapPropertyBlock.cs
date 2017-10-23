using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(4, 4)]
	public unsafe struct ShaderPostprocessBitmapPropertyBlock
	{
		[Field("bitmap index", null)]
		public short BitmapIndex0;
		[Field("animated parameter index", null)]
		public short AnimatedParameterIndex1;
	}
}
#pragma warning restore CS1591
