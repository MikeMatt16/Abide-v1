using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(160, 4)]
	public unsafe struct LightingVariablesBlock
	{
		public enum ObjectAffected0Options
		{
			All_0 = 1,
			Biped_1 = 2,
			Vehicle_2 = 4,
			Weapon_3 = 8,
			Equipment_4 = 16,
			Garbage_5 = 32,
			Projectile_6 = 64,
			Scenery_7 = 128,
			Machine_8 = 256,
			Control_9 = 512,
			LightFixture_10 = 1024,
			SoundScenery_11 = 2048,
			Crate_12 = 4096,
			Creature_13 = 8192,
		}
		[Field("object affected", typeof(ObjectAffected0Options))]
		public int ObjectAffected0;
		[Field("Lightmap brightness offset", null)]
		public float LightmapBrightnessOffset2;
		[Field("primary light", typeof(PrimaryLightStructBlock))]
		[Block("Primary Light Struct", 1, typeof(PrimaryLightStructBlock))]
		public PrimaryLightStructBlock PrimaryLight3;
		[Field("secondary light", typeof(SecondaryLightStructBlock))]
		[Block("Secondary Light Struct", 1, typeof(SecondaryLightStructBlock))]
		public SecondaryLightStructBlock SecondaryLight4;
		[Field("ambient light", typeof(AmbientLightStructBlock))]
		[Block("Ambient Light Struct", 1, typeof(AmbientLightStructBlock))]
		public AmbientLightStructBlock AmbientLight5;
		[Field("lightmap shadows", typeof(LightmapShadowsStructBlock))]
		[Block("Lightmap Shadows Struct", 1, typeof(LightmapShadowsStructBlock))]
		public LightmapShadowsStructBlock LightmapShadows6;
	}
}
#pragma warning restore CS1591
