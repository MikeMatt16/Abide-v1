using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct GlobalGeometryBlockResourceBlock
	{
		public enum Type0Options
		{
			TagBlock_0 = 0,
			TagData_1 = 1,
			VertexBuffer_2 = 2,
		}
		[Field("Type*", typeof(Type0Options))]
		public byte Type0;
		[Field("", null)]
		public fixed byte _1[3];
		[Field("Primary Locator*", null)]
		public short PrimaryLocator2;
		[Field("Secondary Locator*", null)]
		public short SecondaryLocator3;
		[Field("Resource Data Size*", null)]
		public int ResourceDataSize4;
		[Field("Resource Data Offset*", null)]
		public int ResourceDataOffset5;
	}
}
#pragma warning restore CS1591
