using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct ModelMaterialBlock
	{
		public enum MaterialType1Options
		{
			Dirt_0 = 0,
			Sand_1 = 1,
			Stone_2 = 2,
			Snow_3 = 3,
			Wood_4 = 4,
			MetalHollow_5 = 5,
			MetalThin_6 = 6,
			MetalThick_7 = 7,
			Rubber_8 = 8,
			Glass_9 = 9,
			ForceField_10 = 10,
			Grunt_11 = 11,
			HunterArmor_12 = 12,
			HunterSkin_13 = 13,
			Elite_14 = 14,
			Jackal_15 = 15,
			JackalEnergyShield_16 = 16,
			EngineerSkin_17 = 17,
			EngineerForceField_18 = 18,
			FloodCombatForm_19 = 19,
			FloodCarrierForm_20 = 20,
			CyborgArmor_21 = 21,
			CyborgEnergyShield_22 = 22,
			HumanArmor_23 = 23,
			HumanSkin_24 = 24,
			Sentinel_25 = 25,
			Monitor_26 = 26,
			Plastic_27 = 27,
			Water_28 = 28,
			Leaves_29 = 29,
			EliteEnergyShield_30 = 30,
			Ice_31 = 31,
			HunterShield_32 = 32,
		}
		[Field("material name", null)]
		public StringId MaterialName0;
		[Field("material type", typeof(MaterialType1Options))]
		public short MaterialType1;
		[Field("damage section", null)]
		public short DamageSection2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("", null)]
		public fixed byte _4[2];
		[Field("global material name", null)]
		public StringId GlobalMaterialName5;
		[Field("", null)]
		public fixed byte _6[4];
	}
}
#pragma warning restore CS1591
