using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("material_effects", "foot", "����", typeof(MaterialEffectsBlock))]
	[FieldSet(12, 4)]
	public unsafe struct MaterialEffectsBlock
	{
		[Field("effects", null)]
		[Block("Material Effect Block V2", 21, typeof(MaterialEffectBlockV2))]
		public TagBlock Effects0;
		[Field("", null)]
		public fixed byte _1[128];
	}
}
#pragma warning restore CS1591
