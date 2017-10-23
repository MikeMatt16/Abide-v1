using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(80, 4)]
	public unsafe struct AnimationGraphResourcesStructBlock
	{
		public enum InheritanceFlags2Options
		{
			InheritRootTransScaleOnly_0 = 1,
			InheritForUseOnPlayer_1 = 2,
		}
		public enum PrivateFlags3Options
		{
			PreparedForCache_0 = 1,
			Unused_1 = 2,
			ImportedWithCodecCompressors_2 = 4,
			UnusedSmellyFlag_3 = 8,
			WrittenToCache_4 = 16,
			AnimationDataReordered_5 = 32,
		}
		[Field("parent animation graph", null)]
		public TagReference ParentAnimationGraph1;
		[Field("inheritance flags", typeof(InheritanceFlags2Options))]
		public byte InheritanceFlags2;
		[Field("private flags*", typeof(PrivateFlags3Options))]
		public byte PrivateFlags3;
		[Field("animation codec pack*", null)]
		public short AnimationCodecPack4;
		[Field("skeleton nodes*|ABCDCC", null)]
		[Block("Animation Graph Node Block", 255, typeof(AnimationGraphNodeBlock))]
		public TagBlock SkeletonNodesABCDCC5;
		[Field("sound references|ABCDCC", null)]
		[Block("Animation Graph Sound Reference Block", 512, typeof(AnimationGraphSoundReferenceBlock))]
		public TagBlock SoundReferencesABCDCC6;
		[Field("effect references|ABCDCC", null)]
		[Block("Animation Graph Effect Reference Block", 512, typeof(AnimationGraphEffectReferenceBlock))]
		public TagBlock EffectReferencesABCDCC7;
		[Field("blend screens|ABCDCC", null)]
		[Block("Animation Blend Screen Block", 64, typeof(AnimationBlendScreenBlock))]
		public TagBlock BlendScreensABCDCC8;
		[Field("animations*|ABCDCC", null)]
		[Block("Animation Pool Block", 2048, typeof(AnimationPoolBlock))]
		public TagBlock AnimationsABCDCC9;
	}
}
#pragma warning restore CS1591
