using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(256, 16)]
	public unsafe struct PolyhedraBlock
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
		[Field("radius*", null)]
		public float Radius14;
		[Field("aabb half extents*", null)]
		public Vector3 AabbHalfExtents15;
		[Field("", null)]
		public fixed byte _16[4];
		[Field("aabb center*", null)]
		public Vector3 AabbCenter17;
		[Field("", null)]
		public fixed byte _18[4];
		[Field("", null)]
		public fixed byte _19[4];
		[Field("four vectors size*", null)]
		public int FourVectorsSize20;
		[Field("four vectors capacity*", null)]
		public int FourVectorsCapacity21;
		[Field("num vertices*", null)]
		public int NumVertices22;
		[Field("four vectors x*", null)]
		public Vector3 FourVectorsX24;
		[Field("", null)]
		public fixed byte _25[4];
		[Field("four vectors y*", null)]
		public Vector3 FourVectorsY26;
		[Field("", null)]
		public fixed byte _27[4];
		[Field("four vectors z*", null)]
		public Vector3 FourVectorsZ28;
		[Field("", null)]
		public fixed byte _29[4];
		[Field("", null)]
		public fixed byte _31[4];
		[Field("plane equations size*", null)]
		public int PlaneEquationsSize32;
		[Field("plane equations capacity*", null)]
		public int PlaneEquationsCapacity33;
		[Field("", null)]
		public fixed byte _34[4];
	}
}
#pragma warning restore CS1591
