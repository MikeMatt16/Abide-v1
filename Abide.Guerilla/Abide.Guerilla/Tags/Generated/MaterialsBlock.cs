using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(316, 4)]
	public unsafe struct MaterialsBlock
	{
		public enum Flags3Options
		{
			Flammable_0 = 1,
			Biomass_1 = 2,
		}
		public enum OldMaterialType4Options
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
		[Field("name^", null)]
		public StringId Name0;
		[Field("parent name", null)]
		public StringId ParentName1;
		[Field("", null)]
		public fixed byte _2[2];
		[Field("flags", typeof(Flags3Options))]
		public short Flags3;
		[Field("old material type", typeof(OldMaterialType4Options))]
		public short OldMaterialType4;
		[Field("", null)]
		public fixed byte _5[2];
		[Field("general armor", null)]
		public StringId GeneralArmor6;
		[Field("specific armor", null)]
		public StringId SpecificArmor7;
		[Field("physics properties", typeof(MaterialPhysicsPropertiesStructBlock))]
		[Block("Material Physics Properties Struct", 1, typeof(MaterialPhysicsPropertiesStructBlock))]
		public MaterialPhysicsPropertiesStructBlock PhysicsProperties8;
		[Field("old material physics", null)]
		public TagReference OldMaterialPhysics9;
		[Field("breakable surface", null)]
		public TagReference BreakableSurface10;
		[Field("sweeteners", typeof(MaterialsSweetenersStructBlock))]
		[Block("Materials Sweeteners Struct", 1, typeof(MaterialsSweetenersStructBlock))]
		public MaterialsSweetenersStructBlock Sweeteners11;
		[Field("material effects", null)]
		public TagReference MaterialEffects12;
	}
}
#pragma warning restore CS1591
