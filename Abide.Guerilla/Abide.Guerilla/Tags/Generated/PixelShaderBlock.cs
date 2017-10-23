using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("pixel_shader", "pixl", "����", typeof(PixelShaderBlock))]
	[FieldSet(24, 4)]
	public unsafe struct PixelShaderBlock
	{
		[Field("", null)]
		public fixed byte _0[4];
		[Field("compiled_shader", null)]
		[Data(4096)]
		public TagBlock CompiledShader1;
	}
}
#pragma warning restore CS1591
