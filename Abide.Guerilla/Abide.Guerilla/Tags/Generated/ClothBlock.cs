using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("cloth", "clwd", "����", typeof(ClothBlock))]
	[FieldSet(132, 4)]
	public unsafe struct ClothBlock
	{
		public enum Flags0Options
		{
			DoesnTUseWind_0 = 1,
			UsesGridAttachTop_1 = 2,
		}
		[Field("flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("marker attachment name", null)]
		public StringId MarkerAttachmentName1;
		[Field("Shader", null)]
		public TagReference Shader2;
		[Field("grid x dimension", null)]
		public short GridXDimension4;
		[Field("grid y dimension", null)]
		public short GridYDimension5;
		[Field("grid spacing x", null)]
		public float GridSpacingX6;
		[Field("grid spacing y", null)]
		public float GridSpacingY7;
		[Field("properties", typeof(ClothPropertiesBlock))]
		[Block("Cloth Properties", 1, typeof(ClothPropertiesBlock))]
		public ClothPropertiesBlock Properties9;
		[Field("vertices*", null)]
		[Block("Cloth Vertices Block", 128, typeof(ClothVerticesBlock))]
		public TagBlock Vertices11;
		[Field("indices*", null)]
		[Block("Cloth Indices Block", 768, typeof(ClothIndicesBlock))]
		public TagBlock Indices12;
		[Field("strip indices*", null)]
		[Block("Cloth Indices Block", 768, typeof(ClothIndicesBlock))]
		public TagBlock StripIndices13;
		[Field("links*", null)]
		[Block("Cloth Links Block", 640, typeof(ClothLinksBlock))]
		public TagBlock Links14;
	}
}
#pragma warning restore CS1591
