using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(28, 4)]
	public unsafe struct ObjectSpaceNodeDataBlock
	{
		public enum ComponentFlags1Options
		{
			Rotation_0 = 1,
			Translation_1 = 2,
			Scale_2 = 4,
		}
		[Field("node_index*", null)]
		public short NodeIndex0;
		[Field("component flags", typeof(ComponentFlags1Options))]
		public short ComponentFlags1;
		[Field("orientation*", typeof(QuantizedOrientationStructBlock))]
		[Block("Quantized Orientation Struct", 1, typeof(QuantizedOrientationStructBlock))]
		public QuantizedOrientationStructBlock Orientation2;
	}
}
#pragma warning restore CS1591
