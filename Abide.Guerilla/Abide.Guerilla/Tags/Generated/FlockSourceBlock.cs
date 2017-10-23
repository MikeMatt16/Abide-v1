using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(28, 4)]
	public unsafe struct FlockSourceBlock
	{
		[Field("position", null)]
		public Vector3 Position0;
		[Field("starting yaw, pitch:degrees", null)]
		public Vector2 StartingYawPitch1;
		[Field("radius", null)]
		public float Radius2;
		[Field("weight#probability of producing at this source", null)]
		public float Weight3;
	}
}
#pragma warning restore CS1591
