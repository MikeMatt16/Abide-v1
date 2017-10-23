using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct DamageSeatInfoBlock
	{
		[Field("seat label^", null)]
		public StringId SeatLabel0;
		[Field("direct damage scale#0==no damage, 1==full damage", null)]
		public float DirectDamageScale1;
		[Field("damage transfer fall-off radius", null)]
		public float DamageTransferFallOffRadius2;
		[Field("maximum transfer damage scale", null)]
		public float MaximumTransferDamageScale3;
		[Field("minimum transfer damage scale", null)]
		public float MinimumTransferDamageScale4;
	}
}
#pragma warning restore CS1591
