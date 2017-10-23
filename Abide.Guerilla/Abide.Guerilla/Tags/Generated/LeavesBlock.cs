using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(4, 4)]
	public unsafe struct LeavesBlock
	{
		public enum Flags0Options
		{
			ContainsDoubleSidedSurfaces_0 = 1,
		}
		[Field("Flags*", typeof(Flags0Options))]
		public byte Flags0;
		[Field("BSP 2D Reference Count*", null)]
		public int BSP2DReferenceCount1;
		[Field("First BSP 2D Reference*", null)]
		public short FirstBSP2DReference2;
	}
}
#pragma warning restore CS1591
