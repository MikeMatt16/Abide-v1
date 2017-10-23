using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("scenario_cluster_data_resource", "clu*", "����", typeof(ScenarioClusterDataResourceBlock))]
	[FieldSet(60, 4)]
	public unsafe struct ScenarioClusterDataResourceBlock
	{
		[Field("Cluster Data", null)]
		[Block("Scenario Cluster Data Block", 16, typeof(ScenarioClusterDataBlock))]
		public TagBlock ClusterData0;
		[Field("Background Sound Palette", null)]
		[Block("Structure Bsp Background Sound Palette Block", 64, typeof(StructureBspBackgroundSoundPaletteBlock))]
		public TagBlock BackgroundSoundPalette1;
		[Field("Sound Environment Palette", null)]
		[Block("Structure Bsp Sound Environment Palette Block", 64, typeof(StructureBspSoundEnvironmentPaletteBlock))]
		public TagBlock SoundEnvironmentPalette2;
		[Field("Weather Palette", null)]
		[Block("Structure Bsp Weather Palette Block", 32, typeof(StructureBspWeatherPaletteBlock))]
		public TagBlock WeatherPalette3;
		[Field("Atmospheric Fog Palette", null)]
		[Block("Scenario Atmospheric Fog Palette", 127, typeof(ScenarioAtmosphericFogPalette))]
		public TagBlock AtmosphericFogPalette4;
	}
}
#pragma warning restore CS1591
