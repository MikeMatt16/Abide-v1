using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(352, 4)]
	public unsafe struct GlobalHudScreenEffectDefinition
	{
		public enum Flags2Options
		{
			OnlyWhenZoomed_0 = 1,
			MirrorHorizontally_1 = 2,
			MirrorVertically_2 = 4,
			UseNewHotness_3 = 8,
		}
		public enum ScreenEffectFlags15Options
		{
			OnlyWhenZoomed_0 = 1,
		}
		public enum ScreenEffectFlags20Options
		{
			OnlyWhenZoomed_0 = 1,
		}
		[Field("", null)]
		public fixed byte _0[4];
		[Field("flags", typeof(Flags2Options))]
		public short Flags2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("", null)]
		public fixed byte _4[16];
		[Field("mask (fullscreen)", null)]
		public TagReference MaskFullscreen5;
		[Field("mask (splitscreen)", null)]
		public TagReference MaskSplitscreen6;
		[Field("", null)]
		public fixed byte _7[8];
		[Field("", null)]
		public fixed byte _8[20];
		[Field("", null)]
		public fixed byte _9[24];
		[Field("", null)]
		public fixed byte _10[8];
		[Field("", null)]
		public fixed byte _11[24];
		[Field("", null)]
		public fixed byte _12[20];
		[Field("", null)]
		public fixed byte _13[24];
		[Field("screen effect flags", typeof(ScreenEffectFlags15Options))]
		public int ScreenEffectFlags15;
		[Field("", null)]
		public fixed byte _16[32];
		[Field("screen effect", null)]
		public TagReference ScreenEffect17;
		[Field("", null)]
		public fixed byte _18[32];
		[Field("screen effect flags", typeof(ScreenEffectFlags20Options))]
		public int ScreenEffectFlags20;
		[Field("", null)]
		public fixed byte _21[32];
		[Field("screen effect", null)]
		public TagReference ScreenEffect22;
		[Field("", null)]
		public fixed byte _23[32];
	}
}
#pragma warning restore CS1591
