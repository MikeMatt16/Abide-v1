using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(4, 4)]
	public unsafe struct RefBlock
	{
		[Field("node ref or sector ref", null)]
		public int NodeRefOrSectorRef0;
	}
}
#pragma warning restore CS1591
