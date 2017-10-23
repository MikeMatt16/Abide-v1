using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct DecoratorCellCollectionBlock
	{
		[Field("Child Index", null)]
		public short ChildIndex1;
		[Field("Cache Block Index", null)]
		public short CacheBlockIndex3;
		[Field("Group Count", null)]
		public short GroupCount4;
		[Field("Group Start Index", null)]
		public int GroupStartIndex5;
	}
}
#pragma warning restore CS1591
