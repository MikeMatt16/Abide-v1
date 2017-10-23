using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct ParticlesRenderDataBlock
	{
		[Field("position.x*", null)]
		public float PositionX0;
		[Field("position.y*", null)]
		public float PositionY1;
		[Field("position.z*", null)]
		public float PositionZ2;
		[Field("size*", null)]
		public float Size3;
		[Field("color*", null)]
		public ColorRgb Color4;
	}
}
#pragma warning restore CS1591
