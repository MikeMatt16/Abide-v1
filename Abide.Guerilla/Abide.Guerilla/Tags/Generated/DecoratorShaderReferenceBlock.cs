using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct DecoratorShaderReferenceBlock
	{
		[Field("shader", null)]
		public TagReference Shader0;
	}
}
#pragma warning restore CS1591
