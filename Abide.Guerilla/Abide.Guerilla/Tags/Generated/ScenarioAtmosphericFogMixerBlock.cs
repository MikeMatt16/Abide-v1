using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct ScenarioAtmosphericFogMixerBlock
	{
		[Field("", null)]
		public fixed byte _0[4];
		[Field("Atmospheric Fog Source:From Scenario Atmospheric Fog Palette", null)]
		public StringId AtmosphericFogSource1;
		[Field("Interpolator:From Scenario Interpolators", null)]
		public StringId Interpolator2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("", null)]
		public fixed byte _4[2];
	}
}
#pragma warning restore CS1591
