using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("multiplayer_globals", "mulg", "����", typeof(MultiplayerGlobalsBlock))]
	[FieldSet(24, 4)]
	public unsafe struct MultiplayerGlobalsBlock
	{
		[Field("universal", null)]
		[Block("Multiplayer Universal Block", 1, typeof(MultiplayerUniversalBlock))]
		public TagBlock Universal0;
		[Field("runtime", null)]
		[Block("Multiplayer Runtime Block", 1, typeof(MultiplayerRuntimeBlock))]
		public TagBlock Runtime1;
	}
}
#pragma warning restore CS1591
