using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(128, 4)]
	public unsafe struct SoundResponseExtraSoundsStructBlock
	{
		[Field("japanese sound", null)]
		public TagReference JapaneseSound0;
		[Field("german sound", null)]
		public TagReference GermanSound1;
		[Field("french sound", null)]
		public TagReference FrenchSound2;
		[Field("spanish sound", null)]
		public TagReference SpanishSound3;
		[Field("italian sound", null)]
		public TagReference ItalianSound4;
		[Field("korean sound", null)]
		public TagReference KoreanSound5;
		[Field("chinese sound", null)]
		public TagReference ChineseSound6;
		[Field("portuguese sound", null)]
		public TagReference PortugueseSound7;
	}
}
#pragma warning restore CS1591
