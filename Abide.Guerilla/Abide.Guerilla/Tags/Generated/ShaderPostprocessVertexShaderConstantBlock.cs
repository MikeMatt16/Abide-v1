using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(18, 4)]
	public unsafe struct ShaderPostprocessVertexShaderConstantBlock
	{
		[Field("register index", null)]
		public int RegisterIndex0;
		[Field("register bank", null)]
		public int RegisterBank1;
		[Field("data", null)]
		public float Data2;
		[Field("data", null)]
		public float Data3;
		[Field("data", null)]
		public float Data4;
		[Field("data", null)]
		public float Data5;
	}
}
#pragma warning restore CS1591
