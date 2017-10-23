using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(36, 4)]
	public unsafe struct ScenarioCutsceneTitleBlock
	{
		public enum Justification2Options
		{
			Left_0 = 0,
			Right_1 = 1,
			Center_2 = 2,
			CustomTextEntry_3 = 3,
		}
		public enum Font3Options
		{
			TerminalFont_0 = 0,
			BodyTextFont_1 = 1,
			TitleFont_2 = 2,
			SuperLargeFont_3 = 3,
			LargeBodyTextFont_4 = 4,
			SplitScreenHudMessageFont_5 = 5,
			FullScreenHudMessageFont_6 = 6,
			EnglishBodyTextFont_7 = 7,
			HudNumberFont_8 = 8,
			SubtitleFont_9 = 9,
			MainMenuFont_10 = 10,
			TextChatFont_11 = 11,
		}
		[Field("name^", null)]
		public StringId Name0;
		[Field("text bounds (on screen)", null)]
		public Rectangle2 TextBoundsOnScreen1;
		[Field("justification", typeof(Justification2Options))]
		public short Justification2;
		[Field("font", typeof(Font3Options))]
		public short Font3;
		[Field("text color", null)]
		public ColorRgb TextColor4;
		[Field("shadow color", null)]
		public ColorRgb ShadowColor5;
		[Field("fade in time [seconds]", null)]
		public float FadeInTimeSeconds6;
		[Field("up time [seconds]", null)]
		public float UpTimeSeconds7;
		[Field("fade out time [seconds]", null)]
		public float FadeOutTimeSeconds8;
	}
}
#pragma warning restore CS1591
