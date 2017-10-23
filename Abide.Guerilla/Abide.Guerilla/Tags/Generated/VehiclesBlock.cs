using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct VehiclesBlock
	{
		[Field("vehicle^", null)]
		public TagReference Vehicle0;
	}
}
#pragma warning restore CS1591
