using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(4, 4)]
	public unsafe struct ShaderPassPostprocessTextureNewBlock
	{
		[Field("bitmap parameter index", null)]
		public int BitmapParameterIndex0;
		[Field("bitmap extern index", null)]
		public int BitmapExternIndex1;
		[Field("texture stage index", null)]
		public int TextureStageIndex2;
		[Field("flags", null)]
		public int Flags3;
	}
}
#pragma warning restore CS1591
