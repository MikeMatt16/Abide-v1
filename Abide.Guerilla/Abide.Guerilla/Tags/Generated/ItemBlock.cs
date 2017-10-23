using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("item", "item", "obje", typeof(ItemBlock))]
	[FieldSet(156, 4)]
	public unsafe struct ItemBlock
	{
		public enum Flags1Options
		{
			AlwaysMaintainsZUp_0 = 1,
			DestroyedByExplosions_1 = 2,
			UnaffectedByGravity_2 = 4,
		}
		[Field("flags", typeof(Flags1Options))]
		public int Flags1;
		[Field("OLD message index", null)]
		public short OLDMessageIndex2;
		[Field("sort order", null)]
		public short SortOrder3;
		[Field("multiplayer on-ground scale", null)]
		public float MultiplayerOnGroundScale4;
		[Field("campaign on-ground scale", null)]
		public float CampaignOnGroundScale5;
		[Field("pickup message", null)]
		public StringId PickupMessage7;
		[Field("swap message", null)]
		public StringId SwapMessage8;
		[Field("pickup or dual msg", null)]
		public StringId PickupOrDualMsg9;
		[Field("swap or dual msg", null)]
		public StringId SwapOrDualMsg10;
		[Field("dual-only msg", null)]
		public StringId DualOnlyMsg11;
		[Field("picked up msg", null)]
		public StringId PickedUpMsg12;
		[Field("singluar quantity msg", null)]
		public StringId SingluarQuantityMsg13;
		[Field("plural quantity msg", null)]
		public StringId PluralQuantityMsg14;
		[Field("switch-to msg", null)]
		public StringId SwitchToMsg15;
		[Field("switch-to from ai msg", null)]
		public StringId SwitchToFromAiMsg16;
		[Field("", null)]
		public fixed byte _17[148];
		[Field("UNUSED*", null)]
		public TagReference UNUSED18;
		[Field("collision sound", null)]
		public TagReference CollisionSound19;
		[Field("predicted bitmaps", null)]
		[Block("Predicted Bitmaps Block", 8, typeof(PredictedBitmapsBlock))]
		public TagBlock PredictedBitmaps20;
		[Field("", null)]
		public fixed byte _21[92];
		[Field("detonation damage effect", null)]
		public TagReference DetonationDamageEffect22;
		[Field("detonation delay:seconds", null)]
		public FloatBounds DetonationDelay23;
		[Field("detonating effect", null)]
		public TagReference DetonatingEffect24;
		[Field("detonation effect", null)]
		public TagReference DetonationEffect25;
	}
}
#pragma warning restore CS1591
