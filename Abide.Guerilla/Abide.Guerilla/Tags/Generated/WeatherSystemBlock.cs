using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("weather_system", "weat", "����", typeof(WeatherSystemBlock))]
	[FieldSet(188, 4)]
	public unsafe struct WeatherSystemBlock
	{
		[Field("particle system", null)]
		[Block("Global Particle System Lite Block", 1, typeof(GlobalParticleSystemLiteBlock))]
		public TagBlock ParticleSystem0;
		[Field("background plates", null)]
		[Block("Global Weather Background Plate Block", 3, typeof(GlobalWeatherBackgroundPlateBlock))]
		public TagBlock BackgroundPlates1;
		[Field("wind model", typeof(GlobalWindModelStructBlock))]
		[Block("Global Wind Model Struct", 1, typeof(GlobalWindModelStructBlock))]
		public GlobalWindModelStructBlock WindModel2;
		[Field("fade radius", null)]
		public float FadeRadius3;
	}
}
#pragma warning restore CS1591
