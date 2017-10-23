using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(72, 4)]
	public unsafe struct SkyLightBlock
	{
		[Field("Direction Vector", null)]
		public Vector3 DirectionVector0;
		[Field("Direction", null)]
		public Vector2 Direction1;
		[Field("Lens Flare", null)]
		public TagReference LensFlare2;
		[Field("Fog", null)]
		[Block("Sky Light Fog Block", 1, typeof(SkyLightFogBlock))]
		public TagBlock Fog3;
		[Field("Fog Opposite", null)]
		[Block("Sky Light Fog Block", 1, typeof(SkyLightFogBlock))]
		public TagBlock FogOpposite4;
		[Field("Radiosity", null)]
		[Block("Sky Radiosity Light Block", 1, typeof(SkyRadiosityLightBlock))]
		public TagBlock Radiosity5;
	}
}
#pragma warning restore CS1591
