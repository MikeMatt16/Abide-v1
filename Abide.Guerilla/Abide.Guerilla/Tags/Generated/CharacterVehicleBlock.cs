using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(196, 4)]
	public unsafe struct CharacterVehicleBlock
	{
		public enum VehicleFlags3Options
		{
			PassengersAdoptOriginalSquad_0 = 1,
		}
		public enum ObstacleIgnoreSize57Options
		{
			None_0 = 0,
			Tiny_1 = 1,
			Small_2 = 2,
			Medium_3 = 3,
			Large_4 = 4,
			Huge_5 = 5,
			Immobile_6 = 6,
		}
		[Field("unit^", null)]
		public TagReference Unit0;
		[Field("style^", null)]
		public TagReference Style1;
		[Field("", null)]
		public fixed byte _2[32];
		[Field("vehicle flags", typeof(VehicleFlags3Options))]
		public int VehicleFlags3;
		[Field("", null)]
		public fixed byte _4[8];
		[Field("ai pathfinding radius:world units#(Ground vehicles)", null)]
		public float AiPathfindingRadius6;
		[Field("ai destination radius:world units#(All vehicles) Distance within which goal is considered reached", null)]
		public float AiDestinationRadius7;
		[Field("ai deceleration distanceworld units#(All vehicles)Distance from goal at which AI starts to decelerate", null)]
		public float AiDecelerationDistanceworldUnits8;
		[Field("", null)]
		public fixed byte _9[8];
		[Field("ai turning radius#(Warthog, Pelican, Ghost) Idealized average turning radius (should reflect actual vehicle physics)", null)]
		public float AiTurningRadius11;
		[Field("ai inner turning radius (< tr)#(Warthog-type) Idealized minimum turning radius (should reflect actual vehicle physics)", null)]
		public float AiInnerTurningRadiusTr12;
		[Field("ai ideal turning radius (> tr)#(Warthogs, ghosts) Ideal turning radius for rounding turns (barring obstacles, etc.)", null)]
		public float AiIdealTurningRadiusTr13;
		[Field("", null)]
		public fixed byte _14[8];
		[Field("ai banshee steering maximum#(Banshee)", null)]
		public float AiBansheeSteeringMaximum16;
		[Field("ai max steering angle:degrees#(Warthog, ghosts, wraiths)Maximum steering angle from forward (ultimately controls turning speed)", null)]
		public float AiMaxSteeringAngle17;
		[Field("ai max steering delta: degrees#(pelicans, dropships, ghosts, wraiths)Maximum delta in steering angle from one tick to the next (ultimately controls turn acceleration)", null)]
		public float AiMaxSteeringDelta18;
		[Field("ai oversteering scale#(Warthog, ghosts, wraiths)", null)]
		public float AiOversteeringScale19;
		[Field("ai oversteering bounds#(Banshee) Angle to goal at which AI will oversteer", null)]
		public FloatBounds AiOversteeringBounds20;
		[Field("ai sideslip distance#(Ghosts, Dropships) Distance within which Ai will strafe to target (as opposed to turning)", null)]
		public float AiSideslipDistance21;
		[Field("ai avoidance distance:world units#(Banshee-style) Look-ahead distance for obstacle avoidance", null)]
		public float AiAvoidanceDistance22;
		[Field("ai min urgency:[0-1]#(Banshees)The minimum urgency with which a turn can be made (urgency = percent of maximum steering delta)", null)]
		public float AiMinUrgency23;
		[Field("", null)]
		public fixed byte _24[4];
		[Field("ai throttle maximum:(0 - 1)#(All vehicles)", null)]
		public float AiThrottleMaximum26;
		[Field("ai goal min throttle scale#(Warthogs, Dropships, ghosts)scale on throttle when within 'ai deceleration distance' of goal (0...1)", null)]
		public float AiGoalMinThrottleScale27;
		[Field("ai turn min throttle scale#(Warthogs, ghosts) Scale on throttle due to nearness to a turn (0...1)", null)]
		public float AiTurnMinThrottleScale28;
		[Field("ai direction min throttle scale#(Warthogs, ghosts) Scale on throttle due to facing away from intended direction (0...1)", null)]
		public float AiDirectionMinThrottleScale29;
		[Field("ai acceleration scale:(0-1)#(warthogs, ghosts) The maximum allowable change in throttle between ticks", null)]
		public float AiAccelerationScale30;
		[Field("ai throttle blend:(0-1)#(dropships, sentinels) The degree of throttle blending between one tick and the next (0 = no blending)", null)]
		public float AiThrottleBlend31;
		[Field("theoretical max speed:wu/s#(dropships, warthogs, ghosts) About how fast I can go.", null)]
		public float TheoreticalMaxSpeed32;
		[Field("error scale#(dropships, warthogs) scale on the difference between desired and actual speed, applied to throttle", null)]
		public float ErrorScale33;
		[Field("", null)]
		public fixed byte _34[8];
		[Field("ai allowable aim deviation angle", null)]
		public float AiAllowableAimDeviationAngle36;
		[Field("", null)]
		public fixed byte _37[12];
		[Field("ai charge tight angle distance#(All vehicles) The distance at which the tight angle criterion is used for deciding to vehicle charge", null)]
		public float AiChargeTightAngleDistance39;
		[Field("ai charge tight angle:[0-1]#(All vehicles) Angle cosine within which the target must be when target is closer than tight angle distance in order to charge", null)]
		public float AiChargeTightAngle40;
		[Field("ai charge repeat timeout#(All vehicles) Time delay between vehicle charges", null)]
		public float AiChargeRepeatTimeout41;
		[Field("ai charge look-ahead time#(All vehicles) In deciding when to abort vehicle charge, look ahead these many seconds to predict time of contact", null)]
		public float AiChargeLookAheadTime42;
		[Field("ai charge consider distance#Consider charging the target when it is within this range (0 = infinite distance)", null)]
		public float AiChargeConsiderDistance43;
		[Field("ai charge abort distance#Abort the charge when the target get more than this far away (0 = never abort)", null)]
		public float AiChargeAbortDistance44;
		[Field("", null)]
		public fixed byte _45[4];
		[Field("vehicle ram timeout#The ram behavior stops after a maximum of the given number of seconds", null)]
		public float VehicleRamTimeout46;
		[Field("ram paralysis time#The ram behavior freezes the vehicle for a given number of seconds after performing the ram", null)]
		public float RamParalysisTime47;
		[Field("", null)]
		public fixed byte _48[4];
		[Field("ai cover damage threshold#(All vehicles) Trigger a cover when recent damage is above given threshold (damage_vehicle_cover impulse)", null)]
		public float AiCoverDamageThreshold49;
		[Field("ai cover min distance#(All vehicles) When executing vehicle-cover, minimum distance from the target to flee to", null)]
		public float AiCoverMinDistance50;
		[Field("ai cover time#(All vehicles) How long to stay away from the target", null)]
		public float AiCoverTime51;
		[Field("ai cover min boost distance#(All vehicles) Boosting allowed when distance to cover destination is greater then this.", null)]
		public float AiCoverMinBoostDistance52;
		[Field("turtling recent damage threshold:%#If vehicle turtling behavior is enabled, turtling is initiated if 'recent damage' surpasses the given threshold", null)]
		public float TurtlingRecentDamageThreshold53;
		[Field("turtling min time:seconds#If the vehicle turtling behavior is enabled, turtling occurs for at least the given time", null)]
		public float TurtlingMinTime54;
		[Field("turtling timeout:seconds#The turtled state times out after the given number of seconds", null)]
		public float TurtlingTimeout55;
		[Field("", null)]
		public fixed byte _56[8];
		[Field("obstacle ignore size", typeof(ObstacleIgnoreSize57Options))]
		public short ObstacleIgnoreSize57;
		[Field("", null)]
		public fixed byte _58[2];
	}
}
#pragma warning restore CS1591
