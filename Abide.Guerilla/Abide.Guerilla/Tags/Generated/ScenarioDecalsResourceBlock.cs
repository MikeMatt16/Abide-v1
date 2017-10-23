using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("scenario_decals_resource", "dec*", "����", typeof(ScenarioDecalsResourceBlock))]
	[FieldSet(24, 4)]
	public unsafe struct ScenarioDecalsResourceBlock
	{
		[Field("Palette", null)]
		[Block("Scenario Decal Palette Block", 128, typeof(ScenarioDecalPaletteBlock))]
		public TagBlock Palette0;
		[Field("Decals", null)]
		[Block("Scenario Decals Block", 65536, typeof(ScenarioDecalsBlock))]
		public TagBlock Decals1;
	}
}
#pragma warning restore CS1591
