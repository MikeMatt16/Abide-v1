using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct CreateNewVariantStructBlock
	{
		public enum _1Options
		{
			Slayer_0 = 0,
			Oddball_1 = 1,
			Juggernaut_2 = 2,
			King_3 = 3,
			Ctf_4 = 4,
			Invasion_5 = 5,
			Territories_6 = 6,
		}
		[Field("", null)]
		public StringId _0;
		[Field("", typeof(_1Options))]
		public int _1;
		[Field("settings", null)]
		[Block("G Default Variant Settings Block", 112, typeof(GDefaultVariantSettingsBlock))]
		public TagBlock Settings2;
		[Field("", null)]
		public int _3;
		[Field("", null)]
		public fixed byte _4[3];
	}
}
#pragma warning restore CS1591
