using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(60, 4)]
	public unsafe struct MultiplayerUniversalBlock
	{
		[Field("random player names", null)]
		public TagReference RandomPlayerNames0;
		[Field("team names", null)]
		public TagReference TeamNames1;
		[Field("team colors", null)]
		[Block("Multiplayer Color Block", 32, typeof(MultiplayerColorBlock))]
		public TagBlock TeamColors2;
		[Field("multiplayer text", null)]
		public TagReference MultiplayerText3;
	}
}
#pragma warning restore CS1591
