using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(96, 4)]
	public unsafe struct ScenarioSceneryBlock
	{
		[Field("Type", null)]
		public short Type1;
		[Field("Name^", null)]
		public short Name3;
		[Field("Object Data", typeof(ScenarioObjectDatumStructBlock))]
		[Block("Scenario Object Datum Struct", 1, typeof(ScenarioObjectDatumStructBlock))]
		public ScenarioObjectDatumStructBlock ObjectData4;
		[Field("Permutation Data", typeof(ScenarioObjectPermutationStructBlock))]
		[Block("Scenario Object Permutation Struct", 1, typeof(ScenarioObjectPermutationStructBlock))]
		public ScenarioObjectPermutationStructBlock PermutationData5;
		[Field("Scenery Data", typeof(ScenarioSceneryDatumStructV4Block))]
		[Block("Scenario Scenery Datum Struct V4", 1, typeof(ScenarioSceneryDatumStructV4Block))]
		public ScenarioSceneryDatumStructV4Block SceneryData6;
	}
}
#pragma warning restore CS1591
