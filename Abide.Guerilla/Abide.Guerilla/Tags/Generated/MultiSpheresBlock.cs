using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(176, 16)]
	public unsafe struct MultiSpheresBlock
	{
		public enum Flags2Options
		{
			Unused_0 = 1,
		}
		[Field("name^*", null)]
		public StringId Name0;
		[Field("material*", null)]
		public short Material1;
		[Field("flags", typeof(Flags2Options))]
		public short Flags2;
		[Field("relative mass scale", null)]
		public float RelativeMassScale3;
		[Field("friction", null)]
		public float Friction4;
		[Field("restitution", null)]
		public float Restitution5;
		[Field("volume *", null)]
		public float Volume6;
		[Field("mass*", null)]
		public float Mass7;
		[Field("", null)]
		public fixed byte _8[2];
		[Field("phantom*", null)]
		public short Phantom9;
		[Field("", null)]
		public fixed byte _10[4];
		[Field("size*", null)]
		public short Size11;
		[Field("count*", null)]
		public short Count12;
		[Field("", null)]
		public fixed byte _13[4];
		[Field("num spheres*", null)]
		public int NumSpheres14;
		[Field("sphere*", null)]
		public Vector3 Sphere16;
		[Field("", null)]
		public fixed byte _17[4];
	}
}
#pragma warning restore CS1591
