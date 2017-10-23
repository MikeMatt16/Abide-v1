using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(28, 4)]
	public unsafe struct ScenarioStructureBspSphericalHarmonicLightingBlock
	{
		[Field("BSP*", null)]
		public TagReference BSP0;
		[Field("Lighting Points", null)]
		[Block("Scenario Spherical Harmonic Lighting Point", 32768, typeof(ScenarioSphericalHarmonicLightingPoint))]
		public TagBlock LightingPoints1;
	}
}
#pragma warning restore CS1591
