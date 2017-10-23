using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(124, 4)]
	public unsafe struct HudWidgetEffectBlock
	{
		public enum Flags1Options
		{
			ApplyScale_0 = 1,
			ApplyTheta_1 = 2,
			ApplyOffset_2 = 4,
		}
		[Field("flags", typeof(Flags1Options))]
		public short Flags1;
		[Field("", null)]
		public fixed byte _2[2];
		[Field("your mom", typeof(HudWidgetEffectFunctionStructBlock))]
		[Block("Hud Widget Effect Function Struct", 1, typeof(HudWidgetEffectFunctionStructBlock))]
		public HudWidgetEffectFunctionStructBlock YourMom4;
		[Field("your mom", typeof(HudWidgetEffectFunctionStructBlock))]
		[Block("Hud Widget Effect Function Struct", 1, typeof(HudWidgetEffectFunctionStructBlock))]
		public HudWidgetEffectFunctionStructBlock YourMom5;
		[Field("your mom", typeof(HudWidgetEffectFunctionStructBlock))]
		[Block("Hud Widget Effect Function Struct", 1, typeof(HudWidgetEffectFunctionStructBlock))]
		public HudWidgetEffectFunctionStructBlock YourMom7;
		[Field("your mom", typeof(HudWidgetEffectFunctionStructBlock))]
		[Block("Hud Widget Effect Function Struct", 1, typeof(HudWidgetEffectFunctionStructBlock))]
		public HudWidgetEffectFunctionStructBlock YourMom9;
		[Field("your mom", typeof(HudWidgetEffectFunctionStructBlock))]
		[Block("Hud Widget Effect Function Struct", 1, typeof(HudWidgetEffectFunctionStructBlock))]
		public HudWidgetEffectFunctionStructBlock YourMom10;
	}
}
#pragma warning restore CS1591
