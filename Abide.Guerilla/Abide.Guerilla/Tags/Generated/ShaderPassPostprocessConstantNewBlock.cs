using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(7, 4)]
	public unsafe struct ShaderPassPostprocessConstantNewBlock
	{
		[Field("parameter name", null)]
		public StringId ParameterName0;
		[Field("component mask", null)]
		public int ComponentMask1;
		[Field("scale by texture stage", null)]
		public int ScaleByTextureStage2;
		[Field("function index", null)]
		public int FunctionIndex3;
	}
}
#pragma warning restore CS1591
