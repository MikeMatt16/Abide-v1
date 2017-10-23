using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(48, 4)]
	public unsafe struct InheritedAnimationBlock
	{
		[Field("inherited graph*", null)]
		public TagReference InheritedGraph0;
		[Field("node map*", null)]
		[Block("Inherited Animation Node Map Block", 255, typeof(InheritedAnimationNodeMapBlock))]
		public TagBlock NodeMap1;
		[Field("node map flags*", null)]
		[Block("Inherited Animation Node Map Flag Block", 255, typeof(InheritedAnimationNodeMapFlagBlock))]
		public TagBlock NodeMapFlags2;
		[Field("root z offset*", null)]
		public float RootZOffset3;
		[Field("inheritance_flags*", null)]
		public int InheritanceFlags4;
	}
}
#pragma warning restore CS1591
