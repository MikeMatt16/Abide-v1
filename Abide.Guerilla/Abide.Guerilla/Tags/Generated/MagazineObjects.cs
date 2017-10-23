using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct MagazineObjects
	{
		[Field("rounds", null)]
		public short Rounds0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("equipment^", null)]
		public TagReference Equipment2;
	}
}
#pragma warning restore CS1591
