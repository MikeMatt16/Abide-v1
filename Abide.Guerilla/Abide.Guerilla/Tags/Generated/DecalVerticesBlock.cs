using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(32, 4)]
	public unsafe struct DecalVerticesBlock
	{
		public Vector3 Position0;
		[Field("texcoord 0*", null)]
		public Vector2 Texcoord01;
		[Field("texcoord 1*", null)]
		public Vector2 Texcoord12;
		[Field("Color*", null)]
		public ColorRgb Color3;
	}
}
#pragma warning restore CS1591
