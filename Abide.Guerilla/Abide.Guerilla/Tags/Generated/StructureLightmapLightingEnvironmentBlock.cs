using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(220, 4)]
	public unsafe struct StructureLightmapLightingEnvironmentBlock
	{
		public enum ProceduralOveride17Options
		{
			NoOveride_0 = 0,
			CIEClearSky_1 = 1,
			CIEPartlyCloudy_2 = 2,
			CIECloudy_3 = 3,
			DirectionalLight_4 = 4,
			ConeLight_5 = 5,
			SphereLight_6 = 6,
			HemisphereLight_7 = 7,
		}
		public enum Flags18Options
		{
			LeaveMeAlonePlease_0 = 1,
		}
		public Vector3 SamplePoint0;
		[Field("red coefficient*", null)]
		public float RedCoefficient2;
		[Field("green coefficient*", null)]
		public float GreenCoefficient5;
		[Field("blue coefficient*", null)]
		public float BlueCoefficient8;
		[Field("mean incoming light direction", null)]
		public Vector3 MeanIncomingLightDirection10;
		public Vector3 IncomingLightIntensity11;
		[Field("specular bitmap index", null)]
		public int SpecularBitmapIndex12;
		[Field("rotation axis", null)]
		public Vector3 RotationAxis13;
		[Field("rotation speed", null)]
		public float RotationSpeed14;
		[Field("bump direction", null)]
		public Vector3 BumpDirection15;
		[Field("color tint", null)]
		public ColorRgbF ColorTint16;
		[Field("procedural overide", typeof(ProceduralOveride17Options))]
		public short ProceduralOveride17;
		[Field("flags", typeof(Flags18Options))]
		public short Flags18;
		[Field("procedural param0", null)]
		public Vector3 ProceduralParam019;
		[Field("procedural param1.xyz", null)]
		public Vector3 ProceduralParam1Xyz20;
		[Field("procedural param1.w", null)]
		public float ProceduralParam1W21;
	}
}
#pragma warning restore CS1591
