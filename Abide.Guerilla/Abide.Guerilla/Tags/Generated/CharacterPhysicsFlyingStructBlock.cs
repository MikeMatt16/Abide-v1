using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(44, 4)]
	public unsafe struct CharacterPhysicsFlyingStructBlock
	{
		[Field("bank angle:degrees#angle at which we bank left/right when sidestepping or turning while moving forwards", null)]
		public float BankAngle0;
		[Field("bank apply time:seconds#time it takes us to apply a bank", null)]
		public float BankApplyTime1;
		[Field("bank decay time:seconds#time it takes us to recover from a bank", null)]
		public float BankDecayTime2;
		[Field("pitch ratio#amount that we pitch up/down when moving up or down", null)]
		public float PitchRatio3;
		[Field("max velocity:world units per second#max velocity when not crouching", null)]
		public float MaxVelocity4;
		[Field("max sidestep velocity:world units per second#max sideways or up/down velocity when not crouching", null)]
		public float MaxSidestepVelocity5;
		[Field("acceleration:world units per second squared", null)]
		public float Acceleration6;
		[Field("deceleration:world units per second squared", null)]
		public float Deceleration7;
		[Field("angular velocity maximum:degrees per second#turn rate", null)]
		public float AngularVelocityMaximum8;
		[Field("angular acceleration maximum:degrees per second squared#turn acceleration rate", null)]
		public float AngularAccelerationMaximum9;
		[Field("crouch velocity modifier:[0,1]#how much slower we fly if crouching (zero = same speed)", null)]
		public float CrouchVelocityModifier10;
		[Field("", null)]
		public fixed byte _11[16];
	}
}
#pragma warning restore CS1591
