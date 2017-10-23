using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(6, 4)]
	public unsafe struct TextureStageStateBlock
	{
		[Field("state index", null)]
		public int StateIndex0;
		[Field("stage index", null)]
		public int StageIndex1;
		[Field("state value", null)]
		public int StateValue2;
	}
}
#pragma warning restore CS1591
