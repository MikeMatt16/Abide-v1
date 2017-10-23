using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(36, 4)]
	public unsafe struct MaterialEffectBlockV2
	{
		[Field("old materials (DO NOT USE)", null)]
		[Block("Old Material Effect Material Block", 33, typeof(OldMaterialEffectMaterialBlock))]
		public TagBlock OldMaterialsDONOTUSE0;
		[Field("sounds", null)]
		[Block("Material Effect Material Block", 500, typeof(MaterialEffectMaterialBlock))]
		public TagBlock Sounds1;
		[Field("effects", null)]
		[Block("Material Effect Material Block", 500, typeof(MaterialEffectMaterialBlock))]
		public TagBlock Effects2;
	}
}
#pragma warning restore CS1591
