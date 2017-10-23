using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(84, 4)]
	public unsafe struct UiModelSceneReferenceBlock
	{
		public enum Flags1Options
		{
			Unused_0 = 1,
		}
		public enum AnimationIndex2Options
		{
			NONE_0 = 0,
			_00_1 = 1,
			_01_2 = 2,
			_02_3 = 3,
			_03_4 = 4,
			_04_5 = 5,
			_05_6 = 6,
			_06_7 = 7,
			_07_8 = 8,
			_08_9 = 9,
			_09_10 = 10,
			_10_11 = 11,
			_11_12 = 12,
			_12_13 = 13,
			_13_14 = 14,
			_14_15 = 15,
			_15_16 = 16,
			_16_17 = 17,
			_17_18 = 18,
			_18_19 = 19,
			_19_20 = 20,
			_20_21 = 21,
			_21_22 = 22,
			_22_23 = 23,
			_23_24 = 24,
			_24_25 = 25,
			_25_26 = 26,
			_26_27 = 27,
			_27_28 = 28,
			_28_29 = 29,
			_29_30 = 30,
			_30_31 = 31,
			_31_32 = 32,
			_32_33 = 33,
			_33_34 = 34,
			_34_35 = 35,
			_35_36 = 36,
			_36_37 = 37,
			_37_38 = 38,
			_38_39 = 39,
			_39_40 = 40,
			_40_41 = 41,
			_41_42 = 42,
			_42_43 = 43,
			_43_44 = 44,
			_44_45 = 45,
			_45_46 = 46,
			_46_47 = 47,
			_47_48 = 48,
			_48_49 = 49,
			_49_50 = 50,
			_50_51 = 51,
			_51_52 = 52,
			_52_53 = 53,
			_53_54 = 54,
			_54_55 = 55,
			_55_56 = 56,
			_56_57 = 57,
			_57_58 = 58,
			_58_59 = 59,
			_59_60 = 60,
			_60_61 = 61,
			_61_62 = 62,
			_62_63 = 63,
			_63_64 = 64,
		}
		[Field("flags", typeof(Flags1Options))]
		public int Flags1;
		[Field("animation index", typeof(AnimationIndex2Options))]
		public short AnimationIndex2;
		[Field("intro animation delay milliseconds", null)]
		public short IntroAnimationDelayMilliseconds3;
		[Field("render depth bias", null)]
		public short RenderDepthBias4;
		[Field("", null)]
		public fixed byte _5[2];
		[Field("objects", null)]
		[Block("Ui Object Reference Block", 32, typeof(UiObjectReferenceBlock))]
		public TagBlock Objects6;
		[Field("lights", null)]
		[Block("Ui Light Reference Block", 8, typeof(UiLightReferenceBlock))]
		public TagBlock Lights7;
		[Field("animation scale factor", null)]
		public Vector3 AnimationScaleFactor8;
		public Vector3 CameraPosition9;
		[Field("", null)]
		public fixed byte _10[24];
		[Field("fov degress", null)]
		public float FovDegress11;
		[Field("ui viewport", null)]
		public Rectangle2 UiViewport12;
		[Field("UNUSED intro anim", null)]
		public StringId UNUSEDIntroAnim13;
		[Field("UNUSED outro anim", null)]
		public StringId UNUSEDOutroAnim14;
		[Field("UNUSED ambient anim", null)]
		public StringId UNUSEDAmbientAnim15;
	}
}
#pragma warning restore CS1591
