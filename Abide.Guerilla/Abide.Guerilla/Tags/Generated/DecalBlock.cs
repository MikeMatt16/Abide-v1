using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("decal", "deca", "����", typeof(DecalBlock))]
	[FieldSet(188, 4)]
	public unsafe struct DecalBlock
	{
		public enum Flags1Options
		{
			GeometryInheritedByNextDecalInChain_0 = 1,
			InterpolateColorInHsv_1 = 2,
			MoreColors_2 = 4,
			NoRandomRotation_3 = 8,
			UNUSED_4 = 16,
			SAPIENSnapToAxis_5 = 32,
			SAPIENIncrementalCounter_6 = 64,
			UNUSED_7 = 128,
			PreserveAspect_8 = 256,
			UNUSED_9 = 512,
		}
		public enum TypeControlsHowTheDecalWrapsOntoSurfaceGeometry2Options
		{
			Scratch_0 = 0,
			Splatter_1 = 1,
			Burn_2 = 2,
			PaintedSign_3 = 3,
		}
		public enum Layer3Options
		{
			LitAlphaBlendPrelight_0 = 0,
			LitAlphaBlend_1 = 1,
			DoubleMultiply_2 = 2,
			Multiply_3 = 3,
			Max_4 = 4,
			Add_5 = 5,
			Error_6 = 6,
		}
		[Field("flags", typeof(Flags1Options))]
		public short Flags1;
		[Field("type#controls how the decal wraps onto surface geometry", typeof(TypeControlsHowTheDecalWrapsOntoSurfaceGeometry2Options))]
		public short Type2;
		[Field("layer", typeof(Layer3Options))]
		public short Layer3;
		[Field("max overlapping count", null)]
		public short MaxOverlappingCount4;
		[Field("next decal in chain", null)]
		public TagReference NextDecalInChain5;
		[Field("radius:world units#0 defaults to 0.125", null)]
		public FloatBounds Radius6;
		[Field("radius overlap rejection:muliplier", null)]
		public float RadiusOverlapRejection7;
		[Field("", null)]
		public fixed byte _8[16];
		[Field("color lower bounds", null)]
		public ColorRgbF ColorLowerBounds9;
		[Field("color upper bounds", null)]
		public ColorRgbF ColorUpperBounds10;
		[Field("", null)]
		public fixed byte _11[12];
		[Field("", null)]
		public fixed byte _12[4];
		[Field("", null)]
		public fixed byte _13[28];
		[Field("lifetime:seconds", null)]
		public FloatBounds Lifetime14;
		[Field("decay time:seconds", null)]
		public FloatBounds DecayTime15;
		[Field("", null)]
		public fixed byte _16[12];
		[Field("", null)]
		public fixed byte _17[40];
		[Field("", null)]
		public fixed byte _18[2];
		[Field("", null)]
		public fixed byte _19[2];
		[Field("", null)]
		public fixed byte _20[2];
		[Field("", null)]
		public fixed byte _21[2];
		[Field("", null)]
		public fixed byte _22[20];
		[Field("bitmap", null)]
		public TagReference Bitmap23;
		[Field("", null)]
		public fixed byte _24[20];
		[Field("maximum sprite extent:pixels*", null)]
		public float MaximumSpriteExtent25;
		[Field("", null)]
		public fixed byte _26[4];
		[Field("", null)]
		public fixed byte _27[8];
	}
}
#pragma warning restore CS1591
