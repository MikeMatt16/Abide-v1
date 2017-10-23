using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct SkyFogBlock
	{
		[Field("Color", null)]
		public ColorRgbF Color0;
		[Field("Density:[0,1]#Fog density is clamped to this value.", null)]
		public float Density1;
	}
}
#pragma warning restore CS1591
