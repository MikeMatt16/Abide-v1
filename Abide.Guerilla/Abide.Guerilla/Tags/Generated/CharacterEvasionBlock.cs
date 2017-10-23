using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct CharacterEvasionBlock
	{
		[Field("", null)]
		public fixed byte _0[4];
		[Field("Evasion danger threshold#Consider evading when immediate danger surpasses threshold", null)]
		public float EvasionDangerThreshold1;
		[Field("Evasion delay timer#Wait at least this delay between evasions", null)]
		public float EvasionDelayTimer2;
		[Field("Evasion chance#If danger is above threshold, the chance that we will evade. Expressed as chance of evading within a 1 second time period", null)]
		public float EvasionChance3;
		[Field("Evasion proximity threshold#If target is within given proximity, possibly evade", null)]
		public float EvasionProximityThreshold4;
		[Field("", null)]
		public fixed byte _5[12];
		[Field("dive retreat chance#Chance of retreating (fleeing) after danger avoidance dive", null)]
		public float DiveRetreatChance6;
	}
}
#pragma warning restore CS1591
