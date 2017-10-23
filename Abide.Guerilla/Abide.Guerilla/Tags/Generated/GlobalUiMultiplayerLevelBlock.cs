using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(3180, 4)]
	public unsafe struct GlobalUiMultiplayerLevelBlock
	{
		public enum Flags6Options
		{
			Unlockable_0 = 1,
		}
		[Field("Map ID", null)]
		public int MapID0;
		[Field("Bitmap", null)]
		public TagReference Bitmap1;
		[Field("", null)]
		public fixed byte _2[576];
		[Field("", null)]
		public fixed byte _3[2304];
		[Field("Path", null)]
		public LongString Path4;
		[Field("Sort Order", null)]
		public int SortOrder5;
		[Field("Flags", typeof(Flags6Options))]
		public byte Flags6;
		[Field("", null)]
		public fixed byte _7[3];
		[Field("Max Teams None", null)]
		public int MaxTeamsNone8;
		[Field("Max Teams CTF", null)]
		public int MaxTeamsCTF9;
		[Field("Max Teams Slayer", null)]
		public int MaxTeamsSlayer10;
		[Field("Max Teams Oddball", null)]
		public int MaxTeamsOddball11;
		[Field("Max Teams KOTH", null)]
		public int MaxTeamsKOTH12;
		[Field("Max Teams Race", null)]
		public int MaxTeamsRace13;
		[Field("Max Teams Headhunter", null)]
		public int MaxTeamsHeadhunter14;
		[Field("Max Teams Juggernaut", null)]
		public int MaxTeamsJuggernaut15;
		[Field("Max Teams Territories", null)]
		public int MaxTeamsTerritories16;
		[Field("Max Teams Assault", null)]
		public int MaxTeamsAssault17;
		[Field("Max Teams Stub 10", null)]
		public int MaxTeamsStub1018;
		[Field("Max Teams Stub 11", null)]
		public int MaxTeamsStub1119;
		[Field("Max Teams Stub 12", null)]
		public int MaxTeamsStub1220;
		[Field("Max Teams Stub 13", null)]
		public int MaxTeamsStub1321;
		[Field("Max Teams Stub 14", null)]
		public int MaxTeamsStub1422;
		[Field("Max Teams Stub 15", null)]
		public int MaxTeamsStub1523;
	}
}
#pragma warning restore CS1591
