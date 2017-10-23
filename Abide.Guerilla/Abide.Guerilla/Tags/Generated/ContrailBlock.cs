using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("contrail", "cont", "����", typeof(ContrailBlock))]
	[FieldSet(260, 4)]
	public unsafe struct ContrailBlock
	{
		public enum Flags0Options
		{
			FirstPointUnfaded_0 = 1,
			LastPointUnfaded_1 = 2,
			PointsStartPinnedToMedia_2 = 4,
			PointsStartPinnedToGround_3 = 8,
			PointsAlwaysPinnedToMedia_4 = 16,
			PointsAlwaysPinnedToGround_5 = 32,
			EdgeEffectFadesSlowly_6 = 64,
			DontTInheritObjectChangeColor_7 = 128,
		}
		public enum ScaleFlagsTheseFlagsDetermineWhichFieldsAreScaledByTheContrailDensity1Options
		{
			PointGenerationRate_0 = 1,
			PointVelocity_1 = 2,
			PointVelocityDelta_2 = 4,
			PointVelocityConeAngle_3 = 8,
			InheritedVelocityFraction_4 = 16,
			SequenceAnimationRate_5 = 32,
			TextureScaleU_6 = 64,
			TextureScaleV_7 = 128,
			TextureAnimationU_8 = 256,
			TextureAnimationV_9 = 512,
		}
		public enum RenderTypeThisSpecifiesHowTheContrailIsOrientedInSpace8Options
		{
			VerticalOrientation_0 = 0,
			HorizontalOrientation_1 = 1,
			MediaMapped_2 = 2,
			GroundMapped_3 = 3,
			ViewerFacing_4 = 4,
			DoubleMarkerLinked_5 = 5,
		}
		public enum ShaderFlags20Options
		{
			SortBias_0 = 1,
			NonlinearTint_1 = 2,
			DonTOverdrawFpWeapon_2 = 4,
		}
		public enum FramebufferBlendFunction21Options
		{
			AlphaBlend_0 = 0,
			Multiply_1 = 1,
			DoubleMultiply_2 = 2,
			Add_3 = 3,
			Subtract_4 = 4,
			ComponentMin_5 = 5,
			ComponentMax_6 = 6,
			AlphaMultiplyAdd_7 = 7,
			ConstantColorBlend_8 = 8,
			InverseConstantColorBlend_9 = 9,
			None_10 = 10,
		}
		public enum FramebufferFadeMode22Options
		{
			None_0 = 0,
			FadeWhenPerpendicular_1 = 1,
			FadeWhenParallel_2 = 2,
		}
		public enum MapFlags23Options
		{
			Unfiltered_0 = 1,
		}
		public enum Anchor27Options
		{
			WithPrimary_0 = 0,
			WithScreenSpace_1 = 1,
			Zsprite_2 = 2,
		}
		public enum Flags28Options
		{
			Unfiltered_0 = 1,
		}
		public enum UAnimationFunction30Options
		{
			One_0 = 0,
			Zero_1 = 1,
			Cosine_2 = 2,
			CosineVariablePeriod_3 = 3,
			DiagonalWave_4 = 4,
			DiagonalWaveVariablePeriod_5 = 5,
			Slide_6 = 6,
			SlideVariablePeriod_7 = 7,
			Noise_8 = 8,
			Jitter_9 = 9,
			Wander_10 = 10,
			Spark_11 = 11,
		}
		public enum VAnimationFunction35Options
		{
			One_0 = 0,
			Zero_1 = 1,
			Cosine_2 = 2,
			CosineVariablePeriod_3 = 3,
			DiagonalWave_4 = 4,
			DiagonalWaveVariablePeriod_5 = 5,
			Slide_6 = 6,
			SlideVariablePeriod_7 = 7,
			Noise_8 = 8,
			Jitter_9 = 9,
			Wander_10 = 10,
			Spark_11 = 11,
		}
		public enum RotationAnimationFunction40Options
		{
			One_0 = 0,
			Zero_1 = 1,
			Cosine_2 = 2,
			CosineVariablePeriod_3 = 3,
			DiagonalWave_4 = 4,
			DiagonalWaveVariablePeriod_5 = 5,
			Slide_6 = 6,
			SlideVariablePeriod_7 = 7,
			Noise_8 = 8,
			Jitter_9 = 9,
			Wander_10 = 10,
			Spark_11 = 11,
		}
		[Field("flags", typeof(Flags0Options))]
		public short Flags0;
		[Field("scale flags#these flags determine which fields are scaled by the contrail density", typeof(ScaleFlagsTheseFlagsDetermineWhichFieldsAreScaledByTheContrailDensity1Options))]
		public short ScaleFlags1;
		[Field("point generation rate:points per second#this many points are generated per second", null)]
		public float PointGenerationRate3;
		[Field("point velocity:world units per second#velocity added to each point's initial velocity", null)]
		public FloatBounds PointVelocity4;
		[Field("point velocity cone angle:degrees#initial velocity is inside the cone defined by the marker's forward vector and this angle", null)]
		public float PointVelocityConeAngle5;
		[Field("inherited velocity fraction#fraction of parent object's velocity that is inherited by contrail points.", null)]
		public float InheritedVelocityFraction6;
		[Field("render type#this specifies how the contrail is oriented in space", typeof(RenderTypeThisSpecifiesHowTheContrailIsOrientedInSpace8Options))]
		public short RenderType8;
		[Field("", null)]
		public fixed byte _9[2];
		[Field("texture repeats u#texture repeats per contrail segment", null)]
		public float TextureRepeatsU10;
		[Field("texture repeats v#texture repeats across contrail width", null)]
		public float TextureRepeatsV11;
		[Field("texture animation u:repeats per second#the texture along the contrail is animated by this value", null)]
		public float TextureAnimationU12;
		[Field("texture animation v:repeats per second#the texture across the contrail is animated by this value", null)]
		public float TextureAnimationV13;
		[Field("animation rate:frames per second", null)]
		public float AnimationRate14;
		[Field("bitmap", null)]
		public TagReference Bitmap15;
		[Field("first sequence index", null)]
		public short FirstSequenceIndex16;
		[Field("sequence count", null)]
		public short SequenceCount17;
		[Field("", null)]
		public fixed byte _18[64];
		[Field("", null)]
		public fixed byte _19[40];
		[Field("shader flags", typeof(ShaderFlags20Options))]
		public short ShaderFlags20;
		[Field("framebuffer blend function", typeof(FramebufferBlendFunction21Options))]
		public short FramebufferBlendFunction21;
		[Field("framebuffer fade mode", typeof(FramebufferFadeMode22Options))]
		public short FramebufferFadeMode22;
		[Field("map flags", typeof(MapFlags23Options))]
		public short MapFlags23;
		[Field("", null)]
		public fixed byte _24[28];
		[Field("bitmap", null)]
		public TagReference Bitmap26;
		[Field("anchor", typeof(Anchor27Options))]
		public short Anchor27;
		[Field("flags", typeof(Flags28Options))]
		public short Flags28;
		[Field("", null)]
		public fixed byte _29[2];
		[Field("u-animation function", typeof(UAnimationFunction30Options))]
		public short UAnimationFunction30;
		[Field("u-animation period:seconds#0 defaults to 1", null)]
		public float UAnimationPeriod31;
		[Field("u-animation phase", null)]
		public float UAnimationPhase32;
		[Field("u-animation scale:repeats#0 defaults to 1", null)]
		public float UAnimationScale33;
		[Field("", null)]
		public fixed byte _34[2];
		[Field("v-animation function", typeof(VAnimationFunction35Options))]
		public short VAnimationFunction35;
		[Field("v-animation period:seconds#0 defaults to 1", null)]
		public float VAnimationPeriod36;
		[Field("v-animation phase", null)]
		public float VAnimationPhase37;
		[Field("v-animation scale:repeats#0 defaults to 1", null)]
		public float VAnimationScale38;
		[Field("", null)]
		public fixed byte _39[2];
		[Field("rotation-animation function", typeof(RotationAnimationFunction40Options))]
		public short RotationAnimationFunction40;
		[Field("rotation-animation period:seconds#0 defaults to 1", null)]
		public float RotationAnimationPeriod41;
		[Field("rotation-animation phase", null)]
		public float RotationAnimationPhase42;
		[Field("rotation-animation scale:degrees#0 defaults to 360", null)]
		public float RotationAnimationScale43;
		[Field("rotation-animation center", null)]
		public Vector2 RotationAnimationCenter44;
		[Field("", null)]
		public fixed byte _45[4];
		[Field("zsprite radius scale", null)]
		public float ZspriteRadiusScale46;
		[Field("", null)]
		public fixed byte _47[20];
		[Field("point states", null)]
		[Block("Contrail Point States Block", 16, typeof(ContrailPointStatesBlock))]
		public TagBlock PointStates48;
	}
}
#pragma warning restore CS1591
