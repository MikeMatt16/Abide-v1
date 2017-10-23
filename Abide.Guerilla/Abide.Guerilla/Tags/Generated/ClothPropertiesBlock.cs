using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(48, 4)]
	public unsafe struct ClothPropertiesBlock
	{
		public enum IntegrationType0Options
		{
			Verlet_0 = 0,
		}
		[Field("Integration type*", typeof(IntegrationType0Options))]
		public short IntegrationType0;
		[Field("Number iterations#[1-8] sug 1", null)]
		public short NumberIterations1;
		[Field("weight#[-10.0 - 10.0] sug 1.0", null)]
		public float Weight2;
		[Field("drag#[0.0 - 0.5] sug 0.07", null)]
		public float Drag3;
		[Field("wind_scale#[0.0 - 3.0] sug 1.0", null)]
		public float WindScale4;
		[Field("wind_flappiness_scale#[0.0 - 1.0] sug 0.75", null)]
		public float WindFlappinessScale5;
		[Field("longest_rod#[1.0 - 10.0] sug 3.5", null)]
		public float LongestRod6;
		[Field("", null)]
		public fixed byte _7[24];
	}
}
#pragma warning restore CS1591
