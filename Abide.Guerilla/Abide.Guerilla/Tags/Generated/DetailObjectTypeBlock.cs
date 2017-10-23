using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(96, 4)]
	public unsafe struct DetailObjectTypeBlock
	{
		public enum TypeFlags2Options
		{
			Unused_0 = 1,
			Unused_1 = 2,
			InterpolateColorInHSV_2 = 4,
			MoreColors_3 = 8,
		}
		[Field("Name^", null)]
		public String Name0;
		[Field("Sequence Index:[0,15]", null)]
		public int SequenceIndex1;
		[Field("type flags", typeof(TypeFlags2Options))]
		public byte TypeFlags2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("Color Override Factor#Fraction of detail object color to use instead of the base map color in the environment:[0,1]", null)]
		public float ColorOverrideFactor4;
		[Field("", null)]
		public fixed byte _5[8];
		[Field("Near Fade Distance:world units", null)]
		public float NearFadeDistance6;
		[Field("Far Fade Distance:world units", null)]
		public float FarFadeDistance7;
		[Field("Size:world units per pixel", null)]
		public float Size8;
		[Field("", null)]
		public fixed byte _9[4];
		[Field("Minimum Color:[0,1]", null)]
		public ColorRgbF MinimumColor10;
		[Field("Maximum Color:[0,1]", null)]
		public ColorRgbF MaximumColor11;
		[Field("ambient color:[0,255]", null)]
		public ColorArgb AmbientColor12;
		[Field("", null)]
		public fixed byte _13[4];
	}
}
#pragma warning restore CS1591
