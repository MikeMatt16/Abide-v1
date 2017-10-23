using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(92, 4)]
	public unsafe struct StructureBspFakeLightprobesBlock
	{
		[Field("Object Identifier*", typeof(ScenarioObjectIdStructBlock))]
		[Block("Scenario Object Id Struct", 1, typeof(ScenarioObjectIdStructBlock))]
		public ScenarioObjectIdStructBlock ObjectIdentifier0;
		[Field("Render Lighting*", typeof(RenderLightingStructBlock))]
		[Block("Render Lighting Struct", 1, typeof(RenderLightingStructBlock))]
		public RenderLightingStructBlock RenderLighting1;
	}
}
#pragma warning restore CS1591
