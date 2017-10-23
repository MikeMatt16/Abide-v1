using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("scenario_structure_lighting_resource", "sslt", "����", typeof(ScenarioStructureLightingResourceBlock))]
	[FieldSet(12, 4)]
	public unsafe struct ScenarioStructureLightingResourceBlock
	{
		[Field("Structure Lighting", null)]
		[Block("Scenario Structure Bsp Spherical Harmonic Lighting Block", 16, typeof(ScenarioStructureBspSphericalHarmonicLightingBlock))]
		public TagBlock StructureLighting0;
	}
}
#pragma warning restore CS1591
