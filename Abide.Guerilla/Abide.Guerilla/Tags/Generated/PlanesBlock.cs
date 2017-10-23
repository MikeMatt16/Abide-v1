using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 16)]
	public unsafe struct PlanesBlock
	{
		[Field("Plane*", null)]
		public Plane3 Plane0;
	}
}
#pragma warning restore CS1591
