using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(84, 4)]
	public unsafe struct RenderLightingStructBlock
	{
		[Field("Ambient", null)]
		public ColorRgbF Ambient0;
		[Field("Shadow Direction", null)]
		public Vector3 ShadowDirection1;
		[Field("lighting_accuracy", null)]
		public float LightingAccuracy2;
		[Field("Shadow Opacity", null)]
		public float ShadowOpacity3;
		[Field("Primary Direction Color", null)]
		public ColorRgbF PrimaryDirectionColor4;
		[Field("Primary Direction", null)]
		public Vector3 PrimaryDirection5;
		[Field("Secondary Direction Color", null)]
		public ColorRgbF SecondaryDirectionColor6;
		[Field("Secondary Direction", null)]
		public Vector3 SecondaryDirection7;
		[Field("sh Index", null)]
		public short ShIndex8;
		[Field("", null)]
		public fixed byte _9[2];
	}
}
#pragma warning restore CS1591
