using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct PrtLodInfoBlock
	{
		[Field("cluster offset*", null)]
		public int ClusterOffset0;
		[Field("section info*", null)]
		[Block("Prt Section Info", 255, typeof(PrtSectionInfoBlock))]
		public TagBlock SectionInfo1;
	}
}
#pragma warning restore CS1591
