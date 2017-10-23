using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(4, 4)]
	public unsafe struct SurfaceFlagsBlock
	{
		[Field("flags*", null)]
		public int Flags0;
	}
}
#pragma warning restore CS1591
