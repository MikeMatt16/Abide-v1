using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct AnimationSoundEventBlock
	{
		[Field("sound", null)]
		public short Sound0;
		[Field("frame", null)]
		public short Frame1;
		[Field("marker name", null)]
		public StringId MarkerName2;
	}
}
#pragma warning restore CS1591
