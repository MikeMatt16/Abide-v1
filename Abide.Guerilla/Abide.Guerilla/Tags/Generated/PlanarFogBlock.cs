using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("planar_fog", "fog ", "����", typeof(PlanarFogBlock))]
	[FieldSet(132, 4)]
	public unsafe struct PlanarFogBlock
	{
		public enum Flags1Options
		{
			RenderOnlySubmergedGeometry_0 = 1,
			ExtendInfinitelyWhileVisible_1 = 2,
			DonTFloodfill_2 = 4,
			AggressiveFloodfill_3 = 8,
			DoNotRender_4 = 16,
			DoNotRenderUnlessSubmerged_5 = 32,
		}
		[Field("flags", typeof(Flags1Options))]
		public short Flags1;
		[Field("priority", null)]
		public short Priority2;
		[Field("global material name", null)]
		public StringId GlobalMaterialName3;
		[Field("", null)]
		public fixed byte _4[2];
		[Field("", null)]
		public fixed byte _5[2];
		[Field("", null)]
		public fixed byte _6[72];
		[Field("", null)]
		public fixed byte _8[4];
		[Field("maximum density:[0,1]#planar fog density is clamped to this value", null)]
		public float MaximumDensity9;
		[Field("", null)]
		public fixed byte _10[4];
		[Field("opaque distance:world units#the fog becomes opaque (maximum density) at this distance from the viewer", null)]
		public float OpaqueDistance11;
		[Field("", null)]
		public fixed byte _12[4];
		[Field("opaque depth:world units#the fog becomes opaque at this distance below fog plane", null)]
		public float OpaqueDepth13;
		[Field("atmospheric-planar depth:world units#distances above fog plane where atmospheric fog supercedes planar fog and visa-versa", null)]
		public FloatBounds AtmosphericPlanarDepth15;
		[Field("eye offset scale:[-1,1]#negative numbers are bad, mmmkay?", null)]
		public float EyeOffsetScale16;
		[Field("", null)]
		public fixed byte _17[32];
		[Field("color", null)]
		public ColorRgbF Color19;
		[Field("", null)]
		public fixed byte _20[100];
		[Field("patchy fog", null)]
		[Block("Planar Fog Patchy Fog Block", 1, typeof(PlanarFogPatchyFogBlock))]
		public TagBlock PatchyFog21;
		[Field("background sound", null)]
		public TagReference BackgroundSound23;
		[Field("sound environment", null)]
		public TagReference SoundEnvironment24;
		[Field("environment damping factor#scales the surrounding background sound by this much", null)]
		public float EnvironmentDampingFactor25;
		[Field("background sound gain#scale for fog background sound", null)]
		public float BackgroundSoundGain26;
		[Field("enter sound", null)]
		public TagReference EnterSound27;
		[Field("exit sound", null)]
		public TagReference ExitSound28;
		[Field("", null)]
		public fixed byte _29[80];
	}
}
#pragma warning restore CS1591
