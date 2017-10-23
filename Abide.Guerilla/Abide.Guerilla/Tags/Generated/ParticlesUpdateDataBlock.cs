using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(32, 4)]
	public unsafe struct ParticlesUpdateDataBlock
	{
		[Field("velocity.x*", null)]
		public float VelocityX0;
		[Field("velocity.y*", null)]
		public float VelocityY1;
		[Field("velocity.z*", null)]
		public float VelocityZ2;
		[Field("", null)]
		public fixed byte _3[12];
		[Field("mass*", null)]
		public float Mass4;
		[Field("creation time stamp*", null)]
		public float CreationTimeStamp5;
	}
}
#pragma warning restore CS1591
