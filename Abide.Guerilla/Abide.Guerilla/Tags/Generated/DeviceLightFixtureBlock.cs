using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("device_light_fixture", "lifi", "devi", typeof(DeviceLightFixtureBlock))]
	[FieldSet(0, 4)]
	public unsafe struct DeviceLightFixtureBlock
	{
		[Field("", null)]
		public fixed byte _1[64];
	}
}
#pragma warning restore CS1591
