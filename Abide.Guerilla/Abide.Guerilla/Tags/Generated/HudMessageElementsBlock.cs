using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(2, 4)]
	public unsafe struct HudMessageElementsBlock
	{
		[Field("type*", null)]
		public int Type0;
		[Field("data*", null)]
		public int Data1;
	}
}
#pragma warning restore CS1591
