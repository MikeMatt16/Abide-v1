using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(4, 4)]
	public unsafe struct CollisionModelMaterialBlock
	{
		[Field("name^*", null)]
		public StringId Name0;
	}
}
#pragma warning restore CS1591
