using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(40, 4)]
	public unsafe struct MultilingualUnicodeStringReferenceBlock
	{
		[Field("string id", null)]
		public StringId StringId0;
		[Field("english offset", null)]
		public int EnglishOffset1;
		[Field("japanese offset", null)]
		public int JapaneseOffset2;
		[Field("german offset", null)]
		public int GermanOffset3;
		[Field("french offset", null)]
		public int FrenchOffset4;
		[Field("spanish offset", null)]
		public int SpanishOffset5;
		[Field("italian offset", null)]
		public int ItalianOffset6;
		[Field("korean offset", null)]
		public int KoreanOffset7;
		[Field("chinese offset", null)]
		public int ChineseOffset8;
		[Field("portuguese offset", null)]
		public int PortugueseOffset9;
	}
}
#pragma warning restore CS1591
