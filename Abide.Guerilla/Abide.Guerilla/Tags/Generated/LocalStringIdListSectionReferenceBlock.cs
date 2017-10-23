using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct LocalStringIdListSectionReferenceBlock
	{
		[Field("section name^", null)]
		public StringId SectionName0;
		[Field("local string section references", null)]
		[Block("Local String Id List String Reference Block", 64, typeof(LocalStringIdListStringReferenceBlock))]
		public TagBlock LocalStringSectionReferences1;
	}
}
#pragma warning restore CS1591
