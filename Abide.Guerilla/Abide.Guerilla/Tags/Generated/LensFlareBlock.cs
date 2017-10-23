using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("lens_flare", "lens", "����", typeof(LensFlareBlock))]
	[FieldSet(124, 4)]
	public unsafe struct LensFlareBlock
	{
		public enum OcclusionOffsetDirection7Options
		{
			TowardViewer_0 = 0,
			MarkerForward_1 = 1,
			None_2 = 2,
		}
		public enum OcclusionInnerRadiusScale8Options
		{
			None_0 = 0,
			_12_1 = 1,
			_14_2 = 2,
			_18_3 = 3,
			_116_4 = 4,
			_132_5 = 5,
			_164_6 = 6,
		}
		public enum Flags12Options
		{
			Sun_0 = 1,
			NoOcclusionTest_1 = 2,
			OnlyRenderInFirstPerson_2 = 4,
			OnlyRenderInThirdPerson_3 = 8,
			FadeInMoreQuickly_4 = 16,
			FadeOutMoreQuickly_5 = 32,
			ScaleByMarker_6 = 64,
		}
		public enum RotationFunction15Options
		{
			None_0 = 0,
			RotationA_1 = 1,
			RotationB_2 = 2,
			RotationTranslation_3 = 3,
			Translation_4 = 4,
		}
		public enum FalloffFunction21Options
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
		public enum Flags26Options
		{
			Synchronized_0 = 1,
		}
		[Field("falloff angle:degrees", null)]
		public float FalloffAngle1;
		[Field("cutoff angle:degrees", null)]
		public float CutoffAngle2;
		[Field("", null)]
		public fixed byte _3[4];
		[Field("", null)]
		public fixed byte _4[4];
		[Field("occlusion radius:world units#radius of the square used to test occlusion", null)]
		public float OcclusionRadius6;
		[Field("occlusion offset direction", typeof(OcclusionOffsetDirection7Options))]
		public short OcclusionOffsetDirection7;
		[Field("occlusion inner radius scale", typeof(OcclusionInnerRadiusScale8Options))]
		public short OcclusionInnerRadiusScale8;
		[Field("near fade distance:world units#distance at which the lens flare brightness is maximum", null)]
		public float NearFadeDistance9;
		[Field("far fade distance:world units#distance at which the lens flare brightness is minimum; set to zero to disable distance fading", null)]
		public float FarFadeDistance10;
		[Field("bitmap", null)]
		public TagReference Bitmap11;
		[Field("flags", typeof(Flags12Options))]
		public short Flags12;
		[Field("", null)]
		public fixed byte _13[2];
		[Field("", null)]
		public fixed byte _14[76];
		[Field("rotation function", typeof(RotationFunction15Options))]
		public short RotationFunction15;
		[Field("", null)]
		public fixed byte _16[2];
		[Field("rotation function scale:degrees", null)]
		public float RotationFunctionScale17;
		[Field("", null)]
		public fixed byte _18[24];
		[Field("corona scale#amount to stretch the corona", null)]
		public Vector2 CoronaScale19;
		[Field("falloff function", typeof(FalloffFunction21Options))]
		public short FalloffFunction21;
		[Field("", null)]
		public fixed byte _22[2];
		[Field("", null)]
		public fixed byte _23[24];
		[Field("reflections", null)]
		[Block("Reflection", 32, typeof(LensFlareReflectionBlock))]
		public TagBlock Reflections24;
		[Field("flags", typeof(Flags26Options))]
		public short Flags26;
		[Field("", null)]
		public fixed byte _27[2];
		[Field("brightness", null)]
		[Block("Lens Flare Scalar Animation Block", 1, typeof(LensFlareScalarAnimationBlock))]
		public TagBlock Brightness28;
		[Field("color", null)]
		[Block("Lens Flare Color Animation Block", 1, typeof(LensFlareColorAnimationBlock))]
		public TagBlock Color29;
		[Field("rotation", null)]
		[Block("Lens Flare Scalar Animation Block", 1, typeof(LensFlareScalarAnimationBlock))]
		public TagBlock Rotation30;
		[Field("", null)]
		public fixed byte _31[4];
	}
}
#pragma warning restore CS1591
