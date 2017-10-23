using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(12, 4)]
	public unsafe struct ShaderPostprocessBitmapNewBlock
	{
		[Field("bitmap group", null)]
		public int BitmapGroup0;
		[Field("bitmap index", null)]
		public int BitmapIndex1;
		[Field("log bitmap dimension", null)]
		public float LogBitmapDimension2;
	}
}
#pragma warning restore CS1591
