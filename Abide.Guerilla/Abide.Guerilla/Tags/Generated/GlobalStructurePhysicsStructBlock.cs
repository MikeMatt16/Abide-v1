using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(80, 4)]
	public unsafe struct GlobalStructurePhysicsStructBlock
	{
		[Field("*mopp Code", null)]
		[Data(1048576)]
		public TagBlock MoppCode0;
		[Field("", null)]
		public fixed byte _1[4];
		public Vector3 MoppBoundsMin2;
		public Vector3 MoppBoundsMax3;
		[Field("*Breakable Surfaces mopp Code", null)]
		[Data(1048576)]
		public TagBlock BreakableSurfacesMoppCode4;
		[Field("Breakable Surface Key Table", null)]
		[Block("Breakable Surface Key Table Block", 8192, typeof(BreakableSurfaceKeyTableBlock))]
		public TagBlock BreakableSurfaceKeyTable5;
	}
}
#pragma warning restore CS1591
