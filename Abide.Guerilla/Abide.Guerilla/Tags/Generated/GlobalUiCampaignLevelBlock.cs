using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(2904, 4)]
	public unsafe struct GlobalUiCampaignLevelBlock
	{
		[Field("Campaign ID", null)]
		public int CampaignID0;
		[Field("Map ID", null)]
		public int MapID1;
		[Field("Bitmap", null)]
		public TagReference Bitmap2;
		[Field("", null)]
		public fixed byte _3[576];
		[Field("", null)]
		public fixed byte _4[2304];
	}
}
#pragma warning restore CS1591
