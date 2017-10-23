using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(4, 4)]
	public unsafe struct AnimationEffectEventBlock
	{
		[Field("effect", null)]
		public short Effect0;
		[Field("frame", null)]
		public short Frame1;
	}
}
#pragma warning restore CS1591
