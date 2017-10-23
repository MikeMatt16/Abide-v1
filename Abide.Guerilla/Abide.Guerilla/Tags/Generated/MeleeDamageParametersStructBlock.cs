using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(140, 4)]
	public unsafe struct MeleeDamageParametersStructBlock
	{
		[Field("damage pyramid angles", null)]
		public Vector2 DamagePyramidAngles0;
		[Field("damage pyramid depth", null)]
		public float DamagePyramidDepth1;
		[Field("1st hit melee damage", null)]
		public TagReference _1stHitMeleeDamage3;
		[Field("1st hit melee response", null)]
		public TagReference _1stHitMeleeResponse4;
		[Field("2nd hit melee damage", null)]
		public TagReference _2ndHitMeleeDamage5;
		[Field("2nd hit melee response", null)]
		public TagReference _2ndHitMeleeResponse6;
		[Field("3rd hit melee damage", null)]
		public TagReference _3rdHitMeleeDamage7;
		[Field("3rd hit melee response", null)]
		public TagReference _3rdHitMeleeResponse8;
		[Field("lunge melee damage#this is only important for the energy sword", null)]
		public TagReference LungeMeleeDamage9;
		[Field("lunge melee response#this is only important for the energy sword", null)]
		public TagReference LungeMeleeResponse10;
	}
}
#pragma warning restore CS1591
