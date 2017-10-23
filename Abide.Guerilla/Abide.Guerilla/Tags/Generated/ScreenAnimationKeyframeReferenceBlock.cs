using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct ScreenAnimationKeyframeReferenceBlock
	{
		[Field("", null)]
		public fixed byte _0[4];
		[Field("alpha", null)]
		public float Alpha1;
		public Vector3 Position2;
	}
}
#pragma warning restore CS1591
