using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct DecoratorClassesBlock
	{
		public enum Type1Options
		{
			Model_0 = 0,
			FloatingDecal_1 = 1,
			ProjectedDecal_2 = 2,
			ScreenFacingQuad_3 = 3,
			AxisRotatingQuad_4 = 4,
			CrossQuad_5 = 5,
		}
		[Field("name", null)]
		public StringId Name0;
		[Field("type", typeof(Type1Options))]
		public byte Type1;
		[Field("", null)]
		public fixed byte _2[3];
		[Field("scale", null)]
		public float Scale3;
		[Field("permutations", null)]
		[Block("Decorator Permutations Block", 64, typeof(DecoratorPermutationsBlock))]
		public TagBlock Permutations4;
	}
}
#pragma warning restore CS1591
