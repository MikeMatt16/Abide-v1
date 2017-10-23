using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(36, 4)]
	public unsafe struct SoundEffectOverrideParametersBlock
	{
		[Field("name", null)]
		public StringId Name0;
		[Field("input", null)]
		public StringId Input1;
		[Field("range", null)]
		public StringId Range2;
		[Field("time period: seconds", null)]
		public float TimePeriod3;
		[Field("integer value", null)]
		public int IntegerValue4;
		[Field("real value", null)]
		public float RealValue5;
		[Field("function value", typeof(MappingFunctionBlock))]
		[Block("Mapping Function", 1, typeof(MappingFunctionBlock))]
		public MappingFunctionBlock FunctionValue7;
	}
}
#pragma warning restore CS1591
