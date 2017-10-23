using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(96, 4)]
	public unsafe struct RenderModelNodeBlock
	{
		[Field("parent node*", null)]
		public short ParentNode1;
		[Field("first child node*", null)]
		public short FirstChildNode2;
		[Field("next sibling node*", null)]
		public short NextSiblingNode3;
		[Field("import node index*", null)]
		public short ImportNodeIndex4;
		public Vector3 DefaultTranslation5;
		[Field("default rotation*", null)]
		public Quaternion DefaultRotation6;
		[Field("inverse forward*", null)]
		public Vector3 InverseForward7;
		[Field("inverse left*", null)]
		public Vector3 InverseLeft8;
		[Field("inverse up*", null)]
		public Vector3 InverseUp9;
		public Vector3 InversePosition10;
		[Field("inverse scale*", null)]
		public float InverseScale11;
		[Field("distance from parent*", null)]
		public float DistanceFromParent12;
	}
}
#pragma warning restore CS1591
