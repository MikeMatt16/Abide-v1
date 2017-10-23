using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("new_hud_definition", "nhdt", "����", typeof(NewHudDefinitionBlock))]
	[FieldSet(60, 4)]
	public unsafe struct NewHudDefinitionBlock
	{
		[Field("DO NOT USE", null)]
		public TagReference DONOTUSE0;
		[Field("bitmap widgets", null)]
		[Block("Hud Bitmap Widgets", 256, typeof(HudBitmapWidgets))]
		public TagBlock BitmapWidgets1;
		[Field("text widgets", null)]
		[Block("Hud Text Widgets", 256, typeof(HudTextWidgets))]
		public TagBlock TextWidgets2;
		[Field("dashlight data", typeof(NewHudDashlightDataStructBlock))]
		[Block("New Hud Dashlight Data Struct", 1, typeof(NewHudDashlightDataStructBlock))]
		public NewHudDashlightDataStructBlock DashlightData3;
		[Field("screen effect widgets", null)]
		[Block("Hud Screen Effect Widgets", 4, typeof(HudScreenEffectWidgets))]
		public TagBlock ScreenEffectWidgets4;
	}
}
#pragma warning restore CS1591
