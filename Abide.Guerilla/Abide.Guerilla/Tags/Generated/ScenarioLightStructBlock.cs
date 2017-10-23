using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(48, 4)]
	public unsafe struct ScenarioLightStructBlock
	{
		public enum Type0Options
		{
			Sphere_0 = 0,
			Orthogonal_1 = 1,
			Projective_2 = 2,
			Pyramid_3 = 3,
		}
		public enum Flags1Options
		{
			CustomGeometry_0 = 1,
			Unused_1 = 2,
			CinematicOnly_2 = 4,
		}
		public enum LightmapType2Options
		{
			UseLightTagSetting_0 = 0,
			DynamicOnly_1 = 1,
			DynamicWithLightmaps_2 = 2,
			LightmapsOnly_3 = 3,
		}
		public enum LightmapFlags3Options
		{
			Unused_0 = 1,
		}
		[Field("Type", typeof(Type0Options))]
		public short Type0;
		[Field("Flags", typeof(Flags1Options))]
		public short Flags1;
		[Field("Lightmap Type", typeof(LightmapType2Options))]
		public short LightmapType2;
		[Field("Lightmap Flags", typeof(LightmapFlags3Options))]
		public short LightmapFlags3;
		[Field("Lightmap Half Life", null)]
		public float LightmapHalfLife4;
		[Field("Lightmap Light Scale", null)]
		public float LightmapLightScale5;
		[Field("", null)]
		public fixed byte _6[116];
		public Vector3 TargetPoint7;
		[Field("Width:World Units*", null)]
		public float Width8;
		[Field("Height Scale:World Units*", null)]
		public float HeightScale9;
		[Field("Field of View:Degrees*", null)]
		public float FieldOfView10;
		[Field("", null)]
		public fixed byte _11[4];
		[Field("Falloff Distance:World Units*", null)]
		public float FalloffDistance12;
		[Field("Cutoff Distance:World Units (from Far Plane)*", null)]
		public float CutoffDistance13;
		[Field("", null)]
		public fixed byte _14[128];
	}
}
#pragma warning restore CS1591
