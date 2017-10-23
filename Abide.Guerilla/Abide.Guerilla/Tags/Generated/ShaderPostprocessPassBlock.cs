using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(18, 4)]
	public unsafe struct ShaderPostprocessPassBlock
	{
		[Field("shader pass", null)]
		public TagReference ShaderPass0;
		[Field("implementations", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock Implementations1;
	}
}
#pragma warning restore CS1591
