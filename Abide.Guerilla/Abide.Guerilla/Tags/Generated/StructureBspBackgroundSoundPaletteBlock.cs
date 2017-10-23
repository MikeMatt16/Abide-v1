using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(116, 4)]
	public unsafe struct StructureBspBackgroundSoundPaletteBlock
	{
		public enum ScaleFlags5Options
		{
			OverrideDefaultScale_0 = 1,
			UseAdjacentClusterAsPortalScale_1 = 2,
			UseAdjacentClusterAsExteriorScale_2 = 4,
			ScaleWithWeatherIntensity_3 = 8,
		}
		[Field("Name^", null)]
		public String Name0;
		[Field("Background Sound", null)]
		public TagReference BackgroundSound1;
		[Field("Inside Cluster Sound#Play only when player is inside cluster.", null)]
		public TagReference InsideClusterSound2;
		[Field("", null)]
		public fixed byte _3[20];
		[Field("Cutoff Distance", null)]
		public float CutoffDistance4;
		[Field("Scale Flags", typeof(ScaleFlags5Options))]
		public int ScaleFlags5;
		[Field("Interior Scale", null)]
		public float InteriorScale6;
		[Field("Portal Scale", null)]
		public float PortalScale7;
		[Field("Exterior Scale", null)]
		public float ExteriorScale8;
		[Field("Interpolation Speed:1/sec", null)]
		public float InterpolationSpeed9;
		[Field("", null)]
		public fixed byte _10[8];
	}
}
#pragma warning restore CS1591
