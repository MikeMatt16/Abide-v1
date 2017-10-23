using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("user_interface_shared_globals_definition", "wigl", "����", typeof(UserInterfaceSharedGlobalsDefinitionBlock))]
	[FieldSet(652, 4)]
	public unsafe struct UserInterfaceSharedGlobalsDefinitionBlock
	{
		public enum FullScreenHeaderTextFont69Options
		{
			Terminal_0 = 0,
			BodyText_1 = 1,
			Title_2 = 2,
			SuperLargeFont_3 = 3,
			LargeBodyText_4 = 4,
			SplitScreenHudMessage_5 = 5,
			FullScreenHudMessage_6 = 6,
			EnglishBodyText_7 = 7,
			HUDNumberText_8 = 8,
			SubtitleFont_9 = 9,
			MainMenuFont_10 = 10,
			TextChatFont_11 = 11,
		}
		public enum LargeDialogHeaderTextFont70Options
		{
			Terminal_0 = 0,
			BodyText_1 = 1,
			Title_2 = 2,
			SuperLargeFont_3 = 3,
			LargeBodyText_4 = 4,
			SplitScreenHudMessage_5 = 5,
			FullScreenHudMessage_6 = 6,
			EnglishBodyText_7 = 7,
			HUDNumberText_8 = 8,
			SubtitleFont_9 = 9,
			MainMenuFont_10 = 10,
			TextChatFont_11 = 11,
		}
		public enum HalfDialogHeaderTextFont71Options
		{
			Terminal_0 = 0,
			BodyText_1 = 1,
			Title_2 = 2,
			SuperLargeFont_3 = 3,
			LargeBodyText_4 = 4,
			SplitScreenHudMessage_5 = 5,
			FullScreenHudMessage_6 = 6,
			EnglishBodyText_7 = 7,
			HUDNumberText_8 = 8,
			SubtitleFont_9 = 9,
			MainMenuFont_10 = 10,
			TextChatFont_11 = 11,
		}
		public enum QtrDialogHeaderTextFont72Options
		{
			Terminal_0 = 0,
			BodyText_1 = 1,
			Title_2 = 2,
			SuperLargeFont_3 = 3,
			LargeBodyText_4 = 4,
			SplitScreenHudMessage_5 = 5,
			FullScreenHudMessage_6 = 6,
			EnglishBodyText_7 = 7,
			HUDNumberText_8 = 8,
			SubtitleFont_9 = 9,
			MainMenuFont_10 = 10,
			TextChatFont_11 = 11,
		}
		[Field("", null)]
		public fixed byte _0[2];
		[Field("", null)]
		public fixed byte _1[2];
		[Field("", null)]
		public fixed byte _2[16];
		[Field("", null)]
		public fixed byte _3[8];
		[Field("", null)]
		public fixed byte _4[8];
		[Field("", null)]
		public fixed byte _5[16];
		[Field("", null)]
		public fixed byte _6[8];
		[Field("", null)]
		public fixed byte _7[8];
		[Field("overlayed screen alpha mod", null)]
		public float OverlayedScreenAlphaMod9;
		[Field("inc. text update period:milliseconds", null)]
		public short IncTextUpdatePeriod10;
		[Field("inc. text block character:ASCII code", null)]
		public short IncTextBlockCharacter11;
		[Field("callout text scale", null)]
		public float CalloutTextScale12;
		[Field("progress bar color", null)]
		public ColorArgbF ProgressBarColor13;
		[Field("near clip plane distance:objects closer than this are not drawn", null)]
		public float NearClipPlaneDistance14;
		[Field("projection plane distance:distance at which objects are rendered when z=0 (normal size)", null)]
		public float ProjectionPlaneDistance15;
		[Field("far clip plane distance:objects farther than this are not drawn", null)]
		public float FarClipPlaneDistance16;
		[Field("overlayed interface color", null)]
		public ColorArgbF OverlayedInterfaceColor18;
		[Field("", null)]
		public fixed byte _19[12];
		[Field("errors", null)]
		[Block("Ui Error Category Block", 100, typeof(UiErrorCategoryBlock))]
		public TagBlock Errors21;
		[Field("sound tag", null)]
		public TagReference SoundTag23;
		[Field("sound tag", null)]
		public TagReference SoundTag25;
		[Field("sound tag", null)]
		public TagReference SoundTag27;
		[Field("sound tag", null)]
		public TagReference SoundTag29;
		[Field("sound tag", null)]
		public TagReference SoundTag31;
		[Field("sound tag", null)]
		public TagReference SoundTag33;
		[Field("sound tag", null)]
		public TagReference SoundTag35;
		[Field("sound tag", null)]
		public TagReference SoundTag37;
		[Field("sound tag", null)]
		public TagReference SoundTag39;
		[Field("sound tag", null)]
		public TagReference SoundTag41;
		[Field("sound tag", null)]
		public TagReference SoundTag43;
		[Field("", null)]
		public TagReference _44;
		[Field("sound tag", null)]
		public TagReference SoundTag46;
		[Field("", null)]
		public TagReference _47;
		[Field("", null)]
		public TagReference _48;
		[Field("", null)]
		public TagReference _49;
		[Field("global bitmaps tag", null)]
		public TagReference GlobalBitmapsTag51;
		[Field("unicode string list tag", null)]
		public TagReference UnicodeStringListTag53;
		[Field("screen animations", null)]
		[Block("Animation Reference Block", 64, typeof(AnimationReferenceBlock))]
		public TagBlock ScreenAnimations55;
		[Field("shape groups", null)]
		[Block("Shape Group Reference Block", 64, typeof(ShapeGroupReferenceBlock))]
		public TagBlock ShapeGroups57;
		[Field("animations", null)]
		[Block("Persistent Background Animation Block", 100, typeof(PersistentBackgroundAnimationBlock))]
		public TagBlock Animations59;
		[Field("list item skins", null)]
		[Block("List Skin Reference Block", 32, typeof(ListSkinReferenceBlock))]
		public TagBlock ListItemSkins61;
		[Field("button key type strings", null)]
		public TagReference ButtonKeyTypeStrings63;
		[Field("game type strings", null)]
		public TagReference GameTypeStrings64;
		[Field("", null)]
		public TagReference _65;
		[Field("skill mappings", null)]
		[Block("Skill To Rank Mapping Block", 65535, typeof(SkillToRankMappingBlock))]
		public TagBlock SkillMappings67;
		[Field("full screen header text font", typeof(FullScreenHeaderTextFont69Options))]
		public short FullScreenHeaderTextFont69;
		[Field("large dialog header text font", typeof(LargeDialogHeaderTextFont70Options))]
		public short LargeDialogHeaderTextFont70;
		[Field("half dialog header text font", typeof(HalfDialogHeaderTextFont71Options))]
		public short HalfDialogHeaderTextFont71;
		[Field("qtr dialog header text font", typeof(QtrDialogHeaderTextFont72Options))]
		public short QtrDialogHeaderTextFont72;
		[Field("default text color", null)]
		public ColorArgbF DefaultTextColor73;
		[Field("full screen header text bounds", null)]
		public Rectangle2 FullScreenHeaderTextBounds74;
		[Field("full screen button key text bounds", null)]
		public Rectangle2 FullScreenButtonKeyTextBounds75;
		[Field("large dialog header text bounds", null)]
		public Rectangle2 LargeDialogHeaderTextBounds76;
		[Field("large dialog button key text bounds", null)]
		public Rectangle2 LargeDialogButtonKeyTextBounds77;
		[Field("half dialog header text bounds", null)]
		public Rectangle2 HalfDialogHeaderTextBounds78;
		[Field("half dialog button key text bounds", null)]
		public Rectangle2 HalfDialogButtonKeyTextBounds79;
		[Field("qtr dialog header text bounds", null)]
		public Rectangle2 QtrDialogHeaderTextBounds80;
		[Field("qtr dialog button key text bounds", null)]
		public Rectangle2 QtrDialogButtonKeyTextBounds81;
		[Field("main menu music", null)]
		public TagReference MainMenuMusic83;
		[Field("music fade time:milliseconds", null)]
		public int MusicFadeTime84;
	}
}
#pragma warning restore CS1591
