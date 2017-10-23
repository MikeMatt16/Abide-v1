using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(12, 4)]
	public unsafe struct TextValuePairReferenceBlock
	{
		public enum Flags0Options
		{
			DefaultSetting_0 = 1,
		}
		[Field("flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("value", null)]
		public int Value1;
		[Field("label string id", null)]
		public StringId LabelStringId2;
	}
}
#pragma warning restore CS1591
