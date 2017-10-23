using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct ScenarioObjectPermutationStructBlock
	{
		public enum ActiveChangeColors1Options
		{
			Primary_0 = 1,
			Secondary_1 = 2,
			Tertiary_2 = 4,
			Quaternary_3 = 8,
		}
		[Field("Variant Name", null)]
		public StringId VariantName0;
		[Field("Active Change Colors", typeof(ActiveChangeColors1Options))]
		public int ActiveChangeColors1;
		[Field("Primary Color", null)]
		public ColorRgb PrimaryColor2;
		[Field("Secondary Color", null)]
		public ColorRgb SecondaryColor3;
		[Field("Tertiary Color", null)]
		public ColorRgb TertiaryColor4;
		[Field("Quaternary Color", null)]
		public ColorRgb QuaternaryColor5;
		[Field("", null)]
		public fixed byte _6[16];
	}
}
#pragma warning restore CS1591
