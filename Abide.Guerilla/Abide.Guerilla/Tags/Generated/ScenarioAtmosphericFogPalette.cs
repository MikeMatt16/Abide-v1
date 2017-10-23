using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(256, 4)]
	public unsafe struct ScenarioAtmosphericFogPalette
	{
		public enum CameraImmersionFlags35Options
		{
			DisableAtmosphericFog_0 = 1,
			DisableSecondaryFog_1 = 2,
			DisablePlanarFog_2 = 4,
			InvertPlanarFogPriorities_3 = 8,
			DisableWater_4 = 16,
		}
		[Field("Name^", null)]
		public StringId Name0;
		[Field("Color", null)]
		public ColorRgbF Color2;
		[Field("Spread Distance:World Units#How far fog spreads into adjacent clusters: 0 defaults to 1.", null)]
		public float SpreadDistance3;
		[Field("", null)]
		public fixed byte _4[4];
		[Field("Maximum Density:[0,1]#Fog density clamps to this value.", null)]
		public float MaximumDensity5;
		[Field("Start Distance:World Units#Before this distance, there is no fog.", null)]
		public float StartDistance6;
		[Field("Opaque Distance:World Units#Fog becomes opaque (maximum density) at this distance from viewer.", null)]
		public float OpaqueDistance7;
		[Field("Color", null)]
		public ColorRgbF Color9;
		[Field("", null)]
		public fixed byte _10[4];
		[Field("Maximum Density:[0,1]#Fog density clamps to this value.", null)]
		public float MaximumDensity11;
		[Field("Start Distance:World Units#Before this distance, there is no fog.", null)]
		public float StartDistance12;
		[Field("Opaque Distance:World Units#Fog becomes opaque (maximum density) at this distance from viewer.", null)]
		public float OpaqueDistance13;
		[Field("", null)]
		public fixed byte _14[4];
		[Field("Planar Color", null)]
		public ColorRgbF PlanarColor16;
		[Field("Planar Max Density:[0,1]", null)]
		public float PlanarMaxDensity17;
		[Field("Planar Override Amount:[0,1]", null)]
		public float PlanarOverrideAmount18;
		[Field("Planar Min Distance Bias:World Units#Don't ask.", null)]
		public float PlanarMinDistanceBias19;
		[Field("", null)]
		public fixed byte _20[44];
		[Field("Patchy Color", null)]
		public ColorRgbF PatchyColor22;
		[Field("", null)]
		public fixed byte _23[12];
		[Field("Patchy Density:[0,1]", null)]
		public FloatBounds PatchyDensity24;
		[Field("Patchy Distance:World Units", null)]
		public FloatBounds PatchyDistance25;
		[Field("", null)]
		public fixed byte _26[32];
		[Field("Patchy Fog", null)]
		public TagReference PatchyFog27;
		[Field("Mixers", null)]
		[Block("Mixers", 2, typeof(ScenarioAtmosphericFogMixerBlock))]
		public TagBlock Mixers28;
		[Field("Amount:[0,1]", null)]
		public float Amount30;
		[Field("Threshold:[0,1]", null)]
		public float Threshold31;
		[Field("Brightness:[0,1]", null)]
		public float Brightness32;
		[Field("Gamma Power", null)]
		public float GammaPower33;
		[Field("Camera Immersion Flags", typeof(CameraImmersionFlags35Options))]
		public short CameraImmersionFlags35;
		[Field("", null)]
		public fixed byte _36[2];
	}
}
#pragma warning restore CS1591
