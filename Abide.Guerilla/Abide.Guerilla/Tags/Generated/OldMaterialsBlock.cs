using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(44, 4)]
	public unsafe struct OldMaterialsBlock
	{
		[Field("", null)]
		public fixed byte _0[4];
		[Field("new material name", null)]
		public StringId NewMaterialName1;
		[Field("new general material name", null)]
		public StringId NewGeneralMaterialName2;
		[Field("", null)]
		public fixed byte _3[88];
		[Field("", null)]
		public fixed byte _4[48];
		[Field("ground friction scale#fraction of original velocity parallel to the ground after one tick", null)]
		public float GroundFrictionScale6;
		[Field("ground friction normal k1 scale#cosine of angle at which friction falls off", null)]
		public float GroundFrictionNormalK1Scale7;
		[Field("ground friction normal k0 scale#cosine of angle at which friction is zero", null)]
		public float GroundFrictionNormalK0Scale8;
		[Field("ground depth scale#depth a point mass rests in the ground", null)]
		public float GroundDepthScale9;
		[Field("ground damp fraction scale#fraction of original velocity perpendicular to the ground after one tick", null)]
		public float GroundDampFractionScale10;
		[Field("", null)]
		public fixed byte _11[76];
		[Field("", null)]
		public fixed byte _12[624];
		[Field("melee hit sound", null)]
		public TagReference MeleeHitSound13;
	}
}
#pragma warning restore CS1591
