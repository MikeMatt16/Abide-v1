using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(148, 4)]
	public unsafe struct FlockDefinitionBlock
	{
		public enum Flags4Options
		{
			HardBoundariesOnVolume_0 = 1,
			FlockInitiallyStopped_1 = 2,
		}
		[Field("bsp", null)]
		public short Bsp0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("", null)]
		public fixed byte _2[32];
		[Field("bounding volume", null)]
		public short BoundingVolume3;
		[Field("flags", typeof(Flags4Options))]
		public short Flags4;
		[Field("ecology margin:wus#distance from ecology boundary that creature begins to be repulsed", null)]
		public float EcologyMargin5;
		[Field("", null)]
		public fixed byte _6[16];
		[Field("sources", null)]
		[Block("Flock Source Block", 10, typeof(FlockSourceBlock))]
		public TagBlock Sources7;
		[Field("sinks", null)]
		[Block("Flock Sink Block", 10, typeof(FlockSinkBlock))]
		public TagBlock Sinks8;
		[Field("production frequency:boids/sec#How frequently boids are produced at one of the sources (limited by the max boid count)", null)]
		public float ProductionFrequency9;
		[Field("scale", null)]
		public FloatBounds Scale10;
		[Field("", null)]
		public fixed byte _11[12];
		[Field("creature^", null)]
		public TagReference Creature12;
		[Field("boid count", null)]
		public FloatBounds BoidCount13;
		[Field("", null)]
		public fixed byte _14[24];
		[Field("neighborhood radius:world units#distance within which one boid is affected by another", null)]
		public float NeighborhoodRadius17;
		[Field("avoidance radius:world units#distance that a boid tries to maintain from another", null)]
		public float AvoidanceRadius18;
		[Field("forward scale:[0..1]#weight given to boid's desire to fly straight ahead", null)]
		public float ForwardScale19;
		[Field("alignment scale:[0..1]#weight given to boid's desire to align itself with neighboring boids", null)]
		public float AlignmentScale20;
		[Field("avoidance scale:[0..1]#weight given to boid's desire to avoid collisions with other boids, when within the avoidance radius", null)]
		public float AvoidanceScale21;
		[Field("leveling force scale:[0..1]#weight given to boids desire to fly level", null)]
		public float LevelingForceScale22;
		[Field("sink scale:[0..1]#weight given to boid's desire to fly towards its sinks", null)]
		public float SinkScale23;
		[Field("perception angle:degrees#angle-from-forward within which one boid can perceive and react to another", null)]
		public float PerceptionAngle24;
		[Field("average throttle:[0..1]#throttle at which boids will naturally fly", null)]
		public float AverageThrottle25;
		[Field("maximum throttle:[0..1]#maximum throttle applicable", null)]
		public float MaximumThrottle26;
		[Field("position scale:[0..1]#weight given to boid's desire to be near flock center", null)]
		public float PositionScale27;
		[Field("position min radius:wus#distance to flock center beyond which an attracting force is applied", null)]
		public float PositionMinRadius28;
		[Field("position max radius:wus#distance to flock center at which the maximum attracting force is applied", null)]
		public float PositionMaxRadius29;
		[Field("movement weight threshold#The threshold of accumulated weight over which movement occurs", null)]
		public float MovementWeightThreshold30;
		[Field("danger radius:wus#distance within which boids will avoid a dangerous object (e.g. the player)", null)]
		public float DangerRadius31;
		[Field("danger scale#weight given to boid's desire to avoid danger", null)]
		public float DangerScale32;
		[Field("random offset scale:[0..1]#weight given to boid's random heading offset", null)]
		public float RandomOffsetScale34;
		[Field("random offset period:seconds", null)]
		public FloatBounds RandomOffsetPeriod35;
		[Field("", null)]
		public fixed byte _36[24];
		[Field("", null)]
		public fixed byte _37[4];
		[Field("flock name", null)]
		public StringId FlockName38;
	}
}
#pragma warning restore CS1591
