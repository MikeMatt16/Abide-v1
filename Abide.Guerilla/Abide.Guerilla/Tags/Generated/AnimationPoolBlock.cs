using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(124, 4)]
	public unsafe struct AnimationPoolBlock
	{
		public enum Type5Options
		{
			Base_0 = 0,
			Overlay_1 = 1,
			Replacement_2 = 2,
		}
		public enum FrameInfoType6Options
		{
			None_0 = 0,
			DxDy_1 = 1,
			DxDyDyaw_2 = 2,
			DxDyDzDyaw_3 = 3,
		}
		public enum InternalFlags10Options
		{
			Unused0_0 = 1,
			WorldRelative_1 = 2,
			Unused1_2 = 4,
			Unused2_3 = 8,
			Unused3_4 = 16,
			CompressionDisabled_5 = 32,
			OldProductionChecksum_6 = 64,
			ValidProductionChecksum_7 = 128,
		}
		public enum ProductionFlags11Options
		{
			DoNotMonitorChanges_0 = 1,
			VerifySoundEvents_1 = 2,
			DoNotInheritForPlayerGraphs_2 = 4,
		}
		public enum PlaybackFlags12Options
		{
			DisableInterpolationIn_0 = 1,
			DisableInterpolationOut_1 = 2,
			DisableModeIk_2 = 4,
			DisableWeaponIk_3 = 8,
			DisableWeaponAim1stPerson_4 = 16,
			DisableLookScreen_5 = 32,
			DisableTransitionAdjustment_6 = 64,
		}
		public enum DesiredCompression13Options
		{
			BestScore_0 = 0,
			BestCompression_1 = 1,
			BestAccuracy_2 = 2,
			BestFullframe_3 = 3,
			BestSmallKeyframe_4 = 4,
			BestLargeKeyframe_5 = 5,
		}
		public enum CurrentCompression14Options
		{
			BestScore_0 = 0,
			BestCompression_1 = 1,
			BestAccuracy_2 = 2,
			BestFullframe_3 = 3,
			BestSmallKeyframe_4 = 4,
			BestLargeKeyframe_5 = 5,
		}
		[Field("name*^", null)]
		public StringId Name0;
		[Field("node list checksum*", null)]
		public int NodeListChecksum2;
		[Field("production checksum*", null)]
		public int ProductionChecksum3;
		[Field("import_checksum*", null)]
		public int ImportChecksum4;
		[Field("type*", typeof(Type5Options))]
		public byte Type5;
		[Field("frame info type*", typeof(FrameInfoType6Options))]
		public byte FrameInfoType6;
		[Field("blend screen", null)]
		public byte BlendScreen7;
		[Field("node count*", null)]
		public int NodeCount8;
		[Field("frame count*", null)]
		public short FrameCount9;
		[Field("internal flags*", typeof(InternalFlags10Options))]
		public byte InternalFlags10;
		[Field("production flags", typeof(ProductionFlags11Options))]
		public byte ProductionFlags11;
		[Field("playback flags", typeof(PlaybackFlags12Options))]
		public short PlaybackFlags12;
		[Field("desired compression", typeof(DesiredCompression13Options))]
		public byte DesiredCompression13;
		[Field("current compression*", typeof(CurrentCompression14Options))]
		public byte CurrentCompression14;
		[Field("weight", null)]
		public float Weight15;
		[Field("loop frame index", null)]
		public short LoopFrameIndex16;
		[Field("", null)]
		public short _17;
		[Field("", null)]
		public short _18;
		[Field("", null)]
		public fixed byte _19[2];
		[Field("", null)]
		[Data(33724160)]
		public TagBlock _20;
		[Field("", typeof(PackedDataSizesStructBlock))]
		[Block("Packed Data Sizes Struct", 1, typeof(PackedDataSizesStructBlock))]
		public PackedDataSizesStructBlock _21;
		[Field("frame events|ABCDCC", null)]
		[Block("Animation Frame Event Block", 512, typeof(AnimationFrameEventBlock))]
		public TagBlock FrameEventsABCDCC22;
		[Field("sound events|ABCDCC", null)]
		[Block("Animation Sound Event Block", 512, typeof(AnimationSoundEventBlock))]
		public TagBlock SoundEventsABCDCC23;
		[Field("effect events|ABCDCC", null)]
		[Block("Animation Effect Event Block", 512, typeof(AnimationEffectEventBlock))]
		public TagBlock EffectEventsABCDCC24;
		[Field("object-space parent nodes|ABCDCC", null)]
		[Block("Object Space Node Data Block", 255, typeof(ObjectSpaceNodeDataBlock))]
		public TagBlock ObjectSpaceParentNodesABCDCC25;
	}
}
#pragma warning restore CS1591
