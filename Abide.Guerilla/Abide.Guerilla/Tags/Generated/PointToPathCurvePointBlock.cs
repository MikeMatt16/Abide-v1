using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct PointToPathCurvePointBlock
	{
		public Vector3 Position0;
		[Field("t value*", null)]
		public float TValue1;
	}
}
#pragma warning restore CS1591
