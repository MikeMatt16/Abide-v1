using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct StaticSpawnZoneDataStructBlock
	{
		public enum RelevantTeam1Options
		{
			RedAlpha_0 = 1,
			BlueBravo_1 = 2,
			YellowCharlie_2 = 4,
			GreenDelta_3 = 8,
			PurpleEcho_4 = 16,
			OrangeFoxtrot_5 = 32,
			BrownGolf_6 = 64,
			PinkHotel_7 = 128,
			NEUTRAL_8 = 256,
		}
		public enum RelevantGames3Options
		{
			Slayer_0 = 1,
			Oddball_1 = 2,
			KingOfTheHill_2 = 4,
			CaptureTheFlag_3 = 8,
			Race_4 = 16,
			Headhunter_5 = 32,
			Juggernaut_6 = 64,
			Territories_7 = 128,
		}
		public enum Flags4Options
		{
			DisabledIfFlagHome_0 = 1,
			DisabledIfFlagAway_1 = 2,
			DisabledIfBombHome_2 = 4,
			DisabledIfBombAway_3 = 8,
		}
		[Field("Name", null)]
		public StringId Name0;
		[Field("Relevant Team", typeof(RelevantTeam1Options))]
		public int RelevantTeam1;
		[Field("Relevant Games", typeof(RelevantGames3Options))]
		public int RelevantGames3;
		[Field("Flags", typeof(Flags4Options))]
		public int Flags4;
	}
}
#pragma warning restore CS1591
