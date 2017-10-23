using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(36, 4)]
	public unsafe struct AimAssistStructBlock
	{
		[Field("autoaim angle:degrees#the maximum angle that autoaim works at full strength", null)]
		public float AutoaimAngle0;
		[Field("autoaim range:world units#the maximum distance that autoaim works at full strength", null)]
		public float AutoaimRange1;
		[Field("magnetism angle:degrees#the maximum angle that magnetism works at full strength", null)]
		public float MagnetismAngle2;
		[Field("magnetism range:world units#the maximum distance that magnetism works at full strength", null)]
		public float MagnetismRange3;
		[Field("deviation angle:degrees#the maximum angle that a projectile is allowed to deviate from the gun barrel", null)]
		public float DeviationAngle4;
		[Field("", null)]
		public fixed byte _5[4];
		[Field("", null)]
		public fixed byte _6[12];
	}
}
#pragma warning restore CS1591
