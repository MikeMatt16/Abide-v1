using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(36, 4)]
	public unsafe struct UserHintLineSegmentBlock
	{
		public enum Flags0Options
		{
			Bidirectional_0 = 1,
			Closed_1 = 2,
		}
		[Field("Flags", typeof(Flags0Options))]
		public int Flags0;
		public Vector3 Point01;
		[Field("reference frame*", null)]
		public short ReferenceFrame2;
		[Field("", null)]
		public fixed byte _3[2];
		public Vector3 Point14;
		[Field("reference frame*", null)]
		public short ReferenceFrame5;
		[Field("", null)]
		public fixed byte _6[2];
	}
}
#pragma warning restore CS1591
