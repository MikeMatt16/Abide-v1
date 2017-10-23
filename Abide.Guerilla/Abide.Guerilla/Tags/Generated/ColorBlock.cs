using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(48, 4)]
	public unsafe struct ColorBlock
	{
		[Field("name^", null)]
		public String Name0;
		[Field("color", null)]
		public ColorArgbF Color1;
	}
}
#pragma warning restore CS1591
