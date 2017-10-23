using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct DecoratorModelsBlock
	{
		[Field("model name*", null)]
		public StringId ModelName0;
		[Field("index start*", null)]
		public short IndexStart1;
		[Field("index count*", null)]
		public short IndexCount2;
	}
}
#pragma warning restore CS1591
