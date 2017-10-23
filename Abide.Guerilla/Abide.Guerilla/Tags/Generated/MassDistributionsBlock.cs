using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(64, 16)]
	public unsafe struct MassDistributionsBlock
	{
		[Field("center of mass*", null)]
		public Vector3 CenterOfMass0;
		[Field("", null)]
		public fixed byte _1[4];
		[Field("inertia tensor i*", null)]
		public Vector3 InertiaTensorI2;
		[Field("", null)]
		public fixed byte _3[4];
		[Field("inertia tensor j*", null)]
		public Vector3 InertiaTensorJ4;
		[Field("", null)]
		public fixed byte _5[4];
		[Field("inertia tensor k*", null)]
		public Vector3 InertiaTensorK6;
		[Field("", null)]
		public fixed byte _7[4];
	}
}
#pragma warning restore CS1591
