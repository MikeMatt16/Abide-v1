using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct RealVector4dBlock
	{
		[Field("vector3", null)]
		public Vector3 Vector30;
		[Field("w", null)]
		public float W1;
	}
}
#pragma warning restore CS1591
