using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct PrtSectionInfoBlock
	{
		[Field("section index*", null)]
		public int SectionIndex0;
		[Field("pca data offset*", null)]
		public int PcaDataOffset1;
	}
}
#pragma warning restore CS1591
