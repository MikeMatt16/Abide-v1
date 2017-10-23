using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(68, 4)]
	public unsafe struct UserHintParallelogramBlock
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
		public Vector3 Point27;
		[Field("reference frame*", null)]
		public short ReferenceFrame8;
		[Field("", null)]
		public fixed byte _9[2];
		public Vector3 Point310;
		[Field("reference frame*", null)]
		public short ReferenceFrame11;
		[Field("", null)]
		public fixed byte _12[2];
	}
}
#pragma warning restore CS1591
