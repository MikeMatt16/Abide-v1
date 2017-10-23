using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(12, 4)]
	public unsafe struct CollisionModelNodeBlock
	{
		[Field("name^*", null)]
		public StringId Name0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("parent node*", null)]
		public short ParentNode2;
		[Field("next sibling node*", null)]
		public short NextSiblingNode3;
		[Field("first child node*", null)]
		public short FirstChildNode4;
	}
}
#pragma warning restore CS1591
