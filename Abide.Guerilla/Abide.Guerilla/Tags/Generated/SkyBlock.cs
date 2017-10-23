using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("sky", "sky ", "����", typeof(SkyBlock))]
	[FieldSet(220, 4)]
	public unsafe struct SkyBlock
	{
		public enum Flags2Options
		{
			FixedInWorldSpace_0 = 1,
			Depreciated_1 = 2,
			SkyCastsLightFromBelow_2 = 4,
			DisableSkyInLightmaps_3 = 8,
			FogOnlyAffectsContainingClusters_4 = 16,
			UseClearColor_5 = 32,
		}
		[Field("Render Model", null)]
		public TagReference RenderModel0;
		[Field("Animation Graph", null)]
		public TagReference AnimationGraph1;
		[Field("Flags", typeof(Flags2Options))]
		public int Flags2;
		[Field("Render Model Scale#Multiplier by which to scale the model's geometry up or down (0 defaults to standard, 0.03).", null)]
		public float RenderModelScale3;
		[Field("Movement Scale#How much the sky moves to remain centered on the player (0 defaults to 1.0, which means no parallax).", null)]
		public float MovementScale4;
		[Field("Cube Map", null)]
		[Block("Sky Cubemap Block", 1, typeof(SkyCubemapBlock))]
		public TagBlock CubeMap5;
		[Field("Indoor Ambient Color#Indoor ambient light color.", null)]
		public ColorRgbF IndoorAmbientColor7;
		[Field("", null)]
		public fixed byte _8[4];
		[Field("Outdoor Ambient Color#Indoor ambient light color.", null)]
		public ColorRgbF OutdoorAmbientColor9;
		[Field("", null)]
		public fixed byte _10[4];
		[Field("Fog Spread Distance:world units#How far fog spreads into adjacent clusters.", null)]
		public float FogSpreadDistance12;
		[Field("Atmospheric Fog", null)]
		[Block("Sky Atmospheric Fog Block", 1, typeof(SkyAtmosphericFogBlock))]
		public TagBlock AtmosphericFog13;
		[Field("Secondary Fog", null)]
		[Block("Sky Atmospheric Fog Block", 1, typeof(SkyAtmosphericFogBlock))]
		public TagBlock SecondaryFog14;
		[Field("Sky Fog", null)]
		[Block("Sky Fog Block", 1, typeof(SkyFogBlock))]
		public TagBlock SkyFog15;
		[Field("Patchy Fog", null)]
		[Block("Sky Patchy Fog Block", 1, typeof(SkyPatchyFogBlock))]
		public TagBlock PatchyFog16;
		[Field("Amount:[0,1]", null)]
		public float Amount18;
		[Field("Threshold:[0,1]", null)]
		public float Threshold19;
		[Field("Brightness:[0,1]", null)]
		public float Brightness20;
		[Field("Gamma Power", null)]
		public float GammaPower21;
		[Field("Lights", null)]
		[Block("Sky Light Block", 8, typeof(SkyLightBlock))]
		public TagBlock Lights22;
		[Field("Global Sky Rotation", null)]
		public float GlobalSkyRotation24;
		[Field("Shader Functions", null)]
		[Block("Sky Shader Function Block", 8, typeof(SkyShaderFunctionBlock))]
		public TagBlock ShaderFunctions25;
		[Field("Animations", null)]
		[Block("Sky Animation Block", 8, typeof(SkyAnimationBlock))]
		public TagBlock Animations26;
		[Field("", null)]
		public fixed byte _27[12];
		[Field("Clear Color", null)]
		public ColorRgbF ClearColor28;
	}
}
#pragma warning restore CS1591
