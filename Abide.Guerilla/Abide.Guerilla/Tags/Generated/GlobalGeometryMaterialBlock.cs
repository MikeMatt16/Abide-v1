using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(52, 4)]
	public unsafe struct GlobalGeometryMaterialBlock
	{
		[Field("Old Shader*", null)]
		public TagReference OldShader0;
		[Field("Shader^*", null)]
		public TagReference Shader1;
		[Field("Properties*", null)]
		[Block("Material Property", 16, typeof(GlobalGeometryMaterialPropertyBlock))]
		public TagBlock Properties2;
		[Field("", null)]
		public fixed byte _3[4];
		[Field("Breakable Surface Index*", null)]
		public int BreakableSurfaceIndex4;
		[Field("", null)]
		public fixed byte _5[3];
	}
}
#pragma warning restore CS1591
