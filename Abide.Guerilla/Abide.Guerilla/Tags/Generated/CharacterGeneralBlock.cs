using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(12, 4)]
	public unsafe struct CharacterGeneralBlock
	{
		public enum GeneralFlags0Options
		{
			Swarm_0 = 1,
			Flying_1 = 2,
			DualWields_2 = 4,
			UsesGravemind_3 = 8,
		}
		public enum Type1Options
		{
			Elite_0 = 0,
			Jackal_1 = 1,
			Grunt_2 = 2,
			Hunter_3 = 3,
			Engineer_4 = 4,
			Assassin_5 = 5,
			Player_6 = 6,
			Marine_7 = 7,
			Crew_8 = 8,
			CombatForm_9 = 9,
			InfectionForm_10 = 10,
			CarrierForm_11 = 11,
			Monitor_12 = 12,
			Sentinel_13 = 13,
			None_14 = 14,
			MountedWeapon_15 = 15,
			Brute_16 = 16,
			Prophet_17 = 17,
			Bugger_18 = 18,
			Juggernaut_19 = 19,
		}
		[Field("general flags", typeof(GeneralFlags0Options))]
		public int GeneralFlags0;
		[Field("type", typeof(Type1Options))]
		public short Type1;
		[Field("", null)]
		public fixed byte _2[2];
		[Field("", null)]
		public fixed byte _3[100];
		[Field("scariness#the inherent scariness of the character", null)]
		public float Scariness4;
	}
}
#pragma warning restore CS1591
