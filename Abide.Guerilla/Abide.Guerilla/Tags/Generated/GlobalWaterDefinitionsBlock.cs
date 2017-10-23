using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(188, 4)]
	public unsafe struct GlobalWaterDefinitionsBlock
	{
		[Field("Shader", null)]
		public TagReference Shader0;
		[Field("Section", null)]
		[Block("Water Geometry Section Block", 1, typeof(WaterGeometrySectionBlock))]
		public TagBlock Section1;
		[Field("Geometry Block Info*", typeof(GlobalGeometryBlockInfoStructBlock))]
		[Block("Global Geometry Block Info Struct", 1, typeof(GlobalGeometryBlockInfoStructBlock))]
		public GlobalGeometryBlockInfoStructBlock GeometryBlockInfo2;
		[Field("Sun Spot Color", null)]
		public ColorRgbF SunSpotColor3;
		[Field("Reflection Tint", null)]
		public ColorRgbF ReflectionTint4;
		[Field("Refraction Tint", null)]
		public ColorRgbF RefractionTint5;
		[Field("Horizon Color", null)]
		public ColorRgbF HorizonColor6;
		[Field("Sun Specular Power", null)]
		public float SunSpecularPower7;
		[Field("Reflection Bump Scale", null)]
		public float ReflectionBumpScale8;
		[Field("Refraction Bump Scale", null)]
		public float RefractionBumpScale9;
		[Field("Fresnel Scale", null)]
		public float FresnelScale10;
		[Field("Sun Dir Heading", null)]
		public float SunDirHeading11;
		[Field("Sun Dir Pitch", null)]
		public float SunDirPitch12;
		[Field("FOV", null)]
		public float FOV13;
		[Field("Aspect", null)]
		public float Aspect14;
		[Field("Height", null)]
		public float Height15;
		[Field("Farz", null)]
		public float Farz16;
		[Field("rotate_offset", null)]
		public float RotateOffset17;
		[Field("Center", null)]
		public Vector2 Center18;
		[Field("Extents", null)]
		public Vector2 Extents19;
		[Field("Fog Near", null)]
		public float FogNear20;
		[Field("Fog Far", null)]
		public float FogFar21;
		[Field("dynamic_height_bias", null)]
		public float DynamicHeightBias22;
	}
}
#pragma warning restore CS1591
