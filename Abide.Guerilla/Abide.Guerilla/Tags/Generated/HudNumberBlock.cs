using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("hud_number", "hud#", "����", typeof(HudNumberBlock))]
	[FieldSet(100, 4)]
	public unsafe struct HudNumberBlock
	{
		[Field("digits bitmap", null)]
		public TagReference DigitsBitmap0;
		[Field("bitmap digit width", null)]
		public int BitmapDigitWidth1;
		[Field("screen digit width", null)]
		public int ScreenDigitWidth2;
		[Field("x offset", null)]
		public int XOffset3;
		[Field("y offset", null)]
		public int YOffset4;
		[Field("decimal point width", null)]
		public int DecimalPointWidth5;
		[Field("colon width", null)]
		public int ColonWidth6;
		[Field("", null)]
		public fixed byte _7[2];
		[Field("", null)]
		public fixed byte _8[76];
	}
}
#pragma warning restore CS1591
