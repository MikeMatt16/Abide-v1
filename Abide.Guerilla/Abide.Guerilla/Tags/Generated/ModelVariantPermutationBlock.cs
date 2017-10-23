using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(36, 4)]
	public unsafe struct ModelVariantPermutationBlock
	{
		public enum Flags2Options
		{
			CopyStatesToAllPermutations_0 = 1,
		}
		[Field("permutation name^", null)]
		public StringId PermutationName0;
		[Field("", null)]
		public fixed byte _1[1];
		[Field("flags", typeof(Flags2Options))]
		public byte Flags2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("probability:(0,+inf)", null)]
		public float Probability4;
		[Field("states", null)]
		[Block("Model Variant State Block", 10, typeof(ModelVariantStateBlock))]
		public TagBlock States5;
		[Field("", null)]
		public fixed byte _6[5];
		[Field("", null)]
		public fixed byte _7[7];
	}
}
#pragma warning restore CS1591
