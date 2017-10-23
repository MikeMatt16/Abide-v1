using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(32, 4)]
	public unsafe struct ScenarioChildScenarioBlock
	{
		[Field("Child Scenario", null)]
		public TagReference ChildScenario0;
		[Field("", null)]
		public fixed byte _1[16];
	}
}
#pragma warning restore CS1591
