using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(4, 4)]
	public unsafe struct VertexShaderConstantBlock
	{
		[Field("register index", null)]
		public int RegisterIndex0;
		[Field("parameter index", null)]
		public int ParameterIndex1;
		[Field("destination mask", null)]
		public int DestinationMask2;
		[Field("scale by texture stage", null)]
		public int ScaleByTextureStage3;
	}
}
#pragma warning restore CS1591
