using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct CharacterReadyBlock
	{
		[Field("ready time bounds#Character will pause for given time before engaging threat", null)]
		public FloatBounds ReadyTimeBounds0;
	}
}
#pragma warning restore CS1591
