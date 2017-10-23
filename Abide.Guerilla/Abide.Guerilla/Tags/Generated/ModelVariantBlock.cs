using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(72, 4)]
	public unsafe struct ModelVariantBlock
	{
		[Field("name^", null)]
		public StringId Name0;
		[Field("", null)]
		public fixed byte _1[16];
		[Field("regions", null)]
		[Block("Region", 16, typeof(ModelVariantRegionBlock))]
		public TagBlock Regions2;
		[Field("objects", null)]
		[Block("Object", 16, typeof(ModelVariantObjectBlock))]
		public TagBlock Objects3;
		[Field("", null)]
		public fixed byte _4[8];
		[Field("dialogue sound effect", null)]
		public StringId DialogueSoundEffect5;
		[Field("dialogue", null)]
		public TagReference Dialogue6;
	}
}
#pragma warning restore CS1591
