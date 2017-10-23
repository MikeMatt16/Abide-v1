using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(264, 4)]
	public unsafe struct RuntimeCampaignLevelBlock
	{
		[Field("Campaign ID", null)]
		public int CampaignID0;
		[Field("Map ID", null)]
		public int MapID1;
		[Field("Path", null)]
		public LongString Path2;
	}
}
#pragma warning restore CS1591
