using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("colony", "coln", "����", typeof(ColonyBlock))]
	[FieldSet(76, 4)]
	public unsafe struct ColonyBlock
	{
		public enum Flags0Options
		{
			Unused_0 = 1,
		}
		[Field("flags", typeof(Flags0Options))]
		public short Flags0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("", null)]
		public fixed byte _2[4];
		[Field("radius", null)]
		public FloatBounds Radius3;
		[Field("", null)]
		public fixed byte _4[12];
		[Field("debug color", null)]
		public ColorArgbF DebugColor5;
		[Field("base map", null)]
		public TagReference BaseMap6;
		[Field("detail map", null)]
		public TagReference DetailMap7;
	}
}
#pragma warning restore CS1591
