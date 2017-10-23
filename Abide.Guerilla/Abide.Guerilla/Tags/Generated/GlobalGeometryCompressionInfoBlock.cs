using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(56, 4)]
	public unsafe struct GlobalGeometryCompressionInfoBlock
	{
		[Field("Position Bounds x*", null)]
		public FloatBounds PositionBoundsX0;
		[Field("Position Bounds y*", null)]
		public FloatBounds PositionBoundsY1;
		[Field("Position Bounds z*", null)]
		public FloatBounds PositionBoundsZ2;
		[Field("Texcoord Bounds x*", null)]
		public FloatBounds TexcoordBoundsX3;
		[Field("Texcoord Bounds y*", null)]
		public FloatBounds TexcoordBoundsY4;
		[Field("Secondary Texcoord Bounds x*", null)]
		public FloatBounds SecondaryTexcoordBoundsX5;
		[Field("Secondary Texcoord Bounds y*", null)]
		public FloatBounds SecondaryTexcoordBoundsY6;
	}
}
#pragma warning restore CS1591
