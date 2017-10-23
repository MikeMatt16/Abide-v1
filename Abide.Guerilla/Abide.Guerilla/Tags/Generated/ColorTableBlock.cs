using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("color_table", "colo", "����", typeof(ColorTableBlock))]
	[FieldSet(12, 4)]
	public unsafe struct ColorTableBlock
	{
		[Field("colors", null)]
		[Block("Color Block", 512, typeof(ColorBlock))]
		public TagBlock Colors0;
	}
}
#pragma warning restore CS1591
