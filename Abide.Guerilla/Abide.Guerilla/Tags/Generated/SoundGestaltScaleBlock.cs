using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct SoundGestaltScaleBlock
	{
		[Field("", typeof(SoundScaleModifiersStructBlock))]
		[Block("Sound Scale Modifiers Struct", 1, typeof(SoundScaleModifiersStructBlock))]
		public SoundScaleModifiersStructBlock _0;
	}
}
#pragma warning restore CS1591
