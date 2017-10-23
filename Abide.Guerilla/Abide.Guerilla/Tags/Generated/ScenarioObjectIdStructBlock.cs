using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct ScenarioObjectIdStructBlock
	{
		public enum Type2Options
		{
			Biped_0 = 0,
			Vehicle_1 = 1,
			Weapon_2 = 2,
			Equipment_3 = 3,
			Garbage_4 = 4,
			Projectile_5 = 5,
			Scenery_6 = 6,
			Machine_7 = 7,
			Control_8 = 8,
			LightFixture_9 = 9,
			SoundScenery_10 = 10,
			Crate_11 = 11,
			Creature_12 = 12,
		}
		public enum Source3Options
		{
			Structure_0 = 0,
			Editor_1 = 1,
			Dynamic_2 = 2,
			Legacy_3 = 3,
		}
		[Field("Unique ID*", null)]
		public int UniqueID0;
		[Field("Origin BSP Index*", null)]
		public short OriginBSPIndex1;
		[Field("Type*", typeof(Type2Options))]
		public byte Type2;
		[Field("Source*", typeof(Source3Options))]
		public byte Source3;
	}
}
#pragma warning restore CS1591
