using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("multilingual_unicode_string_list", "unic", "����", typeof(MultilingualUnicodeStringListBlock))]
	[FieldSet(68, 4)]
	public unsafe struct MultilingualUnicodeStringListBlock
	{
		[Field("string references", null)]
		[Block("Multilingual Unicode String Reference Block", 9216, typeof(MultilingualUnicodeStringReferenceBlock))]
		public TagBlock StringReferences0;
		[Field("string data utf8", null)]
		[Data(18874368)]
		public TagBlock StringDataUtf81;
		[Field("", null)]
		public fixed byte _2[36];
	}
}
#pragma warning restore CS1591
