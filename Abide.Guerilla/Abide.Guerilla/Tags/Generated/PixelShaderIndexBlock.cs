using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(1, 4)]
	public unsafe struct PixelShaderIndexBlock
	{
		[Field("pixel shader index", null)]
		public int PixelShaderIndex0;
	}
}
#pragma warning restore CS1591
