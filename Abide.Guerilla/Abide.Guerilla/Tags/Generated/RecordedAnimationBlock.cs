using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(64, 4)]
	public unsafe struct RecordedAnimationBlock
	{
		[Field("name^", null)]
		public String Name0;
		[Field("version*", null)]
		public int Version1;
		[Field("raw animation data*", null)]
		public int RawAnimationData2;
		[Field("unit control data version*", null)]
		public int UnitControlDataVersion3;
		[Field("", null)]
		public fixed byte _4[1];
		[Field("length of animation*:ticks", null)]
		public short LengthOfAnimation5;
		[Field("", null)]
		public fixed byte _6[2];
		[Field("", null)]
		public fixed byte _7[4];
		[Field("recorded animation event stream*", null)]
		[Data(2097152)]
		public TagBlock RecordedAnimationEventStream8;
	}
}
#pragma warning restore CS1591
