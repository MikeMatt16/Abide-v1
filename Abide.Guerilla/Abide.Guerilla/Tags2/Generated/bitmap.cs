namespace Abide.Guerilla.Tags2.Generated
{
    public sealed class bitmap : TagBlock
    {
        public bitmap() : base("bitmap", "bitmap_block", 1, new FieldSet(4, new FieldList
        {
            { "Type", "Type controls bitmap geometry. All dimensions must be a power of 2 except for SPRITES and INTERFACE BITMAPS:\n\n" +
                "* 2D TEXTURES: Ordinary 2D textures will be generated.\n" +
                "* 3D TEXTURES: Volume textures will be generated from each sequence of 2D texture slices.\n" +
                "* CUBE MAPS: Generated from each consecutive set of six 2D textures in each sequence, all faces of a cube map must be square and the same size.\n" +
                "* SPRITES: Sprite texture pages will be generated.\n" +
                "* INTERFACE BITMAPS: Similar to 2D TEXTURES but without mipmaps and without the power of 2 restriction." },
            { "Name", EnumType.Enumerator, "2D Textures", "3D Textures", "Cube Maps", "Sprites", "Interface Bitmaps" }
        }))
        {
        }
    }
}
