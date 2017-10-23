using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(56, 4)]
	public unsafe struct DecoratorModelVerticesBlock
	{
		public Vector3 Position0;
		[Field("normal*", null)]
		public Vector3 Normal1;
		[Field("tangent*", null)]
		public Vector3 Tangent2;
		[Field("binormal*", null)]
		public Vector3 Binormal3;
		[Field("texcoord*", null)]
		public Vector2 Texcoord4;
	}
}
#pragma warning restore CS1591
