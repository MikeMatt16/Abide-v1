using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct CheatPowerupsBlock
	{
		[Field("powerup^", null)]
		public TagReference Powerup0;
	}
}
#pragma warning restore CS1591
