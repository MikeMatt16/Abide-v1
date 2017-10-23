using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("light_volume", "MGS2", "����", typeof(LightVolumeBlock))]
	[FieldSet(20, 4)]
	public unsafe struct LightVolumeBlock
	{
		[Field("", null)]
		public fixed byte _1[64];
		[Field("falloff distance from camera:world units", null)]
		public float FalloffDistanceFromCamera2;
		[Field("cutoff distance from camera:world units", null)]
		public float CutoffDistanceFromCamera3;
		[Field("", null)]
		public fixed byte _4[32];
		[Field("volumes", null)]
		[Block("Volume", 16, typeof(LightVolumeVolumeBlock))]
		public TagBlock Volumes5;
	}
}
#pragma warning restore CS1591
