using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(12, 4)]
	public unsafe struct ShaderTextureStateKillStateBlock
	{
		public enum Flags0Options
		{
			AlphaKill_0 = 1,
		}
		public enum ColorkeyMode2Options
		{
			Disabled_0 = 0,
			ZeroAlpha_1 = 1,
			ZeroARGB_2 = 2,
			Kill_3 = 3,
		}
		[Field("flags", typeof(Flags0Options))]
		public short Flags0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("colorkey mode", typeof(ColorkeyMode2Options))]
		public short ColorkeyMode2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("colorkey color", null)]
		public ColorRgb ColorkeyColor4;
	}
}
#pragma warning restore CS1591
