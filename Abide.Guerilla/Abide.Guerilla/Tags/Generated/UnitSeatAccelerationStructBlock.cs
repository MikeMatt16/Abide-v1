using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct UnitSeatAccelerationStructBlock
	{
		[Field("acceleration range:world units per second squared", null)]
		public Vector3 AccelerationRange0;
		[Field("accel action scale:actions fail [0,1+]", null)]
		public float AccelActionScale1;
		[Field("accel attach scale:detach unit [0,1+]", null)]
		public float AccelAttachScale2;
	}
}
#pragma warning restore CS1591
