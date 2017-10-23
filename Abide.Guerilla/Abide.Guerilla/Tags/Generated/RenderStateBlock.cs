using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(5, 4)]
	public unsafe struct RenderStateBlock
	{
		[Field("state index", null)]
		public int StateIndex0;
		[Field("state value", null)]
		public int StateValue1;
	}
}
#pragma warning restore CS1591
