using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct UnitBoostStructBlock
	{
		[Field("boost peak power", null)]
		public float BoostPeakPower0;
		[Field("boost rise power", null)]
		public float BoostRisePower1;
		[Field("boost peak time", null)]
		public float BoostPeakTime2;
		[Field("boost fall power", null)]
		public float BoostFallPower3;
		[Field("dead time", null)]
		public float DeadTime4;
	}
}
#pragma warning restore CS1591
