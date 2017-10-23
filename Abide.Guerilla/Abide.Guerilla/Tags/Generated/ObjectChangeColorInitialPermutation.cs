using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(32, 4)]
	public unsafe struct ObjectChangeColorInitialPermutation
	{
		[Field("weight", null)]
		public float Weight0;
		[Field("color lower bound", null)]
		public ColorRgbF ColorLowerBound1;
		[Field("color upper bound", null)]
		public ColorRgbF ColorUpperBound2;
		[Field("variant name#if empty, may be used by any model variant", null)]
		public StringId VariantName3;
	}
}
#pragma warning restore CS1591
