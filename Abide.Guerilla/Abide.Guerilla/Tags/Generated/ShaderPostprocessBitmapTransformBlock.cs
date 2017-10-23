using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(6, 4)]
	public unsafe struct ShaderPostprocessBitmapTransformBlock
	{
		[Field("parameter index", null)]
		public int ParameterIndex0;
		[Field("bitmap transform index", null)]
		public int BitmapTransformIndex1;
		[Field("value", null)]
		public float Value2;
	}
}
#pragma warning restore CS1591
