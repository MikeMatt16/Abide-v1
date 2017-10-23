using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(92, 4)]
	public unsafe struct ModelNodeBlock
	{
		[Field("name*^", null)]
		public StringId Name0;
		[Field("parent node*", null)]
		public short ParentNode1;
		[Field("first child node*", null)]
		public short FirstChildNode2;
		[Field("next sibling node*", null)]
		public short NextSiblingNode3;
		[Field("", null)]
		public fixed byte _4[2];
		public Vector3 DefaultTranslation5;
		[Field("default rotation*", null)]
		public Quaternion DefaultRotation6;
		[Field("default inverse scale*", null)]
		public float DefaultInverseScale7;
		[Field("default inverse forward*", null)]
		public Vector3 DefaultInverseForward8;
		[Field("default inverse left*", null)]
		public Vector3 DefaultInverseLeft9;
		[Field("default inverse up*", null)]
		public Vector3 DefaultInverseUp10;
		public Vector3 DefaultInversePosition11;
	}
}
#pragma warning restore CS1591
