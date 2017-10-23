using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct FlockSinkBlock
	{
		[Field("position", null)]
		public Vector3 Position0;
		[Field("radius", null)]
		public float Radius1;
	}
}
#pragma warning restore CS1591
