using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(36, 4)]
	public unsafe struct StructureBspEnvironmentObjectPaletteBlock
	{
		[Field("Definition", null)]
		public TagReference Definition0;
		[Field("Model", null)]
		public TagReference Model1;
		[Field("", null)]
		public fixed byte _2[4];
	}
}
#pragma warning restore CS1591
