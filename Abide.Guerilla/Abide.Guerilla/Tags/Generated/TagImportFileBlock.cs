using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(540, 4)]
	public unsafe struct TagImportFileBlock
	{
		[Field("path*", null)]
		public LongString Path0;
		[Field("modification date*", null)]
		public String ModificationDate1;
		[Field("", null)]
		public fixed byte _2[8];
		[Field("", null)]
		public fixed byte _3[88];
		[Field("checksum*:crc32", null)]
		public int Checksum4;
		[Field("size*:bytes", null)]
		public int Size5;
		[Field("zipped data*", null)]
		[Data(134217728)]
		public TagBlock ZippedData6;
		[Field("", null)]
		public fixed byte _7[128];
	}
}
#pragma warning restore CS1591
