using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct SkyCubemapBlock
	{
		[Field("Cube Map Reference", null)]
		public TagReference CubeMapReference0;
		[Field("Power Scale#0 Defaults to 1.", null)]
		public float PowerScale1;
	}
}
#pragma warning restore CS1591
