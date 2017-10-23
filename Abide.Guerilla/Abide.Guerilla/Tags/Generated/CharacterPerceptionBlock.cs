using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(52, 4)]
	public unsafe struct CharacterPerceptionBlock
	{
		public enum PerceptionFlags0Options
		{
			Flag1_0 = 1,
		}
		[Field("perception flags", typeof(PerceptionFlags0Options))]
		public int PerceptionFlags0;
		[Field("max vision distance:world units#maximum range of sight", null)]
		public float MaxVisionDistance1;
		[Field("central vision angle:degrees#horizontal angle within which we see targets out to our maximum range", null)]
		public float CentralVisionAngle2;
		[Field("max vision angle:degrees#maximum horizontal angle within which we see targets at range", null)]
		public float MaxVisionAngle3;
		[Field("", null)]
		public fixed byte _4[4];
		[Field("peripheral vision angle:degrees#maximum horizontal angle within which we can see targets out of the corner of our eye", null)]
		public float PeripheralVisionAngle5;
		[Field("peripheral distance:world units#maximum range at which we can see targets our of the corner of our eye", null)]
		public float PeripheralDistance6;
		[Field("", null)]
		public fixed byte _7[4];
		[Field("", null)]
		public fixed byte _8[24];
		[Field("hearing distance:world units#maximum range at which sounds can be heard", null)]
		public float HearingDistance9;
		[Field("notice projectile chance:[0,1]#random chance of noticing a dangerous enemy projectile (e.g. grenade)", null)]
		public float NoticeProjectileChance10;
		[Field("notice vehicle chance:[0,1]#random chance of noticing a dangerous vehicle", null)]
		public float NoticeVehicleChance11;
		[Field("", null)]
		public fixed byte _12[8];
		[Field("combat perception time:seconds#time required to acknowledge a visible enemy when we are already in combat or searching for them", null)]
		public float CombatPerceptionTime13;
		[Field("guard perception time:seconds#time required to acknowledge a visible enemy when we have been alerted", null)]
		public float GuardPerceptionTime14;
		[Field("non-combat perception time:seconds#time required to acknowledge a visible enemy when we are not alerted", null)]
		public float NonCombatPerceptionTime15;
		[Field("", null)]
		public fixed byte _16[24];
		[Field("first ack. surprise distance:world units#If a new prop is acknowledged within the given distance, surprise is registerd", null)]
		public float FirstAckSurpriseDistance17;
	}
}
#pragma warning restore CS1591
