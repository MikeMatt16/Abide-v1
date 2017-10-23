using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(4, 4)]
	public unsafe struct AnimationIndexStructBlock
	{
		[Field("graph index*", null)]
		public short GraphIndex0;
		[Field("animation*", null)]
		public short Animation1;
	}
}
#pragma warning restore CS1591
