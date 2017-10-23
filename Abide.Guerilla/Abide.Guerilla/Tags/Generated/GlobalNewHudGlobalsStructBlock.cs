using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(260, 4)]
	public unsafe struct GlobalNewHudGlobalsStructBlock
	{
		[Field("hud text", null)]
		public TagReference HudText0;
		[Field("dashlights", null)]
		[Block("Hud Dashlights Block", 9, typeof(HudDashlightsBlock))]
		public TagBlock Dashlights1;
		[Field("waypoint arrows", null)]
		[Block("Hud Waypoint Arrow Block", 4, typeof(HudWaypointArrowBlock))]
		public TagBlock WaypointArrows2;
		[Field("waypoints", null)]
		[Block("Hud Waypoint Block", 8, typeof(HudWaypointBlock))]
		public TagBlock Waypoints3;
		[Field("hud sounds", null)]
		[Block("New Hud Sound Block", 6, typeof(NewHudSoundBlock))]
		public TagBlock HudSounds4;
		[Field("player training data", null)]
		[Block("Player Training Entry Data Block", 32, typeof(PlayerTrainingEntryDataBlock))]
		public TagBlock PlayerTrainingData5;
		[Field("constants", typeof(GlobalNewHudGlobalsConstantsStructBlock))]
		[Block("Global New Hud Globals Constants Struct", 1, typeof(GlobalNewHudGlobalsConstantsStructBlock))]
		public GlobalNewHudGlobalsConstantsStructBlock Constants6;
	}
}
#pragma warning restore CS1591
