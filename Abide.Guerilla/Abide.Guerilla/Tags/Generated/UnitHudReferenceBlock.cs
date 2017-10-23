using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct UnitHudReferenceBlock
	{
		[Field("", null)]
		public fixed byte _0[16];
		[Field("new unit hud interface", null)]
		public TagReference NewUnitHudInterface1;
		[Field("", null)]
		public fixed byte _2[16];
	}
}
#pragma warning restore CS1591
