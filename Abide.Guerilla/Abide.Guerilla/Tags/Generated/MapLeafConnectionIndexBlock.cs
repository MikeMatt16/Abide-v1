using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(4, 4)]
	public unsafe struct MapLeafConnectionIndexBlock
	{
		[Field("connection index*", null)]
		public int ConnectionIndex0;
	}
}
#pragma warning restore CS1591
