using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct STextValuePairReferenceBlockUNUSED
	{
		public enum ValueType1Options
		{
			IntegerNumber_0 = 0,
			FloatingPointNumber_1 = 1,
			Boolean_2 = 2,
			TextString_3 = 3,
		}
		public enum BooleanValue3Options
		{
			FALSE_0 = 0,
			TRUE_1 = 1,
		}
		[Field("value type", typeof(ValueType1Options))]
		public short ValueType1;
		[Field("boolean value", typeof(BooleanValue3Options))]
		public short BooleanValue3;
		[Field("integer value", null)]
		public int IntegerValue4;
		[Field("fp value", null)]
		public float FpValue5;
		[Field("text value string_id", null)]
		public StringId TextValueStringId6;
		[Field("text label string_id", null)]
		public StringId TextLabelStringId8;
	}
}
#pragma warning restore CS1591
