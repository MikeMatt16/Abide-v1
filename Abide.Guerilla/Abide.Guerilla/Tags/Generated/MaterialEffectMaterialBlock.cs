using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(40, 4)]
	public unsafe struct MaterialEffectMaterialBlock
	{
		public enum SweetenerMode4Options
		{
			SweetenerDefault_0 = 0,
			SweetenerEnabled_1 = 1,
			SweetenerDisabled_2 = 2,
		}
		[Field("tag (effect or sound)", null)]
		public TagReference TagEffectOrSound0;
		[Field("secondary tag (effect or sound)", null)]
		public TagReference SecondaryTagEffectOrSound1;
		[Field("material name^", null)]
		public StringId MaterialName2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("sweetener mode", typeof(SweetenerMode4Options))]
		public byte SweetenerMode4;
		[Field("", null)]
		public fixed byte _5[1];
	}
}
#pragma warning restore CS1591
