using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct AnimationTransitionBlock
	{
		[Field("full name^#name of the mode & state of the source", null)]
		public StringId FullName0;
		[Field("state info", typeof(AnimationTransitionStateStructBlock))]
		[Block("Animation Transition State Struct", 1, typeof(AnimationTransitionStateStructBlock))]
		public AnimationTransitionStateStructBlock StateInfo1;
		[Field("destinations|AABBCC", null)]
		[Block("Animation Transition Destination Block", 32, typeof(AnimationTransitionDestinationBlock))]
		public TagBlock DestinationsAABBCC2;
	}
}
#pragma warning restore CS1591
