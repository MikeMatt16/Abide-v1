using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(44, 4)]
	public unsafe struct VertexShaderClassificationBlock
	{
		[Field("", null)]
		public fixed byte _0[4];
		[Field("compiled shader", null)]
		[Data(8192)]
		public TagBlock CompiledShader1;
		[Field("code", null)]
		[Data(65535)]
		public TagBlock Code2;
	}
}
#pragma warning restore CS1591
