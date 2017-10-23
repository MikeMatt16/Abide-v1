using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(4, 4)]
	public unsafe struct Bsp2dReferencesBlock
	{
		[Field("Plane*", null)]
		public short Plane0;
		[Field("BSP 2D Node*", null)]
		public short BSP2DNode1;
	}
}
#pragma warning restore CS1591
