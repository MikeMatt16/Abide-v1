using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("scenario_hs_source_file", "hsc*", "����", typeof(HsSourceFilesBlock))]
	[FieldSet(52, 4)]
	public unsafe struct HsSourceFilesBlock
	{
		[Field("name*", null)]
		public String Name0;
		[Field("source*", null)]
		[Data(262144)]
		public TagBlock Source1;
	}
}
#pragma warning restore CS1591
