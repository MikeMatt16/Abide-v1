using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(60, 4)]
	public unsafe struct CsPointBlock
	{
		[Field("name^", null)]
		public String Name0;
		public Vector3 Position1;
		[Field("reference frame*", null)]
		public short ReferenceFrame2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("surface index", null)]
		public int SurfaceIndex4;
		[Field("facing direction", null)]
		public Vector2 FacingDirection5;
	}
}
#pragma warning restore CS1591
