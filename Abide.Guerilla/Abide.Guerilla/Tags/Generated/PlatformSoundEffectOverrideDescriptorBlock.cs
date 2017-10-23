using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(1, 4)]
	public unsafe struct PlatformSoundEffectOverrideDescriptorBlock
	{
		[Field("override descriptor", null)]
		public int OverrideDescriptor0;
	}
}
#pragma warning restore CS1591
