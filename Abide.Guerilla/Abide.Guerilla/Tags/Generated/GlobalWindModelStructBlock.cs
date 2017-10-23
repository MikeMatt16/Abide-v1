using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(160, 4)]
	public unsafe struct GlobalWindModelStructBlock
	{
		[Field("wind tiling scale", null)]
		public float WindTilingScale0;
		[Field("wind primary heading/pitch/strength", null)]
		public Vector3 WindPrimaryHeadingPitchStrength1;
		[Field("primary rate of change", null)]
		public float PrimaryRateOfChange2;
		[Field("primary min strength", null)]
		public float PrimaryMinStrength3;
		[Field("", null)]
		public fixed byte _4[4];
		[Field("", null)]
		public fixed byte _5[4];
		[Field("", null)]
		public fixed byte _6[12];
		[Field("wind gusting heading/pitch/strength", null)]
		public Vector3 WindGustingHeadingPitchStrength7;
		[Field("gust diretional rate of change", null)]
		public float GustDiretionalRateOfChange8;
		[Field("gust strength rate of change", null)]
		public float GustStrengthRateOfChange9;
		[Field("gust cone angle", null)]
		public float GustConeAngle10;
		[Field("", null)]
		public fixed byte _11[4];
		[Field("", null)]
		public fixed byte _12[4];
		[Field("", null)]
		public fixed byte _13[12];
		[Field("", null)]
		public fixed byte _14[12];
		[Field("", null)]
		public fixed byte _15[12];
		[Field("", null)]
		public fixed byte _16[12];
		[Field("turbulance rate of change", null)]
		public float TurbulanceRateOfChange17;
		[Field("turbulence_scale x, y, z", null)]
		public Vector3 TurbulenceScaleXYZ18;
		[Field("gravity constant", null)]
		public float GravityConstant19;
		[Field("wind_pirmitives", null)]
		[Block("Wind Primitives", 128, typeof(GloalWindPrimitivesBlock))]
		public TagBlock WindPirmitives20;
		[Field("", null)]
		public fixed byte _21[4];
	}
}
#pragma warning restore CS1591
