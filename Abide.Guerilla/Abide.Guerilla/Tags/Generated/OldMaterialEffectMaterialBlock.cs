using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(44, 4)]
	public unsafe struct OldMaterialEffectMaterialBlock
	{
		public enum SweetenerMode4Options
		{
			SweetenerDefault_0 = 0,
			SweetenerEnabled_1 = 1,
			SweetenerDisabled_2 = 2,
		}
		[Field("effect", null)]
		public TagReference Effect0;
		[Field("sound", null)]
		public TagReference Sound1;
		[Field("material name^", null)]
		public StringId MaterialName2;
		[Field("", null)]
		public fixed byte _3[4];
		[Field("sweetener mode", typeof(SweetenerMode4Options))]
		public byte SweetenerMode4;
		[Field("", null)]
		public fixed byte _5[3];
		[Field("", null)]
		public fixed byte _6[4];
	}
}
#pragma warning restore CS1591
