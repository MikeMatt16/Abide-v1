using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct GlobalVisibilityBoundsBlock
	{
		[Field("Position x*", null)]
		public float PositionX0;
		[Field("Position y*", null)]
		public float PositionY1;
		[Field("Position z*", null)]
		public float PositionZ2;
		[Field("Radius*", null)]
		public float Radius3;
		[Field("Node 0*", null)]
		public int Node04;
		[Field("", null)]
		public fixed byte _5[3];
	}
}
#pragma warning restore CS1591
