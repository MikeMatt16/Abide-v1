using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(596, 4)]
	public unsafe struct GlobalTagImportInfoBlock
	{
		[Field("build*", null)]
		public int Build0;
		[Field("version*", null)]
		public LongString Version1;
		[Field("import date*", null)]
		public String ImportDate2;
		[Field("culprit*", null)]
		public String Culprit3;
		[Field("", null)]
		public fixed byte _4[96];
		[Field("import time*", null)]
		public String ImportTime5;
		[Field("", null)]
		public fixed byte _6[4];
		[Field("files*", null)]
		[Block("Import File", 1024, typeof(TagImportFileBlock))]
		public TagBlock Files7;
		[Field("", null)]
		public fixed byte _8[128];
	}
}
#pragma warning restore CS1591
