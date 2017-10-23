using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct HudButtonIconBlock
	{
		public enum Flags5Options
		{
			UseTextFromStringListInstead_0 = 1,
			OverrideDefaultColor_1 = 2,
			WidthOffsetIsAbsoluteIconWidth_2 = 4,
		}
		[Field("sequence index#sequence index into the global hud icon bitmap", null)]
		public short SequenceIndex0;
		[Field("width offset#extra spacing beyond bitmap width for text alignment", null)]
		public short WidthOffset1;
		[Field("offset from reference corner", null)]
		public Vector2 OffsetFromReferenceCorner2;
		[Field("override icon color", null)]
		public ColorArgb OverrideIconColor3;
		[Field("frame rate [0,30]", null)]
		public int FrameRate0304;
		[Field("flags", typeof(Flags5Options))]
		public byte Flags5;
		[Field("text index", null)]
		public short TextIndex6;
	}
}
#pragma warning restore CS1591
