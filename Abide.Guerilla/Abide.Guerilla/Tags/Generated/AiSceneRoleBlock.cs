using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct AiSceneRoleBlock
	{
		public enum Group1Options
		{
			Group1_0 = 0,
			Group2_1 = 1,
			Group3_2 = 2,
		}
		[Field("name^", null)]
		public StringId Name0;
		[Field("group", typeof(Group1Options))]
		public short Group1;
		[Field("", null)]
		public fixed byte _2[2];
		[Field("", null)]
		public fixed byte _3[36];
		[Field("role variants", null)]
		[Block("Ai Scene Role Variants Block", 10, typeof(AiSceneRoleVariantsBlock))]
		public TagBlock RoleVariants4;
	}
}
#pragma warning restore CS1591
