using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct DialogueVariantBlock
	{
		[Field("variant number#variant number to use this dialogue with (must match the suffix in the permutations on the unit's model)", null)]
		public short VariantNumber0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("", null)]
		public fixed byte _2[4];
		[Field("dialogue", null)]
		public TagReference Dialogue3;
	}
}
#pragma warning restore CS1591
