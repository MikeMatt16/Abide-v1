using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(68, 4)]
	public unsafe struct GearBlock
	{
		[Field("loaded torque curve", typeof(TorqueCurveStructBlock))]
		[Block("Torque Curve Struct", 1, typeof(TorqueCurveStructBlock))]
		public TorqueCurveStructBlock LoadedTorqueCurve1;
		[Field("cruising torque curve", typeof(TorqueCurveStructBlock))]
		[Block("Torque Curve Struct", 1, typeof(TorqueCurveStructBlock))]
		public TorqueCurveStructBlock CruisingTorqueCurve3;
		[Field("min time to upshift#seconds", null)]
		public float MinTimeToUpshift5;
		[Field("engine up-shift scale#0-1", null)]
		public float EngineUpShiftScale6;
		[Field("gear ratio", null)]
		public float GearRatio7;
		[Field("min time to downshift#seconds", null)]
		public float MinTimeToDownshift8;
		[Field("engine down-shift scale#0-1", null)]
		public float EngineDownShiftScale9;
		[Field("", null)]
		public fixed byte _10[12];
	}
}
#pragma warning restore CS1591
