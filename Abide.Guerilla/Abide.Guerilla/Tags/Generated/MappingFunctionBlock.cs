using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(12, 4)]
	public unsafe struct MappingFunctionBlock
	{
		[Field("data", null)]
		[Block("Byte Block", 1024, typeof(ByteBlock))]
		public TagBlock Data1;
	}
}
#pragma warning restore CS1591
