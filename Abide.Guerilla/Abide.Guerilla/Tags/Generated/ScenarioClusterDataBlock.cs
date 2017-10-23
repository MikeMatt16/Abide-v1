using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(80, 4)]
	public unsafe struct ScenarioClusterDataBlock
	{
		[Field("BSP*", null)]
		public TagReference BSP0;
		[Field("Background Sounds*", null)]
		[Block("Scenario Cluster Background Sounds Block", 512, typeof(ScenarioClusterBackgroundSoundsBlock))]
		public TagBlock BackgroundSounds1;
		[Field("Sound Environments*", null)]
		[Block("Scenario Cluster Sound Environments Block", 512, typeof(ScenarioClusterSoundEnvironmentsBlock))]
		public TagBlock SoundEnvironments2;
		[Field("BSP Checksum*", null)]
		public int BSPChecksum3;
		[Field("Cluster Centroids*", null)]
		[Block("Scenario Cluster Points Block", 512, typeof(ScenarioClusterPointsBlock))]
		public TagBlock ClusterCentroids4;
		[Field("Weather Properties*", null)]
		[Block("Scenario Cluster Weather Properties Block", 512, typeof(ScenarioClusterWeatherPropertiesBlock))]
		public TagBlock WeatherProperties5;
		[Field("Atmospheric Fog Properties*", null)]
		[Block("Scenario Cluster Atmospheric Fog Properties Block", 512, typeof(ScenarioClusterAtmosphericFogPropertiesBlock))]
		public TagBlock AtmosphericFogProperties6;
	}
}
#pragma warning restore CS1591
