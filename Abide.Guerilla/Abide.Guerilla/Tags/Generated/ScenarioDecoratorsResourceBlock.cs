using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("scenario_decorators_resource", "dc*s", "����", typeof(ScenarioDecoratorsResourceBlock))]
	[FieldSet(24, 4)]
	public unsafe struct ScenarioDecoratorsResourceBlock
	{
		[Field("Decorator", null)]
		[Block("Decorator Placement Definition Block", 1, typeof(DecoratorPlacementDefinitionBlock))]
		public TagBlock Decorator0;
		[Field("Decorator Palette", null)]
		[Block("Scenario Decorator Set Palette Entry Block", 32, typeof(ScenarioDecoratorSetPaletteEntryBlock))]
		public TagBlock DecoratorPalette1;
	}
}
#pragma warning restore CS1591
