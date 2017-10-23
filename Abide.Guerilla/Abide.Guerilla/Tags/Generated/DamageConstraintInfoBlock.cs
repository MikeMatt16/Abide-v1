using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct DamageConstraintInfoBlock
	{
		[Field("physics model constraint name", null)]
		public StringId PhysicsModelConstraintName0;
		[Field("damage constraint name", null)]
		public StringId DamageConstraintName1;
		[Field("damage constraint group name", null)]
		public StringId DamageConstraintGroupName2;
		[Field("group probability scale", null)]
		public float GroupProbabilityScale3;
		[Field("", null)]
		public fixed byte _4[4];
	}
}
#pragma warning restore CS1591
