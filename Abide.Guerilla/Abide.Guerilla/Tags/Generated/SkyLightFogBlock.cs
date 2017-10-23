using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(44, 4)]
	public unsafe struct SkyLightFogBlock
	{
		[Field("Color", null)]
		public ColorRgbF Color0;
		[Field("Maximum Density:[0,1]#Fog density is clamped to this value.", null)]
		public float MaximumDensity1;
		[Field("Start Distance:world units#Before this distance there is no fog.", null)]
		public float StartDistance2;
		[Field("Opaque Distance:world units#Fog becomes opaque (maximum density) at this distance from the viewer.", null)]
		public float OpaqueDistance3;
		[Field("Cone:degrees", null)]
		public FloatBounds Cone5;
		[Field("Atmospheric Fog Influence:[0,1]", null)]
		public float AtmosphericFogInfluence6;
		[Field("Secondary Fog Influence:[0,1]", null)]
		public float SecondaryFogInfluence7;
		[Field("Sky Fog Influence:[0,1]", null)]
		public float SkyFogInfluence8;
	}
}
#pragma warning restore CS1591
