using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(132, 4)]
	public unsafe struct PlayerControlBlock
	{
		[Field("magnetism friction#how much the crosshair slows over enemies", null)]
		public float MagnetismFriction0;
		[Field("magnetism adhesion#how much the crosshair sticks to enemies", null)]
		public float MagnetismAdhesion1;
		[Field("inconsequential target scale#scales magnetism level for inconsequential targets like infection forms", null)]
		public float InconsequentialTargetScale2;
		[Field("", null)]
		public fixed byte _3[12];
		[Field("crosshair location#-1..1, 0 is middle of the screen", null)]
		public Vector2 CrosshairLocation5;
		[Field("seconds to start#how long you must be pegged before you start sprinting", null)]
		public float SecondsToStart7;
		[Field("seconds to full speed#how long you must sprint before you reach top speed", null)]
		public float SecondsToFullSpeed8;
		[Field("decay rate#how fast being unpegged decays the timer (seconds per second)", null)]
		public float DecayRate9;
		[Field("full speed multiplier#how much faster we actually go when at full sprint", null)]
		public float FullSpeedMultiplier10;
		[Field("pegged magnitude#how far the stick needs to be pressed before being considered pegged", null)]
		public float PeggedMagnitude11;
		[Field("pegged angular threshold#how far off straight up (in degrees) we consider pegged", null)]
		public float PeggedAngularThreshold12;
		[Field("", null)]
		public fixed byte _13[8];
		[Field("look default pitch rate:degrees", null)]
		public float LookDefaultPitchRate15;
		[Field("look default yaw rate:degrees", null)]
		public float LookDefaultYawRate16;
		[Field("look peg threshold [0,1]#magnitude of yaw for pegged acceleration to kick in", null)]
		public float LookPegThreshold0117;
		[Field("look yaw acceleration time:seconds#time for a pegged look to reach maximum effect", null)]
		public float LookYawAccelerationTime18;
		[Field("look yaw acceleration scale#maximum effect of a pegged look (scales last value in the look function below)", null)]
		public float LookYawAccelerationScale19;
		[Field("look pitch acceleration time:seconds#time for a pegged look to reach maximum effect", null)]
		public float LookPitchAccelerationTime20;
		[Field("look pitch acceleration scale#maximum effect of a pegged look (scales last value in the look function below)", null)]
		public float LookPitchAccelerationScale21;
		[Field("look autolevelling scale#1 is fast, 0 is none, >1 will probably be really fast", null)]
		public float LookAutolevellingScale22;
		[Field("", null)]
		public fixed byte _23[8];
		[Field("gravity_scale", null)]
		public float GravityScale24;
		[Field("", null)]
		public fixed byte _25[2];
		[Field("minimum autolevelling ticks#amount of time player needs to move and not look up or down for autolevelling to kick in", null)]
		public short MinimumAutolevellingTicks26;
		[Field("minimum angle for vehicle flipping#0 means the vehicle's up vector is along the ground, 90 means the up vector is pointing straight up:degrees", null)]
		public float MinimumAngleForVehicleFlipping27;
		[Field("look function", null)]
		[Block("Look Function Block", 16, typeof(LookFunctionBlock))]
		public TagBlock LookFunction28;
		[Field("minimum action hold time:seconds#time that player needs to press ACTION to register as a HOLD", null)]
		public float MinimumActionHoldTime29;
	}
}
#pragma warning restore CS1591
