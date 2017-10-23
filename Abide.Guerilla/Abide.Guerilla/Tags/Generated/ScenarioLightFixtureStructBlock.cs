using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct ScenarioLightFixtureStructBlock
	{
		[Field("Color", null)]
		public ColorRgbF Color0;
		[Field("Intensity", null)]
		public float Intensity1;
		[Field("Falloff Angle:Degrees", null)]
		public float FalloffAngle2;
		[Field("Cutoff Angle:Degrees", null)]
		public float CutoffAngle3;
	}
}
#pragma warning restore CS1591
