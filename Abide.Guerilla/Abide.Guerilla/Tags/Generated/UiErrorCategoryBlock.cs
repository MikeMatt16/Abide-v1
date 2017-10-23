using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(52, 4)]
	public unsafe struct UiErrorCategoryBlock
	{
		public enum Flags1Options
		{
			UseLargeDialog_0 = 1,
		}
		public enum DefaultButton2Options
		{
			NoDefault_0 = 0,
			DefaultOk_1 = 1,
			DefaultCancel_2 = 2,
		}
		[Field("category name^", null)]
		public StringId CategoryName0;
		[Field("flags", typeof(Flags1Options))]
		public short Flags1;
		[Field("default button", typeof(DefaultButton2Options))]
		public byte DefaultButton2;
		[Field("", null)]
		public fixed byte _3[1];
		[Field("string tag", null)]
		public TagReference StringTag4;
		[Field("default title", null)]
		public StringId DefaultTitle5;
		[Field("default message", null)]
		public StringId DefaultMessage6;
		[Field("default ok", null)]
		public StringId DefaultOk7;
		[Field("default cancel", null)]
		public StringId DefaultCancel8;
		[Field("error block", null)]
		[Block("Ui Error Block", 100, typeof(UiErrorBlock))]
		public TagBlock ErrorBlock9;
	}
}
#pragma warning restore CS1591
