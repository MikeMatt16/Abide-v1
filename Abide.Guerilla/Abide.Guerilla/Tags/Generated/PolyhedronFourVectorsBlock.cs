using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(48, 16)]
	public unsafe struct PolyhedronFourVectorsBlock
	{
		[Field("four vectors x*", null)]
		public Vector3 FourVectorsX0;
		[Field("", null)]
		public fixed byte _1[4];
		[Field("four vectors y*", null)]
		public Vector3 FourVectorsY2;
		[Field("", null)]
		public fixed byte _3[4];
		[Field("four vectors z*", null)]
		public Vector3 FourVectorsZ4;
		[Field("", null)]
		public fixed byte _5[4];
	}
}
#pragma warning restore CS1591
