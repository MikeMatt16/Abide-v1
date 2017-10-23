using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(28, 4)]
	public unsafe struct CameraBlock
	{
		[Field("default unit camera track", null)]
		public TagReference DefaultUnitCameraTrack0;
		[Field("default change pause", null)]
		public float DefaultChangePause1;
		[Field("first person change pause", null)]
		public float FirstPersonChangePause2;
		[Field("following camera change pause", null)]
		public float FollowingCameraChangePause3;
	}
}
#pragma warning restore CS1591
