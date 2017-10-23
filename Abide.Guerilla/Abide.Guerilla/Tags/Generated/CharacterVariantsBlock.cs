using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(12, 4)]
	public unsafe struct CharacterVariantsBlock
	{
		[Field("variant name^", null)]
		public StringId VariantName0;
		[Field("variant index*", null)]
		public short VariantIndex1;
		[Field("", null)]
		public fixed byte _2[2];
		[Field("variant designator*", null)]
		public StringId VariantDesignator3;
	}
}
#pragma warning restore CS1591
