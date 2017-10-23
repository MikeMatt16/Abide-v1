using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(116, 4)]
	public unsafe struct ConstraintBodiesStructBlock
	{
		[Field("name^*", null)]
		public StringId Name0;
		[Field("node a*", null)]
		public short NodeA1;
		[Field("node b*", null)]
		public short NodeB2;
		[Field("a scale*", null)]
		public float AScale3;
		[Field("a forward*", null)]
		public Vector3 AForward4;
		[Field("a left*", null)]
		public Vector3 ALeft5;
		[Field("a up*", null)]
		public Vector3 AUp6;
		public Vector3 APosition7;
		[Field("b scale*", null)]
		public float BScale8;
		[Field("b forward*", null)]
		public Vector3 BForward9;
		[Field("b left*", null)]
		public Vector3 BLeft10;
		[Field("b up*", null)]
		public Vector3 BUp11;
		public Vector3 BPosition12;
		[Field("edge index*", null)]
		public short EdgeIndex13;
		[Field("", null)]
		public fixed byte _14[2];
	}
}
#pragma warning restore CS1591
