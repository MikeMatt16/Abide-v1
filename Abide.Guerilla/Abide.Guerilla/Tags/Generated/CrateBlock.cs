using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("crate", "bloc", "obje", typeof(CrateBlock))]
	[FieldSet(4, 4)]
	public unsafe struct CrateBlock
	{
		public enum Flags0Options
		{
			DoesNotBlockAOE_0 = 1,
		}
		[Field("flags", typeof(Flags0Options))]
		public short Flags0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("", null)]
		public fixed byte _2[124];
	}
}
#pragma warning restore CS1591
