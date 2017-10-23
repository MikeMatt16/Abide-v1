using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(36, 4)]
	public unsafe struct SkyShaderFunctionBlock
	{
		[Field("", null)]
		public fixed byte _0[4];
		[Field("Global Function Name^#Global function that controls this shader value.", null)]
		public String GlobalFunctionName1;
	}
}
#pragma warning restore CS1591
