using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(52, 4)]
	public unsafe struct CharacterPlacementBlock
	{
		[Field("", null)]
		public fixed byte _0[4];
		[Field("few upgrade chance (easy)", null)]
		public float FewUpgradeChanceEasy1;
		[Field("few upgrade chance (normal)", null)]
		public float FewUpgradeChanceNormal2;
		[Field("few upgrade chance (heroic)", null)]
		public float FewUpgradeChanceHeroic3;
		[Field("few upgrade chance (legendary)", null)]
		public float FewUpgradeChanceLegendary4;
		[Field("normal upgrade chance (easy)", null)]
		public float NormalUpgradeChanceEasy5;
		[Field("normal upgrade chance (normal)", null)]
		public float NormalUpgradeChanceNormal6;
		[Field("normal upgrade chance (heroic)", null)]
		public float NormalUpgradeChanceHeroic7;
		[Field("normal upgrade chance (legendary)", null)]
		public float NormalUpgradeChanceLegendary8;
		[Field("many upgrade chance (easy)", null)]
		public float ManyUpgradeChanceEasy9;
		[Field("many upgrade chance (normal)", null)]
		public float ManyUpgradeChanceNormal10;
		[Field("many upgrade chance (heroic)", null)]
		public float ManyUpgradeChanceHeroic11;
		[Field("many upgrade chance (legendary)", null)]
		public float ManyUpgradeChanceLegendary12;
	}
}
#pragma warning restore CS1591
