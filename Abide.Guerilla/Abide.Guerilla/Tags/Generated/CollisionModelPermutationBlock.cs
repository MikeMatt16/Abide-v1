using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(28, 4)]
	public unsafe struct CollisionModelPermutationBlock
	{
		[Field("name^*", null)]
		public StringId Name0;
		[Field("bsps*", null)]
		[Block("Bsp", 64, typeof(CollisionModelBspBlock))]
		public TagBlock Bsps1;
		[Field("bsp_physics*", null)]
		[Block("Collision Bsp Physics Block", 1024, typeof(CollisionBspPhysicsBlock))]
		public TagBlock BspPhysics2;
	}
}
#pragma warning restore CS1591
