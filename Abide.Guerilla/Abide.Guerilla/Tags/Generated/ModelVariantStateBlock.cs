using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(32, 4)]
	public unsafe struct ModelVariantStateBlock
	{
		public enum PropertyFlags2Options
		{
			Blurred_0 = 1,
			HellaBlurred_1 = 2,
			Shielded_2 = 4,
		}
		public enum State3Options
		{
			Default_0 = 0,
			MinorDamage_1 = 1,
			MediumDamage_2 = 2,
			MajorDamage_3 = 3,
			Destroyed_4 = 4,
		}
		[Field("permutation name", null)]
		public StringId PermutationName0;
		[Field("", null)]
		public fixed byte _1[1];
		[Field("property flags", typeof(PropertyFlags2Options))]
		public byte PropertyFlags2;
		[Field("state^", typeof(State3Options))]
		public short State3;
		[Field("looping effect#played while the model is in this state", null)]
		public TagReference LoopingEffect4;
		[Field("looping effect marker name", null)]
		public StringId LoopingEffectMarkerName5;
		[Field("initial probability", null)]
		public float InitialProbability6;
	}
}
#pragma warning restore CS1591
