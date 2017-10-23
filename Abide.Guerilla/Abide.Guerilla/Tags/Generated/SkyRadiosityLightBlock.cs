using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(40, 4)]
	public unsafe struct SkyRadiosityLightBlock
	{
		public enum Flags0Options
		{
			AffectsExteriors_0 = 1,
			AffectsInteriors_1 = 2,
			DirectIlluminationInLightmaps_2 = 4,
			IndirectIlluminationInLightmaps_3 = 8,
		}
		[Field("Flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("Color:#Light color.", null)]
		public ColorRgbF Color1;
		[Field("Power:[0,+inf]#Light power from 0 to infinity.", null)]
		public float Power2;
		[Field("Test Distance:world units#Length of the ray for shadow testing.", null)]
		public float TestDistance3;
		[Field("", null)]
		public fixed byte _4[12];
		[Field("Diameter:degrees#Angular diameter of the light source in the sky.", null)]
		public float Diameter5;
	}
}
#pragma warning restore CS1591
