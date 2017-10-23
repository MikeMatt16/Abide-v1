using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct UserInterfaceWidgetReferenceBlock
	{
		[Field("widget tag", null)]
		public TagReference WidgetTag0;
	}
}
#pragma warning restore CS1591
