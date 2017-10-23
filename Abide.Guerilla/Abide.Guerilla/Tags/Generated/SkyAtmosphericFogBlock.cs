using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct SkyAtmosphericFogBlock
	{
		[Field("Color", null)]
		public ColorRgbF Color0;
		[Field("Maximum Density:[0,1]#Fog density is clamped to this value.", null)]
		public float MaximumDensity1;
		[Field("Start Distance:world units#Before this distance there is no fog.", null)]
		public float StartDistance2;
		[Field("Opaque Distance:world units#Fog becomes opaque (maximum density) at this distance from the viewer.", null)]
		public float OpaqueDistance3;
	}
}
#pragma warning restore CS1591
