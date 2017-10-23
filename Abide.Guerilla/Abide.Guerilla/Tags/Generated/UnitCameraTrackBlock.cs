using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct UnitCameraTrackBlock
	{
		[Field("track", null)]
		public TagReference Track0;
		[Field("", null)]
		public fixed byte _1[12];
	}
}
#pragma warning restore CS1591
