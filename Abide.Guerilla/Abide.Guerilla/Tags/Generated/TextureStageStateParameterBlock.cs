using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(4, 4)]
	public unsafe struct TextureStageStateParameterBlock
	{
		[Field("parameter index", null)]
		public int ParameterIndex0;
		[Field("parameter type", null)]
		public int ParameterType1;
		[Field("state index", null)]
		public int StateIndex2;
		[Field("stage index", null)]
		public int StageIndex3;
	}
}
#pragma warning restore CS1591
