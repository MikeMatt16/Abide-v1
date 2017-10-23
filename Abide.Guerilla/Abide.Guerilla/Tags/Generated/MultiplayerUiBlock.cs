using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(56, 4)]
	public unsafe struct MultiplayerUiBlock
	{
		[Field("random player names", null)]
		public TagReference RandomPlayerNames0;
		[Field("obsolete profile colors", null)]
		[Block("Multiplayer Color Block", 32, typeof(MultiplayerColorBlock))]
		public TagBlock ObsoleteProfileColors1;
		[Field("team colors", null)]
		[Block("Multiplayer Color Block", 32, typeof(MultiplayerColorBlock))]
		public TagBlock TeamColors2;
		[Field("team names", null)]
		public TagReference TeamNames3;
	}
}
#pragma warning restore CS1591
