using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("scenario_structure_lightmap", "ltmp", "����", typeof(ScenarioStructureLightmapBlock))]
	[FieldSet(268, 4)]
	public unsafe struct ScenarioStructureLightmapBlock
	{
		[Field("search distance lower bound", null)]
		public float SearchDistanceLowerBound1;
		[Field("search distance upper bound", null)]
		public float SearchDistanceUpperBound2;
		[Field("luminels per world unit", null)]
		public float LuminelsPerWorldUnit3;
		[Field("output white reference", null)]
		public float OutputWhiteReference4;
		[Field("output black reference", null)]
		public float OutputBlackReference5;
		[Field("output schlick parameter", null)]
		public float OutputSchlickParameter6;
		[Field("diffuse map scale", null)]
		public float DiffuseMapScale7;
		[Field("sun scale", null)]
		public float SunScale8;
		[Field("sky scale", null)]
		public float SkyScale9;
		[Field("indirect scale", null)]
		public float IndirectScale10;
		[Field("prt scale", null)]
		public float PrtScale11;
		[Field("surface light scale", null)]
		public float SurfaceLightScale12;
		[Field("scenario light scale", null)]
		public float ScenarioLightScale13;
		[Field("lightprobe interpolation overide", null)]
		public float LightprobeInterpolationOveride14;
		[Field("", null)]
		public fixed byte _15[72];
		[Field("lightmap groups", null)]
		[Block("Structure Lightmap Group Block", 256, typeof(StructureLightmapGroupBlock))]
		public TagBlock LightmapGroups16;
		[Field("", null)]
		public fixed byte _17[12];
		[Field("errors*", null)]
		[Block("Error Report Category", 64, typeof(GlobalErrorReportCategoriesBlock))]
		public TagBlock Errors18;
		[Field("", null)]
		public fixed byte _19[104];
	}
}
#pragma warning restore CS1591
