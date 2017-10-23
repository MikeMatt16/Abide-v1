using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct LightmapBucketRawVertexBlock
	{
		[Field("primary lightmap color", null)]
		public ColorRgbF PrimaryLightmapColor0;
		[Field("primary lightmap incident direction", null)]
		public Vector3 PrimaryLightmapIncidentDirection1;
	}
}
#pragma warning restore CS1591
