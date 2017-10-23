using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct ObjectAiPropertiesBlock
	{
		public enum AiFlags0Options
		{
			DetroyableCover_0 = 1,
			PathfindingIgnoreWhenDead_1 = 2,
			DynamicCover_2 = 4,
		}
		public enum AiSize3Options
		{
			Default_0 = 0,
			Tiny_1 = 1,
			Small_2 = 2,
			Medium_3 = 3,
			Large_4 = 4,
			Huge_5 = 5,
			Immobile_6 = 6,
		}
		public enum LeapJumpSpeed4Options
		{
			NONE_0 = 0,
			Down_1 = 1,
			Step_2 = 2,
			Crouch_3 = 3,
			Stand_4 = 4,
			Storey_5 = 5,
			Tower_6 = 6,
			Infinite_7 = 7,
		}
		[Field("ai flags", typeof(AiFlags0Options))]
		public int AiFlags0;
		[Field("ai type name#used for combat dialogue, etc.", null)]
		public StringId AiTypeName1;
		[Field("", null)]
		public fixed byte _2[4];
		[Field("ai size", typeof(AiSize3Options))]
		public short AiSize3;
		[Field("leap jump speed", typeof(LeapJumpSpeed4Options))]
		public short LeapJumpSpeed4;
	}
}
#pragma warning restore CS1591
