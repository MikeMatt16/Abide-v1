using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(10, 4)]
	public unsafe struct ShaderPostprocessBitmapBlock
	{
		[Field("parameter index", null)]
		public int ParameterIndex0;
		[Field("flags", null)]
		public int Flags1;
		[Field("bitmap group index", null)]
		public int BitmapGroupIndex2;
		[Field("log bitmap dimension", null)]
		public float LogBitmapDimension3;
	}
}
#pragma warning restore CS1591
