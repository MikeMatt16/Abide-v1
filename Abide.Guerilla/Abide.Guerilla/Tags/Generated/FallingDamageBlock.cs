using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(152, 4)]
	public unsafe struct FallingDamageBlock
	{
		[Field("", null)]
		public fixed byte _0[8];
		[Field("harmful falling distance:world units", null)]
		public FloatBounds HarmfulFallingDistance1;
		[Field("falling damage", null)]
		public TagReference FallingDamage2;
		[Field("", null)]
		public fixed byte _3[8];
		[Field("maximum falling distance:world units", null)]
		public float MaximumFallingDistance4;
		[Field("distance damage", null)]
		public TagReference DistanceDamage5;
		[Field("vehicle environemtn collision damage effect", null)]
		public TagReference VehicleEnvironemtnCollisionDamageEffect6;
		[Field("vehicle killed unit damage effect", null)]
		public TagReference VehicleKilledUnitDamageEffect7;
		[Field("vehicle collision damage", null)]
		public TagReference VehicleCollisionDamage8;
		[Field("flaming death damage", null)]
		public TagReference FlamingDeathDamage9;
		[Field("", null)]
		public fixed byte _10[28];
	}
}
#pragma warning restore CS1591
