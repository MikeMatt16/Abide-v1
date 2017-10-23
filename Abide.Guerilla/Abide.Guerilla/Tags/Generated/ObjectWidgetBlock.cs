using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct ObjectWidgetBlock
	{
		[Field("type", null)]
		public TagReference Type0;
		[Field("", null)]
		public fixed byte _1[16];
	}
}
#pragma warning restore CS1591
