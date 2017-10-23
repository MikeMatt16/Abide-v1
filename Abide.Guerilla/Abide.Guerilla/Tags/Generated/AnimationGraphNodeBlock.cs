using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(32, 4)]
	public unsafe struct AnimationGraphNodeBlock
	{
		public enum ModelFlags4Options
		{
			PrimaryModel_0 = 1,
			SecondaryModel_1 = 2,
			LocalRoot_2 = 4,
			LeftHand_3 = 8,
			RightHand_4 = 16,
			LeftArmMember_5 = 32,
		}
		public enum NodeJointFlags5Options
		{
			BallSocket_0 = 1,
			Hinge_1 = 2,
			NoMovement_2 = 4,
		}
		[Field("name^", null)]
		public StringId Name0;
		[Field("next sibling node index*", null)]
		public short NextSiblingNodeIndex1;
		[Field("first child node index*", null)]
		public short FirstChildNodeIndex2;
		[Field("parent node index*", null)]
		public short ParentNodeIndex3;
		[Field("model flags*", typeof(ModelFlags4Options))]
		public byte ModelFlags4;
		[Field("node joint flags", typeof(NodeJointFlags5Options))]
		public byte NodeJointFlags5;
		[Field("base vector*", null)]
		public Vector3 BaseVector6;
		[Field("vector range*", null)]
		public float VectorRange7;
		[Field("z_pos*", null)]
		public float ZPos8;
	}
}
#pragma warning restore CS1591
