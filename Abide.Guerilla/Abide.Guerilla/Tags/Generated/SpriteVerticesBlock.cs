using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(48, 4)]
	public unsafe struct SpriteVerticesBlock
	{
		public Vector3 Position0;
		[Field("Offset*", null)]
		public Vector3 Offset1;
		[Field("Axis*", null)]
		public Vector3 Axis2;
		[Field("texcoord*", null)]
		public Vector2 Texcoord3;
		[Field("Color*", null)]
		public ColorRgb Color4;
	}
}
#pragma warning restore CS1591
