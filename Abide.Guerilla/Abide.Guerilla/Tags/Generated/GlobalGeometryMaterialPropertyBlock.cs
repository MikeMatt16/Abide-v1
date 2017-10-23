using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct GlobalGeometryMaterialPropertyBlock
	{
		public enum Type0Options
		{
			LightmapResolution_0 = 0,
			LightmapPower_1 = 1,
			LightmapHalfLife_2 = 2,
			LightmapDiffuseScale_3 = 3,
		}
		[Field("Type*", typeof(Type0Options))]
		public short Type0;
		[Field("Int Value*", null)]
		public short IntValue1;
		[Field("Real Value*", null)]
		public float RealValue2;
	}
}
#pragma warning restore CS1591
