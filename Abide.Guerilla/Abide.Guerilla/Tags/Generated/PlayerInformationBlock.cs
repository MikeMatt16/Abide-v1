using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(372, 4)]
	public unsafe struct PlayerInformationBlock
	{
		[Field("unused", null)]
		public TagReference Unused0;
		[Field("", null)]
		public fixed byte _1[28];
		[Field("walking speed:world units per second", null)]
		public float WalkingSpeed2;
		[Field("", null)]
		public fixed byte _3[4];
		[Field("run forward:world units per second", null)]
		public float RunForward4;
		[Field("run backward:world units per second", null)]
		public float RunBackward5;
		[Field("run sideways:world units per second", null)]
		public float RunSideways6;
		[Field("run acceleration:world units per second squared", null)]
		public float RunAcceleration7;
		[Field("sneak forward:world units per second", null)]
		public float SneakForward8;
		[Field("sneak backward:world units per second", null)]
		public float SneakBackward9;
		[Field("sneak sideways:world units per second", null)]
		public float SneakSideways10;
		[Field("sneak acceleration:world units per second squared", null)]
		public float SneakAcceleration11;
		[Field("airborne acceleration:world units per second squared", null)]
		public float AirborneAcceleration12;
		[Field("", null)]
		public fixed byte _13[16];
		public Vector3 GrenadeOrigin14;
		[Field("", null)]
		public fixed byte _15[12];
		[Field("stun movement penalty:[0,1]#1.0 prevents moving while stunned", null)]
		public float StunMovementPenalty16;
		[Field("stun turning penalty:[0,1]#1.0 prevents turning while stunned", null)]
		public float StunTurningPenalty17;
		[Field("stun jumping penalty:[0,1]#1.0 prevents jumping while stunned", null)]
		public float StunJumpingPenalty18;
		[Field("minimum stun time:seconds#all stunning damage will last for at least this long", null)]
		public float MinimumStunTime19;
		[Field("maximum stun time:seconds#no stunning damage will last for longer than this", null)]
		public float MaximumStunTime20;
		[Field("", null)]
		public fixed byte _21[8];
		[Field("first person idle time:seconds", null)]
		public FloatBounds FirstPersonIdleTime22;
		[Field("first person skip fraction:[0,1]", null)]
		public float FirstPersonSkipFraction23;
		[Field("", null)]
		public fixed byte _24[16];
		[Field("coop respawn effect", null)]
		public TagReference CoopRespawnEffect25;
		[Field("binoculars zoom count", null)]
		public int BinocularsZoomCount26;
		[Field("binoculars zoom range", null)]
		public FloatBounds BinocularsZoomRange27;
		[Field("binoculars zoom in sound", null)]
		public TagReference BinocularsZoomInSound28;
		[Field("binoculars zoom out sound", null)]
		public TagReference BinocularsZoomOutSound29;
		[Field("", null)]
		public fixed byte _30[16];
		[Field("active camouflage on", null)]
		public TagReference ActiveCamouflageOn31;
		[Field("active camouflage off", null)]
		public TagReference ActiveCamouflageOff32;
		[Field("active camouflage error", null)]
		public TagReference ActiveCamouflageError33;
		[Field("active camouflage ready", null)]
		public TagReference ActiveCamouflageReady34;
		[Field("flashlight on", null)]
		public TagReference FlashlightOn35;
		[Field("flashlight off", null)]
		public TagReference FlashlightOff36;
		[Field("ice cream", null)]
		public TagReference IceCream37;
	}
}
#pragma warning restore CS1591
