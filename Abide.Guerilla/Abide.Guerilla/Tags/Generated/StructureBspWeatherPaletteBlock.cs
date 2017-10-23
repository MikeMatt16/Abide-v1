using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(152, 4)]
	public unsafe struct StructureBspWeatherPaletteBlock
	{
		[Field("Name^", null)]
		public String Name0;
		[Field("Weather System", null)]
		public TagReference WeatherSystem1;
		[Field("", null)]
		public fixed byte _2[2];
		[Field("", null)]
		public fixed byte _3[2];
		[Field("", null)]
		public fixed byte _4[32];
		[Field("Wind", null)]
		public TagReference Wind5;
		[Field("Wind Direction", null)]
		public Vector3 WindDirection6;
		[Field("Wind Magnitude", null)]
		public float WindMagnitude7;
		[Field("", null)]
		public fixed byte _8[4];
		[Field("Wind Scale Function", null)]
		public String WindScaleFunction9;
	}
}
#pragma warning restore CS1591
