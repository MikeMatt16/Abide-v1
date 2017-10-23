using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(36, 4)]
	public unsafe struct ObjectFunctionBlock
	{
		public enum Flags0Options
		{
			InvertResultOfFunctionIsOneMinusActualResult_0 = 1,
			MappingDoesNotControlsActiveTheCurveMappingCanMakeTheFunctionActiveInactive_1 = 2,
			AlwaysActiveFunctionDoesNotDeactivateWhenAtOrBelowLowerBound_2 = 4,
			RandomTimeOffsetFunctionOffsetsPeriodicFunctionInputByRandomValueBetween0And1_3 = 8,
		}
		[Field("flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("import name", null)]
		public StringId ImportName1;
		[Field("export name", null)]
		public StringId ExportName2;
		[Field("turn off with#if the specified function is off, so is this function", null)]
		public StringId TurnOffWith3;
		[Field("min value#function must exceed this value (after mapping) to be active 0. means do nothing", null)]
		public float MinValue4;
		[Field("default function", typeof(MappingFunctionBlock))]
		[Block("Mapping Function", 1, typeof(MappingFunctionBlock))]
		public MappingFunctionBlock DefaultFunction6;
		[Field("scale by", null)]
		public StringId ScaleBy7;
	}
}
#pragma warning restore CS1591
