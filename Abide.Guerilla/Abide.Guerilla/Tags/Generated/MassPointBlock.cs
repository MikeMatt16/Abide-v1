using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(128, 4)]
	public unsafe struct MassPointBlock
	{
		public enum Flags3Options
		{
			Metallic_0 = 1,
		}
		public enum FrictionType11Options
		{
			Point_0 = 0,
			Forward_1 = 1,
			Left_2 = 2,
			Up_3 = 3,
		}
		[Field("name^*", null)]
		public String Name0;
		[Field("powered mass point", null)]
		public short PoweredMassPoint1;
		[Field("model node*", null)]
		public short ModelNode2;
		[Field("flags", typeof(Flags3Options))]
		public int Flags3;
		[Field("relative mass", null)]
		public float RelativeMass4;
		[Field("mass*", null)]
		public float Mass5;
		[Field("relative density", null)]
		public float RelativeDensity6;
		[Field("density*", null)]
		public float Density7;
		public Vector3 Position8;
		[Field("forward", null)]
		public Vector3 Forward9;
		[Field("up", null)]
		public Vector3 Up10;
		[Field("friction type", typeof(FrictionType11Options))]
		public short FrictionType11;
		[Field("", null)]
		public fixed byte _12[2];
		[Field("friction parallel scale", null)]
		public float FrictionParallelScale13;
		[Field("friction perpendicular scale", null)]
		public float FrictionPerpendicularScale14;
		[Field("radius", null)]
		public float Radius15;
		[Field("", null)]
		public fixed byte _16[20];
	}
}
#pragma warning restore CS1591
