using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(12, 4)]
	public unsafe struct RuntimeLevelsDefinitionBlock
	{
		[Field("Campaign Levels", null)]
		[Block("Runtime Campaign Level Block", 20, typeof(RuntimeCampaignLevelBlock))]
		public TagBlock CampaignLevels0;
	}
}
#pragma warning restore CS1591
