using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("garbage", "garb", "item", typeof(GarbageBlock))]
	[FieldSet(168, 4)]
	public unsafe struct GarbageBlock
	{
		[Field("", null)]
		public fixed byte _0[168];
	}
}
#pragma warning restore CS1591
