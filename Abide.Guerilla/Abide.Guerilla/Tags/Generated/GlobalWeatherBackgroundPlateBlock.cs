using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(960, 4)]
	public unsafe struct GlobalWeatherBackgroundPlateBlock
	{
		public enum Flags22Options
		{
			ForwardMotion_0 = 1,
			AutoPositionPlanes_1 = 2,
			AutoScalePlanesautoUpdateSpeed_2 = 4,
		}
		[Field("texture 0", null)]
		public TagReference Texture00;
		[Field("texture 1", null)]
		public TagReference Texture11;
		[Field("texture 2", null)]
		public TagReference Texture22;
		[Field("plate positions 0", null)]
		public float PlatePositions03;
		[Field("plate positions 1", null)]
		public float PlatePositions14;
		[Field("plate positions 2", null)]
		public float PlatePositions25;
		[Field("move speed 0", null)]
		public Vector3 MoveSpeed06;
		[Field("move speed 1", null)]
		public Vector3 MoveSpeed17;
		[Field("move speed 2", null)]
		public Vector3 MoveSpeed28;
		[Field("texture scale 0", null)]
		public float TextureScale09;
		[Field("texture scale 1", null)]
		public float TextureScale110;
		[Field("texture scale 2", null)]
		public float TextureScale211;
		[Field("jitter 0", null)]
		public Vector3 Jitter012;
		[Field("jitter 1", null)]
		public Vector3 Jitter113;
		[Field("jitter 2", null)]
		public Vector3 Jitter214;
		[Field("plate z near", null)]
		public float PlateZNear15;
		[Field("plate z far", null)]
		public float PlateZFar16;
		[Field("depth blend z near", null)]
		public float DepthBlendZNear17;
		[Field("depth blend z far", null)]
		public float DepthBlendZFar18;
		[Field("opacity 0", null)]
		public float Opacity019;
		[Field("opacity 1", null)]
		public float Opacity120;
		[Field("opacity 2", null)]
		public float Opacity221;
		[Field("flags", typeof(Flags22Options))]
		public int Flags22;
		[Field("tint color0", null)]
		public ColorRgbF TintColor023;
		[Field("tint color1", null)]
		public ColorRgbF TintColor124;
		[Field("tint color2", null)]
		public ColorRgbF TintColor225;
		[Field("mass 1", null)]
		public float Mass126;
		[Field("mass 2", null)]
		public float Mass227;
		[Field("mass 3", null)]
		public float Mass328;
		[Field("", null)]
		public fixed byte _29[736];
	}
}
#pragma warning restore CS1591
