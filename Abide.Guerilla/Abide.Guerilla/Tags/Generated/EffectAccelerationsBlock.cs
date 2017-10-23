using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct EffectAccelerationsBlock
	{
		public enum CreateIn0Options
		{
			AnyEnvironment_0 = 0,
			AirOnly_1 = 1,
			WaterOnly_2 = 2,
			SpaceOnly_3 = 3,
		}
		public enum CreateIn1Options
		{
			EitherMode_0 = 0,
			ViolentModeOnly_1 = 1,
			NonviolentModeOnly_2 = 2,
		}
		[Field("create in", typeof(CreateIn0Options))]
		public short CreateIn0;
		[Field("create in", typeof(CreateIn1Options))]
		public short CreateIn1;
		[Field("location", null)]
		public short Location2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("acceleration", null)]
		public float Acceleration4;
		[Field("inner cone angle:degrees", null)]
		public float InnerConeAngle5;
		[Field("outer cone angle:degrees", null)]
		public float OuterConeAngle6;
	}
}
#pragma warning restore CS1591
