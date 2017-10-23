using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(128, 4)]
	public unsafe struct Magazines
	{
		public enum Flags0Options
		{
			WastesRoundsWhenReloaded_0 = 1,
			EveryRoundMustBeChambered_1 = 2,
		}
		[Field("flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("rounds recharged:per second", null)]
		public short RoundsRecharged1;
		[Field("rounds total initial", null)]
		public short RoundsTotalInitial2;
		[Field("rounds total maximum", null)]
		public short RoundsTotalMaximum3;
		[Field("rounds loaded maximum", null)]
		public short RoundsLoadedMaximum4;
		[Field("", null)]
		public fixed byte _5[4];
		[Field("", null)]
		public fixed byte _6[4];
		[Field("reload time:seconds#the length of time it takes to load a single magazine into the weapon", null)]
		public float ReloadTime7;
		[Field("rounds reloaded", null)]
		public short RoundsReloaded8;
		[Field("", null)]
		public fixed byte _9[2];
		[Field("chamber time:seconds#the length of time it takes to chamber the next round", null)]
		public float ChamberTime10;
		[Field("", null)]
		public fixed byte _11[8];
		[Field("", null)]
		public fixed byte _12[16];
		[Field("reloading effect", null)]
		public TagReference ReloadingEffect13;
		[Field("reloading damage effect", null)]
		public TagReference ReloadingDamageEffect14;
		[Field("chambering effect", null)]
		public TagReference ChamberingEffect15;
		[Field("chambering damage effect", null)]
		public TagReference ChamberingDamageEffect16;
		[Field("", null)]
		public fixed byte _17[12];
		[Field("magazines", null)]
		[Block("Magazine Objects", 8, typeof(MagazineObjects))]
		public TagBlock Magazines18;
	}
}
#pragma warning restore CS1591
