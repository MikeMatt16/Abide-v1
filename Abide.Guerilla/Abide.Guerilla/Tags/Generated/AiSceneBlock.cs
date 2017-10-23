using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(32, 4)]
	public unsafe struct AiSceneBlock
	{
		public enum Flags1Options
		{
			SceneCanPlayMultipleTimes_0 = 1,
			EnableCombatDialogue_1 = 2,
		}
		[Field("name^", null)]
		public StringId Name0;
		[Field("flags", typeof(Flags1Options))]
		public int Flags1;
		[Field("trigger conditions", null)]
		[Block("Ai Scene Trigger Block", 1, typeof(AiSceneTriggerBlock))]
		public TagBlock TriggerConditions2;
		[Field("", null)]
		public fixed byte _3[32];
		[Field("roles", null)]
		[Block("Ai Scene Role Block", 10, typeof(AiSceneRoleBlock))]
		public TagBlock Roles4;
	}
}
#pragma warning restore CS1591
