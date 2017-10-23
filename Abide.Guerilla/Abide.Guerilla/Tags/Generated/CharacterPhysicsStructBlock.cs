using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(160, 4)]
	public unsafe struct CharacterPhysicsStructBlock
	{
		public enum Flags0Options
		{
			CenteredAtOrigin_0 = 1,
			ShapeSpherical_1 = 2,
			UsePlayerPhysics_2 = 4,
			ClimbAnySurface_3 = 8,
			Flying_4 = 16,
			NotPhysical_5 = 32,
			DeadCharacterCollisionGroup_6 = 64,
		}
		[Field("flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("height standing", null)]
		public float HeightStanding1;
		[Field("height crouching", null)]
		public float HeightCrouching2;
		[Field("radius", null)]
		public float Radius3;
		[Field("mass", null)]
		public float Mass4;
		[Field("living material name#collision material used when character is alive", null)]
		public StringId LivingMaterialName5;
		[Field("dead material name#collision material used when character is dead", null)]
		public StringId DeadMaterialName6;
		[Field("", null)]
		public fixed byte _7[16];
		[Field("", null)]
		public fixed byte _8[4];
		[Field("", null)]
		public fixed byte _9[20];
		[Field("dead sphere shapes", null)]
		[Block("Spheres Block", 1024, typeof(SpheresBlock))]
		public TagBlock DeadSphereShapes10;
		[Field("pill shapes", null)]
		[Block("Pills Block", 1024, typeof(PillsBlock))]
		public TagBlock PillShapes11;
		[Field("sphere shapes", null)]
		[Block("Spheres Block", 1024, typeof(SpheresBlock))]
		public TagBlock SphereShapes12;
		[Field("ground physics", typeof(CharacterPhysicsGroundStructBlock))]
		[Block("Character Physics Ground Struct", 1, typeof(CharacterPhysicsGroundStructBlock))]
		public CharacterPhysicsGroundStructBlock GroundPhysics14;
		[Field("flying physics", typeof(CharacterPhysicsFlyingStructBlock))]
		[Block("Character Physics Flying Struct", 1, typeof(CharacterPhysicsFlyingStructBlock))]
		public CharacterPhysicsFlyingStructBlock FlyingPhysics16;
		[Field("dead physics", typeof(CharacterPhysicsDeadStructBlock))]
		[Block("Character Physics Dead Struct", 1, typeof(CharacterPhysicsDeadStructBlock))]
		public CharacterPhysicsDeadStructBlock DeadPhysics18;
		[Field("sentinel physics", typeof(CharacterPhysicsSentinelStructBlock))]
		[Block("Character Physics Sentinel Struct", 1, typeof(CharacterPhysicsSentinelStructBlock))]
		public CharacterPhysicsSentinelStructBlock SentinelPhysics20;
	}
}
#pragma warning restore CS1591
