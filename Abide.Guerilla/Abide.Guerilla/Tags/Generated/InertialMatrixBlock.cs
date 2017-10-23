using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(36, 4)]
	public unsafe struct InertialMatrixBlock
	{
		[Field("yy+zz    -xy     -zx", null)]
		public Vector3 YyZzXyZx0;
		[Field(" -xy    zz+xx    -yz", null)]
		public Vector3 XyZzXxYz1;
		[Field(" -zx     -yz    xx+yy", null)]
		public Vector3 ZxYzXxYy2;
	}
}
#pragma warning restore CS1591
