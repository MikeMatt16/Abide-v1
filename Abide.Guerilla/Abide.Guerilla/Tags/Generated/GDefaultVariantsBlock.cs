using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct GDefaultVariantsBlock
	{
		public enum VariantType1Options
		{
			Slayer_0 = 0,
			Oddball_1 = 1,
			Juggernaut_2 = 2,
			King_3 = 3,
			Ctf_4 = 4,
			Invasion_5 = 5,
			Territories_6 = 6,
		}
		[Field("variant name^", null)]
		public StringId VariantName0;
		[Field("variant type", typeof(VariantType1Options))]
		public int VariantType1;
		[Field("settings", null)]
		[Block("G Default Variant Settings Block", 112, typeof(GDefaultVariantSettingsBlock))]
		public TagBlock Settings2;
		[Field("description index", null)]
		public int DescriptionIndex3;
		[Field("", null)]
		public fixed byte _4[3];
	}
}
#pragma warning restore CS1591
