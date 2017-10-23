using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(48, 4)]
	public unsafe struct CharacterPhysicsGroundStructBlock
	{
		[Field("maximum slope angle:degrees", null)]
		public float MaximumSlopeAngle0;
		[Field("downhill falloff angle:degrees", null)]
		public float DownhillFalloffAngle1;
		[Field("downhill cutoff angle:degrees", null)]
		public float DownhillCutoffAngle2;
		[Field("uphill falloff angle:degrees", null)]
		public float UphillFalloffAngle3;
		[Field("uphill cutoff angle:degrees", null)]
		public float UphillCutoffAngle4;
		[Field("", null)]
		public fixed byte _5[16];
		[Field("downhill velocity scale", null)]
		public float DownhillVelocityScale6;
		[Field("uphill velocity scale", null)]
		public float UphillVelocityScale7;
		[Field("", null)]
		public fixed byte _8[8];
		[Field("", null)]
		public fixed byte _9[20];
		[Field("", null)]
		public fixed byte _10[16];
	}
}
#pragma warning restore CS1591
