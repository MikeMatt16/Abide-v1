using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(32, 4)]
	public unsafe struct ObjectAttachmentBlock
	{
		public enum ChangeColor3Options
		{
			None_0 = 0,
			Primary_1 = 1,
			Secondary_2 = 2,
			Tertiary_3 = 3,
			Quaternary_4 = 4,
		}
		[Field("type^", null)]
		public TagReference Type0;
		[Field("", null)]
		public fixed byte _2[4];
		[Field("change color", typeof(ChangeColor3Options))]
		public short ChangeColor3;
		[Field("", null)]
		public fixed byte _4[2];
		[Field("primary scale", null)]
		public StringId PrimaryScale5;
		[Field("secondary scale", null)]
		public StringId SecondaryScale6;
		[Field("", null)]
		public fixed byte _7[8];
	}
}
#pragma warning restore CS1591
