using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct ClothVerticesBlock
	{
		public Vector3 InitialPosition0;
		[Field("uv*", null)]
		public Vector2 Uv1;
	}
}
#pragma warning restore CS1591
