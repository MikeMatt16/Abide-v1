using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(48, 4)]
	public unsafe struct ScenarioDetailObjectCollectionPaletteBlock
	{
		[Field("Name^", null)]
		public TagReference Name0;
		[Field("", null)]
		public fixed byte _1[32];
	}
}
#pragma warning restore CS1591
