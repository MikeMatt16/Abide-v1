using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct AnimationTransitionDestinationBlock
	{
		[Field("full name^#name of the mode & state this transitions to", null)]
		public StringId FullName0;
		[Field("mode*#name of the mode", null)]
		public StringId Mode1;
		[Field("state info", typeof(AnimationDestinationStateStructBlock))]
		[Block("Animation Destination State Struct", 1, typeof(AnimationDestinationStateStructBlock))]
		public AnimationDestinationStateStructBlock StateInfo2;
		[Field("animation*", typeof(AnimationIndexStructBlock))]
		[Block("Animation Index Struct", 1, typeof(AnimationIndexStructBlock))]
		public AnimationIndexStructBlock Animation3;
	}
}
#pragma warning restore CS1591
