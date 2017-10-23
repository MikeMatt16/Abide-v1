using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct DecoratorPlacementBlock
	{
		[Field("Internal Data 1", null)]
		public int InternalData10;
		[Field("Compressed Position", null)]
		public int CompressedPosition1;
		[Field("Tint Color", null)]
		public ColorRgb TintColor2;
		[Field("Lightmap Color", null)]
		public ColorRgb LightmapColor3;
		[Field("Compressed Light Direction", null)]
		public int CompressedLightDirection4;
		[Field("Compressed Light 2 Direction", null)]
		public int CompressedLight2Direction5;
	}
}
#pragma warning restore CS1591
