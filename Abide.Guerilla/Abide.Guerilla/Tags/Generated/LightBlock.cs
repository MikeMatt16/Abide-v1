using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("light", "ligh", "����", typeof(LightBlock))]
	[FieldSet(272, 4)]
	public unsafe struct LightBlock
	{
		public enum Flags0Options
		{
			NoIlluminationDonTCastAnyPerPixelDynamicLight_0 = 1,
			NoSpecularDonTCastAnySpecularHighlights_1 = 2,
			ForceCastEnvironmentShadowsThroughPortals_2 = 4,
			NoShadowDonTCastAnyStencilShadows_3 = 8,
			ForceFrustumVisibilityOnSmallLight_4 = 16,
			OnlyRenderInFirstPerson_5 = 32,
			OnlyRenderInThirdPerson_6 = 64,
			DonTFadeWhenInvisibleDonTFadeOutThisLightWhenUnderActiveCamouflage_7 = 128,
			MultiplayerOverrideDonTTurnOffInMultiplayer_8 = 256,
			AnimatedGel_9 = 512,
			OnlyInDynamicEnvmapOnlyDrawThisLightInDynamicReflectionMaps_10 = 1024,
			IgnoreParentObjectDonTIlluminateOrShadowTheSingleObjectWeAreAttachedTo_11 = 2048,
			DonTShadowParentDonTShadowTheObjectWeAreAttachedTo_12 = 4096,
			IgnoreAllParentsDonTIlluminateOrShadowAllTheWayUpToOurParentObject_13 = 8192,
			MarchMilestoneHackDonTClickThisUnlessYouKnowWhatYouReDoing_14 = 16384,
			ForceLightInsideWorldEveryUpdateWillPushLightBackInsideTheWorld_15 = 32768,
			EnvironmentDoesntCastStencilShadowsEnvironmentInThisLightWillNotCastStencilShadows_16 = 65536,
			ObjectsDonTCastStencilShadowsObjectsInThisLightWillNotCastStencilShadows_17 = 131072,
			FirstPersonFromCamera_18 = 262144,
			TextureCameraGel_19 = 524288,
			LightFramerateKiller_20 = 1048576,
			AllowedInSplitScreen_21 = 2097152,
			OnlyOnParentBipeds_22 = 4194304,
		}
		public enum Type3Options
		{
			Sphere_0 = 0,
			Orthogonal_1 = 1,
			Projective_2 = 2,
			Pyramid_3 = 3,
		}
		public enum ShadowTapBiasTheLessTapsYouUseTheFasterTheLightButEdgesCanLookWorse7Options
		{
			_3Tap_0 = 0,
			UNUSED_1 = 1,
			_1Tap_2 = 2,
		}
		public enum InterpolationFlags22Options
		{
			BlendInHsvBlendsColorsInHsvRatherThanRgbSpace_0 = 1,
			MoreColorsBlendsColorsThroughMoreHuesGoesTheLongWayAroundTheColorWheel_1 = 2,
		}
		public enum SpecularMask33Options
		{
			Default_0 = 0,
			NoneNoMask_1 = 1,
			GelAlpha_2 = 2,
			GelColor_3 = 3,
		}
		public enum FalloffFunction43Options
		{
			Default_0 = 0,
			Narrow_1 = 1,
			Broad_2 = 2,
			VeryBroad_3 = 3,
		}
		public enum DiffuseContrast44Options
		{
			DefaultLinear_0 = 0,
			High_1 = 1,
			Low_2 = 2,
			VeryLow_3 = 3,
		}
		public enum SpecularContrast45Options
		{
			DefaultOne_0 = 0,
			HighLinear_1 = 1,
			Low_2 = 2,
			VeryLow_3 = 3,
		}
		public enum FalloffGeometry46Options
		{
			Default_0 = 0,
			Directional_1 = 1,
			Spherical_2 = 2,
		}
		public enum DefaultLightmapSetting54Options
		{
			DynamicOnly_0 = 0,
			DynamicWithLightmaps_1 = 1,
			LightmapsOnly_2 = 2,
		}
		public enum FalloffFunctionTheScaleOfTheLightWillDiminishOverTimeAccordingToThisFunction62Options
		{
			Linear_0 = 0,
			Late_1 = 1,
			VeryLate_2 = 2,
			Early_3 = 3,
			VeryEarly_4 = 4,
			Cosine_5 = 5,
			Zero_6 = 6,
			One_7 = 7,
		}
		public enum IlluminationFade65Options
		{
			FadeVeryFar_0 = 0,
			FadeFar_1 = 1,
			FadeMedium_2 = 2,
			FadeClose_3 = 3,
			FadeVeryClose_4 = 4,
		}
		public enum ShadowFade66Options
		{
			FadeVeryFar_0 = 0,
			FadeFar_1 = 1,
			FadeMedium_2 = 2,
			FadeClose_3 = 3,
			FadeVeryClose_4 = 4,
		}
		public enum SpecularFade67Options
		{
			FadeVeryFar_0 = 0,
			FadeFar_1 = 1,
			FadeMedium_2 = 2,
			FadeClose_3 = 3,
			FadeVeryClose_4 = 4,
		}
		public enum Flags71Options
		{
			Synchronized_0 = 1,
		}
		[Field("flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("", null)]
		public fixed byte _1[16];
		[Field("type", typeof(Type3Options))]
		public short Type3;
		[Field("", null)]
		public fixed byte _4[2];
		[Field("size modifer#how the light's size changes with external scale", null)]
		public FloatBounds SizeModifer5;
		[Field("shadow quality bias#larger positive numbers improve quality, larger negative numbers improve speed", null)]
		public float ShadowQualityBias6;
		[Field("shadow tap bias#the less taps you use, the faster the light (but edges can look worse)", typeof(ShadowTapBiasTheLessTapsYouUseTheFasterTheLightButEdgesCanLookWorse7Options))]
		public short ShadowTapBias7;
		[Field("", null)]
		public fixed byte _8[2];
		[Field("", null)]
		public fixed byte _9[24];
		[Field("radius:world units#the radius at which illumination falls off to zero", null)]
		public float Radius11;
		[Field("specular radius:world units#the radius at which specular highlights fall off to zero (if zero, same as maximum radius)", null)]
		public float SpecularRadius12;
		[Field("", null)]
		public fixed byte _13[32];
		[Field("near width:world units#width of the frustum light at its near plane", null)]
		public float NearWidth15;
		[Field("height stretch#how much the gel is stretched vertically (0.0 or 1.0 = aspect ratio same as gel)", null)]
		public float HeightStretch16;
		[Field("field of view:degrees#horizontal angle that the frustum light covers (0.0 = no spread, a parallel beam)", null)]
		public float FieldOfView17;
		[Field("falloff distance#distance from near plane to where the light falloff starts", null)]
		public float FalloffDistance18;
		[Field("cutoff distance#distance from near plane to where illumination falls off to zero", null)]
		public float CutoffDistance19;
		[Field("", null)]
		public fixed byte _20[4];
		[Field("interpolation flags", typeof(InterpolationFlags22Options))]
		public int InterpolationFlags22;
		[Field("bloom bounds:[0..2]", null)]
		public FloatBounds BloomBounds23;
		[Field("specular lower bound", null)]
		public ColorRgbF SpecularLowerBound24;
		[Field("specular upper bound", null)]
		public ColorRgbF SpecularUpperBound25;
		[Field("diffuse lower bound", null)]
		public ColorRgbF DiffuseLowerBound26;
		[Field("", null)]
		public fixed byte _27[4];
		[Field("diffuse upper bound", null)]
		public ColorRgbF DiffuseUpperBound28;
		[Field("brightness bounds:[0..2]", null)]
		public FloatBounds BrightnessBounds29;
		[Field("", null)]
		public fixed byte _30[4];
		[Field("gel map#must be a cubemap for spherical light and a 2d texture for frustum light", null)]
		public TagReference GelMap32;
		[Field("specular mask", typeof(SpecularMask33Options))]
		public short SpecularMask33;
		[Field("", null)]
		public fixed byte _34[2];
		[Field("", null)]
		public fixed byte _35[12];
		[Field("", null)]
		public fixed byte _36[4];
		[Field("", null)]
		public fixed byte _37[80];
		[Field("", null)]
		public fixed byte _38[12];
		[Field("", null)]
		public fixed byte _39[12];
		[Field("", null)]
		public fixed byte _40[12];
		[Field("", null)]
		public fixed byte _41[16];
		[Field("falloff function", typeof(FalloffFunction43Options))]
		public short FalloffFunction43;
		[Field("diffuse contrast", typeof(DiffuseContrast44Options))]
		public short DiffuseContrast44;
		[Field("specular contrast", typeof(SpecularContrast45Options))]
		public short SpecularContrast45;
		[Field("falloff geometry", typeof(FalloffGeometry46Options))]
		public short FalloffGeometry46;
		[Field("", null)]
		public fixed byte _47[8];
		[Field("lens flare", null)]
		public TagReference LensFlare49;
		[Field("bounding radius:world units#used to generate a bounding radius for lensflare-only lights", null)]
		public float BoundingRadius50;
		[Field("light volume", null)]
		public TagReference LightVolume51;
		[Field("", null)]
		public fixed byte _52[8];
		[Field("default lightmap setting", typeof(DefaultLightmapSetting54Options))]
		public short DefaultLightmapSetting54;
		[Field("", null)]
		public fixed byte _55[2];
		[Field("lightmap half life", null)]
		public float LightmapHalfLife56;
		[Field("lightmap light scale", null)]
		public float LightmapLightScale57;
		[Field("", null)]
		public fixed byte _58[20];
		[Field("duration:seconds#the light will last this long when created by an effect", null)]
		public float Duration60;
		[Field("", null)]
		public fixed byte _61[2];
		[Field("falloff function#the scale of the light will diminish over time according to this function", typeof(FalloffFunctionTheScaleOfTheLightWillDiminishOverTimeAccordingToThisFunction62Options))]
		public short FalloffFunction62;
		[Field("", null)]
		public fixed byte _63[8];
		[Field("illumination fade", typeof(IlluminationFade65Options))]
		public short IlluminationFade65;
		[Field("shadow fade", typeof(ShadowFade66Options))]
		public short ShadowFade66;
		[Field("specular fade", typeof(SpecularFade67Options))]
		public short SpecularFade67;
		[Field("", null)]
		public fixed byte _68[2];
		[Field("", null)]
		public fixed byte _69[8];
		[Field("flags", typeof(Flags71Options))]
		public int Flags71;
		[Field("brightness animation", null)]
		[Block("Brightness Animation", 1, typeof(LightBrightnessAnimationBlock))]
		public TagBlock BrightnessAnimation72;
		[Field("color animation", null)]
		[Block("Color Animation", 1, typeof(LightColorAnimationBlock))]
		public TagBlock ColorAnimation73;
		[Field("gel animation", null)]
		[Block("Gel Animation", 1, typeof(LightGelAnimationBlock))]
		public TagBlock GelAnimation74;
		[Field("", null)]
		public fixed byte _75[72];
		[Field("shader", null)]
		public TagReference Shader77;
	}
}
#pragma warning restore CS1591
