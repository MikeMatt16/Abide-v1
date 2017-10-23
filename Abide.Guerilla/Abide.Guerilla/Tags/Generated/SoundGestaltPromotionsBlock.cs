using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(36, 4)]
	public unsafe struct SoundGestaltPromotionsBlock
	{
		[Field("", typeof(SoundPromotionParametersStructBlock))]
		[Block("Sound Promotion Parameters Struct", 1, typeof(SoundPromotionParametersStructBlock))]
		public SoundPromotionParametersStructBlock _0;
	}
}
#pragma warning restore CS1591
