using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(4, 4)]
	public unsafe struct TextureBlock
	{
		[Field("stage index", null)]
		public int StageIndex0;
		[Field("parameter index", null)]
		public int ParameterIndex1;
		[Field("global texture index", null)]
		public int GlobalTextureIndex2;
		[Field("flags", null)]
		public int Flags3;
	}
}
#pragma warning restore CS1591
