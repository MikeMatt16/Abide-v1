using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(40, 4)]
	public unsafe struct SoundEffectTemplateParameterBlock
	{
		public enum Type1Options
		{
			Integer_0 = 0,
			Real_1 = 1,
			FilterType_2 = 2,
		}
		public enum Flags2Options
		{
			ExposeAsFunction_0 = 1,
		}
		[Field("name", null)]
		public StringId Name0;
		[Field("type", typeof(Type1Options))]
		public short Type1;
		[Field("flags", typeof(Flags2Options))]
		public short Flags2;
		[Field("hardware offset", null)]
		public int HardwareOffset3;
		[Field("default enum integer value", null)]
		public int DefaultEnumIntegerValue4;
		[Field("default scalar value", null)]
		public float DefaultScalarValue5;
		[Field("default function", typeof(MappingFunctionBlock))]
		[Block("Mapping Function", 1, typeof(MappingFunctionBlock))]
		public MappingFunctionBlock DefaultFunction7;
		[Field("minimum scalar value", null)]
		public float MinimumScalarValue8;
		[Field("maximum scalar value", null)]
		public float MaximumScalarValue9;
	}
}
#pragma warning restore CS1591
