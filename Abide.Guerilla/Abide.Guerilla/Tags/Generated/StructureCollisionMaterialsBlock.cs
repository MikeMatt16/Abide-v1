using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(36, 4)]
	public unsafe struct StructureCollisionMaterialsBlock
	{
		[Field("Old Shader*", null)]
		public TagReference OldShader0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("Conveyor Surface Index*", null)]
		public short ConveyorSurfaceIndex2;
		[Field("New Shader*", null)]
		public TagReference NewShader3;
	}
}
#pragma warning restore CS1591
