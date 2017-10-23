using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct HsReferencesBlock
	{
		[Field("", null)]
		public fixed byte _0[24];
		[Field("reference*^", null)]
		public TagReference Reference1;
	}
}
#pragma warning restore CS1591
