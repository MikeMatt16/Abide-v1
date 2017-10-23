using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("material_physics", "mpdt", "����", typeof(MaterialPhysicsBlock))]
	[FieldSet(20, 4)]
	public unsafe struct MaterialPhysicsBlock
	{
		[Field("ground friction scale#fraction of original velocity parallel to the ground after one tick", null)]
		public float GroundFrictionScale1;
		[Field("ground friction normal k1 scale#cosine of angle at which friction falls off", null)]
		public float GroundFrictionNormalK1Scale2;
		[Field("ground friction normal k0 scale#cosine of angle at which friction is zero", null)]
		public float GroundFrictionNormalK0Scale3;
		[Field("ground depth scale#depth a point mass rests in the ground", null)]
		public float GroundDepthScale4;
		[Field("ground damp fraction scale#fraction of original velocity perpendicular to the ground after one tick", null)]
		public float GroundDampFractionScale5;
	}
}
#pragma warning restore CS1591
