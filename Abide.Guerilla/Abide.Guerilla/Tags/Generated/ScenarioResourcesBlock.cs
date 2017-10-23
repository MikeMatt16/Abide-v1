using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(36, 4)]
	public unsafe struct ScenarioResourcesBlock
	{
		[Field("References*", null)]
		[Block("Scenario Resource Reference Block", 16, typeof(ScenarioResourceReferenceBlock))]
		public TagBlock References0;
		[Field("Script Source*", null)]
		[Block("Scenario Hs Source Reference Block", 8, typeof(ScenarioHsSourceReferenceBlock))]
		public TagBlock ScriptSource1;
		[Field("AI Resources*", null)]
		[Block("Scenario Ai Resource Reference Block", 2, typeof(ScenarioAiResourceReferenceBlock))]
		public TagBlock AIResources2;
	}
}
#pragma warning restore CS1591
