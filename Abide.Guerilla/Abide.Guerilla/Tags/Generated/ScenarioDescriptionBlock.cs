using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(68, 4)]
	public unsafe struct ScenarioDescriptionBlock
	{
		[Field("descriptive bitmap", null)]
		public TagReference DescriptiveBitmap1;
		[Field("displayed map name", null)]
		public TagReference DisplayedMapName2;
		[Field("scenario tag directory path#this is the path to the directory containing the scenario tag file of the same name", null)]
		public String ScenarioTagDirectoryPath3;
		[Field("", null)]
		public fixed byte _4[4];
	}
}
#pragma warning restore CS1591
