using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct OldUnusedObjectIdentifiersBlock
	{
		[Field("Object ID", typeof(ScenarioObjectIdStructBlock))]
		[Block("Scenario Object Id Struct", 1, typeof(ScenarioObjectIdStructBlock))]
		public ScenarioObjectIdStructBlock ObjectID0;
	}
}
#pragma warning restore CS1591
